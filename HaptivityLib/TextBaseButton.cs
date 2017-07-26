using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleButtonLib
{
    [DefaultProperty("Text")]
    public partial class TextBaseButton : BaseButton
    {
        #region 変数
        protected string mText;
        [Category("カスタム：ボタンテキスト"), Description("ボタンに表示させる文字")]
        [Bindable(true),Browsable(true),EditorBrowsable(EditorBrowsableState.Always)]
        //[通知？、プロパティウィンドウに表示、インテリセンスに表示(ソースを書くところで、[.]と入力したあとに出てくるメソッド一覧)]
        public override string Text
        {
            get
            {
                return mText;
            }
            set
            {
                mText = value;
                //if (mText.Length > 0)
                //{
                //    //文字のサイズをボタン幅に合わせて調整
                //    float size = (this.Size.Width * 0.9f) / (float)mText.Length;
                //    mFont = new Font(mFont.FontFamily, Math.Max(size,8), mFont.Style);
                //}
                Invalidate();
            }
        }

        protected Color mForeColor = Color.White;
        [DefaultValue(typeof(Color),"White")]
        [Category("カスタム：ボタンテキスト"), Description("ボタンに表示させる文字の色")]
        [Bindable(true), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public override Color ForeColor
        {
            get
            {
                return mForeColor;
            }
            set
            {
                mForeColor = value;
                Invalidate();
            }
        }

        protected Font mFont;
        [Category("カスタム：ボタンテキスト"), Description("ボタンに表示させる文字フォント")]
        [Bindable(true), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Font MyFont
        {
            get
            {
                return mFont;
            }
            set
            {
                mFont = value;
                Invalidate();
            }
        }

        public enum Alignment
        {
            UpLeft,
            Up,
            UpRight,
            Left,
            Center,
            Right,
            DownLeft,
            Down,
            DownRight,
        }
        protected Alignment mStringAlignment = Alignment.Center;
        [Category("カスタム：ボタンテキスト"), Description("ボタンに表示させる文字の場所")]
        [Bindable(true), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Alignment StringAlignment
        {
            get
            {
                return mStringAlignment;
            }
            set
            {
                mStringAlignment = value;
                Invalidate();
            }
        }
        #endregion

        public TextBaseButton()
        {
            mText = "";
            mFont = new Font("Arial", 8, FontStyle.Bold);
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            using (Brush brush = new SolidBrush(mForeColor))
            {
                StringFormat strFormat = new StringFormat();
                strFormat.Alignment = (StringAlignment)((int)mStringAlignment % 3);
                strFormat.LineAlignment = (StringAlignment)((int)mStringAlignment / 3);
                pe.Graphics.DrawString(mText, mFont, brush, new RectangleF(new Point(0, 0), this.Size), strFormat);
            }
        }
    }
}
