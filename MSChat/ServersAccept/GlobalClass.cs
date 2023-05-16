using Class_chat;
using Microsoft.Data.Sqlite;
using System;
namespace ServersAccept
{
    public class GlobalClass
    {
        //Команда для подключения к базе данных в файле (т.к. SQLite)
        public static string connectionString = "Data Source=usersdata.db";

        //Проверка есть ли пользователей в  базы данных
        public bool UserConnect { get; set; }

        //Проверяет добавляеться пользователей в  базе данных
        public bool User_Insert { get; set; }

        //Проверяет есть-ли  пользователей в  базе данных
        public bool User_Select_Chats { get; set; }

        //Проверяет  есть-ли  Переписка у пользователей 
        public bool Mess_Chats { get; set; }

        //Содержит id пользователя для проверки  
        public string Current_User { get; set; }

        //Заготовка для фото  добавление  
        //public byte[] Image_User { get; set; }

        //Класс - все аргументы пользователю даже id его изображениея
        public User_photo AUser { get; set; }

        //Класс - значение друзей  у   пользователя
        public User_photo[] List_Friend { get; set; }

        //Класс - чат  пользователя их сообщения в его чате
        public MessСhat[] aChatss { get; set; }

        //Заготовки 
        //public MessСhat List_Mess { get; set; }

        //Содержит id пользользователя для проверки сообщений
        public string Id_Users { get; set; }
        //Для пользователя
        public string Name { get; set; }

        ////Передают Имя  пользользователя и добавляет их список друзей
        public string Searh_Friend { get; set; }

        //Проверяет  Имя  пользользователя и есть ли он таблице пользователи
        public bool _Searh_Freind { get; set; }

        //Содержит id  2 пользользователя (Друга)
        public int Insert_Friend_by_id { get; set; }

        ////Заготовки
        //public int Id_Users_Name { get; set; }

        //Передают сколько пользователей в чате  и их сообщения
        public MessСhat[] Frends_Chat_Wath { get; set; }

        //Содержит id картинки для добавления пользователя картинки 
        public int Id_Image { get; set; }

        //Проверяет если у данного пользователя друзья 
        public bool Friends { get; set; }

        // Для картинки
        public UseImage Items_Image { get; set; }

        public Friends_Image Friends_Image { get; set; }
        /// <Блок процедур >

        public int Id { get; set; }

        //Создают таблицу пользователей 
        public void CreateTable_Users()
        {
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.CommandText = "CREATE TABLE IF NOT EXISTS Users (Id INTEGER NOT NULL UNIQUE," +
                                      "Age INTEGER," +
                                      "Name TEXT  NOT NULL UNIQUE," +
                                      "Password TEXT NOT NULL, " +
                                      "Image INTEGER ," +
                                      "DataMess datetime NOT NULL," +
                                      "Mark boolean NOT NULL," +
                                      "PRIMARY KEY(Id AUTOINCREMENT)" +
                                      "FOREIGN KEY(Image) REFERENCES Files(Id))";
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
        }

        //Создают таблицу друзей 
        public void CreateTable_Friends()
        {
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.CommandText = "CREATE TABLE IF NOT EXISTS Friends (Id INTEGER NOT NULL UNIQUE," +
                                      " IdUserFrom	INTEGER , " +
                                      "IdUserTo	INTEGER , " +
                                      //"Message TEXT NOT NULL," +
                                      "PRIMARY KEY(Id AUTOINCREMENT)" +
                                      "FOREIGN KEY(IdUserFrom) REFERENCES Users(Id), " +
                                      "FOREIGN KEY(IdUserTo) REFERENCES Users(Id))";
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
        }

        //Создают таблицу Чат 
        public void CreateTable_Chat()
        {
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.CommandText = "CREATE TABLE IF NOT EXISTS Chat (Id INTEGER NOT NULL UNIQUE," +
                                      " IdUserFrom	INTEGER , " +
                                      "IdUserTo	INTEGER , " +
                                      "Message TEXT NOT NULL," +
                                      "DataMess datetime NOT NULL," +
                                      "Image  INTEGER ," +
                                      "Mark boolean NOT NULL," +
                                      "PRIMARY KEY(Id AUTOINCREMENT)" +
                                      "FOREIGN KEY(IdUserFrom) REFERENCES Users(Id), " +
                                      "FOREIGN KEY(IdUserTo) REFERENCES Users(Id)" +
                                      "FOREIGN KEY(Image) REFERENCES Files(Id))";
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
        }

        //Создают таблицу Картинка 
        public void CreateTable_Files()
        {
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.CommandText = "CREATE TABLE IF NOT EXISTS Files (Id INTEGER NOT NULL UNIQUE," +
                                      "Name TEXT," +
                                      "Image BLOB NOT NULL," +
                                      "PRIMARY KEY(Id AUTOINCREMENT))";
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
        }


        //Заготовка
        /*   async public void Insert_Friends()
           {

               //string sq = " Select  " +
               //           "p.Id as 'Код', " +
               //           "c.name as 'Отправитель', " +
               //          "b.name as 'Получатель' " +
               //           "From Friends p " +
               //           "Join Users b ON p.IdUserFrom = b.id " +
               //           "Join Users c ON p.IdUserTo = c.id ";
               //using (var connection = new SqliteConnection(GlobalClass.connectionString))
               //{
               //    await connection.OpenAsync();
               //    SqliteCommand command = new SqliteCommand(sq, connection);
               //    await command.ExecuteNonQueryAsync();
               //    command.CommandText = sq;
               //}

           }

          //  string sq = $"INSERT INTO Users ( Messege, DataMess, Mark) VALUES ( {msg.ToString()}'{data}','{dateTime:s}','1')";
            and Password='{pasword}
        */


        //Добавляет пользователей и проверяет при  том что они там уже добавлены
        async public void Insert_User(string data, string pasword, string age, DateTime dateTime)
        {
            try
            {
                string sq = $"INSERT INTO Users (Age,Name,Image,Password,DataMess,Mark) VALUES ({age},'{data}','{Id_Image}','{pasword}','{dateTime:s}','1')";
                using (var connection = new SqliteConnection(GlobalClass.connectionString))
                {
                    await connection.OpenAsync();
                    SqliteCommand command = new SqliteCommand(sq, connection);
                    //        command.Parameters.Add(new SqliteParameter("@buf", buf));

                    await command.ExecuteNonQueryAsync();
                    command.CommandText = sq;
                }
            }
            catch
            {
                //Проверяет пользователей по имени при ошибки дабавления
                string sqlExpressio = $"SELECT * FROM Users  WHERE Name = '{data}'";

                using (var connection = new SqliteConnection(GlobalClass.connectionString))
                {
                    await connection.OpenAsync();
                    SqliteCommand command = new SqliteCommand(sqlExpressio, connection);
                    var n = await command.ExecuteReaderAsync();
                    if (n.HasRows == true)
                    {
                        Console.WriteLine("Такое имя уже есть");
                        User_Insert = true;
                    }
                    else
                    {
                        User_Insert = false;
                    }
                }
            }
        }

        //Добавляет Картинку в таблицу Files  и педают id картинки для пользователей
        async public void Insert_Image(byte[] buf)
        {
            try
            {
                string sq = $"INSERT INTO Files (Image) VALUES (@buf)";
                using (var connection = new SqliteConnection(GlobalClass.connectionString))
                {
                    await connection.OpenAsync();
                    SqliteCommand command = new SqliteCommand(sq, connection);
                    command.Parameters.Add(new SqliteParameter("@buf", buf));
                    await command.ExecuteNonQueryAsync();
                    command.CommandText = sq;
                    command.CommandText = "select last_insert_rowid()";
                    int lastId = Convert.ToInt32(command.ExecuteScalar());
                    //int number = command.ExecuteNonQuery();
                    Id_Image = lastId;

                }
            }
            catch
            {

            }
        }

        //Проверяет  в таблицу Пользователи  пользователя и пароль 
        async public void Select_Users(string data, string pasword)
        {
            //string Name = "";
            string sqlExpressio = $"SELECT * FROM Users  WHERE Name = '{data}' and Password='{pasword}'";

            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sqlExpressio, connection);
                SqliteCommand commandS = new SqliteCommand(sqlExpressio, connection);
                var n = await command.ExecuteReaderAsync();
                SqliteDataReader sqReader = commandS.ExecuteReader();

                if (n.HasRows == true)
                {
                    Console.WriteLine("Такое имя уже есть");
                    UserConnect = true;
                    // Always call Read before accessing data.
                    while (sqReader.Read())
                    {

                        Current_User = sqReader["Id"].ToString();
                        int Image = Convert.ToInt32(sqReader["Image"].ToString());
                        int Id = Convert.ToInt32(Current_User);
                        //  byte[] foto = null;
                        AUser = new User_photo(sqReader["Name"] as string, "", sqReader["Age"] as string, Image, Id, 0);

                        Console.WriteLine(Current_User);
                    }
                }
                else
                {

                    string sqlExpressi = $"SELECT * FROM Users  WHERE Name = '{data}'";
                    using (var connectio = new SqliteConnection(GlobalClass.connectionString))
                    {
                        await connectio.OpenAsync();
                        SqliteCommand _command = new SqliteCommand(sqlExpressi, connectio);
                        SqliteCommand __commandS = new SqliteCommand(sqlExpressi, connectio);
                        var ns = await _command.ExecuteReaderAsync();
                        SqliteDataReader sqReaders = __commandS.ExecuteReader();

                        if (ns.HasRows == true)
                        {
                            //   Console.WriteLine("Такое имя уже есть");
                            // UserConnect = true;
                            // Always call Read before accessing data.
                            while (sqReaders.Read())
                            {
                                //       Current_User = sqReader["Id"].ToString();
                                Id_Users = sqReaders["Id"].ToString();
                                //Еще будет нужна
                                //  int Id = Convert.ToInt32(Current_User);
                                string Friend = sqReaders["Name"].ToString();

                                Name = Friend;

                                //      Console.WriteLine(Current_User);
                            }
                        }
                        else
                        {

                        }
                    }
                }
            }



        }

        // Поиск и выборка картинки по ID 
        async public void Select_Image(Photo data)
        {
            //string Name = "";
            string sqlExpressio = $"SELECT * FROM Files  WHERE Id = '{data.Id}'";

            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sqlExpressio, connection);
                SqliteCommand commandS = new SqliteCommand(sqlExpressio, connection);
                var n = await command.ExecuteReaderAsync();
                SqliteDataReader sqReader = commandS.ExecuteReader();

                if (n.HasRows == true)
                {
                    //Console.WriteLine("Такое имя уже есть");
                    UserConnect = true;
                    // Always call Read before accessing data.
                    while (sqReader.Read())
                    {
                        object Im = sqReader["Image"];
                        //byte[] Image = Convert.FromBase64String(Im.ToString());
                        string StringImage = Convert.ToBase64String(Im as Byte[]);
                        string[] strings = new string[1];
                        strings[0] = StringImage;
                        UseImage useImage = new UseImage(strings, "true", 1);
                        Items_Image = useImage;
                    }
                }
                else
                {
                    UserConnect = false;
                }
            }
        }

        async public void Select_Image_Photo_Friends(Photo_Friends data)
        {
            //string Name = "";

            string sqlExpressi = $"SELECT COUNT(*) AS rec_count  FROM Files  WHERE Id in ({String.Join(",", data.Id)})";

            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sqlExpressi, connection);
                SqliteDataReader sqReader = command.ExecuteReader();
                //while (sqReader.Read())
                //{
                //    UserCount = sqReader.GetInt32(0);
                //}
                sqReader.Read();
                Id = Convert.ToInt32(sqReader["rec_count"].ToString());
            }


            string sqlExpressio = $"SELECT * FROM Files  WHERE Id in ({String.Join(",", data.Id)})";

            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sqlExpressio, connection);
                SqliteCommand commandS = new SqliteCommand(sqlExpressio, connection);
                var n = await command.ExecuteReaderAsync();
                SqliteDataReader sqReader = commandS.ExecuteReader();

                if (n.HasRows == true)
                {
                    //Console.WriteLine("Такое имя уже есть");
                    UserConnect = true;
                    // Always call Read before accessing data.
                    string[] strings = new string[Id];
                    int i = 0;
                    while (sqReader.Read())
                    {
                        //var Im = sqReader["Image"];
                        //byte[] Image = Convert.FromBase64String(Im.ToString());
                        //var a = Im as Byte[];
                        //var b =   Convert.ToBase64String(Im as Byte[]);
                        strings[i] = Convert.ToBase64String(sqReader["Image"] as Byte[]);
                        i++;
                    }
                    Friends_Image friends_ = new Friends_Image(strings, strings.Length);
                    //  UseImage useImage = new UseImage(strings, "true", 1);
                    Friends_Image = friends_;

                }
                else
                {
                    UserConnect = false;
                }
            }
        }

        //Проверяет  в таблицу Друзья количество друзей 1го пользователя
        async public void Select_Friend(string curent_user)
        {
            int UserCount = 0;
            string sqlExpressioCount = $"SELECT COUNT(*)  FROM Friends  WHERE IdUserFrom = '{curent_user}'";
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sqlExpressioCount, connection);
                SqliteCommand commandS = new SqliteCommand(sqlExpressioCount, connection);
                var n = await command.ExecuteReaderAsync();

                SqliteDataReader sqReader = commandS.ExecuteReader();
                if (n.HasRows == true)
                {
                    // UserCount = null;
                    while (sqReader.Read())
                    {
                        UserCount = sqReader.GetInt32(0);
                    }
                    Friends = true;
                }
                else
                {
                    Friends = false;
                }
            }

            if (Friends == true)
            {
                //Проверяет  в таблицу Друзья количество у данного пользователя по Id 
                string sqlExpressio = $"SELECT IdUserTo  FROM Friends  WHERE IdUserFrom = {curent_user}";
                int[] Frend = new int[UserCount];
                int i = 0;

                using (var connection = new SqliteConnection(GlobalClass.connectionString))
                {
                    await connection.OpenAsync();
                    SqliteCommand command = new SqliteCommand(sqlExpressio, connection);
                    SqliteCommand commandS = new SqliteCommand(sqlExpressio, connection);
                    var n = await command.ExecuteReaderAsync();
                    SqliteDataReader sqReader = commandS.ExecuteReader();
                    if (n.HasRows == true)
                    {
                        Console.WriteLine("У пользователя есть друзья");
                        while (sqReader.Read())
                        {
                            Frend[i] = sqReader.GetInt32(0);
                            i = i + 1;
                            //Проходим по созданию фильтра для таблицы Users
                        }
                        //Делаем запрос к таблице Users с фильтром  по всем id с фильтром  и считываем поля в массив List_Friend


                        //Проверяет  в таблицу Пользователи количество у id пользователей 
                        sqlExpressio = $"SELECT * FROM Users  WHERE Id in ({String.Join(",", Frend)})";
                        SqliteCommand commands_Fr = new SqliteCommand(sqlExpressio, connection);
                        SqliteCommand _commandS_Fr = new SqliteCommand(sqlExpressio, connection);
                        var _n = await commands_Fr.ExecuteReaderAsync();
                        SqliteDataReader sqReaders_Fr = _commandS_Fr.ExecuteReader();
                        if (_n.HasRows == true)
                        { //sqReader["Image"] as byte[]
                            int j = 0;
                            User_photo[] UserRG = new User_photo[UserCount];
                            while (sqReaders_Fr.Read())
                            {
                                int Id = Convert.ToInt32(sqReaders_Fr["Id"].ToString());
                                //  byte[] image = null;
                                int Image = Convert.ToInt32(sqReaders_Fr["Image"].ToString());

                                User_photo User = new User_photo(sqReaders_Fr["Name"] as string, "", sqReaders_Fr["Age"] as string, Image, Id, 0);
                                UserRG[j] = User;
                                j++;
                            }
                            Friends = true;

                            List_Friend = UserRG;
                            Friends = true;
                            Console.WriteLine(UserRG);
                        }
                        else
                        {
                            //друзей нет 
                            Friends = false;
                        }
                    }
                    else
                    {
                        Friends = false;
                    }
                }

            }
        }

        //Проверяет  в таблицу пользователи по имени пользователя
        async public void Select_From_Users(string data)
        {
            string sqlExpressi = $"SELECT * FROM Users  WHERE Name = '{data}'";
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sqlExpressi, connection);

                SqliteCommand commandS = new SqliteCommand(sqlExpressi, connection);
                var n = await command.ExecuteReaderAsync();
                //         // Заполняем Dataset
                SqliteDataReader sqReader = commandS.ExecuteReader();

                if (n.HasRows == true)
                {
                    while (sqReader.Read())
                    {

                    }
                    User_Select_Chats = true;
                }
                else
                {
                    User_Select_Chats = false;
                }
            }
        }

        //Проверяет  в таблицу Друзья  добавляет друзей по id 
        async public void Select_From_Users(string IdUserFrom, string IdUserTo)
        {
            Select_From_Users(IdUserFrom);
            Select_From_Users(IdUserTo);
            //Добавляет друзей  у первого пользователя
            string sq = $"INSERT INTO Friends ( IdUserFrom,IdUserTo) VALUES ('{IdUserTo}','{IdUserFrom}')";
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sq, connection);
                await command.ExecuteNonQueryAsync();
                command.CommandText = sq;
            }
            //Добавляет друзей  у второго пользователя
            string sqй = $"INSERT INTO Friends ( IdUserFrom,IdUserTo) VALUES ('{IdUserFrom}','{IdUserTo}')";
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sqй, connection);
                await command.ExecuteNonQueryAsync();
                command.CommandText = sqй;
            }
        }

        //Добавляет  в таблицу Чат  сообщение от пользователя 
        async public void Insert_Message(MessСhat messСhat)
        {
            string sq = $"INSERT INTO Chat ( IdUserFrom,IdUserTo,Message,DataMess,Mark) VALUES ({messСhat.IdUserFrom},{messСhat.IdUserTo},'{messСhat.Message}','{messСhat.DataMess:s}',{messСhat.Mark})";
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sq, connection);
                await command.ExecuteNonQueryAsync();
                command.CommandText = sq;
            }
            int UserCount = 0;
            //Проверяет количество записей  в таблицу Чат  сообщение от пользователя  1 до 2 и от 2 до 1 и х количество
            string sqlExpressioCount = $"SELECT COUNT(*) AS rec_count FROM Chat WHERE ((IdUserFrom = '{messСhat.IdUserFrom}' and IdUserTo = '{messСhat.IdUserTo}') or " +
                                                                                     $"(IdUserTo = '{messСhat.IdUserFrom}' and IdUserFrom = '{messСhat.IdUserTo}'))";
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sqlExpressioCount, connection);
                SqliteDataReader sqReader = command.ExecuteReader();
                //while (sqReader.Read())
                //{
                //    UserCount = sqReader.GetInt32(0);
                //}
                sqReader.Read();
                UserCount = Convert.ToInt32(sqReader["rec_count"].ToString());
            }
            //Проверяет количество записей  в таблицу Чат  сообщение от пользователя  1 до 2 и от 2 до 1 и их передает
            string sqlExpressio = $"SELECT *  FROM Chat  WHERE ((IdUserFrom = '{messСhat.IdUserFrom}' and IdUserTo = '{messСhat.IdUserTo}') or " +
                                                              $"(IdUserTo = '{messСhat.IdUserFrom}' and IdUserFrom = '{messСhat.IdUserTo}'))";
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sqlExpressio, connection);
                SqliteCommand command2 = new SqliteCommand(sqlExpressio, connection);
                var n = await command.ExecuteReaderAsync();
                //         // Заполняем Dataset
                SqliteDataReader sqReader = command2.ExecuteReader();
                // Always call Read before accessing data.
                if (n.HasRows == true)
                {
                    MessСhat[] aClats = new MessСhat[UserCount];
                    int k = 0;

                    while (sqReader.Read())
                    {
                        int Id_message = Convert.ToInt32(sqReader["Id"].ToString());
                        int IdUserFrom = Convert.ToInt32(sqReader["IdUserFrom"].ToString());
                        int IdUserTo = Convert.ToInt32(sqReader["IdUserTo"].ToString());
                        DateTime DataMess = Convert.ToDateTime(sqReader["DataMess"].ToString());
                        int Mark = Convert.ToInt32(sqReader["Mark"].ToString());
                        MessСhat mСhats = new MessСhat(Id_message, IdUserFrom, IdUserTo, sqReader["Message"] as string, DataMess, Mark);
                        //aChat = mСhat;
                        aClats[k] = mСhats;
                        k++;
                    }
                    Frends_Chat_Wath = aClats;
                }
                else
                {

                }
            }
        }

        //Обновляет  редактирования сообщение в чате у  1 до 2 и от 2 до 1 и их передает 
        async public void Update_Message(MessСhat messСhat)
        {
            DateTime dateTime = DateTime.Now;

            string sq = $"UPDATE Chat SET  IdUserFrom = '{messСhat.IdUserFrom}',IdUserTo = '{messСhat.IdUserTo}',Message ='{messСhat.Message}',DataMess ='{dateTime:s}',Mark = '{messСhat.Mark}' WHERE Id = '{messСhat.Id}'";
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sq, connection);
                await command.ExecuteNonQueryAsync();
                command.CommandText = sq;
            }
            int UserCount = 0;
            //Проверяет количество записей  в таблицу Чат  сообщение от пользователя  1 до 2 и от 2 до 1 и х количество
            string sqlExpressioCount = $"SELECT COUNT(*) AS rec_count FROM Chat WHERE ((IdUserFrom = '{messСhat.IdUserFrom}' and IdUserTo = '{messСhat.IdUserTo}') or " +
                                                                                    $" (IdUserTo = '{messСhat.IdUserFrom}' and IdUserFrom = '{messСhat.IdUserTo}'))";
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sqlExpressioCount, connection);
                SqliteDataReader sqReader = command.ExecuteReader();

                sqReader.Read();
                UserCount = Convert.ToInt32(sqReader["rec_count"].ToString());
            }
            //Проверяет количество записей  в таблицу Чат  сообщение от пользователя  1 до 2 и от 2 до 1 и их передает
            string sqlExpressio = $"SELECT *  FROM Chat  WHERE ((IdUserFrom = '{messСhat.IdUserFrom}' and IdUserTo = '{messСhat.IdUserTo}') or " +
                                                            $" (IdUserTo = '{messСhat.IdUserFrom}' and IdUserFrom = '{messСhat.IdUserTo}'))";
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sqlExpressio, connection);
                SqliteCommand command2 = new SqliteCommand(sqlExpressio, connection);
                var n = await command.ExecuteReaderAsync();
                SqliteDataReader sqReader = command2.ExecuteReader();
                // Always call Read before accessing data.
                if (n.HasRows == true)
                {
                    MessСhat[] aClats = new MessСhat[UserCount];
                    int k = 0;
                    while (sqReader.Read())
                    {
                        int Id_message = Convert.ToInt32(sqReader["Id"].ToString());
                        int IdUserFrom = Convert.ToInt32(sqReader["IdUserFrom"].ToString());
                        int IdUserTo = Convert.ToInt32(sqReader["IdUserTo"].ToString());
                        DateTime DataMess = Convert.ToDateTime(sqReader["DataMess"].ToString());
                        int Mark = Convert.ToInt32(sqReader["Mark"].ToString());
                        MessСhat mСhats = new MessСhat(Id_message, IdUserFrom, IdUserTo, sqReader["Message"] as string, DataMess, Mark);
                        aClats[k] = mСhats;
                        k++;
                    }
                    Frends_Chat_Wath = aClats;
                }
                else
                {

                }
            }
        }

        //Удаляет сообщение в чате  сообщение в чате у данного пользователя по 
        async public void Delete_Message_make_up(MessСhat messСhat)
        {
            string sqlExpression = $"DELETE   FROM Chat  WHERE Id = '{messСhat.Id}'";
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {

                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                await command.ExecuteNonQueryAsync();
                command.CommandText = sqlExpression;
            }
            int UserCount = 0;
            //Проверяет количество записей после удаления сообщения  в таблицу Чат   от пользователя  1 до 2 и от 2 до 1

            string sqlExpressioCount = $"SELECT COUNT(*) AS rec_count FROM Chat WHERE ((IdUserFrom = '{messСhat.IdUserFrom}' and IdUserTo = '{messСhat.IdUserTo}') or " +
                                                                                    $" (IdUserTo = '{messСhat.IdUserFrom}' and IdUserFrom = '{messСhat.IdUserTo}'))";
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sqlExpressioCount, connection);
                SqliteDataReader sqReader = command.ExecuteReader();
                sqReader.Read();
                UserCount = Convert.ToInt32(sqReader["rec_count"].ToString());
            }
            //Проверяет чат  после удаления сообщения  в таблицу Чат  сообщение от пользователя  1 до 2 и от 2 до 1 и их передает
            string sqlExpressio = $"SELECT *  FROM Chat  WHERE ((IdUserFrom = '{messСhat.IdUserFrom}' and IdUserTo = '{messСhat.IdUserTo}') or " +
                                                            $" (IdUserTo = '{messСhat.IdUserFrom}' and IdUserFrom = '{messСhat.IdUserTo}'))";
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sqlExpressio, connection);
                SqliteCommand command2 = new SqliteCommand(sqlExpressio, connection);
                var n = await command.ExecuteReaderAsync();
                SqliteDataReader sqReader = command2.ExecuteReader();
                if (n.HasRows == true)
                {
                    MessСhat[] aClats = new MessСhat[UserCount];
                    int k = 0;
                    while (sqReader.Read())
                    {
                        int Id_message = Convert.ToInt32(sqReader["Id"].ToString());
                        int IdUserFrom = Convert.ToInt32(sqReader["IdUserFrom"].ToString());
                        int IdUserTo = Convert.ToInt32(sqReader["IdUserTo"].ToString());
                        DateTime DataMess = Convert.ToDateTime(sqReader["DataMess"].ToString());
                        int Mark = Convert.ToInt32(sqReader["Mark"].ToString());
                        MessСhat mСhats = new MessСhat(Id_message, IdUserFrom, IdUserTo, sqReader["Message"] as string, DataMess, Mark);
                        aClats[k] = mСhats;
                        k++;
                    }
                    Frends_Chat_Wath = aClats;
                }
                else
                {

                }
            }
        }

        //Проверяет чат  количество записей  сообщение от пользователя  1 до 2 и от 2 до 1 и их передает
        async public void Select_Message_Users(User_photo data)
        {

            int UserCount = 0;
            string sqlExpressioCount = $"SELECT COUNT(*) AS rec_count FROM Chat " +
                                       $"WHERE ((IdUserTo = '{data.Id}' and IdUserFrom = '{data.Current}') or " +
                                       $" (IdUserFrom = '{data.Id}' and IdUserTo = '{data.Current}')) ";
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sqlExpressioCount, connection);
                SqliteDataReader sqReader = command.ExecuteReader();
                //while (sqReader.Read())
                //{
                //    UserCount = sqReader.GetInt32(0);
                //}
                sqReader.Read();
                UserCount = Convert.ToInt32(sqReader["rec_count"].ToString());
            }

            //Проверяет чат  сообщение от пользователя  1 до 2 и от 2 до 1 и их передает
            string sqlExpressi = $"SELECT  * FROM Chat WHERE ((IdUserTo = '{data.Id}' and IdUserFrom = '{data.Current}') or " +
                                                           $"(IdUserFrom = '{data.Id}' and IdUserTo = '{data.Current}'))";
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sqlExpressi, connection);
                SqliteCommand commandS = new SqliteCommand(sqlExpressi, connection);
                var n = await command.ExecuteReaderAsync();
                SqliteDataReader sqReader = commandS.ExecuteReader();

                if (n.HasRows == true)
                {
                    //int k = 0;
                    int j = 0;
                    //MessСhat  aChat ; // пока 10
                    MessСhat[] aChats = new MessСhat[UserCount];
                    while (sqReader.Read())
                    {
                        int Id = Convert.ToInt32(sqReader["Id"].ToString());
                        int IdIdUserFrom = Convert.ToInt32(sqReader["IdUserFrom"].ToString());
                        int IdUserTo = Convert.ToInt32(sqReader["IdUserTo"].ToString());
                        int Mark = Convert.ToInt32(sqReader["Mark"].ToString());
                        DateTime DataMess = Convert.ToDateTime(sqReader["DataMess"].ToString());

                        MessСhat mСhat = new MessСhat(Id, IdIdUserFrom, IdUserTo, sqReader["Message"] as string, DataMess, Mark);
                        //aChat = mСhat;
                        aChats[j] = mСhat;
                        j++;

                    }
                    //List_Mess = aChats;                         
                    aChatss = aChats;
                    Mess_Chats = true;

                }
                else
                {
                    //сообщений нет 
                    Mess_Chats = false;
                }
            }
        }

        //Проверяет по имени пользователя и передают его Id
        async public void Searh_Users(Searh_Friends data)
        {
            string sqlExpressio = $"SELECT * FROM Users  WHERE Name = '{data.Name}'";
            using (var connection = new SqliteConnection(GlobalClass.connectionString))
            {
                await connection.OpenAsync();
                SqliteCommand command = new SqliteCommand(sqlExpressio, connection);
                SqliteCommand commandS = new SqliteCommand(sqlExpressio, connection);
                var n = await command.ExecuteReaderAsync();
                SqliteDataReader sqReader = commandS.ExecuteReader();

                if (n.HasRows == true)
                {
                    // Always call Read before accessing data.
                    while (sqReader.Read())
                    {
                        //       Current_User = sqReader["Id"].ToString();
                        Insert_Friend_by_id = Convert.ToInt32(sqReader["Id"].ToString());
                        //Еще будет нужна
                        //  int Id = Convert.ToInt32(Current_User);
                        string Friend = sqReader["Name"].ToString();

                        Searh_Friend = Friend;

                        Console.WriteLine(Current_User);
                    }
                    _Searh_Freind = true;
                }
                else
                {
                    _Searh_Freind = false;
                }
            }
            //Проверяет по имени второго пользователя и передают его Id
            string sqlExpressi = $"SELECT * FROM Users  WHERE Name = '{data.User}'";
            using (var connectio = new SqliteConnection(GlobalClass.connectionString))
            {
                await connectio.OpenAsync();
                SqliteCommand _command = new SqliteCommand(sqlExpressi, connectio);
                SqliteCommand __commandS = new SqliteCommand(sqlExpressi, connectio);
                var ns = await _command.ExecuteReaderAsync();
                SqliteDataReader sqReaders = __commandS.ExecuteReader();

                if (ns.HasRows == true)
                {
                    //   Console.WriteLine("Такое имя уже есть");
                    // UserConnect = true;
                    // Always call Read before accessing data.
                    while (sqReaders.Read())
                    {
                        //       Current_User = sqReader["Id"].ToString();
                        Id_Users = sqReaders["Id"].ToString();
                        //Еще будет нужна
                        //  int Id = Convert.ToInt32(Current_User);
                        string Friend = sqReaders["Name"].ToString();

                        Searh_Friend = Friend;

                        Console.WriteLine(Current_User);
                    }
                }
                else
                {

                }
            }

            //Уже обработали ситуацию, когда _Searh_Freind=false т.е. не найден пользователь для добавления в друзья 
            //Проверяет друзей  у первого пользователя и у 2 пользователя 

            // SELECT * FROM Friends  WHERE IdUserFrom = {Insert_Friend_by_id} and IdUserTo ={Id_Users} and IdUserFrom = {Id_Users} and IdUserTo ={Insert_Friend_by_id}           
            string sqlE = $"SELECT * FROM Friends  WHERE ((IdUserFrom = {Insert_Friend_by_id} and IdUserTo ={Id_Users})" +
                                             $"or (IdUserFrom = {Id_Users} and IdUserTo ={Insert_Friend_by_id}))";

            using (var connections = new SqliteConnection(GlobalClass.connectionString))
            {
                await connections.OpenAsync();
                SqliteCommand command_ = new SqliteCommand(sqlE, connections);
                SqliteCommand commands_ = new SqliteCommand(sqlE, connections);
                //что то произошло
                var n1 = await command_.ExecuteReaderAsync();
                SqliteDataReader sqReader2 = commands_.ExecuteReader();

                // Always call Read before accessing data.
                if (n1.HasRows == true)
                {
                    while (sqReader2.Read())
                    {

                        //   string questions = sqReader["Id"];
                        //.Items.Add(questions);
                        // Console.WriteLine(questions);
                        //  Id_Users =  questions;

                    }
                    Console.WriteLine("Друг уже добавлен");
                }
                else
                {

                    //Добавляет друзей  у первого пользователя и 1 пользователя 
                    string sq = $"INSERT INTO Friends ( IdUserFrom,IdUserTo) VALUES ('{Insert_Friend_by_id}','{Id_Users}')";

                    SqliteCommand command = new SqliteCommand(sq, connections);
                    await command.ExecuteNonQueryAsync();
                    command.CommandText = sq;

                    //Добавляет друзей  у второго пользователя и 1 пользователя 
                    string sq1 = $"INSERT INTO Friends ( IdUserFrom,IdUserTo) VALUES ('{Id_Users}','{Insert_Friend_by_id}')";

                    SqliteCommand comman = new SqliteCommand(sq1, connections);
                    await comman.ExecuteNonQueryAsync();
                    comman.CommandText = sq1;

                }
                Id_Users = "";
                Insert_Friend_by_id = 0;
            }
        }
    }
    /// </summary>
}
