using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleButtonLib;

namespace ClassLibrary1
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
 
        ////設定データを記録するために変数作る（クラスの配列は記録できない？）
        //SimpleButton mSimpleButton1 = new SimpleButton();
        //[Category("カスタム：ボタン"), Description("通常・選択・押下のボタンのイメージ画像")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public SimpleButton SimpleButton1
        //{
        //    get { return mSimpleButton1; }
        //    set { mSimpleButton1 = value; }
        //}
    }
}
