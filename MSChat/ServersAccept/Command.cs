using Class_chat;
using System;
using System.Text.Json;
using System.IO;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Threading;
//using System.Collections.Generic;

namespace ServersAccept
{
    internal class Command
    {
        /// <summary>
        /// 002-Регистрация пользователей
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
  
     
        public   void Registration_users(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
              using (MemoryStream tt2 = new MemoryStream())
              {


                string Class = Program.data_;
                DateTime dateTime = DateTime.Now;
                string responseData = Class;
                User_regis person2 = JsonSerializer.Deserialize<User_regis>(msg);
                globalClass.Insert_Image(person2.Photo);
                globalClass.Insert_User(person2.Name, person2.Pass, person2.Age, dateTime, person2.Id_Telegram);
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

            }
        }

        /// <summary>
        /// 003-Проверка логина и пароля
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
        public void Checks_User_and_password(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try
            {
                using (MemoryStream tt2 = new MemoryStream())
                {
                    UserLogin person3 = JsonSerializer.Deserialize<UserLogin>(msg);
                    var options1 = new JsonSerializerOptions
                    {
                        AllowTrailingCommas = true
                    };

                    globalClass.Select_Users(person3.Name, person3.Pass, person3.Telegram_id);
                    if (globalClass.Name != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            User_Logins user_Logins = new User_Logins("false", null, 1, null);
                            //User_Logins user_Logins = new User_Logins("false", json_List_Friends.Length, json_List_Friends);
                            JsonSerializer.Serialize<User_Logins>(ms, user_Logins, options1);
                            stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                        }
                    }
                    else
                    {
                        if (globalClass.UserConnect == true)
                        {
                            //using (MemoryStream ms = new MemoryStream())
                            //{
                            //    JsonSerializer.Serialize<User_photo>(ms, globalClass.AUser);
                            //    stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                            //}

                            globalClass.Select_Friend(globalClass.Current_User);

                            if (globalClass.Friends == true)
                            {
                                User_photo[] json_List_Friends = new User_photo[globalClass.List_Friend.Length];

                                for (int k = 0; k < globalClass.List_Friend.Length; k++)
                                {
                                    json_List_Friends[k] = globalClass.List_Friend[k];
                                }
                                //User_Logins user_Logins = new User_Logins("true", globalClass.List_Friend.Length, json_List_Friends);

                                //   User_photo_Travel json_List_Friends_after = new User_photo_Travel(, json_List_Friends.Length, json_List_Friends);
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    User_Logins user_Logins = new User_Logins("true", globalClass.AUser, json_List_Friends.Length, json_List_Friends);
                                    JsonSerializer.Serialize<User_Logins>(ms, user_Logins, options1);
                                    stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                                }
                            }
                            else
                            {
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    User_photo[] json_List_Friends = new User_photo[] { };
                                    User_Logins user_Logins = new User_Logins("false", globalClass.AUser, json_List_Friends.Length, json_List_Friends);
                                    //User_Logins user_Logins = new User_Logins("false", json_List_Friends.Length, json_List_Friends);
                                    JsonSerializer.Serialize<User_Logins>(ms, user_Logins, options1);
                                    stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                                }
                            }
                        }
                        else
                        {

                            // отправлять на сервер что такому пользователю нет доступа
                            using (MemoryStream ms = new MemoryStream())
                            {
                                User_Logins user_Logins = new User_Logins("false", null, 0, null);
                                JsonSerializer.Serialize<User_Logins>(ms, user_Logins, options1);
                                stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                            }

                            globalClass.Name = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        /// <summary>
        /// 004-выборка друзей пользователя
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
        public void Sampling_Users_Correspondence(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try
            {
                using (MemoryStream tt2 = new MemoryStream())
                {

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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        /// <summary>
        /// 005-Выборка сообщений переписки с другом пользователя
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
        public void Sampling_Messages_Correspondence(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try
            {
                using (MemoryStream tt2 = new MemoryStream())
                {
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        /// <summary>
        /// 006-Проверяет сообщение для друга
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
        public void Select_Message_Friend(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try
            {
                using (MemoryStream tt2 = new MemoryStream())
                {
                    User_photo person6 = JsonSerializer.Deserialize<User_photo>(msg);
                    globalClass.Select_Message_Users(person6);
                    // MessСhat AChat;
                    var options1 = new JsonSerializerOptions
                    {
                        AllowTrailingCommas = true
                    };
                    if (globalClass.Mess_Chats == true)
                    {

                        MessСhat[] jsonString = new MessСhat[globalClass.aChatss.Length];
                        for (int k = 0; k < globalClass.aChatss.Length; k++)
                        {
                            jsonString[k] = globalClass.aChatss[k];
                        }
                        UseTravel useTravel = new UseTravel("true", jsonString.Length, jsonString);
                        using (MemoryStream fsS1 = new MemoryStream())
                        {
                            JsonSerializer.Serialize<UseTravel>(fsS1, useTravel, options1);
                            stream.Write(fsS1.ToArray(), 0, fsS1.ToArray().Length);
                        }
                    }
                    else
                    {
                        byte[] msgAnswe = System.Text.Encoding.Default.GetBytes("false");
                        stream.Write(msgAnswe, 0, msgAnswe.Length);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        /// <summary>
        /// 007- Получение картинки по ID
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
        public void Search_Image(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try
            {
                using (MemoryStream tt2 = new MemoryStream())
                {
                    Photo Search_Image = JsonSerializer.Deserialize<Photo>(msg);
                    globalClass.Select_Image(Search_Image);
                    if (globalClass.UserConnect)
                    {
                        using (MemoryStream Byte_Image = new MemoryStream())
                        {
                            JsonSerializer.Serialize<UseImage>(Byte_Image, globalClass.Items_Image);
                            stream.Write(Byte_Image.ToArray(), 0, Byte_Image.ToArray().Length);
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        /// <summary>
        /// 008-Добавляет Друзей в чат
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
        public void Searh_Friends(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try
            {
                using (MemoryStream tt2 = new MemoryStream())
                {
                    Searh_Friends searh_Friends = JsonSerializer.Deserialize<Searh_Friends>(msg);
                    globalClass.Searh_Users(searh_Friends);
                    if (globalClass._Searh_Freind)
                    {
                        using (MemoryStream Byte_frends = new MemoryStream())
                        {
                            Searh_Friends searh = new Searh_Friends(globalClass.Searh_Friend, searh_Friends.User);
                            JsonSerializer.Serialize<Searh_Friends>(Byte_frends, searh);
                            stream.Write(Byte_frends.ToArray(), 0, Byte_frends.ToArray().Length);
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        /// <summary>
        /// 009*Добавляет сообщение в чат
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
        public void Insert_Message(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try
            {
                using (MemoryStream tt2 = new MemoryStream())
                {
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        /// <summary>
        /// 010- редактировать сообщение
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
        public void Update_Message(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try
            {


                using (MemoryStream tt2 = new MemoryStream())
                {
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        /// <summary>
        /// 011 - удаление сообщения из чата
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
        public void Delete_Message(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try
            {
                using (MemoryStream tt2 = new MemoryStream())
                {
                    MessСhat Delete_Message = JsonSerializer.Deserialize<MessСhat>(msg);

                    globalClass.Delete_Message_make_up(Delete_Message);

                    if (globalClass.Frends_Chat_Wath == null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {   //Заполняем в пустой класс для принятия на клиенте

                            MessСhat[] json_Update_delete = new MessСhat[] {};
                            //Собераем класс отправки
                            UseTravel Update_chats_make_up_after_delete = new UseTravel("false", json_Update_delete.Length, json_Update_delete);
                            //Серилизуем класс User_photo_Travel 
                            JsonSerializer.Serialize<UseTravel>(ms, Update_chats_make_up_after_delete);
                            //Отправляем
                            stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                        }
                    }
                    else
                    {


                        MessСhat[] json_Update_delete = new MessСhat[globalClass.Frends_Chat_Wath.Length];

                        for (int k = 0; k < globalClass.Frends_Chat_Wath.Length; k++)
                        {
                            json_Update_delete[k] = globalClass.Frends_Chat_Wath[k];
                        }

                        using (MemoryStream ms = new MemoryStream())
                        {
                            UseTravel Update_chats_make_up_after_delete = new UseTravel("true", json_Update_delete.Length, json_Update_delete);
                            JsonSerializer.Serialize<UseTravel>(ms, Update_chats_make_up_after_delete);
                            stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        /// <summary>
        /// 012 - получение списка сообщений (обновление)
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
        public void List_Friens_Message(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try
            {
                using (MemoryStream tt2 = new MemoryStream())
                {
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        /// <summary>
        /// 013 - получение списка друзей (обновление)
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
        public void List_Friens(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try
            {
                using (MemoryStream tt = new MemoryStream())
                {
                    User_photo Select_list_Friends = JsonSerializer.Deserialize<User_photo>(msg);

                    if (string.IsNullOrEmpty(Select_list_Friends.Current.ToString()))
                    {

                    }
                    else
                    {
                        //Проверяем по Id текущего пользователя если 0 то нету
                        if (Select_list_Friends.Current == 0)
                        {

                        }
                        else
                        {
                            globalClass.Select_Friend(Select_list_Friends.Current.ToString());
                            //Проверяем пустые массив  друзей если есть то пустоту посылаем
                            if (globalClass.List_Friend == null)
                            {
                                using (MemoryStream ms = new MemoryStream())
                                {   //Заполняем в пустой класс для принятия на клиенте
                                    User_photo[] json_List_Friends = new User_photo[] { };
                                    //Собераем класс отправки
                                    User_photo_Travel user_Logins = new User_photo_Travel("false", json_List_Friends.Length, json_List_Friends);
                                    //Серилизуем класс User_photo_Travel 
                                    JsonSerializer.Serialize<User_photo_Travel>(ms, user_Logins);
                                    //Отправляем
                                    stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                                }
                            }
                            else//Друзья есть и массив друзей посылаем
                            {
                                //Заполняем размерность массива 
                                User_photo[] json_List_Friends = new User_photo[globalClass.List_Friend.Length];
                                //Получаем в цикле друзейй
                                for (int k = 0; k < globalClass.List_Friend.Length; k++)
                                {
                                    json_List_Friends[k] = globalClass.List_Friend[k];
                                }
                                //Отправляем
                                using (MemoryStream ms = new MemoryStream())
                                {   //Заполняем класс 
                                    User_photo_Travel json_List_Friends_after = new User_photo_Travel("true", json_List_Friends.Length, json_List_Friends);
                                    //Серилизуем класс User_photo_Travel 
                                    JsonSerializer.Serialize<User_photo_Travel>(ms, json_List_Friends_after);
                                    //Отправка на клиенте
                                    stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        /// <summary>
        /// 014 - получение списка друзей (обновление)
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
        public void Search_Image_Friends(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try
            {
                using (MemoryStream tt2 = new MemoryStream())
                {
                    Photo_Friends Search_Image = JsonSerializer.Deserialize<Photo_Friends>(msg);
                    globalClass.Select_Image_Photo_Friends(Search_Image);
                    if (globalClass.UserConnect)
                    {

                        using (MemoryStream Byte_Image = new MemoryStream())
                        {
                            JsonSerializer.Serialize<Friends_Image>(Byte_Image, globalClass.Friends_Image);
                            stream.Write(Byte_Image.ToArray(), 0, Byte_Image.ToArray().Length);
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }


        /// <summary>
        /// 015 - получение списка пользователей зарегистрированых в телеграм боте (обновление)
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
        public void Select_User_Bot(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try
            {
                using (MemoryStream tt2 = new MemoryStream())
                {
                   
                    globalClass.Select_User_From_Bot();
                    if (globalClass.UserConnect)
                    {
                       
                        using (MemoryStream Byte_Image = new MemoryStream())
                        {
                            Bot_Telegram[] bot_Telegrams = new Bot_Telegram[globalClass.list_Bot_Telegram.Length];
                            for(int i=0; i< bot_Telegrams.Length; i++)
                            {
                                bot_Telegrams[i] = globalClass.list_Bot_Telegram[i];
                            }
                            Bot Bot_Telegram = new Bot(bot_Telegrams);
                            JsonSerializer.Serialize<Bot>(Byte_Image ,Bot_Telegram);
                            stream.Write(Byte_Image.ToArray(), 0, Byte_Image.ToArray().Length);
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }




        /// <summary>
        /// 016 - получение списка Друзей в телеграм  (обновление)
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
        public void Select_User_(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try
            {
                using (MemoryStream tt = new MemoryStream())
                {
                    Travel Select_list_Friends = JsonSerializer.Deserialize<Travel>(msg);

                    if (string.IsNullOrEmpty(Select_list_Friends.Id.ToString()))
                    {
                    }
                    else
                    {
                        //Проверяем по Id текущего пользователя если 0 то нету
                        if (Select_list_Friends.Id == 0)
                        {
                        }
                        else
                        {
                           globalClass.Select_User_Id_telegram(Select_list_Friends.Id);
                            //  globalClass.Select_Friend(Select_list_Friends.Current.ToString());
                            //Проверяем пустые массив  друзей если есть то пустоту посылаем
                            globalClass.Select_Friend(globalClass.Id_Telegrams.ToString());
                            if (globalClass.Friends == false)
                            {
                                using (MemoryStream ms = new MemoryStream())
                                {   //Заполняем в пустой класс для принятия на клиенте
                                    User_photo[] json_List_Friends = new User_photo[] { };
                                    //Собераем класс отправки
                                    User_photo_Travel user_Logins = new User_photo_Travel("false", json_List_Friends.Length, json_List_Friends);
                                    //Серилизуем класс User_photo_Travel 
                                    JsonSerializer.Serialize<User_photo_Travel>(ms, user_Logins);
                                    //Отправляем
                                    stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                                }
                            }
                            else//Друзья есть и массив друзей посылаем
                            {
                                //Заполняем размерность массива 
                                User_photo[] json_List_Friends = new User_photo[globalClass.List_Friend.Length];
                                //Получаем в цикле друзейй
                                for (int k = 0; k < globalClass.List_Friend.Length; k++)
                                {
                                    json_List_Friends[k] = globalClass.List_Friend[k];
                                }
                                //Отправляем
                                using (MemoryStream ms = new MemoryStream())
                                {   //Заполняем класс 
                                    User_photo_Travel json_List_Friends_after = new User_photo_Travel("true", json_List_Friends.Length, json_List_Friends);
                                    //Серилизуем класс User_photo_Travel 
                                    JsonSerializer.Serialize<User_photo_Travel>(ms, json_List_Friends_after);
                                    //Отправка на клиенте
                                    stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }


        /// <summary>
        /// 017*Добавляет сообщение в чат из телеграма
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
        public void Insert_Message_Telegram(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try
            {
                using (MemoryStream tt2 = new MemoryStream())
                {
                    Insert_Message_Telegram Insert_Message = JsonSerializer.Deserialize<Insert_Message_Telegram>(msg);
                    //Travel Select_list_Friends = JsonSerializer.Deserialize<Travel>(msg);

                    if (string.IsNullOrEmpty(Insert_Message.Id_User.ToString()))
                    {
                    }
                    else
                    {
                        //Проверяем по Id текущего пользователя если 0 то нету
                        if (Insert_Message.Id_User == 0)
                        {
                        }
                        else
                        {


                            globalClass.Id_Friends(Insert_Message.Friend);
                            globalClass.Select_User_Id_telegram(Insert_Message.Id_User);

                            globalClass.Insert_Message_From_Telegram(Insert_Message, globalClass.Id_Telegram_Useer, globalClass.IdUserTo_Telegram);


                            if (globalClass.Frends_Telegram == null)
                            {
                            }
                            else
                            {
                                MessСhat[] json_Message = new MessСhat[globalClass.Frends_Telegram.Length];
                                //        string a = "";  
                                for (int k = 0; k < globalClass.Frends_Telegram.Length; k++)
                                {
                                    json_Message[k] = globalClass.Frends_Telegram[k];
                                }
                                UseTravel useTravels = new UseTravel("true", json_Message.Length, json_Message);
                                using (MemoryStream Friens_Byte = new MemoryStream())
                                {
                                    JsonSerializer.Serialize<UseTravel>(Friens_Byte, useTravels);
                                    stream.Write(Friens_Byte.ToArray(), 0, Friens_Byte.ToArray().Length);
                                }
                            }
                        }
                    } 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        /// <summary>
        /// 008-Добавляет Друзей в чат
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
        public void Select_id_Friends ( byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try
            {
                using (MemoryStream tt2 = new MemoryStream())
                {
                    _Name searh_Friends = JsonSerializer.Deserialize<_Name>(msg);
                    globalClass.Select_From_Table_User_id_Friends(searh_Friends.__Name);
                  
                        using (MemoryStream Byte_frends = new MemoryStream())
                        {
                            _Name searh = new _Name(globalClass.Searh_Friends_Id_Telegram);
                            JsonSerializer.Serialize<_Name>(Byte_frends, searh);
                            stream.Write(Byte_frends.ToArray(), 0, Byte_frends.ToArray().Length);
                        }
                    
                  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }


        /// <summary>
        /// 019-Добавляет музыку
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="globalClass"></param>
        /// <param name="stream"></param>
        public void Registration_Insert_File_Music(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            using (MemoryStream tt2 = new MemoryStream())
            {


                string Class = Program.data_;
                DateTime dateTime = DateTime.Now;
                string responseData = Class;
                Insert_Fille_Music person2 = JsonSerializer.Deserialize<Insert_Fille_Music>(msg);

                globalClass.Insert_File_Music(person2);

              
                //   globalClass.Insert_User(person2.Name, person2.Pass, person2.Age, dateTime, person2.Id_Telegram);
                using (MemoryStream Insert_Fille_Music = new MemoryStream())
                {
                    byte[] data = new byte[] {};
                    Insert_Fille_Music searh = new Insert_Fille_Music(globalClass.Id_Files_Mp3_Voice_message, data);
                    JsonSerializer.Serialize<Insert_Fille_Music>(Insert_Fille_Music, searh);
                    stream.Write(Insert_Fille_Music.ToArray(), 0, Insert_Fille_Music.ToArray().Length);
                }

            }
        }
        public void Registration_Insert_File_Music_Accept(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try {
                using (MemoryStream tt2 = new MemoryStream())
                {


                    string Class = Program.data_;
                    DateTime dateTime = DateTime.Now;
                    string responseData = Class;
                    Insert_Fille_Music person2 = JsonSerializer.Deserialize<Insert_Fille_Music>(msg);

                    globalClass.Select_Files(person2);


                    //   globalClass.Insert_User(person2.Name, person2.Pass, person2.Age, dateTime, person2.Id_Telegram);
                    using (MemoryStream Insert_Fille_Music = new MemoryStream())
                    {

                        //string StringImage = Convert.ToBase64String(globalClass.WavValue as Byte[]);
                        //var d = Convert.FromBase64String(StringImage);

                        // var d = Convert.ChangeType(globalClass.value,);
                        //        var dss = Convert.FromBase64CharArray(d,0,d.Length);
                        // byte[] bytes = new byte[dss.Length];

                        // bytes = Convert.FromBase64String(dss);
                        // string aA = Ims.ToString();
                        // byte[] Image = Convert.FromBase64String(aA);

                        //Items_Image = useImage;
                        //Select_Fille_Music = useImag;   

                        Insert_Fille_Music useImag = new Insert_Fille_Music(globalClass.id_value, globalClass.WavValue);
                        //   byte[] data = new byte[] { };
        //                Insert_Fille_Music searh = new Insert_Fille_Music(useImag.Id, useImag.Fille);
                        JsonSerializer.Serialize<Insert_Fille_Music>(Insert_Fille_Music, useImag);
                        stream.Write(Insert_Fille_Music.ToArray(), 0, Insert_Fille_Music.ToArray().Length);
                    }

                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            } 
        }



        public void Registration_Insert_Voice_Telegram_Music_Accept(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            try
            {
                using (MemoryStream tt2 = new MemoryStream())
                {


                    string Class = Program.data_;
                    DateTime dateTime = DateTime.Now;
                    string responseData = Class;
                    Insert_Fille_Music_VOICE person2 = JsonSerializer.Deserialize<Insert_Fille_Music_VOICE>(msg);

                    globalClass.Insert_File_Voice_Telegram(person2.Fille);
                    globalClass.Id_Friends(person2.User);
                    globalClass.Insert_Message_Telegrams_Voice(person2.Id, globalClass.IdUserTo_Telegram, globalClass.Id_Files_Mp3_Voice_message_Telegram);
                    //
                    // 
                    //   globalClass.Insert_User(person2.Name, person2.Pass, person2.Age, dateTime, person2.Id_Telegram);

                    if (globalClass.Frends_Chat_Wath == null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {   //Заполняем в пустой класс для принятия на клиенте

                            MessСhat[] json_Update_delete = new MessСhat[] { };
                            //Собераем класс отправки
                            UseTravel Update_chats_make_up_after_delete = new UseTravel("false", json_Update_delete.Length, json_Update_delete);
                            //Серилизуем класс User_photo_Travel 
                            JsonSerializer.Serialize<UseTravel>(ms, Update_chats_make_up_after_delete);
                            //Отправляем
                            stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                        }
                    }
                    else
                    {


                        MessСhat[] json_Update_delete = new MessСhat[globalClass.Frends_Chat_Wath.Length];

                        for (int k = 0; k < globalClass.Frends_Chat_Wath.Length; k++)
                        {
                            json_Update_delete[k] = globalClass.Frends_Chat_Wath[k];
                        }

                        using (MemoryStream ms = new MemoryStream())
                        {
                            UseTravel Update_chats_make_up_after_delete = new UseTravel("true", json_Update_delete.Length, json_Update_delete);
                            JsonSerializer.Serialize<UseTravel>(ms, Update_chats_make_up_after_delete);
                            stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        //public byte[] ConvertToByteArray(object obj)
        //{
        //    if (obj == null)
        //    {
        //        return null;
        //    }

        //    //BinaryFormatter formatter = new BinaryFormatter();
        //    //using (MemoryStream memoryStream = new MemoryStream())
        //    //{
        //    //    formatter.Serialize(memoryStream, obj);
        //    //    return memoryStream.ToArray();
        //    //}

        //    //object Im = sqReader["Image"];
        //    string StringImage = Convert.ToBase64String(obj as Byte[]);
        //    return Convert.FromBase64String(StringImage);
        //}

    }
}
