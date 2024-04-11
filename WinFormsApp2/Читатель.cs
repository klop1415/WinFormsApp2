
namespace WinFormsApp2
{
    public class Читатель
    {
        // номер читательского {например: ЭТ2020-47201}
        public string? Id { get; set; }

        public string? Имя { get; set; }
        public string? Фамилия { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; } = "user";

        public DateTime ДатаРегистрации{ get; set; }

        public override string ToString()
        {
            return Фамилия + " " + Имя;
        }
        
    }
}
