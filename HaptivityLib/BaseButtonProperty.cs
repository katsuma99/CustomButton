using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleButtonLib
{
    public class BaseButtonProperty : UserControl
    {
        protected Image mSelectImage = global::HAPTIVITYLib.Properties.Resources.BtSelect;
        [Category("ボタンイメージ"), Description("ボタンを選択した時のイメージ画像")]
        [Bindable(true), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Image SelectImage
        {
            get { return mSelectImage; }
            set { mSelectImage = value; }
        }

        protected Image mNormalImage = global::HAPTIVITYLib.Properties.Resources.BtNormal;
        [Category("ボタンイメージ"), Description("通常のボタンのイメージ画像")]
        [Bindable(true), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Image NormalImage
        {
            get
            {
                return mNormalImage;
            }
            set
            {
                mNormalImage = value;
            }
        }

        protected Image mPushedImage = global::HAPTIVITYLib.Properties.Resources.BtPushed;
        [Category("ボタンイメージ"), Description("ボタンを押下した時のイメージ画像")]
        [Bindable(true), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Image PushedImage
        {
            get { return mPushedImage; }
            set { mPushedImage = value; }
        }


        public BaseButtonProperty()
        {
        }
    }
}
