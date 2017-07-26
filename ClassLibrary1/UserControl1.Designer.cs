namespace ClassLibrary1
{
    partial class UserControl1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl1));
            this.simpleButton1 = new SimpleButtonLib.SimpleButton();
            this.simpleButton2 = new SimpleButtonLib.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.simpleButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleButton2)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(20, 4);
            this.simpleButton1.MyFont = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.NormalImage = ((System.Drawing.Image)(resources.GetObject("simpleButton1.NormalImage")));
            this.simpleButton1.PushedImage = ((System.Drawing.Image)(resources.GetObject("simpleButton1.PushedImage")));
            this.simpleButton1.SelectImage = ((System.Drawing.Image)(resources.GetObject("simpleButton1.SelectImage")));
            this.simpleButton1.Size = new System.Drawing.Size(120, 48);
            this.simpleButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.simpleButton1.State = SimpleButtonLib.BaseButton.BtState.Normal;
            this.simpleButton1.StringAlignment = SimpleButtonLib.TextBaseButton.Alignment.Center;
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.TabStop = false;
            this.simpleButton1.Text = "simpleButton1";
            // 
            // simpleButton2
            // 
            this.simpleButton2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.simpleButton2.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(20, 90);
            this.simpleButton2.MyFont = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.NormalImage = ((System.Drawing.Image)(resources.GetObject("simpleButton2.NormalImage")));
            this.simpleButton2.PushedImage = ((System.Drawing.Image)(resources.GetObject("simpleButton2.PushedImage")));
            this.simpleButton2.SelectImage = ((System.Drawing.Image)(resources.GetObject("simpleButton2.SelectImage")));
            this.simpleButton2.Size = new System.Drawing.Size(120, 48);
            this.simpleButton2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.simpleButton2.State = SimpleButtonLib.BaseButton.BtState.Normal;
            this.simpleButton2.StringAlignment = SimpleButtonLib.TextBaseButton.Alignment.Center;
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.TabStop = false;
            this.simpleButton2.Text = "simpleButton2";
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Name = "UserControl1";
            ((System.ComponentModel.ISupportInitialize)(this.simpleButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleButton2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SimpleButtonLib.SimpleButton simpleButton1;
        private SimpleButtonLib.SimpleButton simpleButton2;
    }
}
