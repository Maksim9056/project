
using Class_chat;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;

namespace Client_Tbot
{

    internal class Program
    {
        /// <summary>
        /// /Кнопки для команд
        public const string Text_1 = "Привет";

        public const string Text_2 = "Проверить все соообщения";

        public const string Text_3 = "Вывести список сообщений из Программы MSChat";

        private const string? V = "Привет";
        /// </summary>
     
        //Имя пользователя
        public static string? user { get;  set; }

        //Пароль пользователя
        public static string? password { get; set; }

        //Класс из библиотека Chat
        public static CommandCL command = new CommandCL();

        //Класс настроек
        public static Sistem sistem = new Sistem();


        //Для имени друга
        public static string? Friends { get; set; }

        //Для id друга
        public static int ? id_Friends { get; set; }

        //Класс друзей
        public static User_photo[]? msgUser_Logins { get; set; }

        //Токен телеграм
        public static TelegramBotClient client = new TelegramBotClient("6057879360:AAHsQFj0U1rLC1X2Er9v3oLXGf5fCB3quZI");

        public static Command_Tbot command_Tbot = new Command_Tbot();
        static void Main(string[] args)
        {
            //Сохраняет настройки
            sistem?.Setting();

            //Отдельный класс команды


            //Запращиваем   регестрированых пользователей
            command_Tbot?.Select_Message_From_Chats();

            //Стартуем принятия сообщенией и ошибок
            client?.StartReceiving(Update, Error );

            //Ожидаем сообщений боту
            Console.ReadLine();        
        }
        
        //Команда для  отправки что было выполнено
        private static  async void Bot_OnMessage( Update update)
        {
            Message? message = update.Message;

            if (message == null || message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return;

            User? users = message.From;
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            var userData = SessionManager.GetUserData(users);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.

            // Добавляем логику обработки сообщения и обновления данных пользователя

            await client.SendTextMessageAsync(message.Chat.Id, "Echo: " + message?.Text);
        }

        //Последущие использование вместо 
        public class UserData
        {
            public static string? user { get; set; }
            public static string? password { get; set; }
            public static CommandCL command = new CommandCL();
            //    public static  Ip_adres ip_Adres { get; set; }

            public static Sistem sistem = new Sistem();
            public static string? Friends { get; set; }
            public static int id_Friends { get; set; }

            public static User_photo[]? msgUser_Logins { get; set; }
            // Другие свойства, необходимые для хранения данных пользователя
        }

        /// <summary>
        /// Веселый класс не трогать!
        /// </summary>
        public class SessionManager
        {
            private static readonly ConcurrentDictionary<long, UserData> userSessions = new ConcurrentDictionary<long, UserData>();

            public static UserData GetUserData(User user)
            {
                return userSessions.GetOrAdd(user.Id, key => new UserData());
            }

            public static bool RemoveUserData(User user)
            {
                return userSessions.TryRemove(user.Id, out _);
            }
        }

        //Команда для принятия сообщений
        async static Task Update(ITelegramBotClient botClient, Update? update, CancellationToken token)
        {
            //   Command_Tbot command_Tbot = new Command_Tbot();
            //Для отправки назад сообщений
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            Bot_OnMessage(update);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.

            try
            {
    

                //Собщеения сейчас нуту
                Message? message ;

                //Проверяем есть ли значения в update
                if (update != null)
                {
                    //Заполняем сообщение присланое из телеграм
                    message = update?.Message;

                    //Проверяем есть ли сообщение
                    if (message != null)
                    {
                        //Проверяем есть ли сообщение update
                        if (update?.Message?.Text == null)
                        {
                            //Проверяем прислали нам фото в сообщении
                            if (message.Photo != null)
                            {
                                //Отправляем сообщение
#pragma warning disable CS0162 // Обнаружен недостижимый код
                                for (int i = 0; i < message.Photo.Length;i++)
                                {
                                    //Отправляем в телеграм сообщение
                                    await botClient.SendPhotoAsync(message.Chat.Id, InputFile.FromFileId(message.Photo[i].FileId.ToString()));

                                    //Заканчиваем цикл отправки фото
                                    break;

                                }
#pragma warning restore CS0162 // Обнаружен недостижимый код
                            }
                            //Проверяем сообщение есть ли видио 
                            if (message.Video != null)
                            {
                                //Запомнляем файл с видио
                                var voiceMessage = await botClient.GetFileAsync(message.Video.FileId);

                                //Отправляем в телеграм пользователю
                                await botClient.SendAudioAsync(message.Chat.Id, InputFile.FromFileId(voiceMessage.FileId));
                            }
                            //Проверяем сообщения есть ли документ
                            if (message.Document != null)
                            {
                                //Скачиваем файл документа
                                var voiceMessage = await botClient.GetFileAsync(message.Document.FileId);

                                //Отправляем пользователю
                                await botClient.SendDocumentAsync(message.Chat.Id, InputFile.FromFileId(voiceMessage.FileId));
                            }

                            //Отправляем звуковое сообщение
                            if (message?.Voice != null)
                            {
                                //Скачиваем сообщение звуковое от пользователя  update.Message.Voice.FileId
                                //var voiceMessage = await botClient.GetFileAsync(message.Voice.FileId);
                                //MemoryStream T= new MemoryStream();
                                //botClient.DownloadFileAsync(voiceMessage.FilePath, T);


                                var voiceMessage = message.Voice;
                                var fileId = voiceMessage.FileId;

                                // Download audio file
                                var file = await botClient.GetFileAsync(fileId);
                                var filePath = file.FilePath;
                                var download_url = @"https://api.telegram.org/file/bot6057879360:AAHsQFj0U1rLC1X2Er9v3oLXGf5fCB3quZI/" + file.FilePath;

                                using var httpClient = new HttpClient();
                                //var tempPath = Path.GetTempFileName();
                                var s = await httpClient.GetStreamAsync(download_url);
                                //byte[] audioBytes = await GetAudioFile(fileId);
                                //MemoryStream Files101 = new MemoryStream();
                                // Save audio file locally
                                //using (var stream = System.IO.File.Open(filePath, System.IO.FileMode.OpenOrCreate))
                                //{
                                //    await botClient.DownloadFileAsync(filePath, stream);
                                //}
                                byte[] buffer;
                                using (var memoryStream = new MemoryStream())
                                {
                                    s.CopyTo(memoryStream);
                                    buffer = memoryStream.ToArray();
                                }
                                string file111 = Encoding.Default.GetString(buffer);

#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                                command_Tbot?.Insert_Telegram_Message_Voice_Chats(botClient, message, sistem, buffer, user);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.

                                //  voiceMessage.FileId
                           //     var voiceMessages = await botClient.GetFileAsync(message.Voice.FileId);
                                //Отправляем звуковое сообщения пользователю
                                //     await botClient.SendAudioAsync(message.Chat.Id, InputFile.FromFileId(voiceMessage.FileId));
                                return;
                            }
                        }
                        else
                        {
                            //Принемаем команду
                            var Class = update.Message?.Text;

                            //Разделяем команду
                            var Result = Class?.Split(new char[] { ',', ':' });

                            //Проверяем по индексу команду Comand
                            if (Result?[0] == "Comand ")
                            {

                                using (MemoryStream fs = new MemoryStream())
                                {
                                    //Для класс серилизации используем для строки FileFS
                                    string FileFS = "";

                                    //Собрали класс UserLogin
                                    UserLogin tom = new UserLogin(Result[1], Result[2], Convert.ToInt32(message.From?.Id));

                                    //Серилизовали класс UserLogin в MemoryStream fs 
                                    JsonSerializer.Serialize<UserLogin>(fs, tom);

                                    //Декодировали в строку  MemoryStream fs   
                                    FileFS = Encoding.Default.GetString(fs.ToArray());

                                    //Отправили и получили результат
                                    Task.Run(async () => await command.Check_User_Possword(sistem?.IP, FileFS, "003")).Wait();

                                    //Обнуляем строку для следущей команды
                                    FileFS = "";

                                    //Проверяем есть ли пользователь 
                                    if (CommandCL.User_Logins_and_Friends.User_ != null)
                                    {
                                        //Общие количество друзей 
                                        for (int j = 0; j < CommandCL.User_Logins_and_Friends.AClass.Count(); j++)
                                        {
                                            //Обьявляем память
                                            MemoryStream memoryStream = new MemoryStream();

                                            //Присваеваем значение имя
                                            User_photo user_Photo = CommandCL.User_Logins_and_Friends.User_;

                                            //Заполняем текущего друга id
                                            user_Photo.Current = CommandCL.User_Logins_and_Friends.AClass[j].Id;

                                            //Серелизуем  в память
                                            JsonSerializer.Serialize(memoryStream, user_Photo);

                                            //Заполняем текущего пользователя 
                                            CommandCL.User_Logins_and_Friends.User_.Current = CommandCL.User_Logins_and_Friends.AClass[j].Current;

                                            //Формируем из памяти строку json 
                                            FileFS = Encoding.Default.GetString(memoryStream.ToArray());

                                            //Обнуляем текущего пользователя
                                            user_Photo.Current = 0;

                                            //Отправляем на сервер
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                                            Task.Run(async () => await command.Check_Mess_Friend(sistem?.IP, FileFS, "006")).Wait();
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.

                                            //Проверяем есть ли сообщения
                                            if (command?._Answe.ToString() == "true")
                                            {
                                                //Обьявляем количество в MessСhat
                                                MessСhat[]? les = new MessСhat[command._AClass.Count()];
                                                //Десерилизуем класс и получаем класс MessСhat 

                                                using (MemoryStream ms = new MemoryStream())
                                                {
                                                    //Заполняем
                                                    for (int i = 0; i < command._AClass.Count(); i++)
                                                    {
                                                        //Заполняем в строку класс общий
                                                        string? yu = command._AClass[i]?.ToString();

                                                        //Десерилизуем MessСhat
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                                                        MessСhat? useTravel = JsonSerializer.Deserialize<MessСhat?>(yu);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.

                                                        //Присваеваем значение useTravel
#pragma warning disable CS8601 // Возможно, назначение-ссылка, допускающее значение NULL.
                                                        les[i] = useTravel ;
#pragma warning restore CS8601 // Возможно, назначение-ссылка, допускающее значение NULL.

                                                        //Отправляем в телеграм имя друга и сообщения без фильтрации
                                                        await botClient.SendTextMessageAsync(message.Chat.Id, CommandCL.User_Logins_and_Friends.AClass[j].Name.ToString() + ":" + les[i].Message);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //Отправляем Сообщений нету : у пользователя и у друга
                                                await botClient.SendTextMessageAsync(message.Chat.Id, "Сообщений нету : у пользователя " + " " + CommandCL.User_Logins_and_Friends.AClass[j].Name.ToString());
                                            }
                                       
                                        }

                                    }
                                }                            
                            }
                            else
                            {
                                //Получаем команду 
                                var Return = update.Message?.Text;

                                //фильтруем по символам
                                var Results = Return?.Split(new char[] { ':' });

                                //Проверяем команду Имя по индексу
                                if (Results?[0] == "Имя")
                                {
                                    //Запоминаем пользователя имя
                                    user = Results[1];

                                    // Отправляем следущую команду для ввидите пароль и полтзователь
                                    await botClient.SendTextMessageAsync(message.Chat.Id, "Пароль:" + "Пользователя");
                                }
                                else
                                {
                                    //Команда Пароль проверяем по идексу
                                    if (Results?[0] == "Пароль")
                                    {
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                                        command_Tbot?.Select_Message_To_Chats(botClient, message, user, Results[1], sistem);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                                    }
                                    else
                                    {
                                        //Проверяем для команды Friends по индексу
                                        if (Results?[0] == "Friends")
                                        {
                                            command_Tbot?.Friend_Message(botClient, message, sistem);
                                        }
                                        else
                                        {
                                            var Insert_Message = message?.Text;
                                            //   Message: Сообщение Телеграм," Друг имя "
                                            var Message = Insert_Message?.Split(new char[] { ':', ',' });

                                            if (Message?[0] == "Message")
                                            {
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                                                command_Tbot?.Insert_Telegram_Message_Chats(botClient, message, sistem, Message[1], Message[2]);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.

                                            }
                                            else
                                            {

                                                if(Message?[0] == "Друг")
                                                {
                                                    user = Message?[1];
                                                }
                                                else
                                                {

                                                }
                                                //Требует переделать  начнем с новой команды

                                                //Разделяет сообщение
                                                //var insert_Message = Class?.Split(new char[] { ':', ',' });



                                            
                                        

                                                //Заполняет сообщение в чат
                                                message = update.Message;
                              
                                                //Проверяет если сообщение
                                                if (message?.Text != null)
                                                {
                                                    //Начала запуска бота и отправляет информацию
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
                                                            //формирует кнопку по частям
                                                             buttons[i] = new InlineKeyboardButton[data[i].Length];
                                                            //цикл заполняем кнопку
                                                            for (int j = 0; j < data[i].Length; j++)
                                                            {   //Заполняем кнопки
                                                                buttons[i][j] = InlineKeyboardButton.WithCallbackData(data[i][j]);
                                                            }
                                                        }
                                                        //Использованые кнопки
                                                        string[][] ara = new string[][]{
                                                        new string[] { "Для логина в чате введите после имя пользователя: Имя:", "Для логина в чате введите после пароль  пользователя:Пароль: ", " Для выбора друга  в чате введите id друга  :Friends: "},
                                                        };
                                                        
                                                        //Кнопка
                                                        InlineKeyboardButton[][]? buttonss = new InlineKeyboardButton[ara.Length][];
                                                        //Цикл 
                                                        for (int i = 0; i < ara.Length; i++)
                                                        {   //формирует кнопку по частям
                                                            buttons[i] = new InlineKeyboardButton[ara[i].Length];
                                                            //цикл заполняем кнопку
                                                            for (int j = 0; j < data[i].Length; j++)
                                                            {
                                                                //Заполняем кнопки
                                                                buttons[i][j] = InlineKeyboardButton.WithCallbackData(ara[i][j]);
                                                            }
                                                        }

                                                        //Хранит кнопки
                                                         var replyMarkup = new InlineKeyboardMarkup(buttonss);

                                                        //Отправляет кнопки в чат
                                                         Message sesntMessage = await botClient.SendTextMessageAsync(
                                                         //Выбераем из какого чата сообщение и id его 

                                                         chatId: message.Chat.Id,
                                                         //Список команд
                                                         text: "A message with an inline keyboard markup",

                                                         //Задает параметр кнопок
                                                         replyMarkup: new InlineKeyboardMarkup(new[]
                                                         {
                                                             //Кнопки в сообщениях бота

                                                            new InlineKeyboardButton[]
                                                            {
                                                              InlineKeyboardButton.WithCallbackData("Для логина в чате введите после имя пользователя: Имя:","1"),

                                                              InlineKeyboardButton.WithCallbackData( "Для логина в чате введите после пароль пользователя:Пароль: ","2"),

                                                              InlineKeyboardButton.WithCallbackData("Для выбора друга  в чате введите id друга  :Friends:","3" ),

                                                              InlineKeyboardButton.WithCallbackData("Для выбора отправки сообщений из телеграма Message:Пользователь,Друг :Message: ,","4" ),

                                                              InlineKeyboardButton.WithCallbackData("Для выбора Перед отправкой голосовых 1 сообщения   и все из телеграма Друг:имя ,","5" ),
                                                            }
                                                         }
                                                        ));

                                                    }
                                                    else
                                                    {
                                                        //Команда Url  для сообщений
                                                        if (message.Text == "Url")
                                                        {      //Отпраляет Нету
                                                            await botClient.SendTextMessageAsync(message.Chat.Id, "Нету".ToString());
                                                        }

                                                        //Обрабатываем кнопку 
                                                        if (message.Text == "Вывести список сообщений из Программы MSChat")
                                                        {
                                                            //Отпраляем Список сообщений не работает !
                                                            await botClient.SendTextMessageAsync(message.Chat.Id, "Список сообщений не работает !");
                                                        }
                                                        else
                                                        {
                                                            //Команда Проверить все соообщения
                                                            if (message.Text == "Проверить все соообщения")
                                                            {      //Отпраляет Список сообщений не работает !
                                                                await botClient.SendTextMessageAsync(message.Chat.Id, "Проверить все  сообщения не работает !");
                                                            }
                                                            else
                                                            {
                                                                //Пересылка
                                                                if (message.Text.ToLower().Contains("Привет") || message.Text.ToLower() == message.Text || message.Text.Substring(0, 5) == "https")
                                                                {
                                                                    //Отправляет сообщения обратно
                                                                    await botClient.SendTextMessageAsync(message.Chat.Id, message.Text);
                                                                    return;

                                                                }
                                                                else
                                                                {   //Отправляет Привет
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
                        }
                    }
                    //Обрабатываються ошибки Связаными с ботом уже в чате
                    if (update?.CallbackQuery == null)
                    {
                    }
                    else
                    {
                        //Принимает кнопки нажатые в чате в сообщения
                        switch (update?.CallbackQuery.Data)
                        {
                            case "Привет":
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                                _ = await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Привет !");
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
                                break;
                            case "1":
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                                await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Введите:Имя:");
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
                                break;
                            case "2":
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                                await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Введите:Пароль:");
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
                                break;
                            case "3":
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                                await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Введите:Friends:");
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
                                break;
                            case "4":
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                                await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Введите:Message: ,");
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
                                break;
                            case "5":
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                                await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Друг:имя ,");
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
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


        private static IReplyMarkup GetButons()
        {
          // Пример использования
          /*  //ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            //{
            //    new KeyboardButton[] { "Привет", "Проверить все соообщения", "Вывести список сообщений из Программы MSChat"},
            //    })
            //{
            //    ResizeKeyboard = true
            //};

            //return replyKeyboardMarkup;*/

            //Кнопки для отображения в чате
            ReplyKeyboardMarkup replyKeyboardMarkup =
            //Для списка
            new (new[]
            {
                // Названия кнопок в списке
                new KeyboardButton[] { Text_1, Text_2,Text_3},
                })
            {
                //Параметр
                ResizeKeyboard = true
            };

            //Возрашаем кнопки
            return replyKeyboardMarkup;
            //Пример

            /*//InlineKeyboardMarkup inlineKeyboard = new(new[]
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

            //ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            // {
            //     KeyboardButton.WithRequestLocation("Share Location"),
            //       KeyboardButton.WithRequestContact("Share Contact"),
            //});
            //return replyKeyboardMarkup;*/
        }

        //Ошибки в телеграмме
        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}