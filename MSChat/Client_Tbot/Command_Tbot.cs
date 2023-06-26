using Class_chat;
using Org.BouncyCastle.Asn1.Mozilla;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using static System.Net.WebRequestMethods;
//using Client_chat;

namespace Client_Tbot
{

    public class Command_Tbot
    {
       //Запращиваем настройки Ip adress,port для отсылания на ServersAcept
       //public  Ip_adres Seting = new Ip_adres();

       //Для Отправки команд на сервер
       public CommandCL command = new CommandCL();

        public Bot_Telegram[] list_Bot_Telegram = new Bot_Telegram[] { };  

        /// <summary>
        /// Запращиваем Сообщения из чата
        /// </summary>
        public void Select_Message_From_Chats()
        {
            //Команда для запроса
            Task.Run(async () => await command.Select_User_Bot(Program.sistem.IP , "", "015")).Wait();



       

            //Bot  bot_Telegram = new Bot(CommandCL.Id_Telegram.Bot_Telegram.Count());


            Bot_Telegram [] bot_Telegram = new  Bot_Telegram[CommandCL.Id_Telegram.Bot_Telegram.Count];


            for (int i = 0; i < CommandCL.Id_Telegram.Bot_Telegram.Count(); i++)
            {
                bot_Telegram[i] = CommandCL.Id_Telegram.Bot_Telegram[i];
            }
            //  bot_Telegram = CommandCL.Id_Telegram.Bot_Telegram as Bot_Telegram;
           list_Bot_Telegram = bot_Telegram;

            //Блок примерочный
            /////////////////////////////////////////////////////
            ////Проверяем есть ли пользователь 
            //if (CommandCL.User_Logins_and_Friends.User_ != null)
            //{         
            //}
            ////Проверяем количество друзей сдесь специально больше 1  
            //else if (CommandCL.User_Logins_and_Friends.List_Mess != 0)
            //{

            //  //  "Пароль введен не верно!";
            //}
            ////Проверяем  не равен класс друзей == null
            //else if (CommandCL.User_Logins_and_Friends.AClass == null)
            //{
            //    MessageBox.Show("Такой учетной записи нет");
            //}
        }

        /// <summary>
        /// Регистрация
        /// </summary>
        public async void Select_Message_To_Chats(ITelegramBotClient botClient,Message message,string user,string password,Sistem sistem)
        {
            //Проверяем не пуст ли пользователь
            if (user == "")
            {
                //Отправляем команду Имя заполнить
                await botClient.SendTextMessageAsync(message.Chat.Id, "Имя не заполнено !");
            }
            else
            {
                //Обьявим память 
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    //Запомнаем пароль по индексу
                    // password = 
                  

                    //Заполняем класс регистрация
                    UserLogin tom = new UserLogin(user, password, Convert.ToInt32(message.From.Id));

                    //Формирует документ
                    string FileFS = "";

                    //Серилизуем класс UserLogin в json ввиде памяти
                    JsonSerializer.Serialize<UserLogin>(memoryStream, tom);

                    //Получаем из памяти json строку с классом UserLogin
                    FileFS = Encoding.Default.GetString(memoryStream.ToArray());

                    //Отправляем команду на сервер
                    Task.Run(async () => await command.Check_User_Possword(sistem.IP, FileFS, "003")).Wait();

                    //Проверяем есть ли пользователь
                    if (CommandCL.User_Logins_and_Friends.User_ != null)
                    {
                        //Заполняем класс User_photo для друзей  
                        User_photo[] user_Photos = new User_photo[CommandCL.User_Logins_and_Friends.AClass.Count()];

                        //Ищем количество учетных записей друзей
                        for (int j = 0; j < CommandCL.User_Logins_and_Friends.AClass.Count(); j++)
                        {
                            //Заполняем друзей
                            user_Photos[j] = CommandCL.User_Logins_and_Friends.AClass[j];

                            //Отправляем в чат имя друзей и их id 
                            await botClient.SendTextMessageAsync(message.Chat.Id, $"Друзья в {CommandCL.User_Logins_and_Friends.AClass[j].Name} и id друга  {CommandCL.User_Logins_and_Friends.AClass[j].Id} !");
                        }
                        //Запомнили друзей
                 //       msgUser_Logins = user_Photos;
                    }
                    //Отправляем сообщения что пользователель вошел
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Вошли в логин как  {CommandCL.User_Logins_and_Friends.User_.Name} !");
                }
            }
        }

        /// <summary>
        /// Требует переделать !
        /// </summary>
        /// 
        public async void Friend_Message(ITelegramBotClient botClient, Message message, Sistem sistem)
        {



            int Id_Telegram = 0;
        
            for (int i=0;i< list_Bot_Telegram.Length; i++)
            {
                if (Convert.ToInt32(message.From.Id) == list_Bot_Telegram[i].Id_Bot)
                {
                    Id_Telegram = list_Bot_Telegram[i].Id_Bot;
                    break;
                }
                else
                {

                }

            }
            
            if(Id_Telegram == 0)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Авторизуйтесь пожалуйста  !");                 
            }
            else
            {
             // await botClient.SendTextMessageAsync(message.Chat.Id, "Пользователь зарегрировался  !");

              Travel travel = new Travel(Id_Telegram);

              string FileFS = "";

              //Обьявим память 
              MemoryStream memoryStream = new MemoryStream();

              //Серилизуем класс UserLogin в json ввиде памяти
              JsonSerializer.Serialize<Travel>(memoryStream, travel);

            //Получаем из памяти json строку с классом UserLogin
             FileFS = Encoding.Default.GetString(memoryStream.ToArray());
         
             Task.Run(async () => await command.Select_User_(sistem.IP, FileFS,"016")).Wait();

             FileFS = "";

             User_photo[] A = new User_photo[CommandCL.User_Logins_and_Friends.List_Mess];

              for (int I = 0; I < CommandCL.User_Logins_and_Friends.AClass.Count(); I++)
              {
                   A[I] = CommandCL.User_Logins_and_Friends.AClass[I];

                  await botClient.SendTextMessageAsync(message.Chat.Id, $"Друзья  {CommandCL.User_Logins_and_Friends.AClass[I].Name} и" +
                  $" id друга  {CommandCL.User_Logins_and_Friends.AClass[I].Id} !");
              }
              
            }       
        }


        public async void Telegram_Message(ITelegramBotClient botClient, Message message, string Message ,string User , Sistem sistem)
        {

            int Id_Telegram = 0;
            string Users = "";

            for (int i = 0; i < list_Bot_Telegram.Length; i++)
            {
                if (Convert.ToInt32(message.From.Id) == list_Bot_Telegram[i].Id_Bot)
                {
                 //   Id_Telegram = list_Bot_Telegram[i].Id_Bot;

                    Id_Telegram = list_Bot_Telegram[i].Id_user;
                    break;
                }
                else
                {

                }

            }

            if (Id_Telegram == 0)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Авторизуйтесь пожалуйста  !");
            }
            else
            {


                //Заполняет информацию json виде текста о классах
                string FileFS;
                //Заполняем в памяти 
                using (MemoryStream Update = new MemoryStream())
                {
                    //Текущию дату возращает
                    DateTime dateTime = DateTime.Now;

                    //Класс соберает MessСhat
                    User_photo Mes_chat = new User_photo("", User, "", 0,Id_Telegram, Id_Telegram);

                    //Впамяти запоминает и считывает класс MessСhat ввиде json 
                    JsonSerializer.Serialize<User_photo>(Update, Mes_chat);

                    //Воспроизводит из памяти Серилизованный класс MessСhat
                    FileFS = Encoding.Default.GetString(Update.ToArray());
                }




                ////Отправляем редактированое сообщение на сервер
                //Task.Run(async () => await command.Select_Message(sistem.IP, FileFS, "009")).Wait();

            }


        }


        //  string Message, string User, Sistem sistem
     
        /// <summary>
        /// Добавляет сообщение в чат и выводит сообщения пользователя 
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="message"></param>
        public async void Insert_Telegram_Message_Chats(ITelegramBotClient botClient, Message message, Sistem sistem, string Cообщение_пользователя , string Друг )
        {
            int Id_Telegram = 0;
            int Id_Telegrams = 0;
            string Users = "";

            for (int i = 0; i < list_Bot_Telegram.Length; i++)
            {
                if (Convert.ToInt32(message.From.Id) == list_Bot_Telegram[i].Id_Bot)
                {
                    //   Id_Telegram = list_Bot_Telegram[i].Id_Bot;

                    Id_Telegram = list_Bot_Telegram[i].Id_Bot;
                    Id_Telegrams = list_Bot_Telegram[i].Id_user;
                    //   Users = list_Bot_Telegram[i];
                    break;
                }
                else
                {

                }
            }

            if (Id_Telegram == 0)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Авторизуйтесь пожалуйста  !");
            }
            else
            {  
                //Заполняет информацию json виде текста о классах
                string FileFS;

                using (MemoryStream stream = new MemoryStream()) 
                {
                    Insert_Message_Telegram insert_Message_Telegram = new Insert_Message_Telegram(Cообщение_пользователя, Друг, Id_Telegram);
                    JsonSerializer.Serialize<Insert_Message_Telegram>(stream, insert_Message_Telegram);
                    //Воспроизводит из памяти Серилизованный класс Insert_Message_Telegram
                    FileFS = Encoding.Default.GetString(stream.ToArray());
                }
           
                //Отправляем Сообщение сообщение на сервер
                Task.Run(async () => await command.Insert_Message_Telegram(sistem.IP, FileFS, "017")).Wait();



          


         
                if (command._Answe.ToString() == "true")
                {
                    for (int j = 0; j < CommandCL.Travel_Telegram_message.AClass.Count(); j++)
                    {
                        //Заполняем класс MessСhat сообщениями
                        MessСhat[] les = new MessСhat[command._AClass.Count()];

                        //Десерилизуем класс и получаем класс MessСhat

                        //десерилизуем класс по частям MessСhat
                        for (int i = 0; i < command._AClass.Count(); i++)
                        {
                            //Строка ввиде json  класса essСhat
                            string yu = command._AClass[i].ToString();

                            //Десерилизуем класс MessСhat из json строки в класс MessСhat
                            MessСhat useTravel = JsonSerializer.Deserialize<MessСhat>(yu);
                                   //Запомнили чат
                                   les[i] = useTravel;

                         

                            //Условия друг или пользователь пишет кому сообщения  по жтому такие условия 
                            if (les[i].IdUserTo == Convert.ToInt32(les[i].IdUserFrom)  && les[i].IdUserFrom == Convert.ToInt32(les[i].IdUserTo) )
                            {
                                //Пользователь  сообщения  отправляем
                                await botClient.SendTextMessageAsync(message.Chat.Id, message.Chat.FirstName+ ":" + les[i].Message);
                            }
                            else
                            {
                                //Сообщения друга к пользователю
                                if (les[i].IdUserFrom == Convert.ToInt32(les[i].IdUserFrom) && les[i].IdUserTo == Convert.ToInt32(les[i].IdUserTo))
                                {
                                    //Отправляем сообщения друга и его имя
                                    await botClient.SendTextMessageAsync(message.Chat.Id, Друг + ":" + les[i].Message);
                                }
                                else
                                {   //Здесь фильтруем  сообщения пользователя к другу
                                    await botClient.SendTextMessageAsync(message.Chat.Id, message.Chat.FirstName + ":" + les[i].Message);
                                }
                            }

                        }
                        break;
                    }
                }
                else //Если нету сообщений то отправляет "Сообщений нету : у пользователя " и друга имя
                {
                    for (int j = 0; j < CommandCL.User_Logins_and_Friends.AClass.Count(); j++)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Сообщений нету : у пользователя " + " " + CommandCL.User_Logins_and_Friends.AClass[j].Name.ToString());
                    }
                }
            }
        }



        //public async void Friend_Message(ITelegramBotClient botClient, Message message, string user, string password, Sistem sistem)
        // {
        //     //Обьявляем память
        //     using (MemoryStream fs = new MemoryStream())
        //     {

        //         //Для класс серилизации используем для строки FileFS
        //         string FileFS = "";

        //         //Собрали класс UserLogin
        //         UserLogin tom = new UserLogin(user, password, Convert.ToInt32(message.From.Id));

        //         //Серилизовали класс UserLogin в MemoryStream fs 
        //         JsonSerializer.Serialize<UserLogin>(fs, tom);

        //         //Декодировали в строку  MemoryStream fs   
        //         FileFS = Encoding.Default.GetString(fs.ToArray());

        //         //Отправили и получили результат
        //         Task.Run(async () => await command.Check_User_Possword(sistem.IP, FileFS, "003")).Wait();

        //         //Проверяем есть ли пользователь 
        //         FileFS = "";

        //         //Проверяем Есть ли пользователя имя 
        //         if (CommandCL.User_Logins_and_Friends.User_ != null)
        //         {
        //             //Друзей его запоминаю
        //             for (int j = 0; j < CommandCL.User_Logins_and_Friends.AClass.Count(); j++)
        //             {
        //                 //Выделил память
        //                 MemoryStream memoryStream = new MemoryStream();

        //                 User_photo user_Photo = CommandCL.User_Logins_and_Friends.User_;

        //                 //Выбрали друга
        //                 user_Photo.Current = Convert.ToInt32(Results[1]);

        //                 //Имя друга ищем по его id 
        //                 for (int i = 0; i < msgUser_Logins.Length; i++)
        //                 {
        //                     //Проверяем id текущего друга если id совпадает то находим
        //                     if (user_Photo.Current == msgUser_Logins[i].Id)
        //                     {
        //                         //Имя друзей находим по id
        //                         Friends = msgUser_Logins[i].Name;
        //                         break;
        //                     }
        //                     else
        //                     {

        //                     }
        //                 }

        //                 //Серилизуем класс в память
        //                 JsonSerializer.Serialize(memoryStream, user_Photo);

        //                 //Заполняем какой друга выбрали
        //                 CommandCL.User_Logins_and_Friends.User_.Current = CommandCL.User_Logins_and_Friends.AClass[j].Current;

        //                 //Формируем в строку json
        //                 FileFS = Encoding.Default.GetString(memoryStream.ToArray());

        //                 //Обнуляем текущего пользователя  его уже не нужен
        //                 user_Photo.Current = 0;

        //                 //Отправляем на сервер команду по поиску сообщений
        //                 Task.Run(async () => await command.Check_Mess_Friend(sistem.IP, FileFS, "006")).Wait();

        //                 //Проверяем нашли ли сообщения 
        //                 if (command._Answe.ToString() == "true")
        //                 {
        //                     //Заполняем размерность классу MessСhat
        //                     MessСhat[] les = new MessСhat[command._AClass.Count()];

        //                     //Десерилизуем класс и получаем класс MessСhat                                                                                                                       
        //                     for (int i = 0; i < command._AClass.Count(); i++)
        //                     {
        //                         //Получаем строку для Десерилизации
        //                         string yu = command._AClass[i].ToString();

        //                         //Десерилизуем класс MessСhat из json строки в класс MessСhat
        //                         MessСhat useTravel = JsonSerializer.Deserialize<MessСhat>(yu);

        //                         //Заполняем класс
        //                         les[i] = useTravel;

        //                         //Сообщения друга к пользователю
        //                         if (les[i].IdUserTo == Convert.ToInt32(Results[1]) && les[i].IdUserFrom == CommandCL.User_Logins_and_Friends.User_.Id)
        //                         {
        //                             //Отправляем сообщения друга и его имя
        //                             await botClient.SendTextMessageAsync(message.Chat.Id, user + ":" + les[i].Message);
        //                         }
        //                         else
        //                         {
        //                             //Сообщения друга к пользователю
        //                             if (les[i].IdUserFrom == Convert.ToInt32(Results[1]) && les[i].IdUserTo == Convert.ToInt32(CommandCL.User_Logins_and_Friends.User_.Id))
        //                             {
        //                                 //Отправляем сообщения друга и его имя
        //                                 await botClient.SendTextMessageAsync(message.Chat.Id, Friends + ":" + les[i].Message);
        //                             }
        //                             else
        //                             {
        //                                 //Здесь фильтруем  сообщения пользователя к другу
        //                                 await botClient.SendTextMessageAsync(message.Chat.Id, user + ":" + les[i].Message);
        //                             }
        //                         }

        //                     }
        //                     //Обнуляем имя друга
        //                     Friends = null;
        //                     break;


        //                 }
        //                 else  //Если нету сообщений то отправляет "Сообщений нету : у пользователя " и друга имя
        //                 {
        //                     await botClient.SendTextMessageAsync(message.Chat.Id, "Сообщений нету : у пользователя " + " " + CommandCL.User_Logins_and_Friends.AClass[j].Name.ToString());
        //                 }
        //             }
        //         }
        //     }
        // }
        //   using (MemoryStream fs = new MemoryStream())
        //                                {
        //                                    //Для класс серилизации используем для строки FileFS
        //                                    string FileFS = "";

        //    //Собрали класс UserLogin
        //    UserLogin tom = new UserLogin(Result[1], Result[2], Convert.ToInt32(message.From.Id));

        //    //Серилизовали класс UserLogin в MemoryStream fs 
        //    JsonSerializer.Serialize<UserLogin>(fs, tom);

        //                                    //Декодировали в строку  MemoryStream fs   
        //                                    FileFS = Encoding.Default.GetString(fs.ToArray());

        //                                    //Отправили и получили результат
        //                                    Task.Run(async () => await command.Check_User_Possword(sistem.IP, FileFS, "003")).Wait();

        //    //Обнуляем строку для следущей команды
        //    FileFS = "";

        //                                    //Проверяем есть ли пользователь 
        //                                    if (CommandCL.User_Logins_and_Friends.User_ != null)
        //                                    {
        //                                        //Общие количество друзей 
        //                                        for (int j = 0; j<CommandCL.User_Logins_and_Friends.AClass.Count(); j++)
        //                                        {
        //                                            //Обьявляем память
        //                                            MemoryStream memoryStream = new MemoryStream();

        //    //Присваеваем значение имя
        //    User_photo user_Photo = CommandCL.User_Logins_and_Friends.User_;

        //    //Заполняем текущего друга id
        //    user_Photo.Current = CommandCL.User_Logins_and_Friends.AClass[j].Id;

        //                                            //Серелизуем  в память
        //                                            JsonSerializer.Serialize(memoryStream, user_Photo);

        //                                            //Заполняем текущего пользователя 
        //                                            CommandCL.User_Logins_and_Friends.User_.Current = CommandCL.User_Logins_and_Friends.AClass[j].Current;

        //                                            //Формируем из памяти строку json 
        //                                            FileFS = Encoding.Default.GetString(memoryStream.ToArray());

        //                                            //Обнуляем текущего пользователя
        //                                            user_Photo.Current = 0;

        //                                            //Отправляем на сервер
        //                                            Task.Run(async () => await command.Check_Mess_Friend(sistem.IP, FileFS, "006")).Wait();

        //                                            //Проверяем есть ли сообщения
        //                                            if (command._Answe.ToString() == "true")
        //                                            {
        //                                                //Обьявляем количество в MessСhat
        //                                                MessСhat[] les = new MessСhat[command._AClass.Count()];
        //                                                //Десерилизуем класс и получаем класс MessСhat 

        //                                                using (MemoryStream ms = new MemoryStream())
        //                                                {
        //                                                    //Заполняем
        //                                                    for (int i = 0; i<command._AClass.Count(); i++)
        //                                                    {
        //                                                        //Заполняем в строку класс общий
        //                                                        string yu = command._AClass[i].ToString();

        //    //Десерилизуем MessСhat
        //    MessСhat useTravel = JsonSerializer.Deserialize<MessСhat>(yu);

        //    //Присваеваем значение useTravel
        //    les[i] = useTravel;

        //                                                        //Отправляем в телеграм имя друга и сообщения без фильтрации
        //                                                        await botClient.SendTextMessageAsync(message.Chat.Id, CommandCL.User_Logins_and_Friends.AClass[j].Name.ToString() + ":" + les[i].Message);
        //}
        //                                                }
        //                                            }
        //                                            else
        //{
        //    //Отправляем Сообщений нету : у пользователя и у друга
        //    await botClient.SendTextMessageAsync(message.Chat.Id, "Сообщений нету : у пользователя " + " " + CommandCL.User_Logins_and_Friends.AClass[j].Name.ToString());
        //}

        //                                        }

        //                                    }
        //                                }

    }
}
