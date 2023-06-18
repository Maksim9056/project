using Class_chat;
using System.Text;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
//using Client_chat;

namespace Client_Tbot
{

    public class Command_Tbot
    {
       //Запращиваем настройки Ip adress,port для отсылания на ServersAcept
       //public  Ip_adres Seting = new Ip_adres();

       //Для Отправки команд на сервер
       public CommandCL command = new CommandCL();


        //Запращиваем Сообщения из чата
        public void Select_Message_From_Chats()
        {
            //Команда для запроса
            Task.Run(async () => await command.Select_User_Bot(Program.sistem.IP , "", "015")).Wait();
           
        
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
                  

                    //Заполняем класс регестрация
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




    }   
}
