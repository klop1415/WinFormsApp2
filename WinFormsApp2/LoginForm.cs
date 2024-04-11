using System.Text;

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

        Читатель[]? UsersList;
        public LoginForm(Читатель[] usersList) : this()
        {
            UsersList = usersList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            StringBuilder sb = new();
            if (UsersList != null)
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    sb.AppendLine("А чё не ввёл номер читательского? 🤔");
                }
                else
                {
                    if (string.IsNullOrEmpty(textBox2.Text))
                    {
                        sb.AppendLine("А как без пароля то? 🤔");
                    }
                    else
                    {
                        var user = UsersList.FirstOrDefault(u => u.Id == textBox1.Text);
                        if (user != null)
                        {
                            if (user.Password == textBox2.Text)
                            {
                                DialogResult = DialogResult.OK;
                            }else
                                sb.AppendLine("Тут это... пароль не верный 😎");
                        }
                        else
                            sb.AppendLine("А нет такого читателя 🤣");
                    }
                }
            }
            else
            {
                sb.AppendLine("Нет списка читателей");
            }
            label3.Text = sb.ToString();
        }
        


    }
}
