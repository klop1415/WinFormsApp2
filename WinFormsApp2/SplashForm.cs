namespace WinFormsApp2
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
            timer1.Interval = 500; // 0.5 секунд
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.PerformStep();
            if (progressBar1.Value == progressBar1.Maximum)
            {
                timer1.Enabled = false;
                Close();
            }
        }
    }
}
