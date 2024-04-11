namespace WinFormsApp2
{
    internal class Книга
    {
        // регистрационный номер книги ISBN {например: 2-266-11156-6}
        public string Id { get; set; }
        public string Название { get; set; }
        public string Автор { get; set; }
        public string Издательство { get; set; }
        public int КолСтраниц { get; set; }
        public DateTime ДатаИздания { get; set; }
        public DateTime ДатаРегистрации { get; set; }
        
        // номер читательского последнего бравшего книгу
        public string Читатель { get; set; }
        public bool Вналичии { get; set; }
    }
}
