using Class_chat;
//using Client_chat;

namespace Client_Tbot
{

    public class Command_Tbot
    {
       //Запращиваем настройки Ip adress,port для отсылания на ServersAcept
       //public  Ip_adres Seting = new Ip_adres();
       public CommandCL command = new CommandCL();


        //Запращиваем Сообщения из чата
        public void Select_Message_From_Chats()
        {

            Task.Run(async () => await command.Select_User_Bot(Program.sistem.IP , "", "015")).Wait();
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
    }   
}
