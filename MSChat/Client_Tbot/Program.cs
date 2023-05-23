
using System.IO;
using System.Reflection;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = System.IO.File;
using System.Drawing;
namespace Client_Tbot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new TelegramBotClient("6057879360:AAHsQFj0U1rLC1X2Er9v3oLXGf5fCB3quZI");

            client.StartReceiving(Update, Error);

            Console.ReadLine();
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {

            Message message ;
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
             

                          await botClient.SendAudioAsync(message.Chat.Id,InputFile.FromFileId(voiceMessage.FileId));
                        return;

                    }


                    if (message.Text != null)
                    {
                        if (message.Text.ToLower().Contains("Привет") || message.Text.ToLower() == message.Text || message.Text.Substring(0,5) == "https")
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
         

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}