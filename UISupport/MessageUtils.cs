using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{

    #region 一定時間表示subroutine
    public class AutoClosingMessageBox
    {
        int text_max_length = 200; //最大の文字列長の指定
        System.Threading.Timer _timeoutTimer;
        string _caption;
        AutoClosingMessageBox(string text, string caption, int timeout)
        {
            //if (text.length > text_max_length) //内容の長さを制限する追加部分
            //{
            //    text = text.Substring(0, text_max_length);
            //}
            //text = ( text.length > text_max_length ) ? text.Substring( 0, text_max_length ) : text; //三項目演算での記述方法　? の左側が true の時 : の左側が代入されます。 falseの時は右側
            _caption = caption;
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed, null, timeout, System.Threading.Timeout.Infinite);
            MsgBox.ShowActiveFormCenter( text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void Show(string text, string caption, int timeout)
        {
            new AutoClosingMessageBox(text, caption, timeout);
        }
        void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow(null, _caption);
            if (mbWnd != IntPtr.Zero)
            {
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
            _timeoutTimer.Dispose();
        }
        const int WM_CLOSE = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
    }
    #endregion

    #region メッセージボックス表示(位置指定)ライブラリ
    /// <summary>
    /// Win API
    /// </summary>
    public class WinAPI
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentThreadId();
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, HOOKPROC lpfn, IntPtr hInstance, IntPtr threadId);
        [DllImport("user32.dll")]
        public static extern bool UnhookWindowsHookEx(IntPtr hHook);
        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hHook, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        public delegate IntPtr HOOKPROC(int nCode, IntPtr wParam, IntPtr lParam);

        public const int GWL_HINSTANCE = (-6);
        public const int WH_CBT = 5;
        public const int HCBT_ACTIVATE = 5;

        public const int SWP_NOSIZE = 0x0001;
        public const int SWP_NOZORDER = 0x0004;
        public const int SWP_NOACTIVATE = 0x0010;

        public struct RECT
        {
            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }

    /// <summary>
    /// オーナーウィンドウの真中に表示される MessageBox
    /// </summary>
    public class MsgBox
    {

        /// <summary>
        /// 親ウィンドウ
        /// </summary>
        private IWin32Window m_ownerWindow = null;

        /// <summary>
        /// フックハンドル
        /// </summary>
        private IntPtr m_hHook = (IntPtr)0;

        /// <summary>
        /// メッセージボックスを表示する
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <param name="button"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static DialogResult ShowActiveFormCenter(
            string messageBoxText,
            string caption,
            MessageBoxButtons button,
            MessageBoxIcon icon,
            IWin32Window owner = null
            )
        {
            System.Windows.Forms.Cursor.Show();
            MsgBox mbox;
            if (owner == null)
            {
                mbox = new MsgBox(Form.ActiveForm);//フォーム生成時にthis.Activate()しないと、エラーメッセージ生成時にとまる
            }
            else
                mbox = new MsgBox(owner);
            DialogResult result = mbox.Show(messageBoxText, caption, button, icon);
            System.Windows.Forms.Cursor.Hide();

            return result;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="window">親ウィンドウ</param>
        private MsgBox(IWin32Window window)
        {
            m_ownerWindow = window;
        }

        /// <summary>
        /// メッセージボックスを表示する
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <param name="button"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public DialogResult Show(
            string messageBoxText,
            string caption,
            MessageBoxButtons button,
            MessageBoxIcon icon)
        {
            //フォーム生成時にthis.Activate()しないと、エラーメッセージ生成時にとまる
            // フックを設定する。
            IntPtr hInstance = WinAPI.GetWindowLong(m_ownerWindow.Handle, WinAPI.GWL_HINSTANCE);
            IntPtr threadId = WinAPI.GetCurrentThreadId();
            m_hHook = WinAPI.SetWindowsHookEx(WinAPI.WH_CBT, new WinAPI.HOOKPROC(HookProc), hInstance, threadId);

            return MessageBox.Show(m_ownerWindow, messageBoxText, caption, button, icon);
        }

        /// <summary>
        /// フックプロシージャ
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {

            if (nCode == WinAPI.HCBT_ACTIVATE)
            {
                WinAPI.RECT rcForm = new WinAPI.RECT(0, 0, 0, 0);
                WinAPI.RECT rcMsgBox = new WinAPI.RECT(0, 0, 0, 0);

                WinAPI.GetWindowRect(m_ownerWindow.Handle, out rcForm);
                WinAPI.GetWindowRect(wParam, out rcMsgBox);

                // センター位置を計算する。
                int x = (rcForm.Left + (rcForm.Right - rcForm.Left) / 2) - ((rcMsgBox.Right - rcMsgBox.Left) / 2);
                int y = (rcForm.Top + (rcForm.Bottom - rcForm.Top) / 2) - ((rcMsgBox.Bottom - rcMsgBox.Top) / 2);

                WinAPI.SetWindowPos(wParam, 0, x, y, 0, 0, WinAPI.SWP_NOSIZE | WinAPI.SWP_NOZORDER | WinAPI.SWP_NOACTIVATE);

                IntPtr result = WinAPI.CallNextHookEx(m_hHook, nCode, wParam, lParam);

                // フックを解除する。
                WinAPI.UnhookWindowsHookEx(m_hHook);
                m_hHook = (IntPtr)0;

                return result;

            }
            else
            {
                return WinAPI.CallNextHookEx(m_hHook, nCode, wParam, lParam);
            }
        }
    }
    #endregion

}
