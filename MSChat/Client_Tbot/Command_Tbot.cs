using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class_chat;
using System.IO;
using System.Runtime.InteropServices;
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

            //Task.Run(async () => await command.Check_User_Possword(Seting.Ip_adress, FileFS, "003")).Wait();
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
