using Class_chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Net.Sockets;

namespace ServersAccept
{
    internal class Command
    {

        //011 - удаление сообщения из чата
        public void Delete_Message(byte[] msg, GlobalClass globalClass, NetworkStream stream)
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




        //013 - получение списка друзей (обновление)
        public void List_Friens(byte[] msg,GlobalClass globalClass, NetworkStream stream)
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
