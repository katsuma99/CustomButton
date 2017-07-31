using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ModernButtonLib
{
    public partial class HoverEffect : Button
    {
        public enum OperatingState
        {
            Leave,
            Enter,
            Down,
            Up
        }

        public enum Number0To100
        {
            _0, _1, _2, _3, _4, _5, _6, _7, _8, _9,
            _10, _11, _12, _13, _14, _15, _16, _17, _18, _19,
            _20, _21, _22, _23, _24, _25, _26, _27, _28, _29,
            _30, _31, _32, _33, _34, _35, _36, _37, _38, _39,
            _40, _41, _42, _43, _44, _45, _46, _47, _48, _49,
            _50, _51, _52, _53, _54, _55, _56, _57, _58, _59,
            _60, _61, _62, _63, _64, _65, _66, _67, _68, _69,
            _70, _71, _72, _73, _74, _75, _76, _77, _78, _79,
            _80, _81, _82, _83, _84, _85, _86, _87, _88, _89,
            _90, _91, _92, _93, _94, _95, _96, _97, _98, _99, _100,
        }


        #region 変数
        Color mButtonColor = DefaultBackColor;

        OperatingState mState = OperatingState.Leave;
        [Category("カスタム"), Description("初期のボタン状態（通常・選択・決定）")]
        [DefaultValue(typeof(OperatingState), "None")]
        public OperatingState State
        {
            get { return mState; }
            set
            {
                if (mState == value) return;
                mState = value;
                ColorChangeTimer.Start();
            }
        }

        [Browsable(false)]
        public new FlatStyle FlatStyle
        {
            get { return base.FlatStyle; }
            set { base.FlatStyle = value; }
        }

        public int cornerR = 10;// コーナーの角丸のサイズ（直径）
        [Category("カスタム"), Description("角の丸さ")]
        public Number0To100 CornerR
        {
            get
            {
                return (Number0To100)(cornerR / 2);
            }
            set
            {
                if (value > 0)
                    cornerR = (int)value * 2;
                else
                    value = Number0To100._1;

                Refresh();
            }
        }

        [Category("カスタム"), Description("ボタンの色（通常時）")]
        public Color MouseLeaveButtonColor { get; set; } = Color.Black;
        [Category("カスタム"), Description("ボタンの色（マウスオーバー時）")]
        public Color MouseOverButtonColor { get; set; } = Color.Gray;
        [Category("カスタム"), Description("ボタンの色（マウスダウン時）")]
        public Color MouseDownButtonColor { get; set; } = Color.Turquoise;
        #endregion

        public HoverEffect()
        {
            InitializeComponent();
            mState = OperatingState.Leave;
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
                    mButtonColor = MouseLeaveButtonColor;
                    break;
                case OperatingState.Enter:
                    mButtonColor = MouseOverButtonColor;
                    break;
                case OperatingState.Down:
                    mButtonColor = MouseDownButtonColor; 
                    ColorChangeTimer.Stop();
                    break;
                case OperatingState.Up:
                    mButtonColor = MouseOverButtonColor;

                    break;
            }
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            DrawButtonSurface(pe.Graphics);
        }

        // ボタンの表面描画
        private void DrawButtonSurface(Graphics g)
        {
            // 変数初期化
            int w = this.Width - cornerR;
            int h = this.Height - cornerR;
            int harfHeight = (int)(this.Height / 2);

            // ボタンの表面のパス初期化
            GraphicsPath graphPath = new GraphicsPath();
            graphPath.AddArc(0, 0, cornerR, cornerR, 180, 90);
            graphPath.AddArc(w, 0, cornerR, cornerR, 270, 90);
            graphPath.AddArc(w, h, cornerR, cornerR, 0, 90);
            graphPath.AddArc(0, h, cornerR, cornerR, 90, 90);
            graphPath.CloseFigure();


            using (Brush brush = new SolidBrush(mButtonColor))
            {
                g.FillPath(brush, graphPath);
            }

            // ボタンの文字列描画
            //DrawText(g);
            
        }
    }
}
