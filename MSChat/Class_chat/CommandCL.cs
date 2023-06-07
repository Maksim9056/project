﻿//using Class_chat;
using Newtonsoft.Json.Linq;
using System;
//using System.Collections.Generic;
//using System.Drawing;
using System.IO;
//using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
//using System.Text.Json.Nodes;
using System.Threading.Tasks;
//using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Class_chat
{
    public class CommandCL
    {
        //Передает пользователя имя и его друзей
        public static  MsgUser_Logins User_Logins_and_Friends { get; set; }
        //Друзья
        public Searh_Friends  _Friends { get; set; }   
        //Проверка
         public  JToken _Answe  { get; set; }
        //Количество друзей
        public  JToken _List_Mess_count  { get; set; }
        //Класс с друзьями и сообщениями
        public  JToken _AClass  { get; set; }

        // Проверка редактированых 
        public JToken AnsweIm { get; set; }

        //Количество картинок редактированых
        public JToken List_Mess_countIm { get; set; }

        //Класс картинок редактированых
        public JToken AClassIm { get; set; }

        public JToken List_Friends { get; set; }

        //Функция считывания байт из потока и формирование единой строки
        public string Func_Read(Stream str, int length, TcpClient client)
        {
            string Result = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                int cnt = 0;
                Byte[] locbuffer = new byte[length];
                do
                {
                    cnt = str.Read(locbuffer, 0, locbuffer.Length);
                    ms.Write(locbuffer, 0, cnt);
                }
                while (client.Available > 0);
                Result = Encoding.Default.GetString(ms.ToArray());
            }
            return Result;
        }

        // Процедура отправки регистрации пользователей 002
        async public Task Reg_User(String server, string fs, string command)
        {
            try
            {
                //Регистрация
                using (TcpClient client = new TcpClient(server, ConnectSettings.port))
                {
                    //Декодируем Bite []
                    Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
                    NetworkStream stream = client.GetStream();
                    //Отправляем на сервер
                    await stream.WriteAsync(data, 0, data.Length);

                    String responseData = String.Empty;
                    //Функия получения
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
                    //Получаем имя пользователя
                    User_reg.UserName = responseData;
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException:{0}", e.Message);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e.Message);
            }
            catch(Exception )
            {
               // MessageBox.Show(e.Message);
            }
        }


        // Передача 003 проверка пользователя и его пароль 
        async public Task Check_User_Possword(String server, string fs, string command)
        {
            try
            {
                using (TcpClient client = new TcpClient(server, ConnectSettings.port))
                {
                    //Декодируем Bite []
                    byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
                    string Host = System.Net.Dns.GetHostName();
                    NetworkStream stream = client.GetStream();
                    //Отправляем на сервер
                    await stream.WriteAsync(data, 0, data.Length);
                    String responseData = String.Empty;
                    /*
                    //String responseDat = String.Empty;                    
                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    //    int cnt = 0;
                    //    Byte[] locbuffer = new byte[1024];
                    //    do
                    //    {
                    //        cnt = stream.Read(locbuffer, 0, locbuffer.Length);
                    //        ms.Write(locbuffer, 0, cnt);
                    //    } while (client.Available > 0);
                    //    responseData = Encoding.Default.GetString(ms.ToArray());

                    //}
                    */

                    //Функция получаем
                    //responseData = await Task<string>.Run(() =>
                    //{
                    //    return Func_Read(stream, data.Length, client);
                    //});

                    Byte[] readingData = new Byte[256];
                    StringBuilder completeMessage = new StringBuilder();
                    int numberOfBytesRead = 0;
                    do
                    {
                        numberOfBytesRead = stream.Read(readingData, 0, readingData.Length);
                        completeMessage.AppendFormat("{0}", Encoding.Default.GetString(readingData, 0, numberOfBytesRead));
                    }
                    while (stream.DataAvailable);
                    //Получили результат
                    responseData = completeMessage.ToString();


                    //Проверяем
                    if (responseData == "false")
                    {
                        User_Logins_and_Friends = null;
                    }
                    else
                    {
                        //Десерилизуем класс
                        MsgUser_Logins person3 = JsonSerializer.Deserialize<MsgUser_Logins>(responseData);
                        User_Logins_and_Friends = person3;
                        /*
              //        responseData
              //Chats_main a = new Chats_main();
              //Chats_main parent = (Chats_main).this.Owner;
              //parent.NotifyMe(person3);
              //parent.SaveConfig(ConnectSettings.port, IP_ADRES.Ip_adress, person3.User_.Name);
              //userpass.Close();
              /*
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


              //if (responseData == "false")
              //{



              //}
              //else
              //{
              //  User_Logins msgFriends = JsonSerializer.Deserialize<User_Logins>(responseDat);
              /*                    //Int32 bytess = await stream.ReadAsync(data, 0, data.Length);



                                  //if (person3.Name == user)
                                  //{
                                  //    //MessageBox.Show("Подключение пользователя разрешено");
                                  //    //получить перечень друзей
                                  //    Int32 bytesFriend = await stream.ReadAsync(data, 0, 5);
                                  //    responseDat = System.Text.Encoding.Default.GetString(data, 0, bytesFriend);
                                  //    User_photo[] people = null;
                                  //    if (responseDat == "false")
                                  //    {



                                  //    }
                                  //    else
                                  //    {
                                  //        string result = responseDat.Trim(new char[] { '"', '0' });
                                  //        Int32 it = Convert.ToInt32(result);
                                  //        people = new User_photo[it];

                                  //        Int32 bytesFriend1 = await stream.ReadAsync(data, 0, data.Length);
                                  //        //Друзья
                                  //        result = System.Text.Encoding.Default.GetString(data, 0, bytesFriend1);
                                  //        string rez2 = result.Substring(0, result.IndexOf("}"));

                                  //        List<string> tokens = new List<string>(result.Split('}'));

                                  //        for (int j = 0; j < tokens.Count - 1; j++)
                                  //        {
                                  //            string tt = tokens[j] + "}";
                                  //            people[j] = JsonSerializer.Deserialize<User_photo>(tt);
                                  //        }
                                  //    }*/
                        /*
          * 
                     //a.Show();*/
                        /*
                            //a.OpenMes(person3.User_, person3);
                            //userpass.Hide();
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Пользователя нет");
                            //}
                            */
                    }
                }
            }

            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException:{0}", e.Message);
            }
            catch (SocketException)
            {
                //Console.WriteLine("SocketException: {0}", e.Message);
            }
            catch (Exception e)
            {
                  Console.WriteLine("SocketException: {0}", e.Message);
            }

        }


        // Передача 007 получение картинки
        async public Task Get_Image(String server, string fs, string command)
        {
            try
            {
                using (TcpClient client = new TcpClient(server, ConnectSettings.port))
                {   
                    //Декодируем Bite []
                    Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
                    NetworkStream stream = client.GetStream();
                    //Отправляем на сервер
                    await stream.WriteAsync(data, 0, data.Length);
            
                    String responseData = String.Empty;
                    //Функция получения
                    Byte[] readingData = new Byte[256];
                    StringBuilder completeMessage = new StringBuilder();
                    int numberOfBytesRead = 0;
                    do
                    {
                        numberOfBytesRead = stream.Read(readingData, 0, readingData.Length);
                        completeMessage.AppendFormat("{0}", Encoding.Default.GetString(readingData, 0, numberOfBytesRead));
                    }
                    while (stream.DataAvailable);
                    //Получили результат
                    responseData = completeMessage.ToString();

                    //Проверяем условия
                    if (responseData == "false")
                    {
                        //Нету картинки
                    }
                    else
                    {
                        //Так обрабатываем картинки 
                        JObject details = JObject.Parse(responseData);
                        JToken Answe = details.SelectToken("Answe");
                        JToken List_Mess = details.SelectToken("List_Mess");
                        JToken AClass = details.SelectToken("Image");
                        AnsweIm = Answe;
                        List_Mess_countIm = List_Mess;
                        AClassIm = AClass;

                        //UseImage_OutPut msgImage = JsonSerializer.Deserialize<UseImage_OutPut>(responseData);
                        //UserImage = AClass;
                    }
                }
            }
            catch (ArgumentNullException)
            {
              //  MessageBox.Show("ArgumentNullException:{0}", e.Message);
            }
            catch (SocketException)
            {
                //MessageBox.Show("SocketException: {0}", e.Message);
            }
            catch (Exception)
            {
             //   MessageBox.Show(e.Message);
            }

        }

        // Передача картинок друзей
        async public Task Get_Image_Friends(String server, string fs, string command)
        {
            try
            {
                using (TcpClient client = new TcpClient(server, ConnectSettings.port))
                {   //Сформировали в Byte массив весь класс и команду
                    Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
                    NetworkStream stream = client.GetStream();
                    //Отправили на сервер
                    await stream.WriteAsync(data, 0, data.Length);
                    //data = new Byte[1024];
                    String responseData = String.Empty;
                    //Функция получения
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
                    //Проверяем
                    if (responseData == "false")
                    {
                        //Обработаем
                    }
                    else
                    {
                        //Разбераем JObject и JToken удобно без обрезания серилизует байты
                        JObject details = JObject.Parse(responseData);
                        JToken Answe = details.SelectToken("List_Mess");
                        JToken List_Mess = details.SelectToken("Image");
                        List_Friends = List_Mess;                
                        //UseImage_OutPut msgImage = JsonSerializer.Deserialize<UseImage_OutPut>(responseData);
                        //UserImage = AClass;
                 
                    }
                }
            }
            catch (ArgumentNullException )
            {
               // MessageBox.Show("ArgumentNullException:{0}", e.Message);
            }
            catch (SocketException )
            {
              //  MessageBox.Show("SocketException: {0}", e.Message);
            }
            catch (Exception )
            {
                //MessageBox.Show(e.Message);
            }
        }


        // Процедура отправки 006
        async public Task Check_Mess_Friend(String server, string fs, string command)
        {
            try
            {
                using (TcpClient client = new TcpClient(server, ConnectSettings.port))
                {   //Сформировали в Byte массив весь класс и команду
                    Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
                    NetworkStream stream = client.GetStream();
                    //Отправили  на сервер
                    await stream.WriteAsync(data, 0, data.Length);
                    //Назначаем длину 1024
                    //data = new Byte[1024];
                    String responseData = String.Empty;
                    //функция получения но обрезающая 
                    //responseData = await Task<string>.Run(() =>
                    //{
                    //    return Func_Read(stream, data.Length, client);
                    //});

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




                    //получить перечень сообщений
                    if (responseData == "false")
                    {   //Если нету списка
                        _Answe = "false";
                    }
                    else
                    {
                        //Разбераем классом но надо правильно это делать может не сработать 
                        //В данном случаи сработал
                        MsgInfo msgInfo = JsonSerializer.Deserialize<MsgInfo>(responseData);
                        //Разбераем JObject и JToken удобно для повторного использования
                        JObject details = JObject.Parse(responseData);
                        JToken Answe = details.SelectToken("Answe");
                        JToken List_Mess = details.SelectToken("List_Mess");
                        JToken AClass = details.SelectToken("AClass");
                        //Присваеваем значения
                        _Answe = Answe;
                        _List_Mess_count = List_Mess;
                        _AClass = AClass;
                    }

                }
            }
            catch (ArgumentNullException )
            {
             //   MessageBox.Show("ArgumentNullException:{0}", e.Message);
            }
            catch (SocketException )
            {
                //MessageBox.Show("SocketException: {0}", e.Message);
            }
            catch (Exception )
            {
               // MessageBox.Show(e.Message);
            }

        }


        // Проццедура отправки 008
        async public Task Connect_Friends(String server, string fs, string command)
        {
            try
            {
                using (TcpClient client = new TcpClient(server, ConnectSettings.port))
                {    //Сформировали в Byte массив весь класс и команду
                    Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
                    NetworkStream stream = client.GetStream();
                    //Отправили друзей
                    await stream.WriteAsync(data, 0, data.Length);

                    String responseDat = String.Empty;
                    //Функция для получения ответа 
                    Byte[] readingData = new Byte[256];
                    StringBuilder completeMessage = new StringBuilder();
                    int numberOfBytesRead = 0;
                    do
                    {
                        numberOfBytesRead = stream.Read(readingData, 0, readingData.Length);
                        completeMessage.AppendFormat("{0}", Encoding.Default.GetString(readingData, 0, numberOfBytesRead));
                    }
                    while (stream.DataAvailable);
                    responseDat = completeMessage.ToString();
                    //Получили данные в строке и десеризовали класс Searh_Friends
                    Searh_Friends searh_Friends = JsonSerializer.Deserialize<Searh_Friends>(responseDat);
                    _Friends = searh_Friends;                             
                }                                                                 
            }
            catch (ArgumentNullException)
            {
               // MessageBox.Show("ArgumentNullException:{0}", e.Message);
            }
            catch (SocketException )
            {
                //MessageBox.Show("SocketException: {0}", e.Message);
            }
        }


        // Процедура отправки 009
        async public Task Insert_Message(String server, string fs, string command)
        {
            try
            {
                using (TcpClient client = new TcpClient(server, ConnectSettings.port))
                {
                    Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
                    NetworkStream stream = client.GetStream();
                    await stream.WriteAsync(data, 0, data.Length);

                    String responseDat = String.Empty;

                    responseDat = await Task<string>.Run(() =>
                    {
                        return Func_Read(stream, data.Length, client);
                    });
                
                    MsgInfo msgInfo = JsonSerializer.Deserialize<MsgInfo>(responseDat);

                    JObject details = JObject.Parse(responseDat);
                    JToken Answe = details.SelectToken("Answe");
                    JToken List_Mess = details.SelectToken("List_Mess");
                    JToken AClass = details.SelectToken("AClass");
                    _Answe = Answe;
                    _List_Mess_count = List_Mess;
                    _AClass = AClass;                                
                }
            }
            catch (ArgumentNullException)
            {
              //  MessageBox.Show("ArgumentNullException:{0}", e.Message);
            }
            catch (SocketException)
            {
              //  MessageBox.Show("SocketException: {0}", e.Message);
            }
        }


        // Процедура отправки 010
        async public Task Update_Message_make_up(String server, string fs, string command)
        {
            try
            {
                using (TcpClient client = new TcpClient(server, ConnectSettings.port))
                {
                     //Сформировали в Byte массив весь класс и команду
                    Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
                    NetworkStream stream = client.GetStream();
                    //Отправли на сервер
                    await stream.WriteAsync(data, 0, data.Length);
                    String responseDat = String.Empty;
                    //получаем из сервера ответ
                    responseDat = await Task<string>.Run(() =>
                    {
                        return Func_Read(stream, data.Length, client);
                    });
                    // десеризовали класс MsgInfo в д
                    MsgInfo msgInfo = JsonSerializer.Deserialize<MsgInfo>(responseDat);
                    //Разбераем JObject и JToken удобно для повторного использования
                    JObject details = JObject.Parse(responseDat);
                    JToken Answe = details.SelectToken("Answe");
                    JToken List_Mess = details.SelectToken("List_Mess");
                    JToken AClass = details.SelectToken("AClass");
                    _Answe = Answe;
                    _List_Mess_count = List_Mess;
                    _AClass = AClass;
                }
            }
            catch (ArgumentNullException)
            {
               // MessageBox.Show("ArgumentNullException:{0}", e.Message);
            }
            catch (SocketException)
            {
               // MessageBox.Show("SocketException: {0}", e.Message);
            }
        }  


        // Процедура отправки 011
        async public Task Delete_message_make_up(String server, string fs, string command)
        {
            try
            {
                using (TcpClient client = new TcpClient(server, ConnectSettings.port))
                {
                    //Сформировали в Byte массив весь класс и команду
                    Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
                    NetworkStream stream = client.GetStream();
                    //Отправили на сервер
                    await stream.WriteAsync(data, 0, data.Length);                 
                    String responseDat = String.Empty;
                    //получаем строку
                    responseDat = await Task<string>.Run(() =>
                    {
                        return Func_Read(stream, data.Length, client);
                    });
                    //Разбераем классом MsgInfo  но надо правильно это делать может не сработать  
                    //Сдесь сработала десерилизация
                    MsgInfo msgInfo = JsonSerializer.Deserialize<MsgInfo>(responseDat);
                    //Разбераем JObject и JToken удобно для повторного использования
                    JObject details = JObject.Parse(responseDat);
                    JToken Answe = details.SelectToken("Answe");
                    JToken List_Mess = details.SelectToken("List_Mess");
                    JToken AClass = details.SelectToken("AClass");
                    _Answe = Answe;
                    _List_Mess_count = List_Mess;
                    _AClass = AClass;
                }
            }
            catch (ArgumentNullException)
            {
                //MessageBox.Show("ArgumentNullException:{0}", e.Message);
            }
            catch (SocketException)
            {
              //  MessageBox.Show("SocketException: {0}", e.Message);
            }
        }   
    }
}
