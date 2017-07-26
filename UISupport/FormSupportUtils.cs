using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using System.Windows.Forms;
using System.Runtime.InteropServices;//ちらつき防止

namespace WindowsFormsApplication1
{
     public class FormSupportUtils
    {

        Rectangle preWindow = new Rectangle(0, 0, 0, 0);    //最大化前のwindowプロパティ
        Point mousePoint;       //フォーム動かす用のマウス座標
        int display_type = 0;
        Form forcusForm = null;
        System.EventHandler resize;

        public FormSupportUtils(Form f, Control c = null)
        {
            forcusForm = f;
            resize = new System.EventHandler(Resize);

            forcusForm.Resize += resize;
            if (c != null)
            {
                c.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDown);
                c.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMove);
            }
            //ボタンResizeのための初期化
            if (preWindow == new Rectangle(0, 0, 0, 0))
            {
                preWindow = forcusForm.Bounds;
                return;
            }
        }

        #region Resize関連
        /// <summary> formサイズを変更すると呼ばれるイベント </summary>
        private void Resize(object sender, EventArgs e)
        {
            ResizeButton();
        }

        /// <summary> ボタンサイズをFormサイズに合わせてリサイズ </summary>
        public void ResizeButton()
        {
            if (forcusForm.WindowState == FormWindowState.Minimized)//最小化のとき無視する
                return;

            float resize_perX = 1.0f;
            float resize_perY = 1.0f;
            int resize_locationX;
            int resize_locationY;

            //拡大率算出
            resize_perX = (float)forcusForm.Width / preWindow.Width;
            resize_perY = (float)forcusForm.Height / preWindow.Height;
            if (resize_perX == 1 && resize_perY == 1)
                return;

            //BeginUpdate(forcusForm);

            // コントロール全てを列挙
            List<Control> controls = forcusForm.GetAllControls<Control>();
            foreach (var ctl in controls)
            {
                //位置再配置
                resize_locationX = (int)Math.Round(ctl.Left * resize_perX);
                resize_locationY = (int)Math.Round(ctl.Top * resize_perY);
                ctl.Location = new Point(resize_locationX, resize_locationY);

                //リサイズ
                resize_locationX = (int)Math.Round(ctl.Width * resize_perX);
                resize_locationY = (int)Math.Round(ctl.Height * resize_perY);
                ctl.Size = new Size(resize_locationX, resize_locationY);

                if (ctl is TextBox || ctl is ComboBox || ctl is Label || ctl is CheckBox)
                    ctl.Font = new Font(ctl.Font.Name, ctl.Font.Size * resize_perY);

            }

            //EndUpdate(forcusForm);

            preWindow = forcusForm.Bounds;
        }

        public void FitFormToWindow(bool isRatioEvenly = false)
        {
            //現在フォームが存在しているディスプレイを取得
            System.Windows.Forms.Screen s =
                System.Windows.Forms.Screen.FromControl(forcusForm);
            int w = s.Bounds.Width;
            int h = s.Bounds.Height;
            if (isRatioEvenly)
            {
                h = (int)(w / (float)forcusForm.Width * forcusForm.Height);
            }
            SetForcusFormSize(w, h);
            ResizeButton();
        }

        public void SetForcusFormSize(Size s)
        {
            forcusForm.Size = s;
        }
        public void SetForcusFormSize(int w, int h)
        {
            forcusForm.Size = new Size(w, h);
        }

        public void ChangeWindowSize(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up || e.KeyData == Keys.Down || e.KeyData == Keys.F)
            {
                ////ダブルバッファリングを有効にする
                //forcusForm.SetStyle(
                //   ControlStyles.DoubleBuffer |
                //   ControlStyles.UserPaint |
                //   ControlStyles.AllPaintingInWmPaint,
                //   true);

                const int type_max = 3;
                if (e.KeyData == Keys.Up)
                    display_type++;
                if (e.KeyData == Keys.Down)
                    display_type--;

                if (display_type < 0)
                    display_type = type_max;
                else if (display_type > type_max)
                    display_type = 0;

                if (e.KeyData == Keys.F)
                    display_type = type_max + 1;

                forcusForm.WindowState = FormWindowState.Normal;

                int rw, rh;
                switch (display_type)
                {
                    case 0:
                        rw = 640;
                        rh = 480;
                        break;
                    case 1:
                        rw = 800;
                        rh = 600;
                        break;
                    case 2:
                        rw = 1024;
                        rh = 768;
                        break;
                    case 3:
                        rw = 1280;
                        rh = 768;
                        break;
                    default:
                        rw = Screen.PrimaryScreen.Bounds.Width;
                        rh = Screen.PrimaryScreen.Bounds.Height;
                        break;
                }
                ResizeWindow(rw, rh);
            }
        }

        public void ResizeWindow(int w, int h)
        {
            //ボタンResizeのための初期化
            if (preWindow == new Rectangle(0, 0, 0, 0))
            {
                preWindow = forcusForm.Bounds;
            }

            forcusForm.Width = w;
            forcusForm.Height = h;
        }
        #endregion

        #region フォーム移動関連
        private void MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);//位置を記憶する
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                if (forcusForm.WindowState != FormWindowState.Normal)//サイズ全画面時は動かさない
                    return;

                forcusForm.Location = new Point(
                    forcusForm.Location.X + e.X - mousePoint.X,
                    forcusForm.Location.Y + e.Y - mousePoint.Y);
            }
        }
        #endregion
    }
}
