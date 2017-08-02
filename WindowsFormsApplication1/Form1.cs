using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void countDownButton1_OnFinishCountDown(object sender, System.EventArgs e)
        {
            label1.Show();
        }
    }
}
