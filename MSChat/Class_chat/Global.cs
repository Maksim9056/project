using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Class_chat
{

    public class ConnectSettings
    {
        public const  Int32 port = 9595;
   //     public const string IP = "127.0.0.1"; 
    }



    [DataContract]
    public class UserLogin
    {
        public string Name { get; }
        public string Pass { get; set; }
        public UserLogin(string name, string pass)
        {
            Name = name;
            Pass = pass;
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
            Current=curent;
        }
    }


    [DataContract]
    public class User_regis
    {
        //User_regis() { }
        public string Name { get; set; }
        public string Pass { get; set; }
        public string Age { get; set; }
        public byte[] Photo { get; set; }
        public int Id { get; set; }

        public User_regis(string name, string pass, string age, byte[] photo, int id)
        {
            Name = name;
            Pass = pass;
            Age = age;
            Photo = photo;
            Id = id;
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



    public class MsgUser_Logins
    {
        public string Answe { get; }
        public User_photo User_ { get; set; }
        public int List_Mess { get; set; }
        public List< User_photo[]> AClass { get; set; } = new List<User_photo[]>();    

    }




    public class MsgFriends
    {
        
        MsgFriends() { }
        public string Answe { get; set; }
        public int List_Mess { get; set; }
        public List<User_photo> AClass { get; set; } = new List<User_photo>();

    }

    public class _Name
    {
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
        public MessСhat(int id,int idUserFrom, int idUserTo, string message,  DateTime dataMess, int mark)
        {
            Id = id;
            IdUserFrom = idUserFrom;
            IdUserTo = idUserTo;
            Message = message;
            DataMess = dataMess;
            Mark = mark;    
        }
    }

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

    public class MsgInfo
    {
        public string Answe { get; set; }
        public int List_Mess { get; set; }
        public List<MessСhat> AClass { get; set; } = new List<MessСhat>();
    }

    public class __User_regis
    {
        __User_regis() { }
        public string Name { get; set; }

        public string  Pass { get; set; }
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

    public class UseImage
    {
       // UseImage() { }
        public byte[] Bytes { get; set; }
        public UseImage(byte[] bytes)
        {
            Bytes = bytes;
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


