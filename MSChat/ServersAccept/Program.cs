using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Class_chat;
using System.Text.Json;
using System.IO;
using System.Text;
//using System.Net.NetworkInformation;
//using static System.Net.WebRequestMethods;
//using System.IO.Pipes;
//using static System.Net.Mime.MediaTypeNames;


namespace ServersAccept
{
    internal class Program
    {
        public bool Users = false;
        static void Main(string[] args)
        {
            GlobalClass globalClass= new GlobalClass();
            globalClass.CreateTable_Users();
            globalClass.CreateTable_Friends();
            globalClass.CreateTable_Chat();
            globalClass.CreateTable_Files();
            
            TcpListener server = null;
            try
            {
                int MaxThreadsCount = Environment.ProcessorCount;
                ThreadPool.SetMaxThreads(MaxThreadsCount, MaxThreadsCount);
               
                //Int32 port = 9595;

                IPAddress localAddr = IPAddress.Parse("192.168.0.113");//127.0.0.1
                int counter = 0;
                Console.WriteLine();
                server = new TcpListener(localAddr, ConnectSettings.port);
                Console.WriteLine("Конфигурация многопоточного сервера:" + MaxThreadsCount.ToString());
                Console.WriteLine($"Ip-адрес: {localAddr}");//127.0.0.1 System.Net.Sockets.AddressFamily family
                                                            //      string Host = System.Net.Dns.GetHostName();
                /*for (int i = 0; i < System.Net.Dns.GetHostByName(Host).AddressList.Length;i++)
                //{
                //     string  IP = System.Net.Dns.GetHostByName(Host).AddressList[i].ToString();
                //    Console.WriteLine(IP);
                //}*/
                Console.WriteLine("\nСервер запушен");
                server.Start();
                while (true)
                {
                    Console.WriteLine("\nОжидание соединения...");
                    Thread.Sleep(10);
                    //      Thread.MemoryBarrier();
                    ThreadPool.QueueUserWorkItem(ClientProcessing, server.AcceptTcpClient());
                    counter++;
                    Console.Write("\nСоединие№" + counter.ToString() + "!");
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException:{0}", e.Message);
            }
            Console.WriteLine("\nНажмите Enter");
            Console.Read();
        }

        async static void ClientProcessing(object client_obj)
        {
            try
            {
                Byte[] bytes = new Byte[99999999];
                String data;
                using (TcpClient client = client_obj as TcpClient)
                {
                    GlobalClass globalClass = new GlobalClass();

                    NetworkStream stream = client.GetStream();
                    
                    Command command = new Command();
                    
                    int i;
                    while ((i = await stream.ReadAsync(bytes, 0, bytes.Length)) != 0)
                    {
                        //data = System.Text.Encoding.Default.GetString(bytes, 0, i);
                        data = Encoding.Default.GetString(bytes, 0, i);
                        /*       /*string[] words = data.Split(new char[] { ';' }); Encoding.Default
                               //name = words[0];
                               //pass = words[1];*/
                        string comand = data.Substring(0, 3);
                        string json = data.Substring(3, data.Length - 3);

                        byte[] msg = System.Text.Encoding.Default.GetBytes(json);
                        DateTime dateTime = DateTime.Now;
                        switch (comand)
                        {
                            case "001":
                                /* UserLogin person = JsonSerializer.Deserialize<UserLogin>(msg);
                                //Console.WriteLine(person.Name + " " + person.Pass + "\n");
                                // GlobalClass.Insert_User(person.Name, person.Pass, dateTime);*/
                                break;
                            case "002": //Регистрация
                                User_regis person2 = JsonSerializer.Deserialize<User_regis>(msg);
                                globalClass.Insert_Image(person2.Photo);
                                globalClass.Insert_User(person2.Name, person2.Pass, person2.Age, dateTime);

                                if (globalClass.User_Insert == true)
                                {
                                    byte[] msgAnswe = System.Text.Encoding.Default.GetBytes(person2.Name);
                                    stream.Write(msgAnswe, 0, msgAnswe.Length);
                                }
                                else
                                {
                                    byte[] msgAnswe = System.Text.Encoding.Default.GetBytes("false");
                                    stream.Write(msgAnswe, 0, msgAnswe.Length);
                                }
                                break;
                            case "003": //Проверка логина и пароля
                                UserLogin person3 = JsonSerializer.Deserialize<UserLogin>(msg);
                                globalClass.Select_Users(person3.Name, person3.Pass);
                                if (globalClass.UserConnect == true)
                                {
                                    using (MemoryStream ms = new MemoryStream())
                                    {

                                        JsonSerializer.Serialize<User_photo>(ms, globalClass.AUser);
                                        //char[] bytes_ms = new char[ms.Length];
                                        //  byte[] msgAnswe = ms.ToArray();
                                        stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                                    }
                                    await stream.FlushAsync();

                                    globalClass.Select_Friend(globalClass.Current_User);

                                    User_photo AUser;                                //Обработать ситуацию когда нет друзей


                                    if (globalClass.Friends == false)
                                    {
                                        byte[] msgAnswe = System.Text.Encoding.Default.GetBytes("false");
                                        stream.Write(msgAnswe, 0, msgAnswe.Length);
                                    }
                                    else
                                    {
                                        using (MemoryStream ms_count = new MemoryStream())
                                        {

                                            var Countuser = globalClass.List_Friend.Length;
                                            JsonSerializer.Serialize(ms_count, Countuser.ToString().PadLeft(3, '0'));
                                            //   byte[] ms_count_Answe = ms_count.ToArray();
                                            stream.Write(ms_count.ToArray(), 0, ms_count.ToArray().Length);
                                        }
                                        //stream.Flush();
                                        var options = new JsonSerializerOptions
                                        {
                                            AllowTrailingCommas = true
                                        };
                                        //что то не очень
                                        int tt = globalClass.List_Friend.Length;
                                        for (int k = 0; k < tt; k++)
                                        {
                                            using (MemoryStream ms_Friend_Answe = new MemoryStream())
                                            {

                                                AUser = globalClass.List_Friend[k];

                                                JsonSerializer.Serialize<User_photo>(ms_Friend_Answe, AUser, options);
                                                stream.Write(ms_Friend_Answe.ToArray(), 0, ms_Friend_Answe.ToArray().Length);
                                            }
                                        }


                                    }
                                }
                                else
                                {
                                    byte[] msgAnswe = System.Text.Encoding.Default.GetBytes("false");
                                    stream.Write(msgAnswe, 0, msgAnswe.Length);
                                }
                                break;
                            case "004": //выборка друзей пользователя
                                UserLogin person4 = JsonSerializer.Deserialize<UserLogin>(msg);
                                globalClass.Select_From_Users(person4.Name);
                                if (globalClass.User_Select_Chats == true)
                                {
                                    byte[] msgAnswe = System.Text.Encoding.Default.GetBytes(person4.Name);
                                    stream.Write(msgAnswe, 0, msgAnswe.Length);
                                }
                                else
                                {
                                    byte[] msgAnswe = System.Text.Encoding.Default.GetBytes("false");
                                    stream.Write(msgAnswe, 0, msgAnswe.Length);
                                }

                                break;
                            case "005": //выборка сообщений переписки с другом пользователя
                                UserLogin person5 = JsonSerializer.Deserialize<UserLogin>(msg);

                                globalClass.Select_From_Users(person5.Name, person5.Pass);

                                if (globalClass.User_Select_Chats == true)
                                {

                                    byte[] msgAnswe = System.Text.Encoding.Default.GetBytes(person5.Name);
                                    stream.Write(msgAnswe, 0, msgAnswe.Length);
                                }
                                else
                                {
                                    byte[] msgAnswe = System.Text.Encoding.Default.GetBytes("false");
                                    stream.Write(msgAnswe, 0, msgAnswe.Length);
                                }

                                break;
                            case "006": //Проверяет сообщение для друга

                                User_photo person6 = JsonSerializer.Deserialize<User_photo>(msg);

                                globalClass.Select_Message_Users(person6);
                                // MessСhat AChat;

                                var options1 = new JsonSerializerOptions
                                {
                                    AllowTrailingCommas = true
                                };
                                //string FileFS = "";
                                if (globalClass.Mess_Chats == true)
                                {

                                    //     string[] aKey = new string[] { "Answe", "Count", "aClass" };
                                    //    MessСhat[] jsonString = new MessСhat[GlobalClass.List_Mess.Length];
                                    MessСhat[] jsonString = new MessСhat[globalClass.aChatss.Length];
                                    //        string a = "";  
                                    for (int k = 0; k < globalClass.aChatss.Length; k++)
                                    {
                                        jsonString[k] = globalClass.aChatss[k];
                                    }

                                    UseTravel useTravel = new UseTravel("true", jsonString.Length, jsonString);

                                    using (MemoryStream fsS1 = new MemoryStream())
                                    {
                                        JsonSerializer.Serialize<UseTravel>(fsS1, useTravel);
                                        stream.Write(fsS1.ToArray(), 0, fsS1.ToArray().Length);
                                    }

                                }
                                else
                                {
                                    //  string aFalse = "{Answe: \"false\"}";
                                    //   byte[] msgAnswe = System.Text.Encoding.Default.GetBytes(aFalse);
                                    byte[] msgAnswe = System.Text.Encoding.Default.GetBytes("false");
                                    stream.Write(msgAnswe, 0, msgAnswe.Length);
                                  //  stream.Write(msgAnswe, 0, msgAnswe.Length);
                                }

                                break;
                            case "007":
                                //Use_Photo use_Photo = JsonSerializer.Deserialize<Use_Photo>(msg);
                                //GlobalClass.Select_Image_Userss(use_Photo);

                                //UseImage useTravels = GlobalClass.Use_image;

                                //MemoryStream fsS = new MemoryStream();
                                //using (MemoryStream fs = new MemoryStream())
                                //{
                                //    JsonSerializer.Serialize<UseImage>(fsS, useTravels);
                                //}

                                //stream.Write(fsS.ToArray(), 0, fsS.ToArray().Length);
                                break;
                            case "008":

                                Searh_Friends searh_Friends = JsonSerializer.Deserialize<Searh_Friends>(msg);

                                globalClass.Searh_Users(searh_Friends);

                                if (globalClass._Searh_Freind == true)
                                {
                                    Searh_Friends searh = new Searh_Friends(globalClass.Searh_Friend, searh_Friends.User);
                                    using (MemoryStream Byte_frends = new MemoryStream())
                                    {
                                        JsonSerializer.Serialize<Searh_Friends>(Byte_frends, searh);
                                        stream.Write(Byte_frends.ToArray(), 0, Byte_frends.ToArray().Length);
                                    }
                                }
                                else
                                {

                                }
                                break;
                            case "009"://Добавление сообщения с обновлением
                                MessСhat Insert_Message = JsonSerializer.Deserialize<MessСhat>(msg);
                                globalClass.Insert_Message(Insert_Message);


                                MessСhat[] json_Message = new MessСhat[globalClass.Frends_Chat_Wath.Length];
                                //        string a = "";  
                                for (int k = 0; k < globalClass.Frends_Chat_Wath.Length; k++)
                                {
                                    json_Message[k] = globalClass.Frends_Chat_Wath[k];
                                }
                                UseTravel useTravels = new UseTravel("true", json_Message.Length, json_Message);
                                using (MemoryStream Friens_Byte = new MemoryStream())
                                {
                                    JsonSerializer.Serialize<UseTravel>(Friens_Byte, useTravels);
                                    stream.Write(Friens_Byte.ToArray(), 0, Friens_Byte.ToArray().Length);
                                }
                                break;
                            case "010":


                                MessСhat Update_Message = JsonSerializer.Deserialize<MessСhat>(msg);
                                globalClass.Update_Message(Update_Message);
                                MessСhat[] json_Message1 = new MessСhat[globalClass.Frends_Chat_Wath.Length];
                                //        string a = "";  
                                for (int k = 0; k < globalClass.Frends_Chat_Wath.Length; k++)
                                {
                                    json_Message1[k] = globalClass.Frends_Chat_Wath[k];
                                }
                                UseTravel useTravels2 = new UseTravel("true", json_Message1.Length, json_Message1);
                                using (MemoryStream Friens_Byte1 = new MemoryStream())
                                {
                                    JsonSerializer.Serialize<UseTravel>(Friens_Byte1, useTravels2);
                                    stream.Write(Friens_Byte1.ToArray(), 0, Friens_Byte1.ToArray().Length);
                                }
                                break;
                            case "011":

                                command.Delete_Message(msg, globalClass, stream);

                                /*
                                //MessСhat Delete_Message = JsonSerializer.Deserialize<MessСhat>(msg);
                                //globalClass.Delete_Message_make_up(Delete_Message);
                                //MessСhat[] json_Update_delete = new MessСhat[globalClass.Frends_Chat_Wath.Length];
                                ////        string a = "";  
                                //for (int k = 0; k < globalClass.Frends_Chat_Wath.Length; k++)
                                //{
                                //    json_Update_delete[k] = globalClass.Frends_Chat_Wath[k];
                                //}
                                //UseTravel Update_chats_make_up_after_delete = new UseTravel("true", json_Update_delete.Length, json_Update_delete);
                                //using (MemoryStream ms = new MemoryStream())
                                //{
                                //    JsonSerializer.Serialize<UseTravel>(ms, Update_chats_make_up_after_delete);
                                //    stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                                //}
                                */
                                break;
                            case "012": //получение списка сообщений (обновление)
                                User_photo user_Select = JsonSerializer.Deserialize<User_photo>(msg);

                                globalClass.Select_Friend(user_Select.Current.ToString());
                                User_photo AUsers;
                                if (globalClass.Friends == false)
                                {
                                    byte[] msgAnswe = System.Text.Encoding.Default.GetBytes("false");
                                    stream.Write(msgAnswe, 0, msgAnswe.Length);
                                }
                                else
                                {
                                    using (MemoryStream ms_count = new MemoryStream())
                                    {

                                        var Countuser = globalClass.List_Friend.Length;
                                        JsonSerializer.Serialize(ms_count, Countuser.ToString().PadLeft(3, '0'));
                                        //   byte[] ms_count_Answe = ms_count.ToArray();
                                        stream.Write(ms_count.ToArray(), 0, ms_count.ToArray().Length);
                                    }
                                    //stream.Flush();
                                    var options = new JsonSerializerOptions
                                    {
                                        AllowTrailingCommas = true
                                    };
                                    //что то не очень
                                    int tt = globalClass.List_Friend.Length;
                                    for (int k = 0; k < tt; k++)
                                    {
                                        using (MemoryStream ms_Friend_Answe = new MemoryStream())
                                        {

                                            AUsers = globalClass.List_Friend[k];

                                            JsonSerializer.Serialize<User_photo>(ms_Friend_Answe, AUsers, options);
                                            stream.Write(ms_Friend_Answe.ToArray(), 0, ms_Friend_Answe.ToArray().Length);
                                        }
                                    }


                                }   
                                break;
                                case "013": //получение списка друзей (обновление)

                                
                                command.List_Friens(msg, globalClass, stream);

                                /*
                                User_photo Select_list_Friends = JsonSerializer.Deserialize<User_photo>(msg);

                                globalClass.Select_Friend(Select_list_Friends.Current.ToString());

                                User_photo[] json_List_Friends = new User_photo[globalClass.List_Friend.Length];

                                for (int k = 0; k < globalClass.List_Friend.Length; k++)
                                {
                                    json_List_Friends[k] = globalClass.List_Friend[k];
                                }

                                using (MemoryStream ms = new MemoryStream())
                                {
                                    User_photo_Travel json_List_Friends_after = new User_photo_Travel("true", json_List_Friends.Length, json_List_Friends);
                                    JsonSerializer.Serialize<User_photo_Travel>(ms, json_List_Friends_after);
                                    stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                                }
                                */

                                //var options_Friends = new JsonSerializerOptions
                                //{
                                //    AllowTrailingCommas = true
                                //};
                                ////что то не очень
                                //int tt_Friends = globalClass.List_Friend.Length;
                                //for (int k = 0; k < tt_Friends; k++)
                                //{
                                //    using (MemoryStream ms_Friend_Answe = new MemoryStream())
                                //    {

                                //        AUser_Friends = globalClass.List_Friend[k];

                                //        JsonSerializer.Serialize<User_photo>(ms_Friend_Answe, AUser_Friends, options_Friends);
                                //        stream.Write(ms_Friend_Answe.ToArray(), 0, ms_Friend_Answe.ToArray().Length);
                                //    }
                                //}

                                break;
                                //globalClass.Select_Message_Users(user_Select);
                                ////string FileFS = "";
                                //if (globalClass.Mess_Chats == true)
                                //{

                                //    //     string[] aKey = new string[] { "Answe", "Count", "aClass" };
                                //    //    MessСhat[] jsonString = new MessСhat[GlobalClass.List_Mess.Length];
                                //    MessСhat[] jsonString = new MessСhat[globalClass.aChatss.Length];
                                //    //        string a = "";  
                                //    for (int k = 0; k < globalClass.aChatss.Length; k++)
                                //    {
                                //        jsonString[k] = globalClass.aChatss[k];
                                //    }


                                //    using (MemoryStream fsS1 = new MemoryStream())
                                //    {                                  
                                //        UseTravel useTravel = new UseTravel("true", jsonString.Length, jsonString);
                                //        JsonSerializer.Serialize<UseTravel>(fsS1, useTravel);
                                //        stream.Write(fsS1.ToArray(), 0, fsS1.ToArray().Length);
                                //    }

                                //}
                                //else
                                //{
                                //    string aFalse = "{Answe: \"false\"}";
                                //    byte[] msgAnswe = System.Text.Encoding.Default.GetBytes(aFalse);
                                //    stream.Write(msgAnswe, 0, msgAnswe.Length);
                                //}






                                /*int FileNameLenght = data.Length;
                                ////int FileBytesCount = FileBytes.Length;
                                //int messageLenght = sizeof(int) * 2 +  FileNameLenght;
                                //var messageData = new byte[messageLenght];
                                //using (var streamtt = new MemoryStream(messageData))
                                //{
                                //    var writer = new BinaryWriter(streamtt);

                                //    //using (FileStream fs = new FileStream(data, FileMode.Open))
                                //   // {
                                //       // UserLogin USLogin = JsonSerializer.Deserialize<UserLogin>(messageData);
                                //   // }
                                //}


                                //await stream.WriteAsync(msg, 0, msg.Length);
                                //byte[] msgAnswe = System.Text.Encoding.Default.GetBytes("true");

                                //stream.Write(msgAnswe, 0, msgAnswe.Length);
                                //ClientProcessin(); 
                                //Console.WriteLine(name+" "+pass + "\n");
                                //Console.WriteLine(data);
                                //DateTime dateTime = DateTime.Now;

                                // Users = false;
                                //ClientProcessing(name, pass);
                                //if (Users == true)
                                //{

                                //}
                                //else
                                //{     
                                // GlobalClass.Insert_User(name, pass, dateTime);
                                //}*/

                        }
                    }
                    //stream.Close();
                    //client.Close();
                    //stream.Close();
           
                    // ClientProcessin();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
     
        }
    } 
}

        /*async static void ClientProcessin()
        //{
        //    string sq = $"Select * FROM Users ";
        //    using (var connection = new SqliteConnection(GlobalClass.connectionString))
        //    {
        //        await connection.OpenAsync();
        //        SqliteCommand command = new SqliteCommand(sq, connection);
        //        using (SqliteDataReader reader = command.ExecuteReader())
        //        {
        //            if (reader.HasRows) // если есть данные
        //            {
        //                while (reader.Read())   // построчно считываем данные
        //                {
        //                    int Id = reader.GetInt32(0);


        //                   bool Mark = reader.GetBoolean(6);

        //                  // byte[] data = (byte[])reader.GetByte(1);
        //                    string Messeg =reader.GetString(4);
        //                   // int  Mark.reader.GetInt32(6);
        //                   DateTime DataMess =reader.GetDateTime(5);
        //                   Console.WriteLine(Id +"\n");
        //                   Console.WriteLine(Messeg+"\n");
        //                   Console.WriteLine(Mark + "\n");
        //                   Console.WriteLine(DataMess + "\n");
        //                    // pictureBox1.Image = System.Drawing.Image.FromStream(new MemoryStream(data)); ;

        //                    // pictureBox1.BorderStyle = BorderStyle.None;
        //                }

        //            }

        //        }
        //    }
        //}
        //async static void ClientProcessing(string name,string password)
        //{ 
        //    try
        //    {
        //        string sq = $"Select * FROM Users WHERE  Name = '{name}' ,Password = '{password}' ";
        //        using (var connection = new SqliteConnection(GlobalClass.connectionString))
        //        {
        //            await connection.OpenAsync();
        //            SqliteCommand command = new SqliteCommand(sq, connection);
        //            Users = true;
        //        }
             
        //    }
        //    catch(Exception )
        //    {
        //        //    Console.WriteLine(ex.Message);
        //        Users = false;
        //    }
        //}*/
     

