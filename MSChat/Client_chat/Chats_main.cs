using Class_chat;
using System;
//using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
//using Newtonsoft.Json.Linq;
//using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
//using static System.Net.WebRequestMethods;
//using static System.Net.Mime.MediaTypeNames;
//using System.Data;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
//using static System.Net.WebRequestMethods;

namespace Client_chat
{
    public partial class Chats_main : Form
    {
        public Chats_main()
        {
            InitializeComponent();
        }

        string Na_me { get; set; }
        public User_photo[] Friend { get; set; }
        public MessСhat[] allChat { get; set; }
      //  public bool all_Chat { get; set; }
 //       public Image Image { get; set; }
        public int Users { get; set; }
        public int Friends { get; set; }
        public bool Update_Message { get; set; }

        public int Update_id { get; set; }

        public int selectedBiodataId;
        public bool Entrance { get; set; }
        public string Respons { get; set; }
       
        public  CommandCL command = new CommandCL();


        //Отображает Сообщения из чата
        public void Chat(CommandCL command)
        {
            using (MemoryStream Chats = new MemoryStream())
            {
                if (command._Answe != null)
                {
                    if (command._Answe.ToString() == "true")
                    {
                        MessСhat[] les = new MessСhat[command._AClass.Count()];

                        for (int i = 0; i < command._AClass.Count(); i++)
                        {
                            string yu = command._AClass[i].ToString();
                            MessСhat useTravel = JsonSerializer.Deserialize<MessСhat>(yu);
                            les[i] = useTravel;
                        }
                        dataGridViewChat.Rows.Clear();
                        dataGridViewChat.RowCount = les.Count();
                        dataGridViewChat.ColumnCount = 2;
                        DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                        {
                        }
                        allChat = les;
                        dataGridViewChat.Columns.Insert(2, column);
                        for (int i = 0; i < les.Count(); i++)
                        {
                            for (int j = 0; j < 1; j++)
                            {
                                dataGridViewChat.Rows[i].Cells[j].Value = les[i].Message;
                                dataGridViewChat.Columns[j].HeaderText = "Сообщения";
                                if (les[i].IdUserFrom != Users)
                                {
                                    dataGridViewChat.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
                                }
                            }
                            for (int j = 1; j < 2; j++)
                            {
                                dataGridViewChat.Rows[i].Cells[j].Value = les[i].DataMess;
                                dataGridViewChat.Columns[j].HeaderText = "Дата отправки";
                            }
                            for (int j = 2; j < 3; j++)
                            {
                                bool aMark = false;
                                if (les[i].Mark.ToString() == "1")
                                {
                                    aMark = true;
                                }
                                dataGridViewChat.Rows[i].Cells[j].Value = aMark;
                            }
                        }
                        dataGridViewChat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridViewChat.Visible = true;
                    }
                    else
                    {
                        dataGridViewChat.Rows.Clear();
                    }
                }
                else
                {
                    dataGridViewChat.Rows.Clear();
                    //MessageBox.Show("Сообщений нет");
                }
            }
        }


        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
        }


        //MsgUser_Logins s
        public void NotifyMe(MsgUser_Logins responseDat)
        {
            OpenMes(responseDat);
        }

        //Сохраняет файл конфигурации в json
        public void SaveConfig(Int32 Port, string ipAddress,string Name)
        {
            using (FileStream file = new FileStream("Client.json", FileMode.OpenOrCreate))
            {
                Connect_Client_ connect_Client = new Connect_Client_(Port, ipAddress, Name);
                JsonSerializer.Serialize<Connect_Client_>(file,connect_Client);
                IP_ADRES.Ip_adress = ipAddress;
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
                        string ipAddress = toolStripTextBox1.Text;
                        Ping pingSender = new Ping();
                        PingReply reply = pingSender.Send(ipAddress);
                        if (reply.Status == IPStatus.Success)
                        {
                            SaveConfig(ConnectSettings.port, ipAddress,"");
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
                            Console.WriteLine("Ping to {0} failed.", ipAddress);
                        }
                    } 
                   catch   
                   {

                   }
              
                }
                else
                {
                    Entrance = false;
                    MessageBox.Show("Ip-адрес:Не заполнен");
                }
            }
            catch
            {

            }
        }

        //
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        //
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

        //
        private void toolStripButton1_MouseHover(object sender, EventArgs e)
        {
        }


        //Две команды для отправки нового сообщения и редактования сообщения
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
                                MessСhat Mes_chat = new MessСhat(Update_id, Users, Friends, textBox1.Text, dateTime, 1);
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
                            DateTime dateTime = DateTime.Now;
                            MessСhat Mes_chat = new MessСhat(0, Users, Friends, textBox1.Text, dateTime, 1);
                            JsonSerializer.Serialize<MessСhat>(fs, Mes_chat);
                            FileFS = Encoding.Default.GetString(fs.ToArray());
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
            catch
            {

            }
        }

        //Открытие формы и заполнение таблиц
        public void OpenMes(MsgUser_Logins Friends)
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
                    try
                    //Проверяем  если там сообщение если true то заполняет  dataGridViewUser  друзей и их картинки
                    {
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
                    catch (Exception ex)
                    {
                        //Для выведения ошибки при картинке
                        MessageBox.Show(ex.Message);
                    }
                }
            }            
        }      

        //
        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {

        }
  
        //
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
                    toolStripButton1.Image = Image.FromFile(path+ "\\Resources\\Images\\Красный..png"); 
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
                FileInfo fileInfo = new FileInfo(path+"\\Client.json");
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
                MessageBox.Show(a.Message);
            }
        }

        //
        private void dataGridViewUser_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewChat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        //При нажатии dataGridViewUser_Click проверяется список сообщений
        private void dataGridViewUser_Click(object sender, EventArgs e)
        {
            try
            {
                // ???
                view_mess();
            }
            catch 
            {
            }
        }


        //Запращиваем сообщения из друзей id dataGridViewUser_Click и проверяется список сообщений
        private void view_mess()
        {
            try
            {      
                //Проверяем не пустая ли таблица друзей 
                if (dataGridViewUser == null)
                {

                }
                else
                {
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
                                
                                Chat(command);             
                            }
                        }
                    }
                }
            }
            catch (Exception )
            {
                //Здесь ошибки есть По этому так чтобы программа не падала переменую и не создал
                //MessageBox.Show(e.Message);

            }
        }

        //
        private void dataGridViewChat_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewChat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        //
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        //Добавляем друзей
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //Заполняем класс Searh_Friends
                using (MemoryStream Despons_friend = new MemoryStream()) 
                {
                    string FileFS = "";
                    using (MemoryStream fs = new MemoryStream())
                    {
                        Searh_Friends New_Friend = new Searh_Friends(textBox2.Text, Na_me);
                        JsonSerializer.Serialize<Searh_Friends>(fs, New_Friend);
                        FileFS = Encoding.Default.GetString(fs.ToArray());
                     
                    }
                    //Отправляем на сервер поиск такого друга
                    Task.Run(async () => await command.Connect_Friends(IP_ADRES.Ip_adress, FileFS, "008")).Wait();
                    //Просто проверяем и отчищаем
                    var __Friends =  command._Friends;
                    command._Friends = null;
                }
            }
            catch
            {
             // если друзей нету то ошибка возможно или просто не найдет  там по имени  то его не добавиться
            }           
        }

        //Работает но  его скрываем от пользователя так как есть лучше метод визуальный поэтому это не использовал 
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedrowindexs = dataGridViewChat.SelectedCells[0].RowIndex;
                MessСhat tt = allChat[selectedrowindexs];
                textBox1.Text = tt.Message;
                Update_id = tt.Id;
                string FileFS = "";
                using (MemoryStream fs = new MemoryStream())
                {
                    DateTime dateTime = DateTime.Now;
                    MessСhat Mes_chat = new MessСhat(Update_id, Users, Friends, textBox1.Text, dateTime, 1);
                    JsonSerializer.Serialize<MessСhat>(fs, Mes_chat);
                    FileFS = Encoding.Default.GetString(fs.ToArray());
                }
                textBox1.Text = "";
            }
            catch
            {

            }
        }


        //Для функций над contextMenuStrip1  там выбор и удобно
        private void dgrdResults_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                }
            }
            catch { }
        }

        //Для функций над contextMenuStrip1 сдесь просто нажать надо  там выбор и удобно
        private void dgrdResults_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //handle the row selection on right click
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    dataGridViewChat.CurrentCell = dataGridViewChat.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    // Can leave these here - doesn't hurt
                    dataGridViewChat.Rows[e.RowIndex].Selected = true;
                    dataGridViewChat.Focus();

                    //selectedBiodataId = Convert.ToInt32(dataGridViewChat.Rows[e.RowIndex].Cells[1].Value);
                    selectedBiodataId = e.RowIndex;
                }
                catch (Exception)
                {

                }
            }
        }

        //Редактирование работает чатом
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Редактировать запись "+ selectedBiodataId.ToString());
            try
            {      //
                using (MemoryStream Update = new MemoryStream())
                {
                    if (dataGridViewChat == null)
                    {

                    }
                    else
                    {
                        //Поиск dataGridViewChat.SelectedCells[0].RowIndex 
                        int selectedrowindexs = dataGridViewChat.SelectedCells[0].RowIndex;
                        MessСhat tt = allChat[selectedrowindexs];
                        textBox1.Text = tt.Message;
                        Update_id = tt.Id;
                        Update_Message = true;
                        //ДАЕМ РАЗРЕШЕНИЕ ПРИ РЕДАКТИРОВАНИЕИ ОТПРАВКУ
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

        //Удалят строку с сообщением
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedrowindexs = dataGridViewChat.SelectedCells[0].RowIndex;
                MessСhat tt = allChat[selectedrowindexs];
                textBox1.Text = tt.Message;
                Update_id = tt.Id;
                string FileFS = "";
                using (MemoryStream fs = new MemoryStream())
                {
                    DateTime dateTime = DateTime.Now;
                    MessСhat Mes_chat = new MessСhat(Update_id, Users, Friends, textBox1.Text, dateTime, 1);
                    JsonSerializer.Serialize<MessСhat>(fs, Mes_chat);
                    FileFS = Encoding.Default.GetString(fs.ToArray());
                }
                textBox1.Text = "";

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
            catch
            {

            }
        }

        //
        private void dataGridViewUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        //
        private void dataGridViewChat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        //
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //Для обновлений сообщений
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
                    view_mess();
                }
            }
            catch
            {

            }
         }

        //Список друзей обновляет но только добавленных поэтому надо будет доделать это 
        async private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (toolStripLabel1.Text != "")
                {
                    using (TcpClient client = new TcpClient(IP_ADRES.Ip_adress, ConnectSettings.port))
                    {
                        NetworkStream stream = client.GetStream();
                        User_photo tt = new User_photo("", "", "", 0, 0, Users);
                        //tt.Current = Users;
                        string person = JsonSerializer.Serialize<User_photo>(tt);

                        Byte[] data = System.Text.Encoding.Default.GetBytes("013" + person);
                        await stream.WriteAsync(data, 0, data.Length);
                        String responseDat = String.Empty;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            int cnt = 0;
                            Byte[] locbuffer = new byte[1024];
                            do
                            {
                                cnt = await stream.ReadAsync(locbuffer, 0, locbuffer.Length);
                                ms.Write(locbuffer, 0, cnt);
                            } while (client.Available > 0);

                            responseDat = Encoding.Default.GetString(ms.ToArray());
                        }

                        MsgFriends msgFriends = JsonSerializer.Deserialize<MsgFriends>(responseDat);
                        User_photo[] A = new User_photo[msgFriends.AClass.Count];
                        for (int i = 0; i < msgFriends.AClass.Count(); i++)
                        {
                            A[i] = msgFriends.AClass[i];
                        }
                        Friend = A;
                        if (Friend != null)
                        {
                            if (msgFriends.AClass.Count != 0)
                            {
                                using (MemoryStream Friends_Image = new MemoryStream())
                                {
                                    string FileFS = "";
                                    int[] Id = new int[msgFriends.List_Mess];

                                    int[] Id_Friends = new int[msgFriends.List_Mess];
                                    User_photo[] b = new User_photo[msgFriends.List_Mess];
                                    for (int I = 0; I < msgFriends.AClass.Count(); I++)
                                    {
                                        b[I] = msgFriends.AClass[I];
                                        Id_Friends[I] = b[I].Photo;
                                        Id[I] = b[I].Id;
                                    }
                                    Photo_Friends tom = new Photo_Friends(Id_Friends, Id);
                                    JsonSerializer.Serialize<Photo_Friends>(Friends_Image, tom);
                                    FileFS = Encoding.Default.GetString(Friends_Image.ToArray());

                                    Task.Run(async () => await command.Get_Image_Friends(IP_ADRES.Ip_adress, FileFS, "014")).Wait();

                                }
                            }
                            if (msgFriends.Answe == "true")
                            {
                                try
                                {
                                    if (msgFriends.AClass == null)
                                    {
                                    }
                                    else
                                    {
                                        dataGridViewUser.Rows.Clear();
                                        dataGridViewUser.RowCount = Friend.Count();
                                        dataGridViewUser.ColumnCount = 1;
                                        DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                                        imgColumn.Name = "Фото";                        
                                        dataGridViewUser.Columns.Add(imgColumn);                               
                                        for (int i = 0; i < Friend.Count(); i++)
                                        {
                                                DataGridViewTextBoxCell cell0 = (DataGridViewTextBoxCell)dataGridViewUser.Rows[i].Cells[0];
                                                cell0.Value = Friend[i].Name;
                                        }

                                        for (int i = 0; i < Friend.Count(); i++)
                                        {
                                            string Im = command.List_Friends[i].ToString();
                                            byte[] ByteImege = Convert.FromBase64String(Im);
                                            MemoryStream ms = new MemoryStream(ByteImege);
                                            Image returnImagee = Image.FromStream(ms);
                                            DataGridViewImageCell cell1 = (DataGridViewImageCell)dataGridViewUser.Rows[i].Cells["Фото"];
                                            cell1.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                            cell1.Value = returnImagee;
                                        }
                                        dataGridViewUser.Columns[0].HeaderText = "Друзья";
                                        dataGridViewUser.Columns[1].HeaderText = "Фото";                                     
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                            else
                            {
                                dataGridViewUser.Rows.Clear();
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
}

