using Class_chat;
using System;
using System.Text.Json;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

namespace ServersAccept
{
    internal class Command
    {
        //002-Регистрация пользователей
        public void Registration_users(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            using (MemoryStream tt2 = new MemoryStream())
            {
                DateTime dateTime = DateTime.Now;
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
            }
        }

        //003-Проверка логина и пароля
        public void Checks_User_and_password(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            using (MemoryStream tt2 = new MemoryStream())
            {
                UserLogin person3 = JsonSerializer.Deserialize<UserLogin>(msg);

                globalClass.Select_Users(person3.Name, person3.Pass);
                if (globalClass.UserConnect == true)
                {
                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    //    JsonSerializer.Serialize<User_photo>(ms, globalClass.AUser);
                    //    stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                    //}
                    var options1 = new JsonSerializerOptions
                    {
                        AllowTrailingCommas = true
                    };

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
                            //List<User_photo[]> json_List_Friends = new List<User_photo[]>();
                            //json_List_Friends.Add(globalClass.List_Friend);

                            User_photo[] json_List_Friends = new User_photo[] { };
                            User_Logins user_Logins = new User_Logins("false", globalClass.AUser, json_List_Friends.Length, json_List_Friends);
                            //User_Logins user_Logins = new User_Logins("false", json_List_Friends.Length, json_List_Friends);

                            JsonSerializer.Serialize<User_Logins>(ms, user_Logins, options1);
                            stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                            
                        }
                    }
                    /*                //else
                                    //{
                                    //    byte[] msgAnswe = System.Text.Encoding.Default.GetBytes("false");
                                    //    stream.Write(msgAnswe, 0, msgAnswe.Length);
                                    //}
                                    ////Thread.Sleep(100);
                                    //using (MemoryStream tt = new MemoryStream())
                                    //{
                                    //    //   User_photo Select_list_Friends = JsonSerializer.Deserialize<User_photo>(msg);

                                    //    /*         
                                    //                   //        await stream.FlushAsync();
                                    //                   //        globalClass.Select_Friend(globalClass.Current_User);
                                    //                   //        User_photo AUser;                                //Обработана ситуацию когда нет друзей
                                    //                   //        if (globalClass.Friends == false)
                                    //                   //        {
                                    //                   //            byte[] msgAnswe = System.Text.Encoding.Default.GetBytes("false");
                                    //                   //            stream.Write(msgAnswe, 0, msgAnswe.Length);
                                    //                   //        }
                                    //                   //        else
                                    //                   //        {
                                    //                   //            using (MemoryStream ms_count = new MemoryStream())
                                    //                   //            {

                                    //                   //                var Countuser = globalClass.List_Friend.Length;
                                    //                   //                JsonSerializer.Serialize(ms_count, Countuser.ToString().PadLeft(3, '0'));
                                    //                   //                //   byte[] ms_count_Answe = ms_count.ToArray();
                                    //                   //                stream.Write(ms_count.ToArray(), 0, ms_count.ToArray().Length);
                                    //                   //            }
                                    //                   //            //stream.Flush();
                                    //                   //            var options = new JsonSerializerOptions
                                    //                   //            {
                                    //                   //                AllowTrailingCommas = true
                                    //                   //            };
                                    //                   //            //что то не очень
                                    //                   //            int tt = globalClass.List_Friend.Length;
                                    //                   //            for (int k = 0; k < tt; k++)
                                    //                   //            {
                                    //                   //                using (MemoryStream ms_Friend_Answe = new MemoryStream())
                                    //                   //                {
                                    //                   //                    AUser = globalClass.List_Friend[k];
                                    //                   //                    JsonSerializer.Serialize<User_photo>(ms_Friend_Answe, AUser, options);
                                    //                   //                    stream.Write(ms_Friend_Answe.ToArray(), 0, ms_Friend_Answe.ToArray().Length);
                                    //                   //                }
                                    //                   //            }
                                    //                   //        }
                                    //                   //    }
                                    //                   //    else
                                    //                   //    {
                                    //                   //        byte[] msgAnswe = System.Text.Encoding.Default.GetBytes("false");
                                    //                   //        stream.Write(msgAnswe, 0, msgAnswe.Length);
                                    //                   //    }
                                    //                   //}*/
                    //}
                }
            }
        }


        //004-выборка друзей пользователя
        public void Sampling_Users_Correspondence(byte[] msg, GlobalClass globalClass, NetworkStream stream)
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
        
        //005-Выборка сообщений переписки с другом пользователя
        public void Sampling_Messages_Correspondence(byte[] msg, GlobalClass globalClass, NetworkStream stream)
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
        
        //006-Проверяет сообщение для друга
        public void Select_Message_Friend(byte[] msg, GlobalClass globalClass, NetworkStream stream)
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
        
        //008-Добавляет Друзей в чат
        public void Searh_Friends(byte[] msg, GlobalClass globalClass, NetworkStream stream)
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

        //009*Добавляет сообщение в чат
        public void Insert_Message(byte[] msg, GlobalClass globalClass, NetworkStream stream)
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
        
        // 010- редактировать сообщение
        public void Update_Message(byte[] msg, GlobalClass globalClass, NetworkStream stream)
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

        //011 - удаление сообщения из чата
        public void Delete_Message(byte[] msg, GlobalClass globalClass, NetworkStream stream)
        {
            using (MemoryStream tt2 = new MemoryStream())
            {
                MessСhat Delete_Message = JsonSerializer.Deserialize<MessСhat>(msg);

                globalClass.Delete_Message_make_up(Delete_Message);

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

        //012 - получение списка сообщений (обновление)
        public void List_Friens_Message(byte[] msg, GlobalClass globalClass, NetworkStream stream)
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
            
        //013 - получение списка друзей (обновление)
        public void List_Friens(byte[] msg,GlobalClass globalClass, NetworkStream stream)
        {
            using (MemoryStream tt = new MemoryStream())
            {
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
            }
        }

    }
}
