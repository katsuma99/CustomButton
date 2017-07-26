using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleButtonLib
{
    [DefaultEvent("OnReleaseButton")]
    [DefaultProperty("NormalImage")]
    public partial class BaseButton : PictureBox
    {
        public enum BtState
        {
            Normal,
            Select,
            Pushed
        }

        #region 変数
        BtState mState = BtState.Normal;
        [Category("カスタム：ボタンイメージ"), Description("初期のボタン状態（通常・選択・決定）")]
        [DefaultValue(typeof(BtState), "None")]
        public BtState State //名前で自動コードの順番が決まるため、normal,pushed,select,stateの順にSetが実行される
        {
            get { return mState; }
            set { mState = value; DrawNowStateButton(false); }
        }

        protected Image mNormalImage;
        [Category("カスタム：ボタンイメージ"), Description("通常のボタンのイメージ画像")]
        [DefaultValue(null)]
        [NotifyParentProperty(true)]    //親のImageにプロパティ変更を通知して更新してもらう
        public Image NormalImage
        {
            get { return mNormalImage; }
            set { if (value == null) return;  mNormalImage = value; DrawNowStateButton(); }
        }

        protected Image mSelectImage;
        [Category("カスタム：ボタンイメージ"), Description("ボタンを選択した時のイメージ画像")]
        [DefaultValue(null)]
        [NotifyParentProperty(true)]
        public Image SelectImage
        {
            get { return mSelectImage; }
            set { if (value == null) return; mSelectImage = value; DrawNowStateButton(); }
        }

        protected Image mPushedImage;
        [Category("カスタム：ボタンイメージ"), Description("ボタンを押下した時のイメージ画像")]
        [DefaultValue(null)]
        [NotifyParentProperty(true)]
        public Image PushedImage
        {
            get { return mPushedImage; }
            set { if (value == null) return; mPushedImage = value; DrawNowStateButton(); }
        }
        #endregion

        public BaseButton()
        {
            InitializeComponent();
            mState = BtState.Normal;

            if (mSelectImage == null) SelectImage = global::ImageButtonLib.Properties.Resources.BtSelect;
            if (mPushedImage == null) PushedImage = global::ImageButtonLib.Properties.Resources.BtPushed;
            if (mNormalImage == null) NormalImage = global::ImageButtonLib.Properties.Resources.BtNormal;

        }

        ~BaseButton()
        {
            mSelectImage?.Dispose();
            mNormalImage?.Dispose();
            mPushedImage?.Dispose();
        }

        void DrawNowStateButton(bool isResize = true)
        {
            if (mNormalImage == null || mSelectImage == null || mPushedImage == null)
                return;

            switch (mState)
            {
                case BtState.Normal:
                    Image = mNormalImage;
                    if(isResize) Size = mNormalImage.Size;
                    break;
                case BtState.Select:
                    Image = mSelectImage;
                    if (isResize) Size = mSelectImage.Size;
                    break;
                case BtState.Pushed:
                    Image = mPushedImage;
                    if (isResize) Size = mPushedImage.Size;
                    break;
            }
            Refresh();
        }

        #region ボタンイベント処理
        [Category("カスタム：ボタン処理"), Description("ボタンを押下した時に入る処理")]
        public event EventHandler OnPushButtonEvent = (sender, e) => {
            BaseButton btn = sender as BaseButton;
            btn.mState = BtState.Pushed;
            btn.Image = btn.mPushedImage;
        };

        [Category("カスタム：ボタン処理"), Description("ボタンをリリースした時に入る処理")]
        public event EventHandler OnReleaseButtonEvent = (sender, e) => {
            BaseButton btn = sender as BaseButton;
            btn.mState = BtState.Select;
            btn.Image = btn.mSelectImage;
        };

        [Category("カスタム：ボタン処理"), Description("ボタンに侵入した時に入る処理")]
        public event EventHandler OnEnterButtonEvent = (sender, e) => {
            BaseButton btn = sender as BaseButton;
            btn.mState = BtState.Select;
            btn.Image = btn.mSelectImage;
        };

        [Category("カスタム：ボタン処理"), Description("ボタンから退出した時に入る処理")]
        public event EventHandler OnLeaveButtonEvent = (sender, e) => {
            BaseButton btn = sender as BaseButton;
            btn.mState = BtState.Normal;
            btn.Image = btn.mNormalImage;
        };


        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            OnPushButton();
            base.OnMouseDown(mevent);
        }

        public virtual void OnPushButton()
        {
            OnPushButtonEvent(this, EventArgs.Empty);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            OnReleaseButton();
            base.OnMouseUp(mevent);
        }

        public virtual void OnReleaseButton()
        {
            OnReleaseButtonEvent(this, EventArgs.Empty);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            OnEnterButtonEvent(this, EventArgs.Empty);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            OnLeaveButtonEvent(this, EventArgs.Empty);
            base.OnMouseLeave(e);
        }
        #endregion
    }
}
