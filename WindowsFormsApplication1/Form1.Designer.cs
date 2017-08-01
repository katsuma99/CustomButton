namespace WindowsFormsApplication1
{
    partial class Form1
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.textBaseButton1 = new SimpleButtonLib.TextBaseButton();
            this.roundCornerButton1 = new ModernButtonLib.RoundCornerButton();
            this.hoverEffect1 = new ModernButtonLib.HoverEffect();
            ((System.ComponentModel.ISupportInitialize)(this.textBaseButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Default;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Turquoise;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 48);
            this.button1.TabIndex = 1;
            this.button1.Text = "WinFormButton1";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // textBaseButton1
            // 
            this.textBaseButton1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.textBaseButton1.Image = ((System.Drawing.Image)(resources.GetObject("textBaseButton1.Image")));
            this.textBaseButton1.Location = new System.Drawing.Point(152, 101);
            this.textBaseButton1.MyFont = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.textBaseButton1.Name = "textBaseButton1";
            this.textBaseButton1.NormalImage = ((System.Drawing.Image)(resources.GetObject("textBaseButton1.NormalImage")));
            this.textBaseButton1.PushedImage = ((System.Drawing.Image)(resources.GetObject("textBaseButton1.PushedImage")));
            this.textBaseButton1.SelectImage = ((System.Drawing.Image)(resources.GetObject("textBaseButton1.SelectImage")));
            this.textBaseButton1.Size = new System.Drawing.Size(120, 48);
            this.textBaseButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.textBaseButton1.State = SimpleButtonLib.BaseButton.BtState.Normal;
            this.textBaseButton1.StringAlignment = SimpleButtonLib.TextBaseButton.Alignment.Center;
            this.textBaseButton1.TabIndex = 2;
            this.textBaseButton1.TabStop = false;
            this.textBaseButton1.Text = "textBaseButton1";
            // 
            // roundCornerButton1
            // 
            this.roundCornerButton1.CornerR = ModernButtonLib.RoundCornerButton.Number0To100._10;
            this.roundCornerButton1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.roundCornerButton1.ForeColor = System.Drawing.Color.White;
            this.roundCornerButton1.Location = new System.Drawing.Point(59, 26);
            this.roundCornerButton1.MouseDownButtonColor = System.Drawing.Color.Turquoise;
            this.roundCornerButton1.MouseLeaveButtonColor = System.Drawing.Color.Black;
            this.roundCornerButton1.MouseOverButtonColor = System.Drawing.Color.Gray;
            this.roundCornerButton1.Name = "roundCornerButton1";
            this.roundCornerButton1.Size = new System.Drawing.Size(128, 50);
            this.roundCornerButton1.TabIndex = 3;
            this.roundCornerButton1.Text = "roundCornerButton1";
            this.roundCornerButton1.UseVisualStyleBackColor = true;
            // 
            // hoverEffect1
            // 
            this.hoverEffect1.BackColor = System.Drawing.Color.Transparent;
            this.hoverEffect1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.hoverEffect1.CircleAnimationTime = 100;
            this.hoverEffect1.CornerR = ModernButtonLib.RoundCornerButton.Number0To100._10;
            this.hoverEffect1.FadeAnimationTime = 300;
            this.hoverEffect1.FlatAppearance.BorderSize = 0;
            this.hoverEffect1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.hoverEffect1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.hoverEffect1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hoverEffect1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.hoverEffect1.ForeColor = System.Drawing.Color.White;
            this.hoverEffect1.Location = new System.Drawing.Point(59, 203);
            this.hoverEffect1.MouseDownButtonColor = System.Drawing.Color.Turquoise;
            this.hoverEffect1.MouseLeaveButtonColor = System.Drawing.Color.Black;
            this.hoverEffect1.MouseOverButtonColor = System.Drawing.Color.Gray;
            this.hoverEffect1.Name = "hoverEffect1";
            this.hoverEffect1.Size = new System.Drawing.Size(128, 50);
            this.hoverEffect1.State = ModernButtonLib.HoverEffect.OperatingState.Leave;
            this.hoverEffect1.TabIndex = 4;
            this.hoverEffect1.Text = "hoverEffect1";
            this.hoverEffect1.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.hoverEffect1);
            this.Controls.Add(this.roundCornerButton1);
            this.Controls.Add(this.textBaseButton1);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.textBaseButton1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private SimpleButtonLib.TextBaseButton textBaseButton1;
        private ModernButtonLib.RoundCornerButton roundCornerButton1;
        private ModernButtonLib.HoverEffect hoverEffect1;
    }
}

