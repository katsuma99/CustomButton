using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ModernButtonLib
{
    [DefaultEvent("Click")]
    public partial class RoundCornerButton : Button
    {
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
        protected Color mButtonColor;
        protected GraphicsPath graphPath = new GraphicsPath();

        [Browsable(false)]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        [Browsable(false)]
        public new FlatStyle FlatStyle
        {
            get { return base.FlatStyle; }
            set { base.FlatStyle = value; }
        }

        [Browsable(false)]
        public new FlatButtonAppearance FlatAppearance
        {
            get { return base.FlatAppearance; }
        }

        public int cornerR = 20;// コーナーの角丸のサイズ（直径）
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

        public RoundCornerButton()
        {
            InitializeComponent();
            InitSetting();
        }

        private void InitSetting()
        {
            mButtonColor = MouseLeaveButtonColor;
            Font = new Font("Arial", 8, FontStyle.Bold);
            ForeColor = Color.White;
            MakeGraphPath();
            Refresh();
        }

        private void MakeGraphPath()
        {
            // 変数初期化
            int w = this.Width - cornerR;
            int h = this.Height - cornerR;

            // ボタンの表面のパス初期化
            graphPath.AddArc(0, 0, cornerR, cornerR, 180, 90);
            graphPath.AddArc(w, 0, cornerR, cornerR, 270, 90);
            graphPath.AddArc(w, h, cornerR, cornerR, 0, 90);
            graphPath.AddArc(0, h, cornerR, cornerR, 90, 90);
            graphPath.CloseFigure();
        }

        #region ボタンイベント処理
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            mButtonColor = MouseDownButtonColor;
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            mButtonColor = MouseOverButtonColor;
            base.OnMouseUp(mevent);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            mButtonColor = MouseOverButtonColor;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            mButtonColor = MouseLeaveButtonColor;
            base.OnMouseLeave(e);
        }
        #endregion

        protected override void OnPaint(PaintEventArgs pe)
        {
            DrawButtonBack(pe);
            DrawButton(pe.Graphics);
            DrawButtonText(pe.Graphics);
        }

        //丸ボタン外側の矩形部分の描画（透明描画では黒くなる）//base.OnPaintの内部処理？
        protected void DrawButtonBack(PaintEventArgs pe)
        {
            // 親がいない場合は無視
            if (this.Parent == null) return;

            Point offset = new Point(Left, Top);

            // 原点を親コントロールの座標へ
            pe.Graphics.TranslateTransform(
                -offset.X, -offset.Y);
            // 親コントロールを描画
            this.InvokePaintBackground(this.Parent, pe);
            this.InvokePaint(this.Parent, pe);
            // 原点の座標を戻す
            pe.Graphics.TranslateTransform(offset.X, offset.Y);
        }

        // ボタンの描画
        protected void DrawButton(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            using (Brush buttonBrush = new SolidBrush(mButtonColor))
            {
                g.FillPath(buttonBrush, graphPath);
            }
        }

        //テキストの描画
        protected void DrawButtonText(Graphics g)
        {
            using (Brush textBrush = new SolidBrush(ForeColor))
            {
                TextRenderer.DrawText(g,
                                     Text,
                                     Font,
                                     new Rectangle(new Point(0, 0), this.Size),
                                     ForeColor,
                                     TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }
    }
}
