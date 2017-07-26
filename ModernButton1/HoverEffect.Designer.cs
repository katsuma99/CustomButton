﻿namespace ModernButtonLib
{
    partial class HoverEffect
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ColorChangeTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ColorChangeTimer
            // 
            this.ColorChangeTimer.Interval = 1;
            this.ColorChangeTimer.Tick += new System.EventHandler(this.ColorChangeTimer_Tick);
            // 
            // HoverEffect
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.UseVisualStyleBackColor = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer ColorChangeTimer;
    }
}
