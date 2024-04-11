using System.Text.Json;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        List<Читатель> СписокВсехКниг { get; set; } = [];
        List<Читатель> СписокВсехЧитателей { get; set; } = [];
        public Form1()
        {
            InitializeComponent();

            Load += Form1_Load;

            /*            СписокВсехЧитателей = new()
                        {
                            new Читатель {Id="1", Фамилия = "Tom", Имя="Илья", Password="xxxx", Role="admin" },
                            new Читатель {Id="ЭТ2020-47202", Фамилия = "Bob", Имя="Анна", Password="1234"},
                            new Читатель {Id="ЭТ2020-47203", Фамилия = "Sam", Имя="Александр", Password="rter"},
                            new Читатель {Id="ЭТ2020-47204", Фамилия = "Маршак", Имя="Самуил", Password="www"}
                        };
                        listBox1.DataSource = СписокВсехЧитателей;
            */
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
        }

        public Form1(Читатель[] usersList) : this()
        {
            СписокВсехЧитателей = new List<Читатель>(usersList);
            //listBox1.DataSource = СписокВсехЧитателей;

            dataGridView1.DataSource = СписокВсехЧитателей;
            //dataGridView1.Columns.Remove("Password");
            dataGridView1.Columns[3].Visible = false;
        }

        // список читателей изменён
        bool isModified;
        public bool IsModified
        {
            get { return isModified; }
            set
            {
                isModified = value;
                if (isModified)
                {
                    saveButton.Enabled = true;
                    saveButton.BackColor = Color.OrangeRed;
                }
                else
                {
                    saveButton.Enabled = false;
                    saveButton.BackColor = Color.Transparent;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var reader = new Читатель();
            EditReader(reader);
        }

        void EditReader(Читатель reader)
        {
            if (reader is not null)
            {
                Читатель reader2 = new Читатель()
                {
                    Id = reader.Id,
                    Фамилия = reader.Фамилия,
                    Имя = reader.Имя,
                    ДатаРегистрации = reader.ДатаРегистрации
                };
                ReaderEditForm readerEditForm = new ReaderEditForm();
                readerEditForm.DataContext = reader2;

                if (readerEditForm.ShowDialog(this) == DialogResult.OK)
                {
                    var newlist = new List<Читатель>();
                    int index = 0;

                    if (string.IsNullOrEmpty(reader.Id)) // новый читатель
                    {
                        reader2.ДатаРегистрации = DateTime.Now;

                        СписокВсехЧитателей.Add(reader2);
                        newlist.AddRange(СписокВсехЧитателей);
                        index = newlist.Count - 1;
                        IsModified = true;
                    }
                    else // редактирование читателя
                    {
                        // найдем в старом списке читателя с таким читетельским
                        var item = СписокВсехЧитателей
                            .FirstOrDefault(x => x.Id == reader.Id);

                        index = СписокВсехЧитателей.IndexOf(reader);

                        // формируем новый, модифицированный список
                        newlist.AddRange(СписокВсехЧитателей);

                        // ищем и вносим изменения, если есть
                        try
                        {
                            if (item.Id != reader2.Id)
                            {
                                item.Id = reader2.Id;
                                IsModified = true;
                            }
                            if (item.Имя != reader2.Имя)
                            {
                                item.Имя = reader2.Имя;
                                IsModified = true;
                            }
                            if (item.Фамилия != reader2.Фамилия)
                            {
                                item.Фамилия = reader2.Фамилия;
                                IsModified = true;
                            }

                            item.ДатаРегистрации = reader2.ДатаРегистрации;

                            newlist.RemoveAt(index);
                            newlist.Insert(index, item);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    // если есть изменения, то привязываем к форме новый список
                    if (IsModified)
                    {
                        label1.Text = $"isModified: {IsModified}";
                        dataGridView1.DataSource = newlist;
                        //dataGridView1.SelectedIndex = index;
                    }
                }
                readerEditForm.Dispose();
            }
        }

        private void DeleteUser()
        {
            // проверяем, есть ли выделенная строка в таблице
            int index = GetSelectedIndex();
            if (index < 0) return;

            // вызываем форму подтверждения
            ConfirmationForm confirmDialog = new ConfirmationForm();
            if (confirmDialog.ShowDialog(this) == DialogResult.OK)
            {
                // удаляем читетеля из списка
                СписокВсехЧитателей.RemoveAt(index);

                var newlist = new List<Читатель>();
                newlist.AddRange(СписокВсехЧитателей);
                
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = СписокВсехЧитателей;
                
                IsModified = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Save();
        }

        // сохранение данных в файл
        internal async Task Save()
        {
            try
            {
                string ext = "ppp";
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = ext + " Files|*." + ext;
                saveFileDialog.Title = "Выбрать " + ext + " файл";
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.InitialDirectory = Environment.CurrentDirectory;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    using (FileStream createStream = File.Create(saveFileDialog.FileName))
                    {
                        await JsonSerializer
                            .SerializeAsync(createStream, СписокВсехЧитателей);
                        IsModified = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DeleteUser();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            int index = GetSelectedIndex();
            if (index < 0) return;
            
            EditReader(СписокВсехЧитателей[index]);
        }

        int GetSelectedIndex()
        {
            var rows = dataGridView1.SelectedRows;
            if(rows.Count == 0) 
                return -1;
            return rows[0].Index;
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int index = GetSelectedIndex();
            if (index > -1)
            {
                editButton.Enabled = deleteButton.Enabled = true;
                //label1.Text = СписокВсехЧитателей[index].Фамилия;
            }
        }

        // СОРТИРОВКА 
        // порядок сортировки для 6 столбцов
        int[] columnSortSortOrder = [1, 1, 1, 1, 1, 1];
        // если columnSortSortOrder[i] == 1 то сортируем i-й столбец по возрастанию,
        // если columnSortSortOrder[i] == -1 то сортируем i-й столбец по убыванию
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var index = e.ColumnIndex;
            switch (index)
            {
                case 0:
                    СписокВсехЧитателей.Sort(delegate (Читатель x, Читатель y)
                    {
                        if (x.Id == null && y.Id == null) return 0;
                        else if (x.Id == null) return -1;
                        else if (y.Id == null) return 1;
                        else return x.Id.CompareTo(y.Id) * columnSortSortOrder[index];
                    });
                    dataGridView1.Columns[index].HeaderText = columnSortSortOrder[index] == 1 ?
                        "№ 🔻" : "№ 🔺";
                    break;
                case 1:
                    СписокВсехЧитателей.Sort(delegate (Читатель x, Читатель y)
                    {
                        if (x.Имя == null && y.Имя == null) return 0;
                        else if (x.Имя == null) return -1;
                        else if (y.Имя == null) return 1;
                        else return x.Имя.CompareTo(y.Имя) * columnSortSortOrder[index];
                    });
                    dataGridView1.Columns[index].HeaderText = columnSortSortOrder[index] == 1 ?
                        "Имя 🔻" : "Имя 🔺";
                    break;
                case 2: // сортировка по Фамилия
                    СписокВсехЧитателей.Sort(delegate (Читатель x, Читатель y)
                    {
                        if (x.Фамилия == null && y.Фамилия == null) return 0;
                        else if (x.Фамилия == null) return -1;
                        else if (y.Фамилия == null) return 1;
                        else return x.Фамилия.CompareTo(y.Фамилия) * columnSortSortOrder[index];
                    });
                    dataGridView1.Columns[index].HeaderText = columnSortSortOrder[index] == 1 ?
                        "Фамилия🔻" : "Фамилия🔺";
                    break;
                case 5: // сортировка по дате регистрации
                    СписокВсехЧитателей.Sort(delegate (Читатель x, Читатель y)
                    {
                        return x.ДатаРегистрации > y.ДатаРегистрации ?
                        1 * columnSortSortOrder[index] :
                        -1 * columnSortSortOrder[index];
                    });
                    break;
                default:
                    break;
            }
            // меняем порядок сортировки для следующего клика на index-й столбец
            columnSortSortOrder[index] *= -1;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = СписокВсехЧитателей;

            //dataGridView1.Sort(dataGridView1.Columns[index], ListSortDirection.Descending);
        }

        // редактирование читателя при двойном щелчке мыши на строке таблицы
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = GetSelectedIndex();
            if (index < 0) return;

            EditReader(СписокВсехЧитателей[index]);

        }

        private void dataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                // удаление читетеля при нажатии Delete
                case Keys.Delete:
                    DeleteUser();
                    break;

                // редактирование читетеля при нажатии Enter
                case Keys.Enter:
                    int index = GetSelectedIndex();
                    if (index < 0) return;
                    EditReader(СписокВсехЧитателей[index]);
                    break;

                default:
                    break;
            }

        }
    }
}
