using System;
using System.ComponentModel;
using System.Windows.Forms;
using StateButtonLib;

namespace StateButtonLib
{
    [DefaultProperty("CustomButton")]
    public partial class State3Button : StateButtonLib.StateButton
    {
        #region 変数定義
        //設定データを記録するために変数作る（クラスの配列は記録できない？）
        StateButtonProperty mCustomButton1 = new StateButtonProperty();
        [Category("カスタム：ボタン"), Description("通常・選択・押下のボタンのイメージ画像")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StateButtonProperty Button1
        {
            get {return mCustomButton1; }
            set { mCustomButton1 = value; State = SBtState.Button1; }
        }

        StateButtonProperty mCustomButton2 = new StateButtonProperty();
        [Category("カスタム：ボタン"), Description("通常・選択・押下のボタンのイメージ画像")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StateButtonProperty Button2
        {
            get { return mCustomButton2; }
            set { mCustomButton2 = value; State = SBtState.Button2; }
        }

        StateButtonProperty mCustomButton3 = new StateButtonProperty();
        [Category("カスタム：ボタン"), Description("通常・選択・押下のボタンのイメージ画像")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StateButtonProperty Button3
        {
            get { return mCustomButton3; }
            set { mCustomButton3 = value; State = SBtState.Button3; }
        }
        #endregion

        #region 関数
        public State3Button()
        {
            mMaxSBtState = SBtState.Button3;
            InitializeComponent();
            InitCustomButton();
            ResizeStatePattern(mStateMax);
            GetNowCustomButton().ChangeButton(mState);
        }

        //CustomButtonにBtStateを渡すためにStateButtonを渡す
        protected override void InitCustomButton()
        {
            Button1.SetStateButton(this);
            Button2.SetStateButton(this);
            Button3.SetStateButton(this);
        }

        //ステートボタンの状態パターンを変更する
        protected override void ResizeStatePattern(int stateMax)
        {
            Button1.Button = stateMax >= 1 ? this : null;
            Button2.Button = stateMax >= 2 ? this : null;
            Button3.Button = stateMax >= 3 ? this : null;
        }

        //現在のカスタムボタンを取得（ステートボタンはカスタムボタンの集まり）
        public override StateButtonProperty GetNowCustomButton(int nextNum = 0)
        {
            StateButtonProperty cbp = Button1;
            int getState = ((int)State + nextNum) % mStateMax;
            if (getState < 0)
                getState += mStateMax;
            switch ((SBtState)getState)
            {
                case SBtState.Button1: cbp = Button1; break;
                case SBtState.Button2: cbp = Button2; break;
                case SBtState.Button3: cbp = Button3; break;
            }
            return cbp;
        }

        //イメージ画像リサイズ
        private void StateButton_SizeChanged(object sender, EventArgs e)
        {
            StateButtonProperty cbp = null;
            for (int state = 1; state <= 3; state++)
            {
                switch (state)
                {
                    case 1: cbp = Button1; break;
                    case 2: cbp = Button2; break;
                    case 3: cbp = Button3; break;
                }
                cbp.ResizeImage(((PictureBox)sender).Size);
            }
            GetNowCustomButton().ChangeButton(mState);
        }
        #endregion
    }
}
