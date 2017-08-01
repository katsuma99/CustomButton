using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ModernButtonLib
{
    public partial class HoverEffect : RoundCornerButton
    {
        public enum OperatingState
        {
            Leave,
            Enter,
            Down,
            Up
        }

        #region 変数
        Color mButtonFrontColor;
        private Timer mButtonAnimationTimer = new Timer();
        PointF mMousePos = new PointF();
        long mElapsedTime_FadeAnim = 0;
        long mElapsedTime_CircleAnim = 0;

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
                mButtonAnimationTimer.Start();
            }
        }

        [Category("カスタム"), Description("フェードアニメーションの時間")]
        public int FadeAnimationTime { get; set; } = 300;

        [Category("カスタム"), Description("サークルアニメーションの時間")]
        public int CircleAnimationTime { get; set; } = 100;
        #endregion

        public HoverEffect()
        {
            InitializeComponent();
            mState = OperatingState.Leave;
            mButtonAnimationTimer.Interval = 10;
            mButtonAnimationTimer.Tick += new EventHandler(AnimateButton);
        }

        #region ボタンイベント処理
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            State = OperatingState.Down;
            base.OnMouseDown(mevent);
            
            StartButtonAnimation(new PointF(mevent.X, mevent.Y));
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            State = OperatingState.Up;
            base.OnMouseUp(mevent);

            StartButtonAnimation(new PointF(mevent.X, mevent.Y));
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            State = OperatingState.Enter;
            base.OnMouseEnter(e);

            StartButtonAnimation();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            State = OperatingState.Leave;
            base.OnMouseLeave(e);

            StartButtonAnimation();
        }
        #endregion

        protected override void OnPaint(PaintEventArgs pe)
        {
            DrawButtonBack(pe);
            DrawButton(pe.Graphics);
            DrawButtonFront(pe.Graphics);
            DrawButtonText(pe.Graphics);
        }

        // ボタンのアニメーションを描画
        private void DrawButtonFront(Graphics g)
        {
            g.CompositingMode = CompositingMode.SourceOver;
            long elapsedTime = mElapsedTime_FadeAnim;
            int animTime = FadeAnimationTime;
            if (mState == OperatingState.Down || mState == OperatingState.Up)
            {
                elapsedTime = mElapsedTime_CircleAnim;
                animTime = CircleAnimationTime;
            }

            int alpha = (int)(elapsedTime / (float)animTime * 255);
            using (Brush buttonBrush = new SolidBrush(Color.FromArgb(Math.Max(0, Math.Min(255,alpha)), mButtonFrontColor)))
            {
                g.FillPath(buttonBrush, graphPath);
            }
            float radius = mElapsedTime_CircleAnim / (float)CircleAnimationTime * Math.Max(Width,Height);
            using (Brush buttonBrush = new SolidBrush(mButtonFrontColor))
            {
                g.FillEllipse(buttonBrush, mMousePos.X - radius, mMousePos.Y - radius, radius * 2, radius * 2);
            }
        }

        private void StartButtonAnimation(PointF? p = null)
        {
            switch (mState)
            {
                case OperatingState.Leave:
                case OperatingState.Enter:
                    mButtonColor = MouseLeaveButtonColor;
                    mButtonFrontColor = MouseOverButtonColor;
                    break;
                case OperatingState.Down:
                case OperatingState.Up:
                    mButtonColor = MouseOverButtonColor;
                    mButtonFrontColor = MouseDownButtonColor;
                    if (p.HasValue)
                        mMousePos = p.Value;
                    break;
            }
            
            mButtonAnimationTimer.Start();
        }

        void AnimateButton(Object sender, System.EventArgs ea)
        {

            bool isDecrease = mState == OperatingState.Leave || mState == OperatingState.Up;
            int addValue = mButtonAnimationTimer.Interval * (isDecrease ? -1 : 1);
            if (mState != OperatingState.Up)
            {
                mElapsedTime_FadeAnim += addValue;
                mElapsedTime_FadeAnim = Math.Max(0, Math.Min(FadeAnimationTime, mElapsedTime_FadeAnim));
            }
            if (mState != OperatingState.Enter)
            {
                mElapsedTime_CircleAnim += addValue;
                mElapsedTime_CircleAnim = Math.Max(0, Math.Min(CircleAnimationTime, mElapsedTime_CircleAnim));
            }
            else
                mElapsedTime_CircleAnim = 0;

            bool isMaxValue = mElapsedTime_FadeAnim == FadeAnimationTime && mElapsedTime_CircleAnim == CircleAnimationTime;
            bool isMinValue = mElapsedTime_FadeAnim == 0 && mElapsedTime_CircleAnim == 0;
            if (isMaxValue || isMinValue)
                mButtonAnimationTimer.Stop();

            Refresh();
        }
    }
}
