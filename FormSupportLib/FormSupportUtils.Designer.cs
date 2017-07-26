namespace FormSupportLib
{
    partial class FormSupportUtils
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
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
            this.SuspendLayout();
            // 
            // FormSupportUtils
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::FormSupportLib.Properties.Resources.FormSupport;
            this.Name = "FormSupportUtils";
            this.Size = new System.Drawing.Size(32, 32);
            this.Load += new System.EventHandler(this.FormSupportUtils_Load);
            this.VisibleChanged += new System.EventHandler(this.FormSupportUtils_VisibleChanged);
            this.ParentChanged += new System.EventHandler(this.FormSupportUtils_ParentChanged);
            this.ResumeLayout(false);

        }

        #endregion

    }
}
