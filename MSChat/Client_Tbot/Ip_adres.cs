using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServersAccept;
using System.IO;
using System.Net;
using Class_chat;
using System.Text.Json;
using System.IO.Pipes;

namespace Client_Tbot
{
    public class Ip_adres
    {

        public Int32 port { get; set; }
        public string Ip_adress { get; set; }
        public static string IP { get; set; }
        public static Int32 PORT { get; set; }

        public static string path = Environment.CurrentDirectory.ToString();
        public Ip_adres(string ip, Int32 Port)
        {
            Ip_adress = ip;
            port = Port;
        }

        //public Ip_adres()
        //{
        //    try
        //    {
        //        //Путь до файла 
        //        FileInfo fileInfo = new FileInfo(path + "\\Tbot.json");
        //        // Ищем файл с настройками подключения
        //        if (fileInfo.Exists)
        //        {
        //            using (FileStream fileStream = new FileStream("Tbot.js", FileMode.OpenOrCreate))
        //            {
        //                //Десерелизуем класс Ip_adres 
        //                Ip_adres aFile = JsonSerializer.Deserialize<Ip_adres>(fileStream);
        //                //Заполняем Ip_adres
        //                Ip_adress = aFile.Ip_adress;
        //                //Заполняем port
        //                port = aFile.port;
        //            }
        //        }
        //        else
        //        {
        //            //Если нету файла то создаем новый!
        //            using (FileStream file = new FileStream("Tbot.json", FileMode.OpenOrCreate))
        //            {
        //                //Заполняем класс Ip_adres
        //                Ip_adres ip_Adres = new Ip_adres("192.168.0.110", 9595);

        //                //Серелизуем класс Ip_adres
        //                JsonSerializer.Serialize<Ip_adres>(file, ip_Adres);
        //            }
        //        }
        //        IP = Ip_adress;
        //        PORT = port;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}
    }
}
