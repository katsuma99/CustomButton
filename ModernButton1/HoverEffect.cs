﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ModernButtonLib
{
    public partial class HoverEffect : Button
    {
        protected enum OperatingState
        {
            Leave,
            Enter,
            Down,
            Up
        }

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

        private int shadowSize = 0;// 影の大きさ
        private int cornerR = 1;// コーナーの角丸のサイズ（直径）
        Form mFocusForm = null;

        #region 変数
        Color mButtonColor = DefaultBackColor;

        OperatingState mState = OperatingState.Leave;
        [Category("カスタム：ボタンイメージ"), Description("初期のボタン状態（通常・選択・決定）")]
        [DefaultValue(typeof(OperatingState), "None")]
        protected OperatingState State
        {
            get { return mState; }
            set
            {
                if (mState == value) return;
                mState = value;
                ColorChangeTimer.Start();
            }
        }

        [Browsable(false)]
        public new FlatStyle FlatStyle
        {
            get { return base.FlatStyle; }
            set { base.FlatStyle = value; }
        }
        #endregion

        public HoverEffect()
        {
            InitializeComponent();
            mState = OperatingState.Leave;
            FlatStyle = FlatStyle.Flat;
            if(FlatAppearance.MouseDownBackColor.IsEmpty) FlatAppearance.MouseDownBackColor = Color.Turquoise;
            if(FlatAppearance.MouseOverBackColor.IsEmpty) FlatAppearance.MouseOverBackColor = Color.Gray;
            if(Parent == null && mFocusForm != null) this.Parent = mFocusForm;
        }

        private void FormSupportUtils_ParentChanged(object sender, EventArgs e)
        {
            mFocusForm = (Form)this.Parent;//フォーム生成時にthis.Activate()しないと、エラーメッセージ生成時にとまる
        }

        #region ボタンイベント処理
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            State = OperatingState.Down;
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            State = OperatingState.Up;
            base.OnMouseUp(mevent);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            State = OperatingState.Enter;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            State = OperatingState.Leave;
            base.OnMouseLeave(e);
        }
        #endregion

        private void ColorChangeTimer_Tick(object sender, EventArgs e)
        {
            switch (mState)
            {
                case OperatingState.Leave:
                    mButtonColor = BackColor;
                    break;
                case OperatingState.Enter:
                    mButtonColor = FlatAppearance.MouseOverBackColor;
                    break;
                case OperatingState.Down:
                    mButtonColor = FlatAppearance.MouseDownBackColor;
                    ColorChangeTimer.Stop();
                    break;
                case OperatingState.Up:
                    mButtonColor = FlatAppearance.MouseOverBackColor;

                    break;
            }
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            DrawButtonSurface(pe.Graphics);
        }

        // ボタンの表面描画
        private void DrawButtonSurface(Graphics g)
        {
            // 変数初期化
            int offset = shadowSize;
            int w = this.Width - cornerR;
            int h = this.Height - cornerR;
            int harfHeight = (int)(this.Height / 2);

            // ボタンの表面のパス初期化
            GraphicsPath graphPath = new GraphicsPath();
            graphPath.AddArc(offset, offset, cornerR, cornerR, 180, 90);
            graphPath.AddArc(w - offset, offset, cornerR, cornerR, 270, 90);
            graphPath.AddArc(w - offset, h - offset, cornerR, cornerR, 0, 90);
            graphPath.AddArc(offset, h - offset, cornerR, cornerR, 90, 90);
            graphPath.CloseFigure();


            using (Brush brush = new SolidBrush(mButtonColor))
            {
                g.FillPath(brush, graphPath);
            }

            // ボタンの文字列描画
            //DrawText(g);
        }
    }
}
