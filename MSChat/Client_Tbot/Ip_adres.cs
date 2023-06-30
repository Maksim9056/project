using System.Net;
using Class_chat;
using System.Text.Json;
namespace Client_Tbot
{
    //Класс для настроек
    public class Ip_adres
    {
        //Порт 
        public Int32? port { get; set; }
        // Ip_adress для отправки
        public string? Ip_adress { get; set; }
        public Ip_adres(string? ip, Int32? Port)
        {
            Ip_adress = ip;
            port = Port;
        }
    }

    //Класс настроек
    public class Sistem
    {
        //IP
        public string? IP { get; set; } = "";
        //PORT
        public static Int32? PORT { get; set; }
        //Путь до настроек
        public static string? path = Environment.CurrentDirectory.ToString();
        //Метод настрока
        public void Setting()
        {
            //Что то случичилось
            try
            {
                //Путь до файла 
                FileInfo? fileInfo = new FileInfo(path + "\\Tbot.json");
                // Ищем файл с настройками подключения
                if (fileInfo.Exists)
                {
                    using (MemoryStream? memory = new MemoryStream())
                    {

                        using (FileStream? fileStream = new FileStream("Tbot.json", FileMode.OpenOrCreate))
                        {
                            //Десерелизуем класс Ip_adres 
                            Connect_Client_? ip = JsonSerializer.Deserialize<Connect_Client_?>(fileStream);

                            //Заполняем Ip_adres
                            IP = ip?.IP;
                            //Заполняем port
                            PORT = ip?.Port;
                        }
                    }
                }
                else
                {
                    //Если нету файла то создаем новый!
                    using (FileStream? file = new FileStream("Tbot.json", FileMode.OpenOrCreate))
                    {
                        //Заполняем класс Ip_adres
                        Connect_Client_? ip_Adres = new Connect_Client_(9595, IPAddress.Loopback.ToString(), "");

                        //Серелизуем класс Ip_adres
                        JsonSerializer.Serialize<Connect_Client_?>(file, ip_Adres);
                    }

                    using (FileStream? file = new FileStream("Tbot.json", FileMode.OpenOrCreate))
                    {
                        
                        Connect_Client_? ip = JsonSerializer.Deserialize<Connect_Client_?>(file);

                        IP = ip?.IP;
                        //IP = ip_Adres.Ip_adress;

                        //Заполняем port
                        PORT = ip?.Port;
                    }
                }
                //IP = Ip_adress;
                //PORT = port;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }
}

