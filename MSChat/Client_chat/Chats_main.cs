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
using Newtonsoft.Json.Linq;
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
        public bool all_Chat { get; set; }
        public Image Image { get; set; }
        public int Users { get; set; }
        public int Friends { get; set; }
        public bool Update_Message { get; set; }

        public int Update_id { get; set; }

        public int selectedBiodataId;
        public bool Entrance { get; set; }
        public string Respons { get; set; }

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

        /*//MessageBox.Show("Сообщений нет");   
                        //sender.Columns[2].ValueType = typeof(bool);
                                //sender.Columns[2].DefaultCellStyle. = 
                                //  sender.Columns[j].HeaderText = "";*/
        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
        }
        //MsgUser_Logins s
        public void NotifyMe(MsgUser_Logins responseDat)
        {//JToken s

            //JObject details = JObject.Parse(responseDat);

            OpenMes(responseDat);
        }


        //async public void Connect_Servis(String server, string fs, string command)
        //{
        //    try
        //    {
        //        using (TcpClient client = new TcpClient(server, ConnectSettings.port))
        //        {
        //            byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
        //            string Host = System.Net.Dns.GetHostName();
        //            NetworkStream stream = client.GetStream();
        //            await stream.WriteAsync(data, 0, data.Length);


        //        }
        //    }
        //    catch
        //    {

        //    }


        // }

        public void SaveConfig(Int32 Port, string ipAddress,string Name)
        {
            using (FileStream file = new FileStream("Client.json", FileMode.OpenOrCreate))
            {
                Connect_Client_ connect_Client = new Connect_Client_(Port, ipAddress, Name);
                JsonSerializer.Serialize<Connect_Client_>(file,connect_Client);
                IP_ADRES.Ip_adress = ipAddress;
            }
        }


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
                                
                                //this.Hide();
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






        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

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

        private void toolStripButton1_MouseHover(object sender, EventArgs e)
        {
        }


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
                    CommandCL command = new CommandCL();

                    if (Update_Message == true)
                    {    
                        
                        using (MemoryStream _Update_Message = new MemoryStream())
                        {
                            using (MemoryStream Update = new MemoryStream())
                            {
                                DateTime dateTime = DateTime.Now;
                                MessСhat Mes_chat = new MessСhat(Update_id, Users, Friends, textBox1.Text, dateTime, 1);
                                JsonSerializer.Serialize<MessСhat>(Update, Mes_chat);
                                FileFS = Encoding.Default.GetString(Update.ToArray());
                            }

                            //command.Update_Message_make_up(IP_ADRES.Ip_adress, FileFS, "010");
                            Task.Run(async () => await command.Update_Message_make_up(IP_ADRES.Ip_adress, FileFS, "010")).Wait();
                            if (command.___Answe != null)
                            {
                                if (command.___Answe.ToString() == "true")
                                {
                                    MessСhat[] les = new MessСhat[command.___AClass.Count()];

                                    for (int i = 0; i < command.___AClass.Count(); i++)
                                    {
                                        string yu = command.___AClass[i].ToString();
                                        MessСhat useTravel = JsonSerializer.Deserialize<MessСhat>(yu);
                                        les[i] = useTravel;
                                    }
                                    dataGridViewChat.Rows.Clear();
                                    dataGridViewChat.RowCount = les.Count();
                                    dataGridViewChat.ColumnCount = 2;
                                    DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                                    {
                                    }

                                    dataGridViewChat.Columns.Insert(2, column);
                                    //sender.Columns[0].HeaderText = "Друзья";

                                    for (int i = 0; i < les.Count(); i++)
                                    {
                                        for (int j = 0; j < 1; j++)
                                        {
                                            dataGridViewChat.Rows[i].Cells[j].Value = les[i].Message;
                                            if (les[i].IdUserFrom != Users)
                                            {
                                                dataGridViewChat.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
                                            }

                                        }
                                        for (int k = 1; k < 2; k++)
                                        {
                                            dataGridViewChat.Rows[i].Cells[k].Value = les[i].DataMess;
                                        }
                                        for (int b = 2; b < 3; b++)
                                        {
                                            bool aMark = false;
                                            if (les[i].Mark.ToString() == "1")
                                            {
                                                aMark = true;
                                            }
                                            dataGridViewChat.Rows[i].Cells[b].Value = aMark;
                                        }
                                    }
                                    dataGridViewChat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                                    allChat = les;
                                    dataGridViewChat.Visible = true;
                                }
                                else
                                {
                                    dataGridViewChat.Rows.Clear();
                                    //MessageBox.Show("Сообщений нет");
                                }
                                command.___Answe = null;
                                command.___AClass = null;
                                command.___List_Mess_count = null;
                                Update_Message = false;
                                Update_id = 0;
                                textBox1.Text = "";
                            }
                        }
                      //  Update_Message_make_up(IP_ADRES.Ip_adress, FileFS, "010",dataGridViewChat);
                    }
                    else
                    {
                        using (MemoryStream fs = new MemoryStream())
                        {
                            /*
                            //string serialized = buf.ToString();
                            //Searh_Friends New_Friend = new Searh_Friends(textBox2.Text);*/ 
                                    /*
                              //        JsonSerializer.SerializeToDefaultBytes(fs,);

                              //Console.WriteLine("Data has been saved to file");*/
                            DateTime dateTime = DateTime.Now;
                            MessСhat Mes_chat = new MessСhat(0, Users, Friends, textBox1.Text, dateTime, 1);
                            JsonSerializer.Serialize<MessСhat>(fs, Mes_chat);
                            FileFS = Encoding.Default.GetString(fs.ToArray());                       
                        }

                           // чтение данных
                           textBox1.Text = "";
                        // Добавляем сообщение в чат !
                        //Освобождаем ресурсы!
                        using (MemoryStream New_message = new MemoryStream())
                        {
                            //command.Insert_Message(IP_ADRES.Ip_adress, FileFS,"009");
                            Task.Run(async () => await command.Insert_Message(IP_ADRES.Ip_adress, FileFS, "009")).Wait();

                            if (command.__Answe != null)
                            {
                                if (command.__Answe.ToString() == "true")
                                {
                                    MessСhat[] les = new MessСhat[command.__AClass.Count()];
                                    for (int i = 0; i < command.__AClass.Count(); i++)
                                    {
                                        string yu = command.__AClass[i].ToString();
                                        MessСhat useTravel = JsonSerializer.Deserialize<MessСhat>(yu);
                                        les[i] = useTravel;
                                    }
                                    dataGridViewChat.Rows.Clear();
                                    dataGridViewChat.RowCount = les.Count();
                                    dataGridViewChat.ColumnCount = 2;
                                    DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                                    {
                                    }

                                    dataGridViewChat.Columns.Insert(2, column);

                                    for (int i = 0; i < les.Count(); i++)
                                    {
                                        for (int j = 0; j < 1; j++)
                                        {
                                            dataGridViewChat.Rows[i].Cells[j].Value = les[i].Message;
                                            if (les[i].IdUserFrom != Users)
                                            {
                                                dataGridViewChat.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
                                            }
                                        }
                                        for (int j = 1; j < 2; j++)
                                        {
                                            dataGridViewChat.Rows[i].Cells[j].Value = les[i].DataMess;
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
                                    allChat = les;
                                    dataGridViewChat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                                    dataGridViewChat.Visible = true;
                                }
                                else
                                {
                                    dataGridViewChat.Rows.Clear();
                                }
                                command.__Answe = null;
                                command.__AClass = null;
                                command.__List_Mess_count = null;
                                // Insert_Message(IP_ADRES.Ip_adress, FileFS, "009", dataGridViewChat);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }



        //public string Func_Read(Stream str, int length, TcpClient client) 
        //{ 
        //    string Result = string.Empty; 
        //    using (MemoryStream ms = new MemoryStream()) 
        //    { 
        //        int cnt = 0; 
        //        Byte[] locbuffer = new byte[length]; 
        //        do { 
        //            cnt = str.Read(locbuffer, 0, locbuffer.Length); 
        //            ms.Write(locbuffer, 0, cnt); } 
        //        while (client.Available > 0); 
        //        Result = Encoding.Default.GetString(ms.ToArray()); 
        //    } 
        //    return Result; 
        //}

        // Процедура отправки 009

        
    //    async void Insert_Message(String server, string fs, string command, DataGridView sender)
    //    {
    //        try
    //        {
    //            //Int32 port = 9595;

    //            using (TcpClient client = new TcpClient(server, ConnectSettings.port))
    //            {
    //                Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
    //                NetworkStream stream = client.GetStream();
    //                await stream.WriteAsync(data, 0, data.Length);

                 
    //                String responseDat = String.Empty;
                    
    //                //responseDat = Respons;
                 
                    
    //                responseDat = await Task<string>.Run(() =>
    //                {
    //                    return Func_Read(stream, data.Length, client);
    //                }); 

    //                MsgInfo msgInfo = JsonSerializer.Deserialize<MsgInfo>(responseDat);

    //                JObject details = JObject.Parse(responseDat);
    //                JToken Answe = details.SelectToken("Answe");
    //                JToken List_Mess = details.SelectToken("List_Mess");
    //                JToken AClass = details.SelectToken("AClass");
    //                if (Answe.ToString() == "true")
    //                {
    //                    MessСhat[] les = new MessСhat[AClass.Count()];

    //                    for (int i = 0; i < AClass.Count(); i++)
    //                    {
    //                        string yu = AClass[i].ToString();
    //                        MessСhat useTravel = JsonSerializer.Deserialize<MessСhat>(yu);
    //                        les[i] = useTravel;
    //                    }
    //                    sender.Rows.Clear();
    //                    sender.RowCount = les.Count();
    //                    sender.ColumnCount = 2;
    //                    DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
    //                    {
    //                    }

    //                    sender.Columns.Insert(2, column);

    //                    for (int i = 0; i < les.Count(); i++)
    //                    {
    //                        for (int j = 0; j < 1; j++)
    //                        {
    //                            sender.Rows[i].Cells[j].Value = les[i].Message;
    //                            if (les[i].IdUserFrom != Users)
    //                            { sender.Rows[i].Cells[j].Style.ForeColor = Color.Blue; }

    //                        }
    //                        for (int j = 1; j < 2; j++)
    //                        {
    //                            sender.Rows[i].Cells[j].Value = les[i].DataMess;
    //                        }
    //                        for (int j = 2; j < 3; j++)
    //                        {
    //                            bool aMark = false;
    //                            if (les[i].Mark.ToString() == "1")
    //                            {
    //                                aMark = true;
    //                            }
    //                            sender.Rows[i].Cells[j].Value = aMark;
    //                        }
    //                    }
    //                    allChat = les;
    //                    sender.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    //                    sender.Visible = true;
    //                }
    //                else
    //                {
    //                    sender.Rows.Clear();
    //                }
    //            }
    //            //stream.Close();
    //            //client.Close();

    //        }
    //        catch (ArgumentNullException e)
    //        {
    //            MessageBox.Show("ArgumentNullException:{0}", e.Message);
    //        }
    //        catch (SocketException e)
    //        {
    //            MessageBox.Show("SocketException: {0}", e.Message);
    //        }
    //        /* работает 
    //using (MemoryStream ms = new MemoryStream())
    //{
    //    int cnt = 0;
    //    Byte[] locbuffer = new byte[1024];
    //    do
    //    {
    //        cnt = await stream.ReadAsync(locbuffer, 0, locbuffer.Length);
    //        ms.Write(locbuffer, 0, cnt);
    //    } while (client.Available > 0);

    //    responseDat = Encoding.Default.GetString(ms.ToArray());
    //}
    //*/
    //        /*
    //  *
    //                    //data = new Byte[99999909];


    //                    //Byte[] data2 = new Byte[client.ReceiveBufferSize];

    //                    //Int32 bytesMess = await stream.ReadAsync(data2, 0, data2.Length);

    //                    //responseDat = System.Text.Encoding.Default.GetString(data2, 0, bytesMess);*/
    //        
    //                           //TcpClient.Available
    //                           //// буфер ддя получения данных
    //                           //var responseData_New = new byte[1024];
    //                           //// StringBuilder для склеивания полученных данных в одну строку
    //                           //var response = new StringBuilder();
    //                           //int bytes;  // количество полученных байтов
    //                           //do
    //                           //{
    //                           //    // получаем данные
    //                           //    bytes = await stream.ReadAsync(responseData_New, 0, responseData_New.Length);
    //                           //    // преобразуем в строку и добавляем ее в StringBuilder
    //                           //    response.Append(Encoding.Default.GetString(responseData_New, 0, bytes));
    //                           //}
    //                           //while (bytes > 0); // пока данные есть в потоке 

    //                           //Func_Read(stream,1024, client);

    //                           //String responseData = String.Empty;
    //                           

    //    }*/


        // Процедура отправки 010
                                                                                /*
        async void Update_Message_make_up(String server, string fs, string command, DataGridView sender)
        {
            try
            {
                //Int32 port = 9595;

                using (TcpClient client = new TcpClient(server, ConnectSettings.port))
                {
                    Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
                    NetworkStream stream = client.GetStream();
                    await stream.WriteAsync(data, 0, data.Length);

                    //String responseData = String.Empty;
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
                    /*
                    data = new Byte[99999909];
                    Byte[] data2 = new Byte[99999909];
                    Int32 bytesMess = await stream.ReadAsync(data2, 0, data2.Length);
                    responseDat = System.Text.Encoding.Default.GetString(data2, 0, bytesMess);
                    */
                                                                                       /*
                    MsgInfo msgInfo = JsonSerializer.Deserialize<MsgInfo>(responseDat);

                    JObject details = JObject.Parse(responseDat);
                    JToken Answe = details.SelectToken("Answe");
                    JToken List_Mess = details.SelectToken("List_Mess");
                    JToken AClass = details.SelectToken("AClass");
                    if (Answe.ToString() == "true")
                    {
                        MessСhat[] les = new MessСhat[AClass.Count()];

                        for (int i = 0; i < AClass.Count(); i++)
                        {
                            string yu = AClass[i].ToString();
                            MessСhat useTravel = JsonSerializer.Deserialize<MessСhat>(yu);
                            les[i] = useTravel;
                        }
                        sender.Rows.Clear();
                        sender.RowCount = les.Count();
                        sender.ColumnCount = 2;
                        DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                        {
                        }

                        sender.Columns.Insert(2, column);
                        //sender.Columns[0].HeaderText = "Друзья";

                        for (int i = 0; i < les.Count(); i++)
                        {
                            for (int j = 0; j < 1; j++)
                            {
                                sender.Rows[i].Cells[j].Value = les[i].Message;
                                if (les[i].IdUserFrom != Users)
                                {
                                    sender.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
                                }

                            }
                            for (int k = 1; k < 2; k++)
                            {
                                sender.Rows[i].Cells[k].Value = les[i].DataMess;
                            }
                            for (int b = 2; b < 3; b++)
                            {
                                bool aMark = false;
                                if (les[i].Mark.ToString() == "1")
                                {
                                    aMark = true;
                                }
                                sender.Rows[i].Cells[b].Value = aMark;
                            }
                        }

                        sender.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        allChat = les;
                        sender.Visible = true;
                    }
                    else
                    {
                        sender.Rows.Clear();
                        //MessageBox.Show("Сообщений нет");
                    }
                }
                //stream.Close();
                //client.Close();
            }
            catch (ArgumentNullException e)
            {
                MessageBox.Show("ArgumentNullException:{0}", e.Message);
            }
            catch (SocketException e)
            {
                MessageBox.Show("SocketException: {0}", e.Message);
            }
        }
        */
                                                                                              /*
            //JToken s Friends, string responseDat)
      //  MsgUser_Logins person3 = JsonSerializer.Deserialize<MsgUser_Logins>(responseDat);
             //   User_photo user_Photo = null;

                //string Numbers_Friends = Convert.ToString(AClass);
                //user_Photo

         

                //User_photo ruser = JsonSerializer.Deserialize<User_photo>(Friends.User_);
                //JToken Answe = Friends.SelectToken("Answe");
                //JToken List_Mess = Friends.SelectToken("User_");
                //JToken AClass = Friends.SelectToken("List_Mess");
                //JToken AClassы = Friends.SelectToken("AClass");*/
        public void OpenMes(MsgUser_Logins Friends)
        {
           
                toolStripTextBox1.Text = IP_ADRES.Ip_adress;
               CommandCL command = new CommandCL();

            if (Friends != null)
            {
                using(MemoryStream Friends_Image = new MemoryStream())
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
                    Photo_Friends tom = new Photo_Friends(Id_Friends,Id);
                    JsonSerializer.Serialize<Photo_Friends>(Friends_Image, tom);
                    FileFS = Encoding.Default.GetString(Friends_Image.ToArray());

                    Task.Run(async () => await command.Get_Image_Friends(IP_ADRES.Ip_adress, FileFS,"014")).Wait();
                    
                }
              
                toolStripLabel1.Text = Friends.User_.Name;
                using (MemoryStream fs = new MemoryStream())
                {
                    //CommandCL command = new CommandCL();
                    string FileFS = "";
                    Photo tom = new Photo(Friends.User_.Photo, Friends.User_.Current);
                    JsonSerializer.Serialize<Photo>(fs, tom);

                    FileFS = Encoding.Default.GetString(fs.ToArray());

                    Task.Run(async () => await command.Get_Image(IP_ADRES.Ip_adress, FileFS, "007")).Wait();

                    //string Im = command.UserImage.Image.First().ToString(); //AClassIm
                    string Im = command.AClassIm.First().ToString(); 
                    byte[] ByteImege = Convert.FromBase64String(Im);
                    MemoryStream ms = new MemoryStream(ByteImege);
                    Image returnImage = Image.FromStream(ms);
                    toolStripButton2.Image = returnImage;

                }
                //MemoryStream ms = new MemoryStream(ruser.Photo);MsgUser_Logins
                //Image returnImage = Image.FromStream(ms);
                //toolStripButton2.Image = returnImage;

                Na_me = Friends.User_.Name;
                // Na_me = Friends.User_.Name;
                //  int id = 0;   // Answe
                //    toolStripLabel1.Text = Na_me;
                //CommandCL commandS = new CommandCL();

                if (Friends.List_Mess == 0)
                {
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
                    {
                        if (Friends.Answe == "false")
                        {
                        }
                        else
                        {
                            dataGridViewUser.RowCount = Friend.Count();
                            dataGridViewUser.ColumnCount = 1;
                            //DataGridViewTextBoxColumn column0 = new DataGridViewTextBoxColumn();
                            //{
                            //}

                            //dataGridViewChat.Columns.Insert(0, column0);

                            //DataGridViewImageColumn column1 = new DataGridViewImageColumn();
                            //{
                            //}
                            //dataGridViewChat.Columns.Insert(1, column1);
                            //for (int I = 0;I< ms.Length;I++)
                            //{
                            //    images[I] = returnImagee;

                            //}                       
                            DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                            imgColumn.Name = "Images";
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

                                //DataGridViewImageColumn img = new DataGridViewImageColumn();

                                DataGridViewImageCell cell1 = (DataGridViewImageCell)dataGridViewUser.Rows[i].Cells["Images"];
                                cell1.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                //img.Image= returnImagee;
                                cell1.Value = returnImagee;
                                //dataGridViewUser.Rows[i].Cells["Images"].Value = returnImagee;
                            }

                            dataGridViewUser.Columns[0].HeaderText = "Друзья";
                            dataGridViewUser.Columns[1].HeaderText = "Фото";
                            // dataGridViewUser.Columns[1].HeaderText = "Фото Друзей";


                        }
                        Users = Friends.User_.Id;

                        //dataGridView1.DataSource = ds.Tables[0];
                        //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        //dataGridView1.Columns["Код"].Visible = false;
                        //dataGridView1.RowHeadersVisible = false;

                        //for (int i = 0; i < dataGridView1.RowCount; i++)
                        //{
                        //    DataGridViewImageCell cell = (DataGridViewImageCell)dataGridView1.Rows[i].Cells[dataGridView1.Columns["ФотоОтправитель"].Index];
                        //    cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        //    DataGridViewImageCell cell2 = (DataGridViewImageCell)dataGridView1.Rows[i].Cells[dataGridView1.Columns["ФотоПолучатель"].Index];
                        //    cell2.ImageLayout = DataGridViewImageCellLayout.Zoom;

                        //}



                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                string pathImage = Environment.CurrentDirectory.ToString();
                toolStripButton1.Image = Image.FromFile(pathImage + "\\Resources\\Images\\Зеленый.png");
            }

            
        }      

            //if (toolStripButton2.Image == null)
            //{
            //    // toolStripButton1.Image = Image.FromFile();
            //}                         //Friend[i].Name = Convert.ToString(dataGridViewUser.Rows[i].Cells[j].Value);
                //   int h = 0;

 // Друзья.Displayed.ToString(Friend[j].Name   as String);        //Rows[i].Cells[j].Value = 
                            //Друзья.DataGridView.Rows[i].Cells[j].Value= Friend[i].Name;
        /*       //  dataGridViewUser.Visible = true;

                 /*На будущее заготовка для картинки

                 //Use_Photo use_Photo = new Use_Photo(Na_me);
                 //MemoryStream memoryStream = new MemoryStream();
                 //string FileFS = "";
                 //if (UseCompelete.UC)
                 //{
                 //    using (MemoryStream fs = new MemoryStream())
                 //    {
                 //        JsonSerializer.Serialize<Use_Photo>(memoryStream, use_Photo);
                 //        byte[] buffer = new byte[memoryStream.Length];
                 //        memoryStream.Read(buffer, 0, buffer.Length);
                 //        FileFS = Encoding.Default.GetString(buffer);
                 //    }
                 //}
                 //Connectt(IP_ADRES.Ip_adress, FileFS, "007");
                 //toolStripButton2.Image = Image;
                 */
        //toolStrip1.
        //textBox2.Text = aMes;
        //textBox1.Text = aIdUserTo;
        //textBox1.Text = aIdUserFrom;

        //DataTable dt = new DataTable();
        //dataGridViewUser.RowCount = 2;
        //dataGridViewUser.ColumnCount = 1;
        // arr = new string[dataGridViewUser.RowCount, dataGridViewUser.ColumnCount];
        //dataGridViewUser.DataSource = dt;*/


        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {

        }
        //public void pr(bool s )
        //{

        //    if(s!= true)
        //    toolStripButton1.Image = Image.FromFile("Красный.png");

        //}

        private void Chats_main_Load(object sender, EventArgs e)
        {
            //bool s = true;    
            //s = false;

            //    pr(s);
            // BorderStyle.None;
            try
            {
                if (toolStripLabel1.Text != "")
                {


                }
                else 
                {
                    string pathImage = Environment.CurrentDirectory.ToString();
                    toolStripButton1.Image = Image.FromFile(pathImage+"\\Resources\\Images\\Красный..png"); 
                }

                dataGridViewUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridViewUser.RowHeadersVisible = false;
                this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

                dataGridViewChat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridViewChat.RowHeadersVisible = false;

                button2.Visible = false;
                button1.Visible = false;

                // Ищем файл с настройками подключения
                string path = Environment.CurrentDirectory.ToString();
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

        //private void dataGridViewUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //}

        private void dataGridViewUser_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewChat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridViewUser_Click(object sender, EventArgs e)
        {
            try
            {
                view_mess();
            }
            catch 
            {
            }
        }

        private void view_mess()
        {
            try
            {
                int selectedrowindex = dataGridViewUser.SelectedCells[0].RowIndex;
                //DataGridViewRow selectedRow = dataGridViewUser.Rows[selectedrowindex];
                //String Friend = Convert.ToString(selectedRow.Cells[0].Value);
                if (selectedrowindex < 0)
                {

                }else
                {
                    User_photo tt = Friend[selectedrowindex];

                    if (tt == null)
                    {

                    }
                    else

                    {
                        using (MemoryStream Message = new MemoryStream())
                        {


                            tt.Current = Users;
                            string person = JsonSerializer.Serialize<User_photo>(tt);
                            //  Friends = person;


                            User_photo Id_Friend = JsonSerializer.Deserialize<User_photo>(person);
                            Friends = Id_Friend.Id;
                            //_Answe
                            CommandCL command = new CommandCL();

                            //command.Check_Mess_Friend(IP_ADRES.Ip_adress, person, "006");
                            Task.Run(async () => await command.Check_Mess_Friend(IP_ADRES.Ip_adress, person, "006")).Wait();

                            //Chat метод посмотреть ! вместо 3 таких же!
                            Chat(command);

                            // Это уже не надо!
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
                                    //  sender.Rows.Clear();
                                    dataGridViewChat.RowCount = les.Count();
                                    dataGridViewChat.ColumnCount = 2;
                                    DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                                    {
                                        /*
                                        //column.HeaderText = ColumnName.OutOfOffice.ToString();
                                        //column.Name = ColumnName.OutOfOffice.ToString();
                                        //column.AutoSizeMode =
                                        //    DataGridViewAutoSizeColumnMode.DisplayedCells;
                                        //column.FlatStyle = FlatStyle.Standard;
                                        //column.ThreeState = true;
                                        //column.CellTemplate = new DataGridViewCheckBoxCell();
                                        //column.CellTemplate.Style.BackColor = Color.Beige;
                                        */
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
                                            { dataGridViewChat.Rows[i].Cells[j].Style.ForeColor = Color.Blue; }

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
                                            //sender.Columns[2].ValueType = typeof(bool);
                                            //sender.Columns[2].DefaultCellStyle. = 
                                            //  sender.Columns[j].HeaderText = "";
                                        }
                                    }
                                    dataGridViewChat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                                    dataGridViewChat.Visible = true;
                                }
                                else
                                {
                                    dataGridViewChat.Rows.Clear();
                                    //MessageBox.Show("Сообщений нет");
                                }
                            }
                            else
                            {
                                dataGridViewChat.Rows.Clear();
                                //MessageBox.Show("Сообщений нет");
                            }


                            //command._Answe = null;
                            //command._List_Mess_count = null;
                            //command._AClass = null;
                        }
                        //    Check_Mess_Friend(IP_ADRES.Ip_adress, person, "006", dataGridViewChat);
                        //OpenChat(dataGridViewChat);
                    }
                }
            }
            catch (Exception e)
            {
               MessageBox.Show(e.Message);

            }
        }
        /*
        //private static void OpenChat(DataGridView sender)
        //{
        //    try
        //    {
        //        if (all_Chat)
        //        {

        //            sender.RowCount = 2;
        //            sender.ColumnCount = 1;
        //            for (int i = 0; i < allChat.Count(); i++)
        //            {
        //                for (int j = 0; j < 1; j++)
        //                {
        //                  //  sender.Rows[i].Cells[j].Value = allChat[i].Message;
        //                }
        //            }
        //            sender.Visible = true;
        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch(Exception e)
        //    {
        //      MessageBox.Show(e.Message); 
        //    }

        //}

        // Процедура отправки 006
        //async void Check_Mess_Friend(String server, string fs, string command, DataGridView sender)
        //{
        //    try
        //    {
        //        //Int32 port = 9595;

        //        using (TcpClient client = new TcpClient(server, ConnectSettings.port))
        //        {

        //            Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
        //            NetworkStream stream = client.GetStream();
        //            await stream.WriteAsync(data, 0, data.Length);
        //            //data = new Byte[99999999];
        //            //String responseData = String.Empty;
        //            String responseDat = String.Empty;

        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                int cnt = 0;
        //                Byte[] locbuffer = new byte[1024];
        //                do
        //                {
        //                    cnt = await stream.ReadAsync(locbuffer, 0, locbuffer.Length);
        //                    ms.Write(locbuffer, 0, cnt);
        //                } while (client.Available > 0);

        //                responseDat = Encoding.Default.GetString(ms.ToArray());
        //            }
        //            dataGridViewChat.Rows.Clear();

        //            //Int32 bytess = await stream.ReadAsync(data, 0, data.Length);
        //            // responseData = System.Text.Encoding.Default.GetString(data, 0, bytess);
        //            // if (responseData != "false")
        //            // {
        //            //MessСhat aChat = JsonSerializer.Deserialize<MessСhat>(responseData);

        //            //    //получить перечень сообщений
        //            /*
        //            Byte[] data2 = new Byte[99999909];
        //            Int32 bytesMess = await stream.ReadAsync(data2, 0, data2.Length);
                    

        //            //   responseDat =System.Text.Encoding.ASCII.GetString(data,0, bytesMess);

        //            //   data =Convert.FromBase64String(responseDat);
        //            //responseDat =System.Text.Encoding.Default.(data);

        //            //responseDat = System.Text.Encoding.Default.GetString(data, 0, bytesMess);
        //            responseDat = System.Text.Encoding.Default.GetString(data2, 0, bytesMess);
        //            */
                /*
        //            if (responseDat == "false")
        //            {



        //            }
        //            else
        //            {
        //                MsgInfo msgInfo = JsonSerializer.Deserialize<MsgInfo>(responseDat);

        //                //Encoding Default = Encoding.Default;
        //                //Encoding ascii = Encoding.ASCII;

        //                //string input = "Auspuffanlage \"Century\" f├╝r";
        //                //string output = ascii.GetString(Encoding.Convert(Default, ascii, Default.GetBytes(responseDat)));
        //                //all_Chat = true;
        //                //var a = AClass.Values("IdUserFrom");

        //                //string Answe1 = Answe.ToString();
        //                //string List_Mess1 = List_Mess.ToString();
        //                //string AClass1 = AClass.ToString();
        //                //string[] les = new string[] {AClass1};

        //                //JToken id = AClass.Values("IdUserFrom") as JToken;
        //                //JToken IdUserFrom = AClass.Values("IdUserFrom") as JToken;
        //                //JToken IdUserTo = AClass.Values("IdUserTo") as JToken;
        //                //JToken Message = AClass.Values("Message") as JToken; 
        //                //JToken DataMess = AClass.Values("DataMess") as JToken;
        //                //JToken Mark = AClass.Values("Mark") as JToken;

        //                JObject details = JObject.Parse(responseDat);
        //                JToken Answe = details.SelectToken("Answe");
        //                JToken List_Mess = details.SelectToken("List_Mess");
        //                JToken AClass = details.SelectToken("AClass");
        //                if (Answe.ToString() == "true")
        //                {


        //                    MessСhat[] les = new MessСhat[AClass.Count()];

        //                    for (int i = 0; i < AClass.Count(); i++)
        //                    {
        //                        string yu = AClass[i].ToString();
        //                        MessСhat useTravel = JsonSerializer.Deserialize<MessСhat>(yu);
        //                        les[i] = useTravel;
        //                    }
        //                    //  sender.Rows.Clear();
        //                    sender.RowCount = les.Count();
        //                    sender.ColumnCount = 2;


        //                    allChat = les;
        //                    DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
        //                    {
        //                        //column.HeaderText = ColumnName.OutOfOffice.ToString();
        //                        //column.Name = ColumnName.OutOfOffice.ToString();
        //                        //column.AutoSizeMode =
        //                        //    DataGridViewAutoSizeColumnMode.DisplayedCells;
        //                        //column.FlatStyle = FlatStyle.Standard;
        //                        //column.ThreeState = true;
        //                        //column.CellTemplate = new DataGridViewCheckBoxCell();
        //                        //column.CellTemplate.Style.BackColor = Color.Beige;
        //                    }
                         

        //                        for (int i = 0; i < les.Count(); i++)
        //                    {
        //                        for (int j = 0; j < 1; j++)
        //                        {
        //                            sender.Rows[i].Cells[j].Value = les[i].Message;
        //                            sender.Columns[j].HeaderText = "Сообщения";
        //                            if (les[i].IdUserFrom != Users)
        //                            { sender.Rows[i].Cells[j].Style.ForeColor = Color.Blue; }

        //                        }
        //                        for (int j = 1; j < 2; j++)
        //                        {
        //                            sender.Rows[i].Cells[j].Value = les[i].DataMess;
        //                            sender.Columns[j].HeaderText = "Дата отправки";


        //                        }
        //                        for (int j = 2; j < 3; j++)
        //                        {
        //                            bool aMark = false;
        //                            if (les[i].Mark.ToString() == "1")
        //                            {
        //                                aMark = true;
        //                            }

        //                            //sender.Columns[2].ValueType = typeof(bool);
        //                            //sender.Columns[2].DefaultCellStyle. = 
        //                            sender.Rows[i].Cells[j].Value = aMark;
        //                            //  sender.Columns[j].HeaderText = "";
        //                        }
        //                    }
        //                    sender.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        //                    sender.Visible = true;
        //                }
        //                else
        //                {
        //                    sender.Rows.Clear();
        //                    //MessageBox.Show("Сообщений нет");
        //                }









        //                //JsonObject jsonObj = new JsonObject(JsonSerializer.SerializeToNode<UseTravel>(responseData));
        //                //int k = int.Parse(jsonObj["kill"].ToString())

        //                //string yy = Convert.ToBase64String(data);
        //                //string yy2 = yy.Substring(yy.IndexOf("{"), yy.Length - yy.IndexOf("{") - 1);
        //                //var txt2 = Encoding.Default.GetString(Convert.FromBase64String(str));
        //                //string rr = JsonSerializer.Deserialize<string>(responseDat);
        //                //string response12 = Newtonsoft.Json.JsonConvert.SerializeObject(bytesMess);

        //                //Int32 bytesMess = await stream.ReadAsync(data, 0, 5);
        //                //    responseDat = System.Text.Encoding.Default.GetString(data, 0, bytesMess);
        //                //UseTravel useTravel = JsonSerializer.DeserializeAsync<UseTravel>(data2.);
        //                //UseTravel useTravel = JsonSerializer.Deserialize<UseTravel>(tr);
        //                //MessageBox.Show($"{useTravel}");
        //                //string result = responseDat.Trim(new char[] { '"', '0' });
        //                //    Int32 it = Convert.ToInt32(result);
        //                //    MessСhat[] mChat = new MessСhat[it];
        //                //    Int32 bytesChat1 = await stream.ReadAsync(data, 0, data.Length);
        //                //    result = System.Text.Encoding.Default.GetString(data, 0, bytesChat1);
        //                //    string rez2 = result.Substring(0, result.IndexOf("}"));
        //                //    List<string> tokens = new List<string>(result.Split('}'));
        //                //  useTravel.aClass

        //                //    for (int j = 0; j < tokens.Count - 1; j++)
        //                //    {
        //                //        string tt = tokens[j] + "}";
        //                //        mChat[j] = JsonSerializer.Deserialize<MessСhat>(tt);
        //                //    }
        //                //allChat = mChat;

        //                //   }
        //                //    else
        //                //    { 
        //                //        MessageBox.Show("Сообщений нет");
        //                //    }
        //            }
        //            //stream.Close();
        //            //client.Close();

        //            // stream.Close();
        //            //   client.Close();

        //        }
        //    }
        //    catch (ArgumentNullException e)
        //    {
        //        MessageBox.Show("ArgumentNullException:{0}", e.Message);
        //    }
        //    catch (SocketException e)
        //    {
        //        MessageBox.Show("SocketException: {0}", e.Message);
        //    }
        //    //catch(Exception e)
        //    //{
        //    //    MessageBox.Show(e.Message);
        //    //}

        //}
        //async static void Connectt(String server, string fs, string command  )
        //{
        //    try
        //    {
        //        Int32 port = 9595;
        //        TcpClient client = new TcpClient(server, port);
        //        Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
        //        NetworkStream stream = client.GetStream();
        //        await stream.WriteAsync(data, 0, data.Length);
        //        data = new Byte[99999909];
        //        String responseData = String.Empty;
        //        String responseDat = String.Empty;             
        //        Byte[] data2 = new Byte[99999909];
        //        Int32 bytesMess = await stream.ReadAsync(data2, 0, data2.Length);
        //        responseDat = System.Text.Encoding.Default.GetString(data2, 0, bytesMess);
        //        UseImage useTravel = JsonSerializer.Deserialize<UseImage>(responseDat);

        //       if (useTravel.Bytes != null)
        //       {
        //          MemoryStream ms = new MemoryStream(useTravel.Bytes);
        //          Image returnImage = Image.FromStream(ms);
        //          // returnImage;
        //           Image = returnImage;
        //       }


        //     }
        //    catch (ArgumentNullException e)
        //    {
        //        MessageBox.Show("ArgumentNullException:{0}", e.Message);
        //    }
        //     catch (SocketException e)
        //    {
        //        MessageBox.Show("SocketException: {0}", e.Message);
        //    }
        // }*/

        private void dataGridViewChat_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewChat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (MemoryStream Despons_friend = new MemoryStream()) {
                    CommandCL command = new CommandCL();

                    string FileFS = "";
                    using (MemoryStream fs = new MemoryStream())
                    {
                        Searh_Friends New_Friend = new Searh_Friends(textBox2.Text, Na_me);
                     
                        JsonSerializer.Serialize<Searh_Friends>(fs, New_Friend);
                        FileFS = Encoding.Default.GetString(fs.ToArray());
                     
                    }
                    //command.Connect_Friends(IP_ADRES.Ip_adress, FileFS, "008");
                    Task.Run(async () => await command.Connect_Friends(IP_ADRES.Ip_adress, FileFS, "008")).Wait();

                    var __Friends =  command._Friends;
                    command._Friends = null;
                }
            }
            catch
            {

            }   
            /*
                        //Console.WriteLine("Data has been saved to file");      
            //string serialized = buf.ToString();
        //   JsonSerializer.SerializeToDefaultBytes(fs,);
            // Connect_Friends(IP_ADRES.Ip_adress, FileFS, "008");*/
        }

        // Проццедура отправки 008
        /*
        //async void Connect_Friends(String server, string fs, string command)
        //{
        //    try
        //    {
        //        //Int32 port = 9595;
        //        using (TcpClient client = new TcpClient(server, ConnectSettings.port))
        //        {
        //            Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
        //            NetworkStream stream = client.GetStream();
        //            await stream.WriteAsync(data, 0, data.Length);

        //            String responseDat = String.Empty;

        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                int cnt = 0;
        //                Byte[] locbuffer = new byte[1024];
        //                do
        //                {
        //                    cnt = await stream.ReadAsync(locbuffer, 0, locbuffer.Length);
        //                    ms.Write(locbuffer, 0, cnt);
        //                } while (client.Available > 0);

        //                responseDat = Encoding.Default.GetString(ms.ToArray());
        //            }

        //            /*
        //            data = new Byte[99999909];
        //            Int32 bytesMess = await stream.ReadAsync(data, 0, data.Length);
        //             //   responseDat =System.Text.Encoding.ASCII.GetString(data,0, bytesMess);

        //            //   data =Convert.FromBase64String(responseDat);
        //            //responseDat =System.Text.Encoding.Default.(data);
        //            //responseDat = System.Text.Encoding.Default.GetString(data, 0, bytesMess);
        //            responseDat = System.Text.Encoding.Default.GetString(data, 0, bytesMess);
        //            */
               /*
        //            Searh_Friends searh_Friends = JsonSerializer.Deserialize<Searh_Friends>(responseDat);



        //         //   searh_Friends.Name


        //            /*                 //for (int i = 0; i < searh_Friends.Name i++)
        //                             //{
        //                             //    for (int j = 0; j < 1; j++)
        //                             //    {
        //                             //        // Друзья.Displayed.ToString(Friend[j].Name   as String);        //Rows[i].Cells[j].Value = 
        //                             //        //Друзья.DataGridView.Rows[i].Cells[j].Value= Friend[i].Name;
        //                             //        sender.Rows[i].Cells[j].Value = Friend[i].Name;
        //                             //        //Friend[i].Name = Convert.ToString(dataGridViewUser.Rows[i].Cells[j].Value);
        //                             //    }
        //                             //}*/
                      /*
        //        }
        //    }
        //    catch (ArgumentNullException e)
        //    {
        //        MessageBox.Show("ArgumentNullException:{0}", e.Message);
        //    }
        //    catch (SocketException e)
        //    {
        //        MessageBox.Show("SocketException: {0}", e.Message);
        //    }
        //}*/
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
             
                //Delete_message_make_up(IP_ADRES.Ip_adress, FileFS, "011", dataGridViewChat);
            }
            catch
            {

            }
        }
        // Процедура отправки 011
        /*
        //async void Delete_message_make_up(String server, string fs, string command, DataGridView sender)
        //{
        //    try
        //    {
        //        using (TcpClient client = new TcpClient(server, ConnectSettings.port))
        //        {
        //            Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
        //            NetworkStream stream = client.GetStream();
        //            await stream.WriteAsync(data, 0, data.Length);

        //            //String responseData = String.Empty;
        //            String responseDat = String.Empty;

        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                int cnt = 0;
        //                Byte[] locbuffer = new byte[1024];
        //                do
        //                {
        //                    cnt = await stream.ReadAsync(locbuffer, 0, locbuffer.Length);
        //                    ms.Write(locbuffer, 0, cnt);
        //                } while (client.Available > 0);

        //                responseDat = Encoding.Default.GetString(ms.ToArray());
        //            }

        //            /*
        //            data = new Byte[99999909];
        //            //    //получить перечень сообщений
        //            Byte[] data2 = new Byte[99999909];
        //            Int32 bytesMess = await stream.ReadAsync(data2, 0, data2.Length);
        //            responseDat = System.Text.Encoding.Default.GetString(data2, 0, bytesMess);
        //            */
               /*
        //            MsgInfo msgInfo = JsonSerializer.Deserialize<MsgInfo>(responseDat);

        //            JObject details = JObject.Parse(responseDat);
        //            JToken Answe = details.SelectToken("Answe");
        //            JToken List_Mess = details.SelectToken("List_Mess");
        //            JToken AClass = details.SelectToken("AClass");
        //            if (Answe.ToString() == "true")
        //            {
        //                MessСhat[] les = new MessСhat[AClass.Count()];

        //                for (int i = 0; i < AClass.Count(); i++)
        //                {
        //                    string yu = AClass[i].ToString();
        //                    MessСhat useTravel = JsonSerializer.Deserialize<MessСhat>(yu);
        //                    les[i] = useTravel;
        //                }
        //                sender.Rows.Clear();
        //                sender.RowCount = les.Count();
        //                sender.ColumnCount = 2;


        //                allChat = les;
        //                DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
        //                {
        //                }

        //                sender.Columns.Insert(2, column);

        //                for (int i = 0; i < les.Count(); i++)
        //                {
        //                    for (int j = 0; j < 1; j++)
        //                    {
        //                        sender.Rows[i].Cells[j].Value = les[i].Message;
        //                        if (les[i].IdUserFrom != Users)
        //                        { sender.Rows[i].Cells[j].Style.ForeColor = Color.Blue; }

        //                    }
        //                    for (int j = 1; j < 2; j++)
        //                    {
        //                        sender.Rows[i].Cells[j].Value = les[i].DataMess;
        //                    }
        //                    for (int j = 2; j < 3; j++)
        //                    {
        //                        bool aMark = false;
        //                        if (les[i].Mark.ToString() == "1")
        //                        {
        //                            aMark = true;
        //                        }


        //                        sender.Rows[i].Cells[j].Value = aMark;
        //                    }
        //                }
        //                sender.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        //                sender.Visible = true;
        //            }
        //            else
        //            {
        //                sender.Rows.Clear();
        //                //MessageBox.Show("Сообщений нет");
        //            }
        //        }
        //    }
        //    catch (ArgumentNullException e)
        //    {
        //        MessageBox.Show("ArgumentNullException:{0}", e.Message);
        //    }
        //    catch (SocketException e)
        //    {
        //        MessageBox.Show("SocketException: {0}", e.Message);
        //    }
        //}*/

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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Редактировать запись "+ selectedBiodataId.ToString());
            try
            {      //
                using (MemoryStream Update = new MemoryStream())
                {
                    int selectedrowindexs = dataGridViewChat.SelectedCells[0].RowIndex;
                    MessСhat tt = allChat[selectedrowindexs];
                    textBox1.Text = tt.Message;
                    Update_id = tt.Id;
                    Update_Message = true;
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
                    CommandCL command = new CommandCL();
                    //command.Delete_message_make_up(IP_ADRES.Ip_adress, FileFS, "011");
                    Task.Run(async () => await command.Delete_message_make_up(IP_ADRES.Ip_adress, FileFS, "011")).Wait();

                    if (command.____Answe != null)
                    {
                        if (command.____Answe.ToString() == "true")
                        {
                            MessСhat[] les = new MessСhat[command.____AClass.Count()];

                            for (int i = 0; i < command.____AClass.Count(); i++)
                            {
                                string yu = command.____AClass[i].ToString();
                                MessСhat useTravel = JsonSerializer.Deserialize<MessСhat>(yu);
                                les[i] = useTravel;
                            }
                            dataGridViewChat.Rows.Clear();
                            dataGridViewChat.RowCount = les.Count();
                            dataGridViewChat.ColumnCount = 2;


                            allChat = les;
                            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                            {
                            }

                            dataGridViewChat.Columns.Insert(2, column);

                            for (int i = 0; i < les.Count(); i++)
                            {
                                for (int j = 0; j < 1; j++)
                                {
                                    dataGridViewChat.Rows[i].Cells[j].Value = les[i].Message;
                                    if (les[i].IdUserFrom != Users)
                                    { dataGridViewChat.Rows[i].Cells[j].Style.ForeColor = Color.Blue; }

                                }
                                for (int j = 1; j < 2; j++)
                                {
                                    dataGridViewChat.Rows[i].Cells[j].Value = les[i].DataMess;
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
                            //MessageBox.Show("Сообщений нет");
                        }

                        command.____Answe = null;
                        command.____List_Mess_count = null;
                        command.____AClass = null;


                        //   Delete_message_make_up(IP_ADRES.Ip_adress, FileFS, "011", dataGridViewChat);}
                    }
                }
            }
            catch
            {

            }
        }

        private void dataGridViewUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridViewChat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

         private void timer1_Tick(object sender, EventArgs e)
         {
            try
            {
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

        async private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (toolStripLabel1.Text != "")
                {

                    using (TcpClient client = new TcpClient(IP_ADRES.Ip_adress, ConnectSettings.port))
                    {
                        NetworkStream stream = client.GetStream();

                        //int selectedrowindex = dataGridViewUser.SelectedCells[0].RowIndex;
                        //User_photo tt = Friend[selectedrowindex];
                        User_photo tt = new User_photo("","","",0,0, Users);
                        //tt.Current = Users;

                        string person = JsonSerializer.Serialize<User_photo>(tt);

                        Byte[] data = System.Text.Encoding.Default.GetBytes("013" + person);
                        await stream.WriteAsync(data, 0, data.Length);

                        //String responseData = String.Empty;
                        String responseDat = String.Empty;


                        //User_photo_Travel
                        //Int32 bytesFriend = await stream.ReadAsync(data, 0, data.Length);

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
                        for (int i = 0; i < Friend.Count(); i++)
                        {
                            A[i] = msgFriends.AClass[i];
                        }
                        Friend = A;

                        /*    //responseDat = System.Text.Encoding.Default.GetString(data, 0, bytesFriend);
                            //User_photo[] people = null;
                            // Друзья.Displayed.ToString(Friend[j].Name   as String);        //Rows[i].Cells[j].Value = 
                            //Друзья.DataGridView.Rows[i].Cells[j].Value= Friend[i].Name;*/
                        if (msgFriends.Answe == "true")
                        {
                            //}   
                            try
                            {
                                if (msgFriends.AClass == null)
                                {
                                }
                                else
                                {
                                    //dataGridViewUser.RowCount = msgFriends.AClass.Count();
                                    //dataGridViewUser.ColumnCount = 1;
                                    dataGridViewUser.RowCount = msgFriends.AClass.Count();
                                    dataGridViewUser.ColumnCount = 1;
                                    for (int i = 0; i < msgFriends.AClass.Count(); i++)
                                    {
                                        for (int j = 0; j < 1; j++)
                                        {
                                            dataGridViewUser.Rows[i].Cells[j].Value = msgFriends.AClass[i].Name;
                                            //Friend[i].Name = Convert.ToString(dataGridViewUser.Rows[i].Cells[j].Value);
                                        }
                                    }
                                    //  dataGridViewUser.Columns[0].HeaderText = "Друзья";
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
  /*                          //string result = responseDat.Trim(new char[] { '"', '0' });
                            //Int32 it = Convert.ToInt32(result);
                            //people = new User_photo[it];
                            //// int i =0;
                            //Int32 bytesFriend1 = await stream.ReadAsync(data, 0, data.Length);
                            ////Друзья
                            //result = System.Text.Encoding.Default.GetString(data, 0, bytesFriend1);
                            //string rez2 = result.Substring(0, result.IndexOf("}"));
                            ////string rez2 = result.Insert(result.IndexOf("}") + 1, ",");
                            //List<string> tokens = new List<string>(result.Split('}'));

                            //for (int j = 0; j < tokens.Count - 1; j++)
                            //{
                            //    string tt2 = tokens[j] + "}";
                            //    people[j] = JsonSerializer.Deserialize<User_photo>(tt2);



                                Users = .Id*/
                            ;
                        }
                        //dataGridViewChat.Rows.Clear();


                    }
                }
            }
            catch
            {

            }
        }
    }
}

