using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace StateButtonLib
{
    public enum BtState
    {
        Normal,
        Select,
        Pushed
    }

    [DefaultEvent("OnReleaseButtonEvent")]
    public abstract class StateButton : PictureBox
    {
        #region 変数
        public BtState mState = BtState.Normal;
        [DefaultValue(typeof(BtState), "None")]
        [Category("カスタム：ボタン"), Description("ボタンの初期状態（編集時にも変更すると確認できる）")]
        public BtState InitState
        {
            get { return mState; }
            set { mState = value; GetNowCustomButton().ChangeButton(mState); }
        }

        public enum SBtState
        {
            Button1,
            Button2,
            Button3,
            Button4,
            Button5,
            Button6,
            Button7,
            Button8,
            ButtonHightDummy,//ループするため、maxの1つ上にトリガー
            ButtonLowDummy,//ループするため、minの1つ下にトリガー
        }

        protected SBtState mMaxSBtState;
        public int mCustomButtonState = 0;
        [Category("カスタム：ステート"), Description("ステートボタンの現在のパターン")]
        [DefaultValue(0)]
        public SBtState State
        {
            get
            {
                return (SBtState)mCustomButtonState;
            }
            set
            {
                mCustomButtonState = (int)value;
                if (mCustomButtonState == (int)SBtState.ButtonLowDummy)
                    mCustomButtonState = mStateMax - 1;
                else if (mCustomButtonState >= mStateMax)
                    mCustomButtonState = 0;

                GetNowCustomButton().ChangeButton(mState);
            }
        }

        public int mStateMax = 1;
        [Category("カスタム：ステート"), Description("ステートボタンのパターン数(max:3)")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(0)]
        public int StateMax
        {
            get { return mStateMax; }
            set
            {
                mStateMax = Math.Min((int)mMaxSBtState+1, value); ResizeStatePattern(mStateMax);
            }
        }
        #endregion

        #region　関数
        //CustomButtonにBtStateを渡すためにStateButtonを渡す
        protected abstract void InitCustomButton();
        //ステートボタンの状態パターンを変更する
        protected abstract void ResizeStatePattern(int stateMax);
        //現在のカスタムボタンを取得（ステートボタンはカスタムボタンの集まり）
        public abstract StateButtonProperty GetNowCustomButton(int nextNum = 0);
        #endregion

        #region イベント
        [Category("カスタム：ボタン処理"), Description("ボタンを押下した時に入る処理")]
        public event EventHandler OnPushButtonEvent = (sender, e) => { };
        [Category("カスタム：ボタン処理"), Description("ボタンをリリースした時に入る処理")]
        public event EventHandler OnReleaseButtonEvent = (sender, e) => { };
        [Category("カスタム：ボタン処理"), Description("ボタンに侵入した時に入る処理")]
        public event EventHandler OnEnterButtonEvent = (sender, e) => { };
        [Category("カスタム：ボタン処理"), Description("ボタンから退出した時に入る処理")]
        public event EventHandler OnLeaveButtonEvent = (sender, e) => { };

        public void OnPushButton() { OnPushButtonEvent(this, EventArgs.Empty); }
        public void OnReleaseButton() { OnReleaseButtonEvent(this, EventArgs.Empty); }
        public void OnEnterButton() { OnEnterButtonEvent(this, EventArgs.Empty); }
        public void OnLeaveButton() { OnLeaveButtonEvent(this, EventArgs.Empty); }

        #region ボタンイベント処理
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            GetNowCustomButton().OnPushButton();
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            GetNowCustomButton().OnReleaseButton();
            base.OnMouseUp(mevent);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            GetNowCustomButton().OnEnterButton();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            GetNowCustomButton().OnLeaveButton();
            base.OnMouseLeave(e);
        }
        #endregion
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            GetNowCustomButton().OnPaint(pe);
        }
        #endregion
    }

    [DefaultProperty("NormalImage")]
    [TypeConverter(typeof(CustomButtonPropertyConverter))]
    public class StateButtonProperty
    {
        #region 変数
        PictureBox mButton = null;
        [Browsable(false)]
        public PictureBox Button
        {
            get { return mButton; }
            set { mButton = value;}
        }

        #region ボタン画像
        protected List<Image> mButtonImage;
        Image mOriNormalImage = null;
        [Description("通常のボタンのイメージ画像")]
        [DefaultValue(null)]
        [NotifyParentProperty(true)]    //親のImageにプロパティ変更を通知して更新してもらう
        public Image NormalImage
        {
            get { return mButtonImage[(int)BtState.Normal]; }
            set
            {
                if (value == null) return; mButtonImage[(int)BtState.Normal] = value;
                mOriNormalImage = (Image)value.Clone(); ChangeButton(BtState.Normal);
                //リサイズ用にフォントとボタンのサイズ比率記録
                if (mFont != null && mText.Length != 0)
                    mTextScale = mFont.Size * mText.Length / NormalImage.Size.Width;
            }
        }

        Image mOriSelectlImage = null;
        [Description("ボタンを選択した時のイメージ画像")]
        [DefaultValue(null)]
        [NotifyParentProperty(true)]
        public Image SelectImage
        {
            get { return mButtonImage[(int)BtState.Select]; }
            set
            {
                if (value == null) return; mButtonImage[(int)BtState.Select] = value;
                mOriSelectlImage = (Image)value.Clone(); ChangeButton(BtState.Select);
            }
        }

        Image mOriPushedImage = null;
        [Description("ボタンを押下した時のイメージ画像")]
        [DefaultValue(null)]
        [NotifyParentProperty(true)]
        public Image PushedImage
        {
            get { return mButtonImage[(int)BtState.Pushed]; }
            set
            {
                if (value == null) return; mButtonImage[(int)BtState.Pushed] = value;
                mOriPushedImage = (Image)value.Clone(); ChangeButton(BtState.Pushed);
            }
        }
        #endregion

        #region ボタンテキスト
        protected string mText;
        [Description("ボタンに表示させる文字")]
        [DefaultValue("StateButton")]
        public string MyText
        {
            get
            {
                return mText;
            }
            set
            {
                mText = value;
                if (mButton == null)
                    return;
                mButton.Invalidate();
            }
        }

        protected Color mForeColor = Color.White;
        [DefaultValue(typeof(Color), "White")]
        [Description("ボタンに表示させる文字の色")]
        public Color MyForeColor
        {
            get
            {
                return mForeColor;
            }
            set
            {
                mForeColor = value;
                if (mButton != null) mButton.Invalidate();
            }
        }

        float mTextScale = -1;
        protected Font mFont;
        [Description("ボタンに表示させる文字フォント")]
        public Font MyFont
        {
            get
            {
                return mFont;
            }
            set
            {
                mFont = value;
                if (mButton == null) return;

                mButton.Invalidate();
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
        public Alignment MyStringAlignment
        {
            get
            {
                return mStringAlignment;
            }
            set
            {
                mStringAlignment = value;
                if (mButton == null) return;
                mButton.Invalidate();
            }
        }
        #endregion

        #endregion

        StateButton mStateButton = null;
        public StateButtonProperty()
        {
            InitImage();
            InitText();
        }

        public void SetStateButton(StateButton stateButton)
        {
            mStateButton = stateButton;
        }

        void InitImage()
        {
            if (mButtonImage != null)
                return;
            mButtonImage = new List<Image>();
            mButtonImage.Add(global::CustomButtonLib.Properties.Resources.BtNormal);
            mOriNormalImage = (Image)mButtonImage[0].Clone();
            mButtonImage.Add(global::CustomButtonLib.Properties.Resources.BtSelect);
            mOriSelectlImage = (Image)mButtonImage[1].Clone();
            mButtonImage.Add(global::CustomButtonLib.Properties.Resources.BtPushed);
            mOriPushedImage = (Image)mButtonImage[2].Clone();
            ChangeButton(BtState.Normal);
        }

        void InitText()
        {
            mText = "StateButton";
            mFont = new Font("Arial", 8, FontStyle.Bold);
        }

        ~StateButtonProperty()
        {
            SelectImage?.Dispose();
            NormalImage?.Dispose();
            PushedImage?.Dispose();
            mFont?.Dispose();
        }

        public void ChangeButton(BtState state)
        {
            if (Button == null || mButtonImage == null || NormalImage == null || SelectImage == null || PushedImage == null)
                return;

            switch (state)
            {
                case BtState.Normal:
                    Button.Image = NormalImage;
                    Button.Size = NormalImage.Size;
                    break;
                case BtState.Select:
                    Button.Image = SelectImage;
                    Button.Size = SelectImage.Size;
                    break;
                case BtState.Pushed:
                    Button.Image = PushedImage;
                    Button.Size = PushedImage.Size;
                    break;
            }
            Button.Refresh();
        }

        public void ResizeImage(Size size)
        {
            if (mOriNormalImage == null)
                return;
            for (int state = 0; state < mButtonImage.Count; state++)
            {
                switch (state)
                {
                    case (int)BtState.Normal:
                        mButtonImage[state] = new Bitmap(mOriNormalImage, size);
                        break;
                    case (int)BtState.Select:
                        mButtonImage[state] = new Bitmap(mOriSelectlImage, size);
                        break;
                    case (int)BtState.Pushed:
                        mButtonImage[state] = new Bitmap(mOriPushedImage, size);
                        break;
                }

            if(mButton != null && mTextScale != -1) mFont = new Font(mFont.FontFamily, mButton.Size.Width * mTextScale / mText.Length, mFont.Style);
            }
        }

        #region イベント処理
        #region ボタンイベント処理
        public void OnPushButton()
        {
            if (Button == null || mStateButton == null) return;

            mStateButton.mState = BtState.Pushed;
            Button.Image = PushedImage;
            mStateButton.OnPushButton();
        }

        public void OnReleaseButton()
        {
            if (Button == null || mStateButton == null) return;

            mStateButton.mState = BtState.Select;
            Button.Image = SelectImage;
            mStateButton.OnReleaseButton();

            if (++mStateButton.mCustomButtonState >= mStateButton.mStateMax) mStateButton.mCustomButtonState = 0;
        }

        public void OnEnterButton()
        {
            if (Button == null || mStateButton == null) return;

            mStateButton.mState = BtState.Select;
            Button.Image = SelectImage;
            mStateButton.OnEnterButton();
        }

        public void OnLeaveButton()
        {
            if (Button == null || mStateButton == null) return;

            mStateButton.mState = BtState.Normal;
            Button.Image = NormalImage;
            mStateButton.OnLeaveButton();
        }
        #endregion

        public void OnPaint(PaintEventArgs pe)
        {
            using (Brush brush = new SolidBrush(mForeColor))
            {
                StringFormat strFormat = new StringFormat();
                strFormat.Alignment = (StringAlignment)((int)mStringAlignment % 3);
                strFormat.LineAlignment = (StringAlignment)((int)mStringAlignment / 3);
                pe.Graphics.DrawString(mText, mFont, brush, new RectangleF(new Point(0, 0), mButton.Size), strFormat);
            }
        }
        #endregion
    }

    public class CustomButtonPropertyConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            else
                return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            else
                return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {

            StateButtonProperty baseButtonProp = value as StateButtonProperty;
            if (baseButtonProp == null || destinationType != typeof(string))
                return base.ConvertTo(context, culture, value, destinationType);

            if (baseButtonProp.Button == null)
                return string.Format("Out of Range");
            else
                return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
