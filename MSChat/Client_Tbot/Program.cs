
using System.IO;
using System.Reflection;
using System.Threading;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using File = System.IO.File;
using Telegram.Bot;
using System.Drawing;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;
using System.Globalization;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;
using Telegram.Bot.Types.Enums;
using static System.Net.WebRequestMethods;
namespace Client_Tbot
{
    internal class Program
    {

        static void Main(string[] args)
        {
          var client = new TelegramBotClient("6057879360:AAHsQFj0U1rLC1X2Er9v3oLXGf5fCB3quZI");
          client.StartReceiving( Update, Error);
          Console.ReadLine();

        }



        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            Command_Tbot command_Tbot = new Command_Tbot();
       
            Message message;


            if (update != null)
            {


                message = update.Message;
                if (message != null)
                {



                    if (message.Photo != null)
                    {




                        await botClient.SendDocumentAsync(message.Chat.Id, InputFile.FromFileId(message.Photo[0].FileSize.ToString()));



                        //  var  voiceMessage = message.Photo[i].FileId;


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


                    if (message.Text != null)
                    {
                        

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