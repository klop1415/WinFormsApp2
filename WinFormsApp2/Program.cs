using System.Text.Json;

namespace WinFormsApp2
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            
            //var loginform = new LoginForm(usersList);

            //if (loginform.ShowDialog() != DialogResult.OK)
            //{
            //    Application.Exit();
            //}else
                Application.Run(new Form1());
        }

        // загрузка данных из файла
        internal static int[] Open(string file)
        {
            if (File.Exists(file))
            {
                try
                {
                    using (FileStream openStream = File.OpenRead(file))
                    {
                        var list =
                            JsonSerializer.Deserialize<int[]>(openStream);
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
                MessageBox.Show("Нет файла!");

            return null;
        }
    }
}