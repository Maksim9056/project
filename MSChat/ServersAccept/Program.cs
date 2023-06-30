using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Class_chat;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


namespace ServersAccept
{   
    internal class Program
    {
        public bool Users = false;
        static public string Ip_Adress { get; set; }
        static public Int32 port { get; set; }
        //[Obsolete]
      public static string data_ { get; set; }

    
        static void Main(string[] args)
        {
            GlobalClass.TypeSQL = 2;
            GlobalClass globalClass = new GlobalClass();
            globalClass.CreateTable_Files();
            globalClass.CreateTable_Users();
            globalClass.CreateTable_Friends();
            globalClass.CreateTable_Chat();
            TcpListener server = null;
            try
            {
                int MaxThreadsCount = Environment.ProcessorCount;
                ThreadPool.SetMaxThreads(MaxThreadsCount, MaxThreadsCount);
                SaveOpen();
                IPAddress localAddr = IPAddress.Parse(Ip_Adress);
                Console.WriteLine();
                server = new TcpListener(localAddr,port);
                Console.WriteLine("Конфигурация многопоточного сервера:" + MaxThreadsCount.ToString());
                Console.WriteLine("Пользователь:" + Environment.UserName.ToString());
                Console.WriteLine("IP-адрес :" + Ip_Adress.ToString());
                Console.WriteLine("Путь:" + Environment.CurrentDirectory.ToString());
                //Console.WriteLine(Environment.MachineName);
                //Console.WriteLine(Environment.WorkingSet);
                /*   //  Console.WriteLine(Environment.WorkingSet);
                 */
                /* IPHostEntry ipEntry = Dns.GetHostByName(Dns.GetHostName());
            foreach (var a in ipEntry.AddressList) {
                Console.WriteLine(a);

            }*/
                /*   //string Host = System.Net.Dns.GetHostName();
                   //string Ip_adres=      System.Net.Dns.Resolve();
                   Console.WriteLine($"Ip-адрес: {localAddr}");            
                   //127.0.0.1 System.Net.Sockets.AddressFamily family   */
                //Console.WriteLine("\nСервер запушен");
                int counter = 0;
                RegisterCommands();
                server.Start();
          

                Console.WriteLine("\nСервер запушен");
                while (true)
                {
                    Console.WriteLine("\nОжидание соединения...");
                    ThreadPool.UnsafeQueueUserWorkItem(ClientProcessing, server.AcceptTcpClient());
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


        static Dictionary<string, Action<byte[], GlobalClass, NetworkStream>> FDictCommands = new Dictionary<string, Action<byte[], GlobalClass, NetworkStream>>();

        static void RegisterCommands()
        {
            Command command = new Command();
            FDictCommands.Add("001", new Action<byte[], GlobalClass, NetworkStream>(command.Registration_users));
            FDictCommands.Add("002", new Action<byte[], GlobalClass, NetworkStream>(command.Registration_users));
            FDictCommands.Add("003", new Action<byte[], GlobalClass, NetworkStream>(command.Checks_User_and_password));
            FDictCommands.Add("004", new Action<byte[], GlobalClass, NetworkStream>(command.Sampling_Users_Correspondence));
            FDictCommands.Add("005", new Action<byte[], GlobalClass, NetworkStream>(command.Sampling_Messages_Correspondence));
            FDictCommands.Add("006", new Action<byte[], GlobalClass, NetworkStream>(command.Select_Message_Friend));
            FDictCommands.Add("007", new Action<byte[], GlobalClass, NetworkStream>(command.Search_Image));
            FDictCommands.Add("008", new Action<byte[], GlobalClass, NetworkStream>(command.Searh_Friends));
            FDictCommands.Add("009", new Action<byte[], GlobalClass, NetworkStream>(command.Insert_Message));
            FDictCommands.Add("010", new Action<byte[], GlobalClass, NetworkStream>(command.Update_Message));
            FDictCommands.Add("011", new Action<byte[], GlobalClass, NetworkStream>(command.Delete_Message));
            FDictCommands.Add("012", new Action<byte[], GlobalClass, NetworkStream>(command.List_Friens_Message));
            FDictCommands.Add("013", new Action<byte[], GlobalClass, NetworkStream>(command.List_Friens));
            FDictCommands.Add("014", new Action<byte[], GlobalClass, NetworkStream>(command.Search_Image_Friends));
            FDictCommands.Add("015", new Action<byte[], GlobalClass, NetworkStream>(command.Select_User_Bot));
            FDictCommands.Add("016", new Action<byte[], GlobalClass, NetworkStream>(command.Select_User_));
            FDictCommands.Add("017", new Action<byte[], GlobalClass, NetworkStream>(command.Insert_Message_Telegram));
            FDictCommands.Add("018", new Action<byte[], GlobalClass, NetworkStream>(command.Select_id_Friends));
            FDictCommands.Add("019", new Action<byte[], GlobalClass, NetworkStream>(command.Registration_Insert_File_Music));
            FDictCommands.Add("020", new Action<byte[], GlobalClass, NetworkStream>(command.Registration_Insert_File_Music_Accept));
            FDictCommands.Add("021", new Action<byte[], GlobalClass, NetworkStream>(command.Registration_Insert_Voice_Telegram_Music_Accept));
        }

        static void HandleCommand(string aCommand, byte[] data, GlobalClass cls, NetworkStream ns)
        {
            Action<byte[], GlobalClass, NetworkStream> actionCommand;
            if (FDictCommands.TryGetValue(aCommand, out actionCommand)) actionCommand(data, cls, ns);
            else
            {
                // Если не нашли, то обрабатываем это }
            }
        }

        private static void ClientProcessing(object client_obj)
        {
            try
            {
                using (TcpClient client = client_obj as TcpClient)
                {   
                    /*
                    //byte[] bytes = new byte[99999999];
                   //    string data;
                   // StringBuilder data;
                   // /*
                    //int i;
                    //while ((i = await stream.ReadAsync(bytes, 0, bytes.Length)) != 0)
                    //{
                    //    data = Encoding.Default.GetString(bytes, 0, i);
                    //    string comand = data.Substring(0, 3);
                    //    string json = data.Substring(3, data.Length - 3);
                    //    byte[] msg = Encoding.Default.GetBytes(json);

                    //    //Заменяет работу switch (comand)
                    //    HandleCommand(comand, msg, globalClass, stream);


                    //}                */

                    GlobalClass globalClass = new GlobalClass();
                    NetworkStream stream = client.GetStream();
                    Command command = new Command();
      
                    String responseData = String.Empty;
                    Byte[] readingData = new Byte[256];
                    StringBuilder completeMessage = new StringBuilder();
                    int numberOfBytesRead = 0;
                    do
                    {
                        numberOfBytesRead = stream.Read(readingData, 0, readingData.Length);
                        completeMessage.AppendFormat("{0}", Encoding.Default.GetString(readingData, 0, numberOfBytesRead));
                    }
                    while (stream.DataAvailable);

                    responseData = completeMessage.ToString();

                    string comand = responseData.Substring(0, 3);
                    string json = responseData.Substring(3, responseData.Length - 3);

                    data_ = json;
                    byte[] msg = Encoding.Default.GetBytes(json);
                    HandleCommand(comand, msg, globalClass, stream);
                }
            }
            catch(Exception e) 
            {/*Exception e*/
                // Console.WriteLine(e.Source);
                Console.WriteLine(e.Message);
            }
        }
 /*             //switch (comand)
                                     //{
                                     //    case "001":
                                     //       
                                     //        UserLogin person = JsonSerializer.Deserialize<UserLogin>(msg);
                                     //        //Console.WriteLine(person.Name + " " + person.Pass + "\n");
                                     //        // GlobalClass.Insert_User(person.Name, person.Pass, dateTime);
                                     //        
                                     //        break;
                                     //    case "002": //002-Регистрация пользователей
                                     //        command.Registration_users(msg, globalClass, stream);
                                     //        break;
                                     //    case "003": //003-Проверка логина и пароля
                                     //        command.Checks_User_and_password(msg, globalClass, stream);
                                     //        break;
                                     //    case "004": //004-выборка друзей пользователя
                                     //        command.Sampling_Users_Correspondence(msg, globalClass, stream);
                                     //        break;
                                     //    case "005": //005-Выборка сообщений переписки с другом пользователя
                                     //        command.Sampling_Messages_Correspondence(msg, globalClass, stream);
                                     //        break;
                                     //    case "006": //006-Проверяет сообщение для друга
                                     //        command.Select_Message_Friend(msg, globalClass, stream);
                                     //        break;
                                     //    case "007"://007-Заготовки для отправки фото
                                     //                        //Use_Photo use_Photo = JsonSerializer.Deserialize<Use_Photo>(msg);
                                     //                           //GlobalClass.Select_Image_Userss(use_Photo);

                                     //                           //UseImage useTravels = GlobalClass.Use_image;

                                     //                           //MemoryStream fsS = new MemoryStream();
                                     //                           //using (MemoryStream fs = new MemoryStream())
                                     //                           //{
                                     //                           //    JsonSerializer.Serialize<UseImage>(fsS, useTravels);
                                     //                           //}

                                     //                           //stream.Write(fsS.ToArray(), 0, fsS.ToArray().Length);
                                     //        break;
                                     //    case "008": //008 - Добавляет Друзей в чат
                                     //        command.Searh_Friends(msg, globalClass, stream);
                                     //        break;
                                     //    case "009"://Добавление сообщения с обновлением
                                     //        command.Insert_Message(msg, globalClass, stream);
                                     //        break;
                                     //    case "010":// 10- редактировать сообщение
                                     //        command.Update_Message(msg, globalClass, stream);
                                     //        break;
                                     //    case "011"://11 -Удаляет сообщение из чата 
                                     //        command.Delete_Message(msg, globalClass, stream);
                                     //      
                                     //        //MessСhat Delete_Message = JsonSerializer.Deserialize<MessСhat>(msg);
                                     //        //globalClass.Delete_Message_make_up(Delete_Message);
                                     //        //MessСhat[] json_Update_delete = new MessСhat[globalClass.Frends_Chat_Wath.Length];
                                     //        ////        string a = "";  
                                     //        //for (int k = 0; k < globalClass.Frends_Chat_Wath.Length; k++)
                                     //        //{
                                     //        //    json_Update_delete[k] = globalClass.Frends_Chat_Wath[k];
                                     //        //}
                                     //        //UseTravel Update_chats_make_up_after_delete = new UseTravel("true", json_Update_delete.Length, json_Update_delete);
                                     //        //using (MemoryStream ms = new MemoryStream())
                                     //        //{
                                     //        //    JsonSerializer.Serialize<UseTravel>(ms, Update_chats_make_up_after_delete);
                                     //        //    stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                                     //        //}
                                     //        
                                     //        break;
                                     //    case "012": //получение списка сообщений (обновление)
                                     //        command.List_Friens_Message(msg, globalClass, stream);
                                     //        break;
                                     //    case "013": //получение списка друзей (обновление)                          
                                     //        command.List_Friens(msg, globalClass, stream);
                                     //        break;
                                     //    default:
                                     //        break;
                                     //}*/



        /// <summary>
        /// Загружаеться Ip_address и port
        /// </summary>
        static public void SaveOpen()
        {
            try
            {
                string path = Environment.CurrentDirectory.ToString();
                FileInfo fileInfo = new FileInfo(path + "\\Server.json");
                if (fileInfo.Exists)
                {
                    using (FileStream fs = new FileStream("Server.json", FileMode.Open))
                    {
                        Connect_server _aFile = JsonSerializer.Deserialize<Connect_server>(fs);
                        Ip_Adress = _aFile.Ip_address;
                        port = _aFile.Port;
                    }
                }
                else
                {
                    using (FileStream fileStream = new FileStream("Server.json", FileMode.OpenOrCreate))
                    {
                        Connect_server_ connect_Server_ = new Connect_server_(IPAddress.Loopback.ToString(), ConnectSettings.port);
                        JsonSerializer.Serialize<Connect_server_>(fileStream, connect_Server_);

                    }

                    using (FileStream fileStream = new FileStream("Server.json", FileMode.OpenOrCreate))
                    {
                        Connect_server aFile = JsonSerializer.Deserialize<Connect_server>(fileStream);
                        Ip_Adress = aFile.Ip_address;
                        port = aFile.Port;


              

                    }
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}

