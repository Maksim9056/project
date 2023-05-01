using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Class_chat;
using System.Text;


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
                IPAddress localAddr = IPAddress.Parse(ConnectSettings.IP);
                int counter = 0;
                Console.WriteLine();
                server = new TcpListener(localAddr, ConnectSettings.port);
                Console.WriteLine("Конфигурация многопоточного сервера:" + MaxThreadsCount.ToString());
                Console.WriteLine(Environment.UserName);
                Console.WriteLine(Environment.MachineName);
                Console.WriteLine(Environment.CurrentDirectory);
                Console.WriteLine(Environment.TickCount);
                Console.WriteLine(Environment.WorkingSet);
             /*   //string Host = System.Net.Dns.GetHostName();
                //string Ip_adres=      System.Net.Dns.Resolve();
                Console.WriteLine($"Ip-адрес: {localAddr}");            
                //127.0.0.1 System.Net.Sockets.AddressFamily family   */         
                Console.WriteLine("\nСервер запушен");             
                server.Start();
                while (true)
                {
                    Console.WriteLine("\nОжидание соединения...");
                    ThreadPool.UnsafeQueueUserWorkItem(ClientProcessing, server.AcceptTcpClient());
                    /*
                    // QueueUserWorkItem             
                    // ThreadPool.QueueUserWorkItem;   
                    //      Thread.MemoryBarrier();
                    */
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
           
                using (TcpClient client = client_obj as TcpClient)
                {
                    byte[] bytes = new byte[99999999];
                    string data;
                    GlobalClass globalClass = new GlobalClass();
                    NetworkStream stream = client.GetStream();
                    Command command = new Command();
                    int i;
                    while ((i = await stream.ReadAsync(bytes, 0, bytes.Length)) != 0)
                    {
                        data = Encoding.Default.GetString(bytes, 0, i);
                        string comand = data.Substring(0, 3);
                        string json = data.Substring(3, data.Length - 3);
                        byte[] msg = System.Text.Encoding.Default.GetBytes(json);
                        switch (comand)
                        {
                            case "001":
                                /* 
                                UserLogin person = JsonSerializer.Deserialize<UserLogin>(msg);
                                //Console.WriteLine(person.Name + " " + person.Pass + "\n");
                                // GlobalClass.Insert_User(person.Name, person.Pass, dateTime);
                                */
                                break;
                            case "002": //002-Регистрация пользователей
                                command.Registration_users(msg, globalClass, stream);
                                break;
                            case "003": //003-Проверка логина и пароля
                                command.Checks_User_and_password(msg, globalClass, stream);
                                break;
                            case "004": //004-выборка друзей пользователя
                                command.Sampling_Users_Correspondence(msg, globalClass, stream);
                                break;
                            case "005": //005-Выборка сообщений переписки с другом пользователя
                                command.Sampling_Messages_Correspondence(msg, globalClass, stream);
                                break;
                            case "006": //006-Проверяет сообщение для друга
                                command.Select_Message_Friend(msg, globalClass, stream);
                                break;
                            case "007"://007-Заготовки для отправки фото
                                /*                   //Use_Photo use_Photo = JsonSerializer.Deserialize<Use_Photo>(msg);
                                                   //GlobalClass.Select_Image_Userss(use_Photo);

                                                   //UseImage useTravels = GlobalClass.Use_image;

                                                   //MemoryStream fsS = new MemoryStream();
                                                   //using (MemoryStream fs = new MemoryStream())
                                                   //{
                                                   //    JsonSerializer.Serialize<UseImage>(fsS, useTravels);
                                                   //}

                                                   //stream.Write(fsS.ToArray(), 0, fsS.ToArray().Length);*/
                                break;
                            case "008": //008 - Добавляет Друзей в чат
                                command.Searh_Friends(msg, globalClass, stream);
                                break;
                            case "009"://Добавление сообщения с обновлением
                                command.Insert_Message(msg, globalClass, stream);
                                break;
                            case "010":// 10- редактировать сообщение
                                command.Update_Message(msg, globalClass, stream);
                                break;
                            case "011"://11 -Удаляет сообщение из чата 
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
                                command.List_Friens_Message(msg, globalClass, stream);
                                break;
                            case "013": //получение списка друзей (обновление)                          
                                command.List_Friens(msg, globalClass, stream);
                                break;
                            default:
                                break;
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}

       