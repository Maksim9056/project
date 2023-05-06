using Class_chat;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_chat
{
    internal class CommandCL
    {

        // Процедура отправки регистрации пользователей 002
        //async void Reg_User(String server, string fs, string command, string user, Form userpass)
        //{
        //    try
        //    {


        //        using (TcpClient client = new TcpClient(server, ConnectSettings.port))
        //        {
        //            Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
        //            NetworkStream stream = client.GetStream();

        //            await stream.WriteAsync(data, 0, data.Length);

        //            String responseData = String.Empty;

        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                int cnt = 0;
        //                Byte[] locbuffer = new byte[1024];
        //                do
        //                {
        //                    cnt = await stream.ReadAsync(locbuffer, 0, locbuffer.Length);
        //                    ms.Write(locbuffer, 0, cnt);
        //                } while (client.Available > 0);

        //                responseData = Encoding.Default.GetString(ms.ToArray());
        //            }

        //            /*
        //            data = new Byte[999999990];
        //            Int32 bytess = await stream.ReadAsync(data, 0, data.Length);
        //            responseData = System.Text.Encoding.Default.GetString(data, 0, bytess);
        //            */

        //            if (responseData == user)
        //            {
        //                MessageBox.Show("Пользователь уже есть");
        //            }
        //            else
        //            {
        //                MessageBox.Show("Добавление пользователя разрешено");
        //                //using (Password_Users a = new Password_Users())
        //                //{
        //                //Chats_main parent = (Chats_main)this.Owner;
        //                //parent.Show();
        //                userpass.Close();

        //                //a.ShowDialog();
        //                //}
        //            }
        //        }
        //    }

        //    catch (ArgumentNullException e)
        //    {
        //        Console.WriteLine("ArgumentNullException:{0}", e.Message);
        //    }
        //    catch (SocketException e)
        //    {
        //        Console.WriteLine("SocketException: {0}", e.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("SocketException: {0}", e.Message);
        //    }

        //}




        // Передача 003
        //async void Check_User_Possword(String server, string fs, string command, string user, Form userpass)
        //{
        //    using (TcpClient client = new TcpClient(server, ConnectSettings.port))
        //    {
        //        try
        //        {

        //            byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
        //            string Host = System.Net.Dns.GetHostName();
        //            NetworkStream stream = client.GetStream();
        //            await stream.WriteAsync(data, 0, data.Length);
        //            //data = new Byte[99999999];
        //            String responseData = String.Empty;
        //            //String responseDat = String.Empty;                    
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                int cnt = 0;
        //                Byte[] locbuffer = new byte[1024];
        //                do
        //                {
        //                    cnt = await stream.ReadAsync(locbuffer, 0, locbuffer.Length);
        //                    ms.Write(locbuffer, 0, cnt);
        //                } while (client.Available > 0);
        //                responseData = Encoding.Default.GetString(ms.ToArray());
        //                Clas(responseData);

        //            }
        //            if (responseData == "false")
        //            {
        //            }
        //            else
        //            {
        //                MsgUser_Logins person3 = JsonSerializer.Deserialize<MsgUser_Logins>(responseData);
        //                //        responseData
        //                Chats_main a = new Chats_main();
        //                Chats_main parent = (Chats_main)this.Owner;
        //                parent.NotifyMe(person3);
        //                parent.SaveConfig(ConnectSettings.port, IP_ADRES.Ip_adress, person3.User_.Name);
        //                userpass.Close();
        //                /*
        //                //using (MemoryStream ms = new MemoryStream())
        //                //{
        //                //    int cnt = 0;
        //                //    Byte[] locbuffer = new byte[1024];
        //                //    do
        //                //    {
        //                //        cnt = await stream.ReadAsync(locbuffer, 0, locbuffer.Length);
        //                //        ms.Write(locbuffer, 0, cnt);
        //                //    } while (client.Available > 0);

        //                //    responseDat = Encoding.Default.GetString(ms.ToArray());
        //                //}          


        //                //if (responseData == "false")
        //                //{



        //                //}
        //                //else
        //                //{
        //                //  User_Logins msgFriends = JsonSerializer.Deserialize<User_Logins>(responseDat);
        //                /*                    //Int32 bytess = await stream.ReadAsync(data, 0, data.Length);



        //                                    //if (person3.Name == user)
        //                                    //{
        //                                    //    //MessageBox.Show("Подключение пользователя разрешено");
        //                                    //    //получить перечень друзей
        //                                    //    Int32 bytesFriend = await stream.ReadAsync(data, 0, 5);
        //                                    //    responseDat = System.Text.Encoding.Default.GetString(data, 0, bytesFriend);
        //                                    //    User_photo[] people = null;
        //                                    //    if (responseDat == "false")
        //                                    //    {



        //                                    //    }
        //                                    //    else
        //                                    //    {
        //                                    //        string result = responseDat.Trim(new char[] { '"', '0' });
        //                                    //        Int32 it = Convert.ToInt32(result);
        //                                    //        people = new User_photo[it];

        //                                    //        Int32 bytesFriend1 = await stream.ReadAsync(data, 0, data.Length);
        //                                    //        //Друзья
        //                                    //        result = System.Text.Encoding.Default.GetString(data, 0, bytesFriend1);
        //                                    //        string rez2 = result.Substring(0, result.IndexOf("}"));

        //                                    //        List<string> tokens = new List<string>(result.Split('}'));

        //                                    //        for (int j = 0; j < tokens.Count - 1; j++)
        //                                    //        {
        //                                    //            string tt = tokens[j] + "}";
        //                                    //            people[j] = JsonSerializer.Deserialize<User_photo>(tt);
        //                                    //        }
        //                                    //    }*/
        //                //a.Show();
        //                /*
        //                //a.OpenMes(person3.User_, person3);
        //                //userpass.Hide();
        //                //}
        //                //else
        //                //{
        //                //    MessageBox.Show("Пользователя нет");
        //                //}
        //                */
        //            }
        //        }
        //        catch (ArgumentNullException)
        //        {
        //            //Console.WriteLine("ArgumentNullException:{0}", e.Message);
        //        }
        //        catch (SocketException)
        //        {
        //            //Console.WriteLine("SocketException: {0}", e.Message);
        //        }
        //        catch (Exception)
        //        {
        //            //   Console.WriteLine("SocketException: {0}", e.Message);
        //        }

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

        //                    sender.Columns.Insert(2, column);

        //                    for (int i = 0; i < les.Count(); i++)
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

        // Проццедура отправки 008
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

        //            Searh_Friends searh_Friends = JsonSerializer.Deserialize<Searh_Friends>(responseDat);



        //            //   searh_Friends.Name


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


        //}




        //// Процедура отправки 009
        //async void Insert_Message(String server, string fs, string command, DataGridView sender)
        //{
        //    try
        //    {
        //        //Int32 port = 9595;

        //        using (TcpClient client = new TcpClient(server, ConnectSettings.port))
        //        {
        //            Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
        //            NetworkStream stream = client.GetStream();
        //            await stream.WriteAsync(data, 0, data.Length);

        //            /*
        //            //TcpClient.Available
        //            //// буфер ддя получения данных
        //            //var responseData_New = new byte[1024];
        //            //// StringBuilder для склеивания полученных данных в одну строку
        //            //var response = new StringBuilder();
        //            //int bytes;  // количество полученных байтов
        //            //do
        //            //{
        //            //    // получаем данные
        //            //    bytes = await stream.ReadAsync(responseData_New, 0, responseData_New.Length);
        //            //    // преобразуем в строку и добавляем ее в StringBuilder
        //            //    response.Append(Encoding.Default.GetString(responseData_New, 0, bytes));
        //            //}
        //            //while (bytes > 0); // пока данные есть в потоке 

        //            //Func_Read(stream,1024, client);

        //            //String responseData = String.Empty;
        //            */
        //            String responseDat = String.Empty;

        //            //responseDat = Respons;
        //            /* работает 
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
        //            */

        //            responseDat = await Task<string>.Run(() =>
        //            {
        //                return Func_Read(stream, data.Length, client);
        //            });
        //            /*
        //             * 
        //                               //data = new Byte[99999909];


        //                               //Byte[] data2 = new Byte[client.ReceiveBufferSize];

        //                               //Int32 bytesMess = await stream.ReadAsync(data2, 0, data2.Length);

        //                               //responseDat = System.Text.Encoding.Default.GetString(data2, 0, bytesMess);*/

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
        //                allChat = les;
        //                sender.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //                sender.Visible = true;
        //            }
        //            else
        //            {
        //                sender.Rows.Clear();
        //            }
        //        }
        //        //stream.Close();
        //        //client.Close();

        //    }
        //    catch (ArgumentNullException e)
        //    {
        //        MessageBox.Show("ArgumentNullException:{0}", e.Message);
        //    }
        //    catch (SocketException e)
        //    {
        //        MessageBox.Show("SocketException: {0}", e.Message);
        //    }

        //}



        //// Процедура отправки 010
        //async void Update_Message_make_up(String server, string fs, string command, DataGridView sender)
        //{
        //    try
        //    {
        //        //Int32 port = 9595;

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
        //            Byte[] data2 = new Byte[99999909];
        //            Int32 bytesMess = await stream.ReadAsync(data2, 0, data2.Length);
        //            responseDat = System.Text.Encoding.Default.GetString(data2, 0, bytesMess);
        //            */

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
        //                DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
        //                {
        //                }

        //                sender.Columns.Insert(2, column);
        //                //sender.Columns[0].HeaderText = "Друзья";

        //                for (int i = 0; i < les.Count(); i++)
        //                {
        //                    for (int j = 0; j < 1; j++)
        //                    {
        //                        sender.Rows[i].Cells[j].Value = les[i].Message;
        //                        if (les[i].IdUserFrom != Users)
        //                        {
        //                            sender.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
        //                        }

        //                    }
        //                    for (int k = 1; k < 2; k++)
        //                    {
        //                        sender.Rows[i].Cells[k].Value = les[i].DataMess;
        //                    }
        //                    for (int b = 2; b < 3; b++)
        //                    {
        //                        bool aMark = false;
        //                        if (les[i].Mark.ToString() == "1")
        //                        {
        //                            aMark = true;
        //                        }
        //                        sender.Rows[i].Cells[b].Value = aMark;
        //                    }
        //                }

        //                sender.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //                allChat = les;
        //                sender.Visible = true;
        //            }
        //            else
        //            {
        //                sender.Rows.Clear();
        //                //MessageBox.Show("Сообщений нет");
        //            }
        //        }
        //        //stream.Close();
        //        //client.Close();
        //    }
        //    catch (ArgumentNullException e)
        //    {
        //        MessageBox.Show("ArgumentNullException:{0}", e.Message);
        //    }
        //    catch (SocketException e)
        //    {
        //        MessageBox.Show("SocketException: {0}", e.Message);
        //    }
        //}


        // Процедура отправки 011
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
        //}



    }
}
