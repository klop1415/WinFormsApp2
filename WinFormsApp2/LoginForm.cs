namespace WinFormsApp2
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            var form = new SplashForm();
            form.ShowDialog();

            InitializeComponent();
            label3.Text = "";

        }


        private void button1_Click(object sender, EventArgs e)
        {
        }

    }
}
