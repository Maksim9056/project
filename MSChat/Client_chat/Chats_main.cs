using Class_chat;
using System;
using  static System.Drawing.Image;
using  System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.CoreAudioApi;
using NAudio;
using System.Data.SqlTypes;
using System.Media;
using System.IO.Pipes;
using System.Collections;
using System.Diagnostics;
using System.Numerics;

namespace Client_chat
{
    public partial class Chats_main : Form
    {
        public Chats_main()
        {
            InitializeComponent();
        }

      
        /// <summary>
        /// Для Поиска друзей
        /// </summary>
        string Na_me { get; set; }
        /// <summary>
        /// Постояные значения используем для пользователей
        /// </summary>

        public User_photo[] Friend { get; set; }
        /// <summary>
        /// Постояные значения используем для фильтра для чата
        /// </summary>
        public MessСhat[] allChat { get; set; }

        /// <summary>
        /// id пользователя
        /// </summary>
        public int Users { get; set; }

        /// <summary>
        /// id другу
        /// </summary>        
        public int Friends { get; set; }

        /// <summary>
        /// Обновлять сообщение
        /// </summary>
        public bool Update_Message { get; set; }

        /// <summary>
        /// id сообщения
        /// </summary>
        public int Update_id { get; set; }

        /// <summary>
        /// Для отображения при выборке [Редактировать] ,[Удалить]
        /// </summary>
        public int selectedBiodataId;

        /// <summary>
        /// Проверяет есть ли  подключения
        /// </summary>
        public bool Entrance { get; set; }

        /// <summary>
        /// Экземпляр класса CommandCL
        /// </summary>
        public CommandCL command = new CommandCL();


        //Отображает Сообщения из чата
        public void Chat(CommandCL command)
        {
            try
            {
                using (MemoryStream Chats = new MemoryStream())
                {
                    // Проверяем не равно ли   null
                    if (command._Answe != null)
                    {
                        //Здесь проверяем есть ли true то заполняет чат
                        if (command._Answe.ToString() == "true")
                        {
                            //Заполняем размерность классу
                            MessСhat[] les = new MessСhat[command._AClass.Count()];
                            //Десерилизуем класс и получаем класс MessСhat 
                            for (int i = 0; i < command._AClass.Count(); i++)
                            {
                                string yu = command._AClass[i].ToString();
                                MessСhat useTravel = JsonSerializer.Deserialize<MessСhat>(yu);
                                les[i] = useTravel;
                            }
                            //Отчищаем чат
                            dataGridViewChat.Rows.Clear();
                            //Устанавливаем количество столбцов
                            dataGridViewChat.RowCount = les.Count();

                            //Устанавливаем количество 2 колонок
                            dataGridViewChat.ColumnCount = 2;
                            //Для прочтения

                            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                            {
                            }
                            //Передаем значения от les до  allChat
                            allChat = les;
                            //Добавляем колонкку признак активности
                            dataGridViewChat.Columns.Insert(2, column);
                            //Проверяем   i < les.Count() больше ли  размерности массива
                            for (int i = 0; i < les.Count(); i++)
                            {
                                //Выводим в    dataGridViewChat.Rows[i].Cells[j].Value = les[i].Message сообщение
                                for (int j = 0; j < 1; j++)
                                {
                                    dataGridViewChat.Rows[i].Cells[j].Value = les[i].Message;
                                    //Присваем заголовок колонке
                                    dataGridViewChat.Columns[j].HeaderText = "Сообщения";
                                    //Проверяем от кого сообщения и заполняем голлубым
                                    if (les[i].IdUserFrom != Users)
                                    {
                                        dataGridViewChat.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
                                    }
                                }
                                //Отправка
                                for (int j = 1; j < 2; j++)
                                {  //Выводим в dataGridViewChat дату отправления сообщения
                                    dataGridViewChat.Rows[i].Cells[j].Value = les[i].DataMess;
                                    //Заголовок дата отправки
                                    dataGridViewChat.Columns[j].HeaderText = "Дата отправки";
                                }
                                //Признак активности
                                for (int j = 2; j < 3; j++)
                                {
                                    bool aMark = false;
                                    if (les[i].Mark.ToString() == "1")
                                    {
                                        aMark = true;
                                    }
                                    //Выводим галочку
                                    dataGridViewChat.Rows[i].Cells[j].Value = aMark;
                                }
                            }
                            //Даем размерность автоматически но слимитами
                            dataGridViewChat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dataGridViewChat.Visible = true;
                        }
                        else
                        {
                            //Отчищаем чат
                            dataGridViewChat.Rows.Clear();
                        }
                    }
                    else
                    {
                        //Если нет сообщений
                        dataGridViewChat.Rows.Clear();
                        //MessageBox.Show("Сообщений нет");
                    }
                }
            }
            catch(Exception)
            {

            }
        }

        //Сылка
        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
        }


        /// <summary>
        /// MsgUser_Logins для Входа
        /// </summary>
        /// <param name="responseDat"></param>
        public void NotifyMe(MsgUser_Logins responseDat)
        {
            //Авторизация
            OpenMes(responseDat);
        }

        /// <summary>
        /// Сохраняет файл конфигурации в json
        /// </summary>
        /// <param name="Port"></param>
        /// <param name="ipAddress"></param>
        /// <param name="Name"></param>
        public void SaveConfig(Int32 Port, string ipAddress, string Name)
        {
            try
            {
                //Чтения файла Client.json"
                using (FileStream file = new FileStream("Client.json", FileMode.OpenOrCreate))
                {
                    //Заполняем класс Connect_Client_
                    Connect_Client_ connect_Client = new Connect_Client_(Port, ipAddress, Name);
                    //Серелизуем класс Connect_Client_
                    JsonSerializer.Serialize<Connect_Client_>(file, connect_Client);
                    //Ip adress присваеваем
                    IP_ADRES.Ip_adress = ipAddress;
                }
            }
            catch(Exception)
            {

            }
        }

        //
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBox1.Text != "")
                {
                    Entrance = true;
                    //  IPStatus status;
                    try
                    {
                        //IP_ADRES.Ip_adress =  toolStripTextBox1.Text;
                        /* Ping p = new Ping();
                       //  PingReply pr = p.Send(IP_ADRES.Ip_adress );
                         //status = pr.Status;                  
                         //if (status != IPStatus.Success)
                         //{
                         //    MessageBox.Show("Сервер работает");
                         //   //IPAddress.Loopback.ToString();

                         //}
                         //else
                         //{
                         //   MessageBox.Show("Сервер временно недоступен!");
                         //}*/
                        //Получаем Ip adress
                        string ipAddress = toolStripTextBox1.Text;
                        //Ping пингуем 
                        Ping pingSender = new Ping();
                        //Отправляем получаем
                        PingReply reply = pingSender.Send(ipAddress);
                        //Проверяем ответ
                        if (reply.Status == IPStatus.Success)
                        {
                            SaveConfig(ConnectSettings.port, ipAddress, "");
                            toolStripButton1.BackColor = Color.Gray;
                            //Подключения к сервуру
                            toolStripButton1.ForeColor = Color.Gray;
                            using (Password_Users a = new Password_Users())
                            {
                                a.ShowDialog(this);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ping to {0} failed.", ipAddress);
                        }
                    }
                    catch
                    {

                    }

                }
                else
                {
                    //Устанавливаем значение что пользователь здесь
                    Entrance = false;
                    //Выводим что пустой ip adress
                    MessageBox.Show("Ip-адрес:Не заполнен");
                }
            }
            catch
            {
                //ошибки
            }
        }

        //Сылка 
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        //Уже неиспользуем
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedrowindexs = dataGridViewChat.SelectedCells[0].RowIndex;
                MessСhat tt = allChat[selectedrowindexs];
                textBox1.Text = tt.Message;
                Update_id = tt.Id;
                Update_Message = true;
            }
            catch
            {

            }
        }

        //Сылка
        private void toolStripButton1_MouseHover(object sender, EventArgs e)
        {
        }


        /// <summary>
        /// Две команды для отправки нового сообщения и редактования сообщения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string FileFS = "";
                //Отправка сообщений
                if (textBox1.Text == "")
                {
                    //MessageBox.Show("Cообщение пустое!");
                }
                else
                {
                    //Проверка для редактированого сообщение отправить
                    if (Update_Message == true)
                    {
                        using (MemoryStream _Update_Message = new MemoryStream())
                        {
                            //Заполняем клас для отправки на сервере
                            using (MemoryStream Update = new MemoryStream())
                            {
                                DateTime dateTime = DateTime.Now;
                                MessСhat Mes_chat = new MessСhat(Update_id, Users, Friends, textBox1.Text, dateTime, 1,0);
                                JsonSerializer.Serialize<MessСhat>(Update, Mes_chat);
                                FileFS = Encoding.Default.GetString(Update.ToArray());
                            }
                            //Отправляем редактированое сообщение на сервер
                            Task.Run(async () => await command.Update_Message_make_up(IP_ADRES.Ip_adress, FileFS, "010")).Wait();
                            //Стираем соообщения 
                            dataGridViewChat.Rows.Clear();
                            //Заполняем сразу в чат соообщение
                            Chat(command);
                            Update_Message = false;
                            Update_id = 0;
                            textBox1.Text = "";
                        }
                    }
                    else
                    {
                        //Заполняем класс с чатом новое сообщение
                        using (MemoryStream fs = new MemoryStream())
                        {
                           if(command.Insert_Fille_Music_id == null)
                            {
                                DateTime dateTime = DateTime.Now;
                                MessСhat Mes_chat = new MessСhat(0, Users, Friends, textBox1.Text, dateTime, 1, 0);
                                JsonSerializer.Serialize<MessСhat>(fs, Mes_chat);
                                FileFS = Encoding.Default.GetString(fs.ToArray());
                            }
                            else 
                            {
                                DateTime dateTime = DateTime.Now;
                                MessСhat Mes_chat = new MessСhat(0, Users, Friends, textBox1.Text, dateTime, 1, command.Insert_Fille_Music_id.Id);
                                JsonSerializer.Serialize<MessСhat>(fs, Mes_chat);
                                FileFS = Encoding.Default.GetString(fs.ToArray());
                            }
                         
                        }
                        // чтение данных
                        textBox1.Text = "";
                        // Добавляем сообщение в чат !
                        //Освобождаем ресурсы!
                        //Отправляем новое сообщение  и чат обновляем
                        using (MemoryStream New_message = new MemoryStream())
                        {
                            Task.Run(async () => await command.Insert_Message(IP_ADRES.Ip_adress, FileFS, "009")).Wait();
                            //Стираем сообщения
                            dataGridViewChat.Rows.Clear();
                            //Заполняем чат
                            Chat(command);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Открытие формы и заполнение таблиц
        /// </summary>
        /// <param name="Friends"></param>
        public void OpenMes(MsgUser_Logins Friends)
        {
            try
            {
                Users = Friends.User_.Id;
                //Заполняеться Ip_adress для отправки
                toolStripTextBox1.Text = IP_ADRES.Ip_adress;
                //Имя заполняем
                toolStripLabel1.Text = Friends.User_.Name;
                //Путь к каталогу программы
                string pathImage = Environment.CurrentDirectory.ToString();
                //Кнопка включение
                toolStripButton1.Image = Image.FromFile(pathImage + "\\Resources\\Images\\Зеленый.png");
                // Заполняем  клас где  id фото и пользователя текущего
                using (MemoryStream fs = new MemoryStream())
                {
                    string FileFS = "";
                    Photo tom = new Photo(Friends.User_.Photo, Friends.User_.Current);
                    JsonSerializer.Serialize<Photo>(fs, tom);

                    FileFS = Encoding.Default.GetString(fs.ToArray());

                    //Отправляем на сервер  картинку id и пользователя id для получение картинки
                    Task.Run(async () => await command.Get_Image(IP_ADRES.Ip_adress, FileFS, "007")).Wait();
                    //Выбераем первый элемент картинки 
                    string Im = command.AClassIm.First().ToString();
                    //С конвертировали в  массив byte для вывода картинки
                    byte[] ByteImege = Convert.FromBase64String(Im);
                    //Передали в Поток памяти размер массива byte картинки пользователя
                    MemoryStream ms = new MemoryStream(ByteImege);
                    //Заполнение в  image по потоку картинку передали
                    Image returnImage = Image.FromStream(ms);
                    //Показываем картинку  текущему пользователю
                    toolStripButton2.Image = returnImage;
                }
                //Проверка когда нету друзей и их картинки 
                if (Friends != null)
                {
                    //Количество друзей
                    if (Friends.AClass.Count != 0)
                    {
                        //Заполняем  класс  Photo_Friends текущую id картинки друзей и id друзей
                        using (MemoryStream Friends_Image = new MemoryStream())
                        {
                            string FileFS = "";
                            int[] Id = new int[Friends.List_Mess];

                            int[] Id_Friends = new int[Friends.List_Mess];
                            // int[] Id_Ph = new int[Friends.List_Mess];

                            User_photo[] A = new User_photo[Friends.List_Mess];
                            for (int I = 0; I < Friends.AClass.Count(); I++)
                            {
                                A[I] = Friends.AClass[I];
                                Id_Friends[I] = A[I].Photo;
                                Id[I] = A[I].Id;
                            }
                            // A[0].Id
                            Photo_Friends tom = new Photo_Friends(Id_Friends, Id);
                            JsonSerializer.Serialize<Photo_Friends>(Friends_Image, tom);
                            FileFS = Encoding.Default.GetString(Friends_Image.ToArray());
                            //Отправляем на сервер и получаем от туда картинку ввиде строки
                            Task.Run(async () => await command.Get_Image_Friends(IP_ADRES.Ip_adress, FileFS, "014")).Wait();
                        }
                    }
                    //Заполняем пользователя имя
                    Na_me = Friends.User_.Name;
                    //Проверяем количество друзей
                    if (Friends.List_Mess == 0)
                    {
                        //обработать когда их 0
                    }
                    else
                    {
                        User_photo[] A = new User_photo[Friends.List_Mess];
                        for (int I = 0; I < Friends.AClass.Count(); I++)
                        {
                            A[I] = Friends.AClass[I];
                        }
                        Friend = A;
                        //  Friend[0].Id= 0;
                        //Проверяем  если там сообщение если true то заполняет  dataGridViewUser  друзей и их картинки

                        if (Friends.Answe == "false")
                        {
                        }
                        else
                        {
                            // Количество друзей для опрелении  в RowCount
                            dataGridViewUser.RowCount = Friend.Count();
                            //Колонку по умолчанию
                            dataGridViewUser.ColumnCount = 1;
                            //Добоавляем колонку друзей
                            DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                            //Называем колонку для картинок
                            imgColumn.Name = "Images";
                            //Добавляем название колонки
                            dataGridViewUser.Columns.Add(imgColumn);
                            //Заполняем Имя друзей
                            for (int i = 0; i < Friend.Count(); i++)
                            {
                                DataGridViewTextBoxCell cell0 = (DataGridViewTextBoxCell)dataGridViewUser.Rows[i].Cells[0];
                                cell0.Value = Friend[i].Name;
                            }
                            //Заполняем картинки
                            for (int i = 0; i < Friend.Count(); i++)
                            {
                                //С Конвертировали  из массива строк в строку для размера 1 картинки
                                string Im = command.List_Friends[i].ToString();
                                //Конвертировали строку в 
                                byte[] ByteImege = Convert.FromBase64String(Im);
                                //Получили размер картинки
                                MemoryStream ms = new MemoryStream(ByteImege);
                                //Получили картинку осталось применить
                                Image returnImagee = Image.FromStream(ms);
                                //Заполняем в колонку Images для картинки
                                DataGridViewImageCell cell1 = (DataGridViewImageCell)dataGridViewUser.Rows[i].Cells["Images"];
                                //Размер для картинки задаю DataGridViewImageCellLayout.Zoom
                                cell1.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                //В ячейку
                                cell1.Value = returnImagee;
                            }
                            //Даю название колонке  
                            dataGridViewUser.Columns[0].HeaderText = "Друзья";
                            //Даю название колонке  для картинок
                            dataGridViewUser.Columns[1].HeaderText = "Фото";
                        }
                        // Получаем id пользователя текущего
                        Users = Friends.User_.Id;
                    }

                }
            }
            catch (Exception ex)
            {
                //Для выведения ошибки при картинке          
                MessageBox.Show(ex.Message);
            }
        }

        //Для картинки
        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// форма чата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chats_main_Load(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.CurrentDirectory.ToString();

                //Проверяем зашел ли пользователь в свою учетную
                if (toolStripLabel1.Text != "")
                {
                    //???
                }
                else
                {
                    //Заполняем картинку 
                    toolStripButton1.Image = Image.FromFile(path + "\\Resources\\Images\\Красный..png");
                }

                dataGridViewUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridViewUser.RowHeadersVisible = false;
                this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

                dataGridViewChat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridViewChat.RowHeadersVisible = false;
                //Делаем не видимые кнопки
                button2.Visible = false;
                button1.Visible = false;

                // Ищем файл с настройками подключения
                FileInfo fileInfo = new FileInfo(path + "\\Client.json");
                if (fileInfo.Exists)
                {
                    using (FileStream fs = new FileStream("Client.json", FileMode.OpenOrCreate))
                    {
                        Connect_Client_ aFile = JsonSerializer.Deserialize<Connect_Client_>(fs);
                        IP_ADRES.Ip_adress = aFile.IP;
                        Connect_Client.UserName = aFile.UserName;
                        toolStripTextBox1.Text = aFile.IP;
                    }
                    //Console.WriteLine($"Имя файла: {fileInfo.Name}");
                    //Console.WriteLine($"Время создания: {fileInfo.CreationTime}");
                    //Console.WriteLine($"Размер: {fileInfo.Length}");
                }
            }
            catch (Exception a)
            {
                //Выводим ошибку
                MessageBox.Show(a.Message);
            }
        }

        /// <summary>
        /// Задем ширину столбцов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewUser_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {  //Задем ширину столбцов dataGridViewUser
            dataGridViewUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// При нажатии dataGridViewUser_Click проверяется список сообщений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewUser_Click(object sender, EventArgs e)
        {
            try
            {
                //Функция сообщения автоматически обновляет при клике
                view_mess();
            }
            catch
            {
                //Если нет нажатия в dataGridViewUser_
            }
        }


        /// <summary>
        /// Запращиваем сообщения из друзей id dataGridViewUser_Click и проверяется список сообщений
        /// </summary>
        private void view_mess()
        {
            try
            {
                //Проверяем не пустая ли таблица друзей 
                if (dataGridViewUser == null)
                {
                    //нету друзей
                }
                else
                {
                    //отчищаем чат
                    dataGridViewChat.Rows.Clear();
                    //При выделении по RowIndex по ячейке он заполняеться
                    int selectedrowindex = dataGridViewUser.SelectedCells[0].RowIndex;
                    //Проверяем не пустой selectedrowindex   
                    if (false)
                    //if (selectedrowindex != 0)
                    {

                    }
                    else
                    {
                        //Проверяем не пустой selectedrowindex
                        //Ищем в классе  через selectedrowindex если есть то заполняем в класс User_photo в атрибут tt
                        User_photo tt = Friend[selectedrowindex];
                        //Проверяем 
                        if (tt == null)
                        {

                        }
                        else
                        {
                            //Заполняем в классс 
                            using (MemoryStream Message = new MemoryStream())
                            {
                                tt.Current = Users;
                                string person = JsonSerializer.Serialize<User_photo>(tt);
                                //  Friends = person;
                                User_photo Id_Friend = JsonSerializer.Deserialize<User_photo>(person);
                                Friends = Id_Friend.Id;

                                //Отправляем на сервер и получаем соообщения
                                Task.Run(async () => await command.Check_Mess_Friend(IP_ADRES.Ip_adress, person, "006")).Wait();

                                //Стераем сообщения  dataGridViewChat и потом заполняем
                                dataGridViewChat.Rows.Clear();
                                //Заполняем чат
                                Chat(command);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //Здесь ошибки есть По этому так чтобы программа не падала переменую и не создал
                //MessageBox.Show(e.Message);

            }
        }


        /// <summary>
        /// Задем ширину столбцов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewChat_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Задем ширину столбцов dataGridViewChat
            dataGridViewChat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        //
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Добавляем друзей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //Заполняем класс Searh_Friends
                using (MemoryStream Despons_friend = new MemoryStream())
                {
                    string FileFS = "";
                    //Серилизуем класс Searh_Friends и заполняем FileFS классом серилизованым
                    using (MemoryStream fs = new MemoryStream())
                    {
                        //Заполнили класс нашим пользователем и другом
                        Searh_Friends New_Friend = new Searh_Friends(textBox2.Text, Na_me);
                        //
                        JsonSerializer.Serialize<Searh_Friends>(fs, New_Friend);
                        FileFS = Encoding.Default.GetString(fs.ToArray());

                    }
                    //Отправляем на сервер поиск такого друга
                    Task.Run(async () => await command.Connect_Friends(IP_ADRES.Ip_adress, FileFS, "008")).Wait();
                    //Просто проверяем и отчищаем
                    var __Friends = command._Friends;
                    command._Friends = null;
                    textBox2.Text = null;
                }
            }
            catch
            {
                // если друзей нету то ошибка возможно или просто не найдет  там по имени  то его не добавиться
            }
        }

        /// <summary>
        /// Работает но  его скрываем от пользователя так как есть лучше метод визуальный поэтому это не использовал 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Проверяем если данные dataGridViewChat
                if (dataGridViewChat == null)
                {

                }
                else
                {


                    //Выделили id ячейки выбраной
                    int selectedrowindexs = dataGridViewChat.SelectedCells[0].RowIndex;
                    //Определяем по фильтру id данного чата

                    MessСhat tt = allChat[selectedrowindexs];
                    //Даем значение но надо проверять
                    textBox1.Text = tt.Message;
                    Update_id = tt.Id;
                    string FileFS = "";
                    using (MemoryStream fs = new MemoryStream())
                    {
                        DateTime dateTime = DateTime.Now;
                        MessСhat Mes_chat = new MessСhat(Update_id, Users, Friends, textBox1.Text, dateTime, 1,0);
                        JsonSerializer.Serialize<MessСhat>(fs, Mes_chat);
                        FileFS = Encoding.Default.GetString(fs.ToArray());
                    }
                    textBox1.Text = "";
                }

            }
            catch
            {

            }
        }


        /// <summary>
        /// Для функций над contextMenuStrip1  там выбор и удобно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdResults_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {   //При нажатие на правую кнопку ячейки чат  появляеться contextMenuStrip1
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    //Появляеться ячейки чат  появляеться contextMenuStrip1
                    contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                }
            }
            catch
            {
                //Ошибки если будут!
            }
        }

        /// <summary>
        /// Для функций над contextMenuStrip1 сдесь просто нажать надо  там выбор и удобно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdResults_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //handle the row selection on right click
            if (e.Button == MouseButtons.Right)
            {
                try
                {   //при простой  нажатии теперь обратно возращаеться чат только убераеться contextMenuStrip1
                    dataGridViewChat.CurrentCell = dataGridViewChat.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    // Can leave these here - doesn't hurt
                    dataGridViewChat.Rows[e.RowIndex].Selected = true;
                    //Устанавливаем фокус
                    dataGridViewChat.Focus();

                    //selectedBiodataId = Convert.ToInt32(dataGridViewChat.Rows[e.RowIndex].Cells[1].Value);

                    //Возращаем id
                    selectedBiodataId = e.RowIndex;
                }
                catch (Exception)
                {
                    //Обрабатываем ошибки
                }
            }
        }

        /// <summary>
        /// Редактирование работает чатом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Редактировать запись "+ selectedBiodataId.ToString());
            try
            {      //
                using (MemoryStream Update = new MemoryStream())
                {
                    if (dataGridViewChat == null)
                    {
                        //Проверка на пустоту
                    }
                    else
                    {
                        //Проверка Сообщения польщователя или друга !
                        //Пишим текущему пользователю что сообщения друга нельзя трогать!


                        foreach (DataGridViewCell cell in dataGridViewChat.SelectedCells)
                        {
                            if (cell.Value.ToString().Contains(dataGridViewChat.SelectedCells[0].Value.ToString()))
                            {
                                if (cell.Style.ForeColor == Color.Blue)
                                {
                                    MessageBox.Show("Сообщения друга нельзя редактировать!");
                                }
                                else
                                {
                                    
                                    //Поиск dataGridViewChat.SelectedCells[0].RowIndex 
                                    //Выбераем ячейку сообщение
                                    int selectedrowindexs = dataGridViewChat.SelectedCells[0].RowIndex;
                                    //Находим по фильтру id dataGridViewChat
                                    MessСhat tt = allChat[selectedrowindexs];
                                    //Передаем значение для редактирования
                                    textBox1.Text = tt.Message;
                                    //id редактированного сообщения
                                    Update_id = tt.Id;
                                    //ДАЕМ РАЗРЕШЕНИЕ ПРИ РЕДАКТИРОВАНИЕИ ОТПРАВКУ
                                    Update_Message = true;
                                }
                            }
                            else
                            {


                            }
                            break;

                        }
                    }
                }
            }
            catch (ArgumentNullException B)
            {
                MessageBox.Show("ArgumentNullException:{0}", B.Message);
            }
            catch (SocketException B)
            {
                MessageBox.Show("SocketException: {0}", B.Message);
            }
        }

        /// <summary>
        /// Удалят строку с сообщением
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewChat == null)
                {
                    //Проверка на пустоту
                }
                else
                {
                    //Проверка Сообщения польщователя или друга !
                    foreach (DataGridViewCell cell in dataGridViewChat.SelectedCells)
                    {
                        if (cell.Value.ToString().Contains(dataGridViewChat.SelectedCells[0].Value.ToString()))
                        {
                            //Пишим текущему пользователю что сообщения друга нельзя трогать!

                            if (cell.Style.ForeColor == Color.Blue)
                            {
                                MessageBox.Show("Сообщения друга нельзя удалять!");
                            }
                            else
                            {
                                int selectedrowindexs = dataGridViewChat.SelectedCells[0].RowIndex;
                                //Находим по фильтру id dataGridViewChat
                                MessСhat tt = allChat[selectedrowindexs];
                                //Сообщения для удаления
                                textBox1.Text = tt.Message;
                                //id сообщения для удаления
                                Update_id = tt.Id;
                                //Для отправки
                                string FileFS = "";
                                using (MemoryStream fs = new MemoryStream())
                                {
                                    //Определяем текущию дату
                                    DateTime dateTime = DateTime.Now;
                                    //Заполняем класс
                                    MessСhat Mes_chat = new MessСhat(Update_id, Users, Friends, textBox1.Text, dateTime, 1, 0);
                                    //Серилизуем класс
                                    JsonSerializer.Serialize<MessСhat>(fs, Mes_chat);
                                    //Декодируем в строку
                                    FileFS = Encoding.Default.GetString(fs.ToArray());
                                }
                                //Отчищаем
                                textBox1.Text = "";
                                //Отправляем на сервер м получаем
                                using (MemoryStream Delete_dispons = new MemoryStream())
                                {
                                    //Отправляем на сервер и там удаляеться сообщение
                                    Task.Run(async () => await command.Delete_message_make_up(IP_ADRES.Ip_adress, FileFS, "011")).Wait();
                                    //Отчищаем dataGridViewChat
                                    dataGridViewChat.Rows.Clear();
                                    //Заполняем чат 
                                    Chat(command);
                                }
                            }
                            break;

                        }
                        else
                        {
                            //получает id dataGridViewChat                        
                        }

                    }
                }
            }
            catch
            {
                //Если нету id dataGridViewChat обработали
            }
        }

        /// <summary>
        /// Автоматически ширину задают dataGridViewUser_CellClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Автоматически ширину задают dataGridViewUser
            dataGridViewUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        /// <summary>
        /// Автоматически ширину задают dataGridViewChat_CellClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewChat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Автоматически ширину задают dataGridViewChat
            dataGridViewChat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Не убрали
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //есть сылка
        }

        /// <summary>
        /// Для обновлений сообщений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //Проверяется авторизовался ли пользователь в учетную запись
                if (toolStripLabel1.Text == "")
                {
                }
                else
                {
                    //Выводим чат автоматически и получаем его
                    view_mess();
                }
            }
            catch
            {
                //Если будет ошибка при пустоте
            }
        }

        /// <summary>
        /// Список друзей обновляет но только добавленных поэтому надо будет доделать это 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                //Проверяется авторизовался ли пользователь в учетную запись
                if (toolStripLabel1.Text != "")
                {
                    using (TcpClient client = new TcpClient(IP_ADRES.Ip_adress, ConnectSettings.port))
                    {
                        NetworkStream stream = client.GetStream();
                        //Отправляем текущего пользователя
                        User_photo tt = new User_photo("", "", "", 0, 0, Users);
                        //tt.Current = Users;
                        //Серелизуем класс User_photo в строку
                        string person = JsonSerializer.Serialize<User_photo>(tt);
                        //Отправляем на сервер и Декодируем и соединяем команду и класс User_photo
                        Byte[] data = System.Text.Encoding.Default.GetBytes("013" + person);
                        //Отправили на сервер
                        await stream.WriteAsync(data, 0, data.Length);

                        String responseDat = String.Empty;
                        //Получаем с сервера ответ
                        using (MemoryStream ms = new MemoryStream())
                        {
                            //Возращаем ответ
                            int cnt = 0;
                            Byte[] locbuffer = new byte[1024];
                            do
                            {
                                cnt = await stream.ReadAsync(locbuffer, 0, locbuffer.Length);
                                ms.Write(locbuffer, 0, cnt);
                            } while (client.Available > 0);
                            //Получаем ответ
                            responseDat = Encoding.Default.GetString(ms.ToArray());
                        }
                        //Десерилизуем класс MsgFriends и используем
                        MsgFriends msgFriends = JsonSerializer.Deserialize<MsgFriends>(responseDat);
                        //Заполняем размерность класса User_photo для принятия
                        User_photo[] A = new User_photo[msgFriends.AClass.Count];
                        //Получаем друзей
                        for (int i = 0; i < msgFriends.AClass.Count(); i++)
                        {
                            A[i] = msgFriends.AClass[i];
                        }
                        //Получили друзей и присвоили значения
                        Friend = A;
                        //Проверяем есть ли друзья 
                        if (Friend != null)
                        {
                            //Проверяем размерность класса больше 0
                            if (msgFriends.AClass.Count != 0)
                            {
                                using (MemoryStream Friends_Image = new MemoryStream())
                                {
                                    //Для классов
                                    string FileFS = "";
                                    //Передаем Id пользователю
                                    int[] Id = new int[msgFriends.List_Mess];
                                    //Передаем Id_Friends друзьям
                                    int[] Id_Friends = new int[msgFriends.List_Mess];
                                    //Даем размерность User_photo коичество друзей
                                    User_photo[] b = new User_photo[msgFriends.List_Mess];
                                    //Заполняем id друзей картинок и id пользователй
                                    for (int I = 0; I < msgFriends.AClass.Count(); I++)
                                    {
                                        b[I] = msgFriends.AClass[I];
                                        Id_Friends[I] = b[I].Photo;
                                        Id[I] = b[I].Id;
                                    }
                                    //Собераем класс
                                    Photo_Friends tom = new Photo_Friends(Id_Friends, Id);
                                    //Серелизуем
                                    JsonSerializer.Serialize<Photo_Friends>(Friends_Image, tom);
                                    //Декодируем в строку
                                    FileFS = Encoding.Default.GetString(Friends_Image.ToArray());
                                    //Отправляем на сервер
                                    Task.Run(async () => await command.Get_Image_Friends(IP_ADRES.Ip_adress, FileFS, "014")).Wait();

                                }
                            }
                            //Получаем ответ если фото друзей
                            if (msgFriends.Answe == "true")
                            {
                                try
                                {
                                    //Есть ли друзья
                                    if (msgFriends.AClass == null)
                                    {
                                        //Если нету фото не выводим
                                    }
                                    else
                                    {
                                        //Отчищаем таблицу dataGridViewUser
                                        dataGridViewUser.Rows.Clear();
                                        //Устанавливаем значие количество таблицу dataGridViewUser
                                        dataGridViewUser.RowCount = Friend.Count();
                                        //Колонку 1
                                        dataGridViewUser.ColumnCount = 1;
                                        //Для картинки подписываем
                                        DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                                        //Назначаем заголовок Фото
                                        imgColumn.Name = "Фото";
                                        //Добавляем колонку
                                        dataGridViewUser.Columns.Add(imgColumn);
                                        //Заполняем друзей
                                        for (int i = 0; i < Friend.Count(); i++)
                                        {
                                            DataGridViewTextBoxCell cell0 = (DataGridViewTextBoxCell)dataGridViewUser.Rows[i].Cells[0];
                                            // Имя друзей
                                            cell0.Value = Friend[i].Name;
                                        }
                                        //Заполняем картинку друзей
                                        for (int i = 0; i < Friend.Count(); i++)
                                        {
                                            //Получаем конвертируем из массива строк в строку
                                            string Im = command.List_Friends[i].ToString();
                                            //Конвертируем в байты
                                            byte[] ByteImege = Convert.FromBase64String(Im);
                                            //Задаем значения MemoryStream
                                            MemoryStream ms = new MemoryStream(ByteImege);
                                            //Получаем картинку
                                            Image returnImagee = Image.FromStream(ms);
                                            //Заголовок фото
                                            DataGridViewImageCell cell1 = (DataGridViewImageCell)dataGridViewUser.Rows[i].Cells["Фото"];
                                            //Даем размерность фото
                                            cell1.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                            //Присваеваем значение картинки
                                            cell1.Value = returnImagee;
                                        }
                                        //Заголовок для колонки друзей
                                        dataGridViewUser.Columns[0].HeaderText = "Друзья";
                                        //Заголовок для колонки Фото друзей
                                        dataGridViewUser.Columns[1].HeaderText = "Фото";
                                    }
                                }
                                catch (Exception)
                                {
                                    //Обрабатываем ошибку
                                    // MessageBox.Show(ex.Message);
                                }
                            }
                            else
                            {
                                //Отчищаем
                                dataGridViewUser.Rows.Clear();
                            }
                        }
                    }
                }
            }
            catch
            {
                //Обрабатываем ошибки 
            }
        }

        // WaveIn - поток для записи
        WaveIn waveIn;
        //Класс для записи в файл
        WaveFileWriter writer;
        //Имя файла для записи
        string outputFilename = "Тест.mp3";

        //Получение данных из входного буфера 
      //  [Obsolete]

        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailable), sender, e);
                }
                else
                {
                    //Записываем данные из буфера в файл
#pragma warning disable CS0618 // Тип или член устарел
                    writer.WriteData(e.Buffer, 0, e.BytesRecorded);
#pragma warning restore CS0618 // Тип или член устарел
                }
            }
            catch(Exception)
            {

            }
        }

        //Завершаем запись
        void StopRecording(object sender, EventArgs e)
        {
            try
            {


                waveIn.StopRecording();
                MessageBox.Show("StopRecording");

                //     var path = Environment.CurrentDirectory.ToString();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    MemoryStream memoryStreams = new MemoryStream();
                    string FileFS = "";


                    int i = 0;

                    using (FileStream fileStream = new FileStream(outputFilename, FileMode.OpenOrCreate))
                    {
                        byte[] bytes = new byte[fileStream.Length];
                        i = fileStream.Read(bytes, 0, bytes.Length);

                        memoryStream.Write(bytes, 0, bytes.Length);
                    }

                    // чтение из файла
                    //using (FileStream fileStream = File.OpenRead(outputFilename))
                    //{
                    //    // выделяем массив для считывания данных из файла
                    //    byte[] buffer = new byte[fileStream.Length];
                    //    // считываем данные
                    //    fileStream.Read(buffer, 0, buffer.Length);
                    //    // декодируем байты в строку
                    //    //string textFromFile = Encoding.Default.GetString(buffer);
                    //    //Console.WriteLine($"Текст из файла: {textFromFile}");

                    //}


                    Insert_Fille_Music insert_Fille_Music = new Insert_Fille_Music(0, memoryStream.ToArray());

                    JsonSerializer.Serialize<Insert_Fille_Music>(memoryStreams, insert_Fille_Music);

                    FileFS = Encoding.Default.GetString(memoryStreams.ToArray());
                    Task.Run(async () => await command.Stream_Filles_music(IP_ADRES.Ip_adress, FileFS, "019")).Wait();

                    if (command.Insert_Fille_Music_id == null)
                    {

                    }
                    else
                    {
                        textBox1.Text = "Голосовое сообщение ";
                        button3_Click(sender, e);
                    }
                }
            }
            catch(Exception)
            {

            }
        }

        //Окончание записи
        private void waveIn_RecordingStopped(object sender, EventArgs e)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new EventHandler(waveIn_RecordingStopped), sender, e);
                }
                else
                {
                    waveIn.Dispose();
                    waveIn = null;
                    writer.Close();
                    writer = null;
                }
            }
            catch(Exception)
            {

            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (waveIn != null)
                {
                    StopRecording(sender, e);
                }
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
         
        }

        //
        //private void button7_MouseMove(object sender, MouseEventArgs e)
        //{
        //    try
        //    {
        //        using(MemoryStream memory = new MemoryStream())
        //        {
        //            label1.Text = "Start Recording";
        //            waveIn = new WaveIn();
        //            //Дефолтное устройство для записи (если оно имеется)
        //            //встроенный микрофон ноутбука имеет номер 0
        //            waveIn.DeviceNumber = 0;
        //            //Прикрепляем к событию DataAvailable обработчик, возникающий при наличии записываемых данных
        //            waveIn.DataAvailable += waveIn_DataAvailable;
        //            //Прикрепляем обработчик завершения записи

        //            waveIn.RecordingStopped += waveIn_RecordingStopped;
        //            //Формат wav-файла - принимает параметры - частоту дискретизации и количество каналов(здесь mono)
        //            waveIn.WaveFormat = new WaveFormat(8000, 1);
        //            //Инициализируем объект WaveFileWriter
        //            writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
        //            //Начало записи
        //            waveIn.StartRecording();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void button7_MouseLeave(object sender, EventArgs e)
        //{
        //    if (waveIn != null)
        //    {
        //        StopRecording();
        //    }
        //    // outputFilename
        //}



        private void прослушатьСообщениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewChat == null)
                {

                }
                else
                {



                    //int tt=0;
                    int selectedrowindexs = dataGridViewChat.SelectedCells[0].RowIndex;

                    //Находим по фильтру id dataGridViewChat
                    //for(int i=0; i < allChat.Length; i++)
                    //{
                    //    if (allChat[i].Id == selectedrowindexs)
                    //    {
                    //        tt = allChat[i].Files;
                    //        break;
                    //    }

                    //}

                    //Сообщения для удаления
                    //textBox1.Text = tt.Message;
                    //id сообщения для удаления
                    // Update_id = tt.Id;


                    string FileFS = "";

                    var Сообщение = allChat[selectedrowindexs].Files;



                    if (Сообщение == 0)
                    {

                    }
                    else
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {

                            byte[] bytes = new byte[] { };
                            Insert_Fille_Music insert_Fille_Music = new Insert_Fille_Music(Сообщение, bytes);
                            JsonSerializer.Serialize<Insert_Fille_Music>(ms, insert_Fille_Music);

                            FileFS = Encoding.Default.GetString(ms.ToArray());
                            //FileFS = "";
                            Task.Run(async () => await command.Stream_Fille_accept_music(IP_ADRES.Ip_adress, FileFS, "020")).Wait();
                        }
                        if (command.Select_Fille_Music_id == null)
                        {

                        }
                        else
                        {

                            Insert_Fille_Music _Fille_Music = command.Select_Fille_Music_id;
                            /*
                            var path = Environment.CurrentDirectory.ToString();
                            string NameFile = path + "\\AudioFiles\\AudioFile.mp3";

                            Random rand = new Random();

                            if (File.Exists(path))
                            {
                                NameFile = NameFile + rand.Next(1000000000) as String;
                            }

                            //string filePath = "name.mp3";
                            File.WriteAllBytes(NameFile, _Fille_Music.Fille);
                            //Process.Start(NameFile);
                            //using (FileStream fileStream = new FileStream(NameFile, FileMode.OpenOrCreate))
                            //{


                            //    fileStream.Write(_Fille_Music.Fille, 0, _Fille_Music.Fille.Length);
                            //}

                            //   using(FileStream FS = new CreateParams )
                            //   var path = Environment.CurrentDirectory.ToString();

                            SoundPlayer simpleSound = new SoundPlayer($"{NameFile}");
                            simpleSound.Play();

                            //simpleSound WindowsMediaPlayer
                            //  WindowsMediaPlayer
                            //WMPLib.WindowsMediaPlayer WMP = new WMPLib.WindowsMediaPlayer();
                            //this.Text = WMP.versionInfo;
                            //WMP.URL = @"D:\sound.mp3 ";
                            //WMP.controls.play();
                            */

                            // Воспроизводит звук из памяти из формата byte[]
                            using (MemoryStream fileStream = new MemoryStream(_Fille_Music.Fille))
                            {
                                SoundPlayer simpleSound2 = new SoundPlayer(fileStream);
                                simpleSound2.Play();
                            }

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void слушатьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        //   MainForm mainForm = new MainForm();
            
        //    mainForm.ShowDialog();
        }
        //[Obsolete]//
        private void button8_Click(object sender, EventArgs e)
        {

            try
            {
                MessageBox.Show("Start Recording");
                waveIn = new WaveIn();
                //Дефолтное устройство для записи (если оно имеется)
                //встроенный микрофон ноутбука имеет номер 0
                waveIn.DeviceNumber = 0;

                //Прикрепляем к событию DataAvailable обработчик, возникающий при наличии записываемых данных
#pragma warning disable CS0612 // Тип или член устарел
                waveIn.DataAvailable += waveIn_DataAvailable;
#pragma warning restore CS0612 // Тип или член устарел
                              //Прикрепляем обработчик завершения записи

                waveIn.RecordingStopped += waveIn_RecordingStopped;
                //Формат wav-файла - принимает параметры - частоту дискретизации и количество каналов(здесь mono)
                waveIn.WaveFormat = new WaveFormat(8000, 1);
                //Инициализируем объект WaveFileWriter
                writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                //Начало записи
                waveIn.StartRecording();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                Application.ExitThread();
                Environment.Exit(0);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }
    }
}

