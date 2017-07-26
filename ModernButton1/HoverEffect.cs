using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModernButtonLib
{
    public partial class HoverEffect : RoundCornerButton
    {
        protected enum OperatingState
        {
            Leave,
            Enter,
            Down,
            Up
        }

        #region 変数
        OperatingState mState = OperatingState.Leave;
        [Category("カスタム：ボタンイメージ"), Description("初期のボタン状態（通常・選択・決定）")]
        [DefaultValue(typeof(OperatingState), "None")]
        protected OperatingState State
        {
            get { return mState; }
            set
            {
                if (mState == value) return;
                mState = value;
                ColorChangeTimer.Start();
            }
        }

        
        #endregion

        public HoverEffect()
        {
            InitializeComponent();
            mState = OperatingState.Leave;
        }

        ~HoverEffect()
        {
        }

        #region ボタンイベント処理
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            State = OperatingState.Down;
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            State = OperatingState.Up;
            base.OnMouseUp(mevent);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            State = OperatingState.Enter;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            State = OperatingState.Leave;
            base.OnMouseLeave(e);
        }
        #endregion

        private void ColorChangeTimer_Tick(object sender, EventArgs e)
        {
            switch (mState)
            {
                case OperatingState.Leave:
                    break;
                case OperatingState.Enter:
                    break;
                case OperatingState.Down:

                    ColorChangeTimer.Stop();
                    break;
                case OperatingState.Up:


                    break;
            }
            Refresh();
        }
    }
}
