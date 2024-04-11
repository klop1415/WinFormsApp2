using System.Text;
using System.Text.RegularExpressions;

namespace WinFormsApp2
{
    public partial class ReaderEditForm : Form
    {
        // паттерн правильной фамилии кирилицей
        string name1Pattern = @"^[абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ]{2,24}$";
        // паттерн правильной фамилии латиницей
        
        string name2Pattern = @"^[A-Za-z]{2,24}$";
        // паттерн правильного номера читетельского билета
        string name3Pattern = @"^[\w-]{5,24}$";
        
        public ReaderEditForm()
        {
            InitializeComponent();
            label4.Text = "";

        }

        private void ReaderEditForm_Load(object sender, EventArgs e)
        {
            textBox1.DataBindings.Add(new Binding("Text", DataContext, "Имя",
                false, DataSourceUpdateMode.OnPropertyChanged, ""));
            textBox2.DataBindings.Add(new Binding("Text", DataContext, "Фамилия",
                false, DataSourceUpdateMode.OnPropertyChanged, ""));
            textBox3.DataBindings.Add(new Binding("Text", DataContext, "Id",
                false, DataSourceUpdateMode.OnPropertyChanged, ""));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new();
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                sb.AppendLine("Надо обязательно ввести имя 😒");
            }
            else
            {
                // проверяем написание имени либо кирилицей, либо латиницей, но не одновременно
                if (!Regex.IsMatch(textBox1.Text, name1Pattern, RegexOptions.IgnoreCase))
                {
                    if (!Regex.IsMatch(textBox1.Text, name2Pattern, RegexOptions.IgnoreCase))
                    {
                        sb.AppendLine("Имя должно иметь минимум 2 буквы. И накаих цифр или знаков!");
                    }
                }
            }

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                sb.AppendLine("Надо обязательно ввести фамилию");
            }
            else
            {
                // проверяем написание фамилии либо кирилицей, либо латиницей, но не одновременно
                if (!Regex.IsMatch(textBox2.Text, name1Pattern, RegexOptions.IgnoreCase))
                {
                    if (!Regex.IsMatch(textBox2.Text, name2Pattern, RegexOptions.IgnoreCase))
                    {
                        sb.AppendLine("Фамилия должна иметь минимум 2 буквы либо кирилицей, либо латиницей, но не одновременно. И накаих цифр или знаков!");
                    }
                }
            }

            if (string.IsNullOrEmpty(textBox3.Text))
            {
                sb.AppendLine("Без номера читательского билета никак нельзя 😎");
            }
            else
            {
                if (!Regex.IsMatch(textBox3.Text, name3Pattern, RegexOptions.IgnoreCase))
                {
                    sb.AppendLine("Неправильный номер читательского!");
                }
            }

            if (string.IsNullOrEmpty(sb.ToString()))
            {
                DialogResult = DialogResult.OK; // ошибок нет
            }
            else {
                // вывод на экран сообщений об ошибках
                label4.Text = sb.ToString();
                DialogResult = DialogResult.None;
            }
        }
    }
}
