using System;
using System.Collections.Generic;
using System.Deployment.Internal;
//using System.Net;
using System.Runtime.Serialization;
//using System.Text.Json.Serialization;

namespace Class_chat
{

    [Serializable]
    public class Travel
    {
        public int Id { get; set; }
        public Travel(int id)
        {
            Id = id;
        }
        public Travel() { }
    }
    
    [Serializable]
    public class Bot_Telegram
    {
        Bot_Telegram() { }
        public int Id_user { get; set; }
        public int Id_Bot { get; set; }
        public Bot_Telegram(int id_user, int id_bot)
        {
            Id_user = id_user;
            Id_Bot = id_bot;
        }
    }

    public class Bot
    {
        Bot() { }
       public  Bot_Telegram[] Bot_Telegram { get; set; }
        public Bot(Bot_Telegram[] bot_Telegram)
        {
            Bot_Telegram = bot_Telegram;
        }
    }

    [Serializable]
    public class List_Bot_Telegram
    {
       public List<Bot_Telegram> Bot_Telegram { get; set; } = new List<Bot_Telegram>();

    }


    public static class Connect_Client
    {
        public static Int32 Port { get; set; }
        public static string IP  { get; set; }
        public static string UserName { get; set; }
        //public Connect_Client(Int32 port, string ip,string userName) 
        //{
        //    Port=port;
        //    IP = ip;
        //    UserName=userName;
        //}
    }
    public class User_reg
    {
        public static string UserName { get; set; }
        public User_reg(string userName)
        {
            UserName = userName;
        }
    }


    public class Connect_Client_
    {
        public Int32 Port { get; set; }
        public string IP { get; set; }
        public string UserName { get; set; }
        public Connect_Client_(Int32 port, string ip, string userName)
        {
            Port = port;
            IP = ip;
            UserName = userName;
        }
    }

    public class Connect_server
    {
        public string Ip_address { get; set; }
        public Int32 Port { get; set; }
    }

    public class Connect_server_
    {
        public string Ip_address { get; set; }
        public Int32 Port { get; set; }
        public Connect_server_(string ip, Int32 port)
        {
            Ip_address = ip;
            Port = port;
        }
    }


    public class ConnectSettings
    {
        public const Int32 port = 9595;
       // public const string IP { get; set; }
    }



    // [DataContract]
    public class UserLogin
    {
        //public UserLogin() { }
        public string Name { get; }
        public string Pass { get; set; }
        public int  Telegram_id { get; set; }
        public UserLogin(string name, string pass, int telegram_id)
        {
            Name = name;
            Pass = pass;
            Telegram_id = telegram_id;
        }
    }


    [DataContract]
    public class Friends
    {
        public string Name { get; set; }
        public string Pass { get; set; }
        public int Current { get; set; }

        public Friends(string name, string pass, int curent)
        {
            Name = name;
            Pass = pass;
            Current = curent;
        }
    }



    [Serializable]
    public class User_regis
    {
     //   User_regis() { }
        public string Name { get; set; }
        public string Pass { get; set; }
        public string Age { get; set; }
        public byte[] Photo { get; set; }
        public int Id { get; set; }
        public int Id_Telegram { get; set; }
       
        public User_regis(string name, string pass, string age, byte[] photo, int id, int id_Telegram)
        {
            Name = name;
            Pass = pass;
            Age = age;
            Photo = photo;
            Id = id;
            Id_Telegram = id_Telegram;
        }
    }

    public class User_photo
    {
        User_photo() { }
        public string Name { get; set; }
        public string Pass { get; set; }
        public string Age { get; set; }
        public int Photo { get; set; }
        public int Id { get; set; }
        public int Current { get; set; }
        public User_photo(string name, string pass, string age, int photo, int id, int current)
        {
            Name = name;
            Pass = pass;
            Age = age;
            Photo = photo;
            Id = id;
            Current = current;
        }
    }

    // класс отправки 
    public class User_photo_Travel
    {
        User_photo_Travel() { }
        public string Answe { get; }
        public int List_Mess { get; set; }
        public User_photo[] AClass { get; set; }
        public User_photo_Travel(string answe, int count, User_photo[] aclass)
        {
            Answe = answe;
            List_Mess = count;
            AClass = aclass;
        }
    }

    // класс приема User_photo_Travel
    public class MsgFriends
    {
        //MsgFriends() { }
        public string Answe { get; set; }
        public int List_Mess { get; set; }
        public List<User_photo> AClass { get; set; } = new List<User_photo>();
    }


    /// <summary>
    /// класс отправки MsgUser_Logins
    /// </summary>
    public class User_Logins
    {
        User_Logins() { }
        public string Answe { get; }
        public User_photo User_ { get; set; }
        public int List_Mess { get; set; }
        public User_photo[] AClass { get; set; }
     

        public User_Logins (string answe, User_photo user_, int list_Mess, User_photo[] aClass)
        {
            Answe = answe;
            User_ = user_;
            List_Mess = list_Mess;
            AClass = aClass;
        }
    }


    /// <summary>
    /// класс приема User_Logins
    /// </summary>
    public class MsgUser_Logins
    {
        public string Answe { get; }
        public User_photo User_ { get; set; }
        public int List_Mess { get; set; }
        public List<User_photo> AClass { get; set; } = new List<User_photo>();

    }


    public class _Name
    {
        public _Name() { }
        public string __Name { get; set; }
        public _Name(string name)
        {
            __Name = name;
        }
    }

    public class MessСhat
    {
        public MessСhat() { }
        public int Id { get; set; }
        public int IdUserFrom { get; set; }
        public int IdUserTo { get; set; }
        public string Message { get; set; }
        public DateTime DataMess { get; set; }
        public int Mark { get; set; }
        public int Files { get; set; }

        public MessСhat(int id, int idUserFrom, int idUserTo, string message, DateTime dataMess, int mark, int files)
        {
            Id = id;
            IdUserFrom = idUserFrom;
            IdUserTo = idUserTo;
            Message = message;
            DataMess = dataMess;
            Mark = mark;
            Files = files;
        }
    }

    // класс отправки MsgInfo
    public class UseTravel
    {
        UseTravel() { }
        public string Answe { get; }
        public int List_Mess { get; set; }
        public MessСhat[] AClass { get; set; }
        public UseTravel(string answe, int count, MessСhat[] aclass)
        {
            Answe = answe;
            List_Mess = count;
            AClass = aclass;
        }
    }

    // класс приема UseTravel
    public class MsgInfo
    {
        public string Answe { get; set; }
        public int List_Mess { get; set; }
        public List<MessСhat> AClass { get; set; } = new List<MessСhat>();
    }


    public class Photo
    {
        Photo() { }
        public int Id { get; set; }
        public int Current { get; set; }

        public Photo(int id, int current)
        {
            Id = id;
            Current = current;
        }
    }

    public class Photo_Friends
    {
        Photo_Friends() { }
        public int[] Id { get; set; }
        public int[] Current { get; set; }

        public Photo_Friends(int[] id, int[] current)
        {
            Id = id;
            Current = current;
        }
    }

    public class Friends_Image
    {
        Friends_Image() { }
        public int List_Mess { get; set; }
        public string[] Image { get; set; }
        public Friends_Image(string[] image, int list_Mess)
        {
            Image = image;
            List_Mess = list_Mess;
        }
    }


    // класс для отправки UseImage_OutPut
    public class UseImage
    {
        UseImage() { }
        public string Answe { get; set; }
        public int List_Mess { get; set; }
        public string[] Image { get; set; }
        public UseImage(string[] image, string answe, int list_Mess)
        {
            Image = image;
            Answe = answe;
            List_Mess = list_Mess;
        }
    }

    // класс для приема UseImage
    public class UseImage_OutPut
    {
        public string Answe { get; set; }
        public int List_Mess { get; set; }
        public List<UseImage> Image { get; set; } = new List<UseImage>();
    }


    public class __User_regis
    {
        __User_regis() { }
        public string Name { get; set; }
        public string Pass { get; set; }
        public string Age { get; set; }
        public byte[] Photo { get; set; }
        public int Id { get; set; }

        public __User_regis(string name, string pass, string age, byte[] photo, int id)
        {
            Name = name;
            Pass = pass;
            Age = age;
            Photo = photo;
            Id = id;
        }
    }

    public class Searh_Friends
    {
        Searh_Friends() { }
        public string Name { get; set; }
        public string User { get; set; }
        public Searh_Friends(string name, string user)
        {
            Name = name;
            User = user;
        }
    }

    /// <summary>
    /// Для отправки из телеграм сообщений в чат
    /// </summary>
    public class Insert_Message_Telegram
    {
        public Insert_Message_Telegram() { }

        public string Message { get; set; }

        public string Friend { get; set; }

        public int Id_User { get; set; }
        public Insert_Message_Telegram(string message, string friend, int id_User)
        {
            Message = message;
            Friend = friend;
            Id_User = id_User;
        }
    }


    public class Insert_Fille_Music
    {
        Insert_Fille_Music() { }
        public int  Id { get; set; }
        public byte[] Fille { get; set; }
        public Insert_Fille_Music(int id, byte[] fille)
        {
            Id = id;
            Fille = fille;
        }
    }

    public class Insert_Fille_Music_VOICE
    {
        Insert_Fille_Music_VOICE() { }
        public int Id { get; set; }
        public byte[] Fille { get; set; }
        public string  User { get; set; }  
        public Insert_Fille_Music_VOICE(int id, byte[] fille, string user)
        {
            Id = id;
            Fille = fille;
            User = user;
        }
    }

}

//}   Friends[] UserRG = new Friends[] {

//             new Friends(name, Pass)


//        };

//public class User_reg
//{
//    public string Name { get; }
//    public string Pass { get; set; }
//    public string Age { get; set; }
//    public byte[] Photo { get; set; }

//    public User_reg(string name, string pass, string age, byte[] photo)
//    {
//        Name = name;
//        Pass = pass;
//        Age = age;
//        Photo = photo;
//    }

//}


//public class Global
//{
//        public string Name { get; set; }
//        public int Age { get; set; }
//        public Global(string name, int age)
//        {
//            Name = name;
//            Age = age;
//        }
//      async public void Globals()
//    {
//        string NameFile = "user1";
//                    var rand = new Random();
//        var Global = new Global(Name, Age)
//        {


//        };


//        //if (File.Exists(NameFile + ".xml"))
//        //{ 

//        //    NameFile = NameFile + rand.Next(100) as String;
//        //}

//        {
//            //File.Replace("person.xml", NameFile + ".xml", NameFile + ".xml");
//            Console.WriteLine();
//             string json = JsonSerializer.Serialize(NameFile);
//              Console.WriteLine(json);
//               Global restoredPerson = JsonSerializer.Deserialize<Global>(json);
//              Console.WriteLine(restoredPerson?.Name); // Tom
//            //if (File.Exists(NameFile + ".xml"))
//            //{
//            //    NameFile = NameFile + rand.Next(101) as String;
//            //}
//            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
//          {
//              await JsonSerializer.SerializeAsync<Global>(fs,NameFile );
//              Console.WriteLine("Data has been saved to file");
//          }
//              // чтение данных
//         using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
//         {
//            Global person = await JsonSerializer.DeserializeAsync<Global>(fs);
//           Console.WriteLine($"Name: {person?.Name}  Age: {person?.Age}");
//         }
//    }
//}


