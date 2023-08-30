using System;
using System.Text;
using System.Windows.Forms;
using Class_chat;
using System.Text.Json;
using System.IO;
//using System.Collections.Generic;
//using System.Runtime.Serialization.Json;
using System.Drawing;
using System.Threading.Tasks;
//using System.Linq;
//using Newtonsoft.Json.Linq;
//using System.Net;

namespace Client_chat
{
    public partial class Password_Users : Form
    {
        public Password_Users()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Размео постояное значение
        /// </summary>
        private const int ButtonSizeIncrease = 20;

        /// <summary>
        /// Отрисовка
        /// </summary>
        private Point _originalButtonLocation;

        /// <summary>
        /// размерность 
        /// </summary>
        private Size _originalButtonSize;

        /// <summary>
        /// Регестрация
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            using (User_create Регестрироваться = new User_create())
            {
                //Переход в форму регестрация
                Регестрироваться.ShowDialog(this);
                //this.Hide();
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            //Проверяем имя на пустоту
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                if (textBox1.Text == "Имя")
                { textBox1.Text = string.Empty; }
                else
                { textBox1.Text = textBox1.Text; }
            }
        }



        /// <summary>
        /// При вводе пароля появляються * 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Проверяем на пустоту
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                //Есть ли значение пароль
                if (textBox2.Text == "Пароль")
                {
                    textBox2.Text = string.Empty;
                    textBox2.UseSystemPasswordChar = true;
                }
                else
                {
                    //присваеваем пароль   
                    textBox2.Text = textBox2.Text;
                    //Маскируем пароль *
                    textBox2.PasswordChar = '*';

                }
            }
        }

        /// <summary>
        /// Для увиличения кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            //Формула
            button1.Width = _originalButtonSize.Width + ButtonSizeIncrease;
            button1.Height = _originalButtonSize.Height + ButtonSizeIncrease;
            //отрисовка
            button1.Location = new Point(_originalButtonLocation.X - ButtonSizeIncrease / 2,
                                          _originalButtonLocation.Y - ButtonSizeIncrease / 2);
        }


        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (Password_Users Form = new Password_Users())
                {
                    //Проверяем пароль 
                    if (textBox2.Text == "")
                    {
                        MessageBox.Show("Пароль не заполнен!");
                    }
                    else
                    {
                        using (MemoryStream fs = new MemoryStream())
                        {
                            //Создаем  экземпляр класс CommandCL
                            CommandCL command = new CommandCL();
                            //Для класс серилизации используем для строки FileFS
                            string FileFS = "";
                            //Собрали класс UserLogin
                            UserLogin tom = new UserLogin(textBox1.Text, textBox2.Text,0);
                            //Серилизовали класс UserLogin в MemoryStream fs 
                            JsonSerializer.Serialize<UserLogin>(fs, tom);
                            //Декодировали в строку  MemoryStream fs   
                            FileFS = Encoding.Default.GetString(fs.ToArray());
                            /*
                                   //command.Check_User_Possword(IP_ADRES.Ip_adress, FileFS, "003");

                                   //var result = command.Check_User_Possword(IP_ADRES.Ip_adress, FileFS, "003").GetAwaiter().GetResult();

                                   //var result = Task.Run(async () => await command.Check_User_Possword(IP_ADRES.Ip_adress, FileFS, "003"));

                                   //var task = command.Check_User_Possword(IP_ADRES.Ip_adress, FileFS, "003");
                                   //var task = command.Check_User_Possword(IP_ADRES.Ip_adress, FileFS, "003");
                                   //task.Wait();*/
                            //Отправили и получили результат
                            Task.Run(async () => await command.Check_User_Possword(IP_ADRES.Ip_adress, FileFS, "003")).Wait();
                            //Проверяем есть ли пользователь 
                            if (CommandCL.User_Logins_and_Friends.User_ != null)
                            {
                                Chats_main a = new Chats_main();
                                Chats_main parent = (Chats_main)this.Owner;
                                parent.NotifyMe(CommandCL.User_Logins_and_Friends);
                                parent.SaveConfig(ConnectSettings.port, IP_ADRES.Ip_adress, CommandCL.User_Logins_and_Friends.User_.Name);
                                CommandCL.User_Logins_and_Friends = null;
                                this.Close();
                            }
                            //Проверяем количество друзей сдесь специально больше 1  
                            else if (CommandCL.User_Logins_and_Friends.List_Mess != 0)
                            {

                                MessageBox.Show("Пароль введен не верно!");
                            }
                            //Проверяем  не равен класс друзей == null
                            else if (CommandCL.User_Logins_and_Friends.AClass == null)
                            {
                                MessageBox.Show("Такой учетной записи нет");
                            }
                        }
                        Form.Close();
                    }
                }
            }
            catch
            {
                //
            }
        }

        /// <summary>
        /// Постояное значени кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Password_Users_Load(object sender, EventArgs e)
        {
            //Сдесь храним постояное значение
            _originalButtonLocation = button1.Location;
            //Сдесь храним постояное значение
            _originalButtonSize = button1.Size;
            //Сдесь храним постояное значение
            textBox1.Text = Connect_Client.UserName;
            
        }

         /// <summary>
         /// Уменьшает размер когда нету  курсора мыши
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            //Возращает прежний размер кнопки
            button1.Size = _originalButtonSize;
            //Возращает прежний размер кнопки
            button1.Location = _originalButtonLocation;
        }
    }
}
//// Передача 003
        //async void Check_User_Possword(String server, string fs, string command)
        //{
        //    using (TcpClient client = new TcpClient(server, ConnectSettings.port))
        //    {
        //        try
        //        {
                  
        //            byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
        //            string Host = System.Net.Dns.GetHostName();
        //            NetworkStream stream = client.GetStream();
        //            await stream.WriteAsync(data, 0, data.Length);
        //            //data = new Byte[99999999];
        //            String responseData = String.Empty;
        //            //String responseDat = String.Empty;                    
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                int cnt = 0;
        //                Byte[] locbuffer = new byte[1024];
        //                do
        //                {
        //                    cnt = await stream.ReadAsync(locbuffer, 0, locbuffer.Length);
        //                    ms.Write(locbuffer, 0, cnt);
        //                } while (client.Available > 0);
        //                responseData = Encoding.Default.GetString(ms.ToArray());  
        //               Clas(responseData);

        //            }
        //            if (responseData == "false")
        //            {
        //            }
        //            else
        //            {
        //                MsgUser_Logins person3 = JsonSerializer.Deserialize<MsgUser_Logins>(responseData);
        //                //        responseData
        //                Chats_main a = new Chats_main();
        //                Chats_main parent = (Chats_main)this.Owner;
        //                parent.NotifyMe(person3);
        //                parent.SaveConfig(ConnectSettings.port, IP_ADRES.Ip_adress, person3.User_.Name);
        //               // userpass.Close();
        //                /*
        //                //using (MemoryStream ms = new MemoryStream())
        //                //{
        //                //    int cnt = 0;
        //                //    Byte[] locbuffer = new byte[1024];
        //                //    do
        //                //    {
        //                //        cnt = await stream.ReadAsync(locbuffer, 0, locbuffer.Length);
        //                //        ms.Write(locbuffer, 0, cnt);
        //                //    } while (client.Available > 0);

        //                //    responseDat = Encoding.Default.GetString(ms.ToArray());
        //                //}          


        //                //if (responseData == "false")
        //                //{



        //                //}
        //                //else
        //                //{
        //                //  User_Logins msgFriends = JsonSerializer.Deserialize<User_Logins>(responseDat);
        //                /*                    //Int32 bytess = await stream.ReadAsync(data, 0, data.Length);



        //                                    //if (person3.Name == user)
        //                                    //{
        //                                    //    //MessageBox.Show("Подключение пользователя разрешено");
        //                                    //    //получить перечень друзей
        //                                    //    Int32 bytesFriend = await stream.ReadAsync(data, 0, 5);
        //                                    //    responseDat = System.Text.Encoding.Default.GetString(data, 0, bytesFriend);
        //                                    //    User_photo[] people = null;
        //                                    //    if (responseDat == "false")
        //                                    //    {



        //                                    //    }
        //                                    //    else
        //                                    //    {
        //                                    //        string result = responseDat.Trim(new char[] { '"', '0' });
        //                                    //        Int32 it = Convert.ToInt32(result);
        //                                    //        people = new User_photo[it];

        //                                    //        Int32 bytesFriend1 = await stream.ReadAsync(data, 0, data.Length);
        //                                    //        //Друзья
        //                                    //        result = System.Text.Encoding.Default.GetString(data, 0, bytesFriend1);
        //                                    //        string rez2 = result.Substring(0, result.IndexOf("}"));

        //                                    //        List<string> tokens = new List<string>(result.Split('}'));

        //                                    //        for (int j = 0; j < tokens.Count - 1; j++)
        //                                    //        {
        //                                    //            string tt = tokens[j] + "}";
        //                                    //            people[j] = JsonSerializer.Deserialize<User_photo>(tt);
        //                                    //        }
        //                                    //    }*/                 
        //                //a.Show();
        //                /*
        //                //a.OpenMes(person3.User_, person3);
        //                //userpass.Hide();
        //                //}
        //                //else
        //                //{
        //                //    MessageBox.Show("Пользователя нет");
        //                //}
        //                */
        //            }
        //        }
        //        catch (ArgumentNullException)
        //        {
        //            //Console.WriteLine("ArgumentNullException:{0}", e.Message);
        //        }
        //        catch (SocketException)
        //        {
        //            //Console.WriteLine("SocketException: {0}", e.Message);
        //        }
        //        catch (Exception)
        //        {
        //            //   Console.WriteLine("SocketException: {0}", e.Message);
        //        }

        //    }
        //}        */*/*/


    

        //

