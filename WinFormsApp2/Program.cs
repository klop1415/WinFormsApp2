using System.Text.Json;

namespace WinFormsApp2
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            
            var usersList = Open("userslist.ppp");
            if(usersList is null)
            {
                Application.Exit();
                return;
            }

            var form = new LoginForm(usersList);

            if (form.ShowDialog() != DialogResult.OK)
            {
                Application.Exit();
            }else
                Application.Run(new Form1(usersList));
        }

        // загрузка данных из файла
        internal static Читатель[] Open(string file)
        {
            if (File.Exists(file))
            {
                try
                {
                    using (FileStream openStream = File.OpenRead(file))
                    {
                        var list =
                            JsonSerializer.Deserialize<Читатель[]>(openStream);
                        return list;
                    }
                }
                catch (UnauthorizedAccessException uAEx)
                {
                    MessageBox.Show($"Доступ к файлу закрыт: {uAEx.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }else
                MessageBox.Show("Нет файла читателей!");
            return null;
        }
    }
}