using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ModernButtonLib
{
    public partial class RoundCornerButton : Button
    {
		private int shadowSize = 6;     // 影の大きさ
        private int cornerR = 10;       // コーナーの角丸のサイズ（直径）

        #region "デザイン時外部公開プロパティ"
        [Category("ModernDesign")]
        [Browsable(true)]
        [Description("角の丸さを指定します。（半径）")]
        public int CornerR
        {
            get
            {
                return (int)(cornerR / 2);
            }
            set
            {
                if (value > 0)
                    cornerR = value * 2;
                else
                    throw new ArgumentException("Corner R", "0 以上の値を入れてください。");

                RenewPadding();
                Refresh();
            }
        }

        [Category("ModernDesign")]
        [Browsable(true)]
        [Description("影の大きさを指定します。")]
        public int ShadowSize
        {
            get
            {
                return shadowSize;
            }
            set
            {
                if (value >= 0)
                    shadowSize = value;
                else
                    throw new ArgumentException("ShadowSize", "0 以上の値を入れてください。");

                RenewPadding();
                Refresh();
            }
        }
        #endregion

        public RoundCornerButton()
        {
            InitializeComponent();

            // コントロールのサイズが変更された時に Paint イベントを発生させる
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.BackColor = Color.Transparent;

            RenewPadding();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            Refresh();
            Graphics g = this.CreateGraphics();
            DrawButtonCorner(g, FlatAppearance.MouseOverBackColor);
            g.Dispose();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            Refresh();

            if (this.Focused)
            {
                Graphics g = this.CreateGraphics();
                DrawButtonCorner(g, BackColor);
                g.Dispose();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            //if (!mouseDowning)
            //{
            //    mouseDowning = true;
                Refresh();
                Graphics g = this.CreateGraphics();
                DrawButtonSurfaceDown(g);
            //DrawButtonCorner(g, FlatAppearance.MouseDownBackColor);
                g.Dispose();
            //}

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            //if (mouseDowning)
            //{
            //    mouseDowning = false;
                Refresh();
                Graphics g = this.CreateGraphics();
                DrawButtonCorner(g, FlatAppearance.MouseOverBackColor);
                g.Dispose();
            //}

            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawButtonSurface(e.Graphics);
        }

        #region 角を丸めたボタンを描画

        // Padding サイズ更新
        private void RenewPadding()
        {
            int harfCornerR = (int)(cornerR / 2);
            int adjust = (int)(Math.Cos(45 * Math.PI / 180) * harfCornerR);
            this.Padding = new Padding(harfCornerR + shadowSize - adjust);
        }

        // ボタンの文字列を描画
        private void DrawText(Graphics g)
        {
            // 描画領域の設定
            Rectangle rectangle = new Rectangle(this.Padding.Left,
                                                this.Padding.Top,
                                                this.Width - this.Padding.Left - this.Padding.Right,
                                                this.Height - this.Padding.Top - this.Padding.Bottom);

            // 文字列が描画領域に収まるように調整
            StringBuilder sb = new StringBuilder();
            StringBuilder sbm = new StringBuilder();

            foreach (char c in Text)
            {
                sbm.Append(c);
                Size size = TextRenderer.MeasureText(sbm.ToString(), this.Font);

                if (size.Width > rectangle.Width - this.Font.Size)
                {
                    sbm.Remove(sbm.Length - 1, 1);
                    sbm.Append(c);
                    sbm.AppendLine("");
                    sb.Append(sbm.ToString());
                    sbm = new StringBuilder();
                }
            }
            sb.Append(sbm.ToString());

            // 調整済みの文字列を描画
            TextRenderer.DrawText(g,
                sb.ToString(),
                this.Font,
                rectangle,
                this.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        // ボタンの描画品質設定
        private void SetSmoothMode(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
        }

        // 影用のパスを取得
        private GraphicsPath GetShadowPath()
        {
            GraphicsPath gp = new GraphicsPath();

            int w = this.Width - cornerR;
            int h = this.Height - cornerR;
            /*
			int ox = this.Width;
			int oy = this.Height;

			if (ox < oy)
			{
				float ratio = (float)shadowSize / oy;
				ox = shadowSize - (int)(ratio * ox);
				oy = 0;
			}
			else
			{
				float ratio = (float)shadowSize / ox;
				ox = 0;
				oy = shadowSize - (int)(ratio * oy);
			}

			gp.AddArc(ox, oy, cornerR, cornerR, 180, 90);
			gp.AddArc(w - ox, oy, cornerR, cornerR, 270, 90);
			gp.AddArc(w - ox, h - oy, cornerR, cornerR, 0, 90);
			gp.AddArc(ox, h - oy, cornerR, cornerR, 90, 90);
			gp.CloseFigure();
			*/
            gp.AddArc(0, 0, cornerR, cornerR, 180, 90);
            gp.AddArc(w, 0, cornerR, cornerR, 270, 90);
            gp.AddArc(w, h, cornerR, cornerR, 0, 90);
            gp.AddArc(0, h, cornerR, cornerR, 90, 90);
            gp.CloseFigure();

            return gp;
        }

        // 影用のブラシ取得
        private PathGradientBrush GetShadowBrush(GraphicsPath graphicsPath)
        {
            PathGradientBrush brush = new PathGradientBrush(graphicsPath);
            ColorBlend colorBlend = new ColorBlend();
            float pos = 0;

            if (this.Width < this.Height)
                pos = ((float)shadowSize * 2 / this.Height);
            else
                pos = ((float)shadowSize * 2 / this.Width);

            colorBlend.Positions = new float[3] { 0.0f, pos, 1.0f };

            colorBlend.Colors = new Color[3] {
                    Color.FromArgb(0, Color.White),
                    Color.FromArgb(20, 0, 0, 0),
                    Color.FromArgb(20, 0, 0, 0)
            };

            brush.CenterColor = Color.Black;
            brush.CenterPoint = new PointF(this.Width / 2, this.Height / 2);
            brush.InterpolationColors = colorBlend;

            return brush;
        }

        // ボタンの表面描画
        private void DrawButtonSurface(Graphics g)
        {
            // 描画品質設定
            SetSmoothMode(g);

            // 変数初期化
            int offset = shadowSize;
            int w = this.Width - cornerR;
            int h = this.Height - cornerR;
            int harfHeight = (int)(this.Height / 2);

            // 影用のパス初期化
            GraphicsPath shadowPath = null;
            if (shadowSize > 0)
                shadowPath = GetShadowPath();

            // ボタンの表面のパス初期化
            GraphicsPath graphPath = new GraphicsPath();
            graphPath.AddArc(offset, offset, cornerR, cornerR, 180, 90);
            graphPath.AddArc(w - offset, offset, cornerR, cornerR, 270, 90);
            graphPath.AddArc(w - offset, h - offset, cornerR, cornerR, 0, 90);
            graphPath.AddArc(offset, h - offset, cornerR, cornerR, 90, 90);
            graphPath.CloseFigure();

            // ボタンのハイライト部分のパス初期化
            offset += 1;
            //cornerR -= 1;
            GraphicsPath graphPath2 = new GraphicsPath();
            graphPath2.AddArc(offset, offset, cornerR, cornerR, 180, 90);
            graphPath2.AddArc(w - offset, offset, cornerR, cornerR, 270, 90);
            graphPath2.AddLine(this.Width - offset, offset + (int)(cornerR / 2), this.Width - offset, harfHeight);
            graphPath2.AddLine(offset, harfHeight, offset, harfHeight);
            graphPath2.CloseFigure();


            // 影用のブラシ初期化
            PathGradientBrush shadowBrush = null;
            if (shadowSize > 0)
                shadowBrush = GetShadowBrush(shadowPath);

            // ボタンの表面用のブラシ初期化
            Brush fillBrush = new SolidBrush(BackColor);

            // 影 → 表面 → ハイライトの順番でパスを塗る
            if (shadowSize > 0)
                g.FillPath(shadowBrush, shadowPath);
            g.FillPath(fillBrush, graphPath);

            // ボタンの文字列描画
            DrawText(g);

            // 後処理
            if (shadowSize > 0)
                shadowPath.Dispose();
            graphPath.Dispose();
            graphPath2.Dispose();

            if (shadowSize > 0)
                shadowBrush.Dispose();
            fillBrush.Dispose();
        }

        // ボタンの表面描画　（マウス Down イベント時）
        private void DrawButtonSurfaceDown(Graphics g)
        {
            // 描画品質設定
            SetSmoothMode(g);

            // 変数初期化
            int offset = shadowSize;
            int w = this.Width - cornerR;
            int h = this.Height - cornerR;


            // ボタンの表面用のブラシ初期化
            Brush fillBrush = new SolidBrush(Color.FromArgb(30, Color.Black));

            GraphicsPath graphPath = new GraphicsPath();
            graphPath.AddArc(offset, offset, cornerR, cornerR, 180, 90);
            graphPath.AddArc(w - offset, offset, cornerR, cornerR, 270, 90);
            graphPath.AddArc(w - offset, h - offset, cornerR, cornerR, 0, 90);
            graphPath.AddArc(offset, h - offset, cornerR, cornerR, 90, 90);
            graphPath.CloseFigure();

            g.FillPath(fillBrush, graphPath);

            graphPath.Dispose();
            fillBrush.Dispose();
        }

        // マウスがボタンの領域に入った時に、枠に色をつける処理
        // と、フォーカスが当たっている時に、枠に色をつける処理用
        private void DrawButtonCorner(Graphics g, Color color)
        {
            // 描画品質設定
            SetSmoothMode(g);

            // 変数初期化
            int offset = shadowSize;
            int w = this.Width - cornerR;
            int h = this.Height - cornerR;

            // ペンの初期化
            Pen cornerPen = new Pen(Color.FromArgb(128, color), 2);
            //Pen cornerPen = new Pen(color, 1.5f);

            // 描画領域の初期化
            GraphicsPath graphPath = new GraphicsPath();
            graphPath.AddArc(offset, offset, cornerR, cornerR, 180, 90);
            graphPath.AddArc(w - offset, offset, cornerR, cornerR, 270, 90);
            graphPath.AddArc(w - offset, h - offset, cornerR, cornerR, 0, 90);
            graphPath.AddArc(offset, h - offset, cornerR, cornerR, 90, 90);
            graphPath.CloseFigure();

            // 描画
            g.DrawPath(cornerPen, graphPath);

            // 後処理
            graphPath.Dispose();
            cornerPen.Dispose();
        }
        #endregion

    }
}
