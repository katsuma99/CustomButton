using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;//ちらつき防止
using System.Drawing;

namespace FormSupportLib
{
    public static class ControlEx
    {
        /// <summary> window座標を求めてからform座標(フォームからの相対位置)にする </summary>
        public static void ChangeParent(this Control ch, Control newPar)
        {
            int posX = ch.Location.X;
            int posY = ch.Location.Y;
            if (ch.Parent != null)
            {
                posX += ch.Parent.Location.X;
                posY += ch.Parent.Location.Y;
            }
            ch.Parent = newPar;
            if (newPar != null)
            {
                posX -= newPar.Location.X;
                posY -= newPar.Location.Y;
            }
            ch.Location = new Point(posX, posY);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        public const int WM_SETREDRAW = 0x000B;
        /// <summary> コントロール(子コントロールも含む)の描画を停止します。 <para>control : 対象コントロール</para></summary>
        public static void StopDraw(this Control control)
        {
            SendMessage(control.Handle, WM_SETREDRAW, 0, 0);
        }

        /// <summary> コントロール(子コントロールも含む)の描画を開始します。 <para>control : 対象コントロール</para></summary>
        public static void StartDraw(this Control control)
        {
            SendMessage(control.Handle, WM_SETREDRAW, 1, 0);
            control.Refresh();
        }

        /// <summary> 指定のコントロール上の全てのジェネリック型 Tコントロールを取得する。List＜Control＞ controls = GetAllControls＜Control＞(forcusForm); </summary>
        public static List<T> GetAllControls<T>(this Control top) where T : Control
        {
            List<T> buf = new List<T>();
            foreach (Control ctrl in top.Controls)
            {
                if (ctrl is T) buf.Add((T)ctrl);
                buf.AddRange(GetAllControls<T>(ctrl));
            }
            return buf;
        }
    }
}
