using System.Windows.Forms;
using System.ComponentModel;
using System;

namespace CountDownButtonLib
{
    public partial class CountDownButton : Button
    {
        #region 変数
        int mCountNum = 0;
        int mCountMax = 4;
        string mStartText = "";

        [Category("カスタム"), Description("初期の最大カウント")]
        public int CountMax
        {
            get { return mCountMax; }
            set { mCountMax = value; InitCount(); }
        }

        [Category("カスタム"), Description("カウント開始時に表示するテキスト")]
        public string StartText
        {
            get { return mStartText; }
            set { mStartText = value; InitCount(); }
        }

        [Category("カスタム"), Description("カウント終了時に表示するテキスト")]
        public string FinishText { get; set; } = "Finish";
        #endregion

        #region ボタンイベント処理
        [Category("カスタム"), Description("カウント開始時の処理")]
        public event EventHandler OnStartCountDown = (sender, e) => {
            CountDownButton btn = sender as CountDownButton;
            btn.Text = btn.StartText;
        };
        [Category("カスタム"), Description("カウント終了時の処理")]
        public event EventHandler OnFinishCountDown = (sender, e) => {
            CountDownButton btn = sender as CountDownButton;
            btn.Text = btn.FinishText;
        };
        #endregion

        public CountDownButton()
        {
            InitializeComponent();
            InitCount();
        }

        private void InitCount()
        {
            mCountNum = CountMax;
            DrawText();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            mCountNum--;
            DrawText();
            base.OnMouseDown(mevent);
        }

        private void DrawText()
        {
            Text = mCountNum.ToString();
            if (mCountNum == mCountMax)
                OnStartCountDown(this, EventArgs.Empty);
            else if (mCountNum == 0)
                OnFinishCountDown(this, EventArgs.Empty);
            else if (mCountNum < 0)
                InitCount();
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
