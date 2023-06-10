
//using System.IO;
//using System.Reflection;
//using System.Threading;
//using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
//using File = System.IO.File;
using Telegram.Bot;
//using System.Drawing;
using Telegram.Bot.Types.ReplyMarkups;
//using System.Collections.Generic;
//using System.Globalization;
//using Telegram.Bot.Types.ReplyMarkups;
//using static System.Net.Mime.MediaTypeNames;
//using Telegram.Bot.Types.Enums;
//using static System.Net.WebRequestMethods;
//using Newtonsoft.Json.Linq;
//using System.IO.Pipes;
using Class_chat;
using System.Text;
using System.Text.Json;
using System;
using System.IO;
//using Class_chat;

namespace Client_Tbot
{


    //public   class Пароль
    //{
    //    string пароль { get; set; }

    //    public Пароль(string Пароль)
    //    {
    //        пароль = Пароль;
    //    }
    //}


    internal class Program
    {
        public static CommandCL command = new CommandCL();
        //    public static  Ip_adres ip_Adres { get; set; }

       public static Sistem sistem = new Sistem();


        static void Main(string[] args)
        {
            sistem.Setting();
            var client = new TelegramBotClient("6057879360:AAHsQFj0U1rLC1X2Er9v3oLXGf5fCB3quZI");
           

            client.StartReceiving(Update, Error);
            Console.ReadLine();
           
        }


        
        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            try
            {

                Command_Tbot command_Tbot = new Command_Tbot();
                Message message;
                if (update != null)
                {
                    message = update.Message;
                    if (message != null)
                    {
                        //   var Result = Class.Substring(0, Class.Length - 5);

                        if (update.Message?.Text == null)
                        {

                            if (message.Photo != null)
                            {
                                for (int i = 0; i < message.Photo.Length; i++)
                                {

                                    await botClient.SendPhotoAsync(message.Chat.Id, InputFile.FromFileId(message.Photo[i].FileId.ToString()));


                                    //  var  voiceMessage = message.Photo[i].FileId;
                                    break;

                                }
                            }



                            if (message.Video != null)
                            {
                                var voiceMessage = await botClient.GetFileAsync(message.Video.FileId);


                                await botClient.SendAudioAsync(message.Chat.Id, InputFile.FromFileId(voiceMessage.FileId));

                            }
                            if (message.Document != null)
                            {

                                var voiceMessage = await botClient.GetFileAsync(message.Document.FileId);


                                await botClient.SendAudioAsync(message.Chat.Id, InputFile.FromFileId(voiceMessage.FileId));
                            }

                            if (message.Voice != null)
                            {

                                //string bity = message.Voice.MimeType;

                                //      byte[] data = Convert.FromBase64String(bity); 

                                //FileStream memoryStream = new FileStream(bity, FileMode.Open);

                                //var MS = memoryStream.ReadByte();
                                ////memoryStream.Close();

                                //MemoryStream memoryStream1 = new MemoryStream(MS);
                                //InputFileStream fileStream = InputFile.FromStream(memoryStream1);

                                var voiceMessage = await botClient.GetFileAsync(update.Message.Voice.FileId);


                                await botClient.SendAudioAsync(message.Chat.Id, InputFile.FromFileId(voiceMessage.FileId));
                                return;

                            }
                        }
                        else
                        {
                            var Class = update.Message?.Text;
                            var Result = Class?.Split(new char[] { ',', ':' });
                            if (Result[0] == "Comand ")
                            {
                                //Result[1];
                                //Result[2];
                                //var Class = update.Message?.Text;
                                //var Result = Class?.Split(new char[] { ',', ':' });
                                using (MemoryStream fs = new MemoryStream())
                                {
                                    //Создаем  экземпляр класс CommandCL
                                    //CommandCL command = new CommandCL();
                                    //Для класс серилизации используем для строки FileFS
                                    string FileFS = "";
                                    //Собрали класс UserLogin
                                    UserLogin tom = new UserLogin(Result[1], Result[2]);
                                    //Серилизовали класс UserLogin в MemoryStream fs 
                                    JsonSerializer.Serialize<UserLogin>(fs, tom);
                                    //Декодировали в строку  MemoryStream fs   
                                    FileFS = Encoding.Default.GetString(fs.ToArray());
                                    //Отправили и получили результат
                                    Task.Run(async () => await command.Check_User_Possword(sistem.IP, FileFS, "003")).Wait();
                                    //Проверяем есть ли пользователь 
                                    FileFS = "";





                                    if (CommandCL.User_Logins_and_Friends.User_ != null)
                                    {
                                        for (int j = 0; j < CommandCL.User_Logins_and_Friends.AClass.Count(); j++)
                                        {

                                            MemoryStream memoryStream = new MemoryStream();
                                            User_photo user_Photo = CommandCL.User_Logins_and_Friends.User_;
                                            user_Photo.Current = CommandCL.User_Logins_and_Friends.AClass[j].Id;
                                            JsonSerializer.Serialize(memoryStream, user_Photo);
                                            CommandCL.User_Logins_and_Friends.User_.Current = CommandCL.User_Logins_and_Friends.AClass[j].Current;
                                            //         JsonSerializer.Serialize<User_photo> (memoryStream, user_Photo);


                                            FileFS = Encoding.Default.GetString(memoryStream.ToArray());
                                            user_Photo.Current = 0;
                                            Task.Run(async () => await command.Check_Mess_Friend(sistem.IP, FileFS, "006")).Wait();

                                            if (command._Answe.ToString() == "true")
                                            {
                                                MessСhat[] les = new MessСhat[command._AClass.Count()];
                                                //Десерилизуем класс и получаем класс MessСhat 
                                                using (MemoryStream ms = new MemoryStream())
                                                {

                                                    for (int i = 0; i < command._AClass.Count(); i++)
                                                    {
                                                        string yu = command._AClass[i].ToString();
                                                        MessСhat useTravel = JsonSerializer.Deserialize<MessСhat>(yu);
                                                        les[i] = useTravel;
                                                        await botClient.SendTextMessageAsync(message.Chat.Id, CommandCL.User_Logins_and_Friends.AClass[j].Name.ToString() + ":" + les[i].Message);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                await botClient.SendTextMessageAsync(message.Chat.Id, "Сообщений нету : у пользователя " + " " + CommandCL.User_Logins_and_Friends.AClass[j].Name.ToString());
                                            }
                                            //command.
                                            //Chats_main a = new Chats_main();
                                            //Chats_main parent = (Chats_main)this.Owner;
                                            //parent.NotifyMe(CommandCL.User_Logins_and_Friends);
                                            //parent.SaveConfig(ConnectSettings.port, IP_ADRES.Ip_adress, CommandCL.User_Logins_and_Friends.User_.Name);
                                            //CommandCL.User_Logins_and_Friends = null;
                                            //this.Close();
                                        }

                                    }
                                }
                            }
                            else
                            {
                                message = update?.Message;

                                //UserLogin person5 = JsonSerializer.Deserialize<UserLogin>(FileFS);
                                // pasword = person5.Name;
                                // пароль = person5.Pass;



                                if (message.Text != null)
                                {

                                    //Пример
                                    if (message.Text == "/start")
                                    {

                                        string[][] data = new string[][] {
                             new string[] { "A1", "A2", "A3" },
                             new string[] { "B1", "B2", "B3" },
                             new string[] { "C1", "C2", "C3" },
                             };

                                        // Создаем объект таблицы
                                        InlineKeyboardButton[][] buttons = new InlineKeyboardButton[data.Length][];
                                        for (int i = 0; i < data.Length; i++)
                                        {
                                            buttons[i] = new InlineKeyboardButton[data[i].Length];
                                            for (int j = 0; j < data[i].Length; j++)
                                            {
                                                buttons[i][j] = InlineKeyboardButton.WithCallbackData(data[i][j]);
                                            }
                                        }
                                        // Отправляем сообщение с таблицей
                                        var replyMarkup = new InlineKeyboardMarkup(buttons);
                                        //  parseMode: ParseMode.MarkdownV2)
                                        var tt = await botClient.SendTextMessageAsync(message.Chat.Id, $"Here is your Button :", replyMarkup: GetButons());
                                        //await botClient.SendTextMessageAsync(
                                        //      chatId: message.Chat.Id,
                                        //       text: "Please choose:",
                                        // replyMarkup: new InlineKeyboardMarkup(new[]
                                        // {
                                        //       new InlineKeyboardButton[]
                                        //        {
                                        //         InlineKeyboardButton.WithCallbackData("Option 1", "1"),
                                        //         InlineKeyboardButton.WithCallbackData("Option 2", "2"),
                                        //        }
                                        //  }
                                        // )
                                        // );
                                    }
                                    else
                                    {

                                        //Обрабатываем кнопку 
                                        if (message.Text == "Вывести список сообщений из Программы MSChat")
                                        {

                                            //    Task.Run(async () => await command.Update_Message_make_up("192.168.0.110" , "010")).;

                                            await botClient.SendTextMessageAsync(message.Chat.Id, "Список сообщений не работает !");


                                        }
                                        else
                                        {

                                            if (message.Text == "Проверить все соообщения")
                                            {
                                                await botClient.SendTextMessageAsync(message.Chat.Id, "Проверить все  сообщения не работает !");
                                            }
                                            else
                                            {



                                                if (message.Text.ToLower().Contains("Привет") || message.Text.ToLower() == message.Text || message.Text.Substring(0, 5) == "https")
                                                {
                                                    await botClient.SendTextMessageAsync(message.Chat.Id, message.Text);
                                                    return;

                                                }
                                                else
                                                {
                                                    await botClient.SendTextMessageAsync(message.Chat.Id, "Привет!");
                                                    return;
                                                }
                                            }
                                        }
                                    }


                                }
                            }


                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static IReplyMarkup GetButons()
        {

            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "Привет", "Проверить все соообщения", "Вывести список сообщений из Программы MSChat"},
                })
            {
                ResizeKeyboard = true
            };

            return replyKeyboardMarkup;
            //InlineKeyboardMarkup inlineKeyboard = new(new[]
            //{
            //// first row
            //new []
            //{
            //    InlineKeyboardButton.WithCallbackData(text: "1.1", callbackData: "11"),
            //    InlineKeyboardButton.WithCallbackData(text: "1.2", callbackData: "12"),
            //},
            //// second row
            // new []
            //{
            //    InlineKeyboardButton.WithCallbackData(text: "2.1", callbackData: "21"),
            //    InlineKeyboardButton.WithCallbackData(text: "2.2", callbackData: "22"),
            //},
            // });
            //return inlineKeyboard;


        }


        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}