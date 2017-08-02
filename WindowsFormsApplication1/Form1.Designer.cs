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
            this.label1 = new System.Windows.Forms.Label();
            this.countDownButton1 = new CountDownButtonLib.CountDownButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(49, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 63);
            this.label1.TabIndex = 0;
            this.label1.Text = "カウント終了で　出現するラベル";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // countDownButton1
            // 
            this.countDownButton1.CountMax = 4;
            this.countDownButton1.FinishText = "Finish";
            this.countDownButton1.Location = new System.Drawing.Point(53, 40);
            this.countDownButton1.Name = "countDownButton1";
            this.countDownButton1.Size = new System.Drawing.Size(150, 60);
            this.countDownButton1.StartText = "Start";
            this.countDownButton1.TabIndex = 1;
            this.countDownButton1.Text = "Start";
            this.countDownButton1.UseVisualStyleBackColor = true;
            this.countDownButton1.OnFinishCountDown += new System.EventHandler(this.countDownButton1_OnFinishCountDown);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.countDownButton1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Label label1;
        private CountDownButtonLib.CountDownButton countDownButton1;
    }
}

