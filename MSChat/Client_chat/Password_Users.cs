using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Class_chat;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Drawing;
using System.Linq;

namespace Client_chat
{
    public partial class Password_Users : Form
    {
        public Password_Users()
        {
            InitializeComponent();
        }
        private const int ButtonSizeIncrease = 20;
        private Point _originalButtonLocation;
        private Size _originalButtonSize;
        public  User_regis[] List_Friend { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            User_create Регестрироваться = new User_create();
            Регестрироваться.Show();
            Hide();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                if (textBox1.Text == "Имя")
                { textBox1.Text = string.Empty; }
                else
                { textBox1.Text = textBox1.Text; }
            }
        }

        async  void Connect(String server, string fs, string command, string user, Form userpass)
        {
            using (TcpClient client = new TcpClient(server, ConnectSettings.port))
            {
                try
                {

                    DataContractJsonSerializer formater = new DataContractJsonSerializer(typeof(User_regis));


                    Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
                    string Host = System.Net.Dns.GetHostName();
                    NetworkStream stream = client.GetStream();

                    await stream.WriteAsync(data, 0, data.Length);
                    data = new Byte[99999999];
                    String responseData = String.Empty;
                    String responseDat = String.Empty;
                    // responseData = System.Text.Encoding.Default.GetString(data, 0, bytess);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        int cnt = 0;
                        Byte[] locbuffer = new byte[1024];
                        do
                        {
                            cnt = await stream.ReadAsync(locbuffer, 0, locbuffer.Length);
                            ms.Write(locbuffer, 0, cnt);
                        } while (client.Available > 0);

                        responseData = Encoding.Default.GetString(ms.ToArray());
                    }
                    if (responseData == "false")
                    {



                    }
                    else
                    {


                        User_photo person3 = JsonSerializer.Deserialize<User_photo>(responseData);


                        using (MemoryStream ms = new MemoryStream())
                        {
                            int cnt = 0;
                            Byte[] locbuffer = new byte[1024];
                            do
                            {
                                cnt = await stream.ReadAsync(locbuffer, 0, locbuffer.Length);
                                ms.Write(locbuffer, 0, cnt);
                            } while (client.Available > 0);

                            responseDat = Encoding.Default.GetString(ms.ToArray());
                        }

                        if (responseData == "false")
                        {



                        }
                        else
                        {

                            MsgFriends msgFriends = JsonSerializer.Deserialize<MsgFriends>(responseDat);


                            //Int32 bytess = await stream.ReadAsync(data, 0, data.Length);



                            //if (person3.Name == user)
                            //{
                            //    //MessageBox.Show("Подключение пользователя разрешено");
                            //    //получить перечень друзей
                            //    Int32 bytesFriend = await stream.ReadAsync(data, 0, 5);
                            //    responseDat = System.Text.Encoding.Default.GetString(data, 0, bytesFriend);
                            //    User_photo[] people = null;
                            //    if (responseDat == "false")
                            //    {



                            //    }
                            //    else
                            //    {
                            //        string result = responseDat.Trim(new char[] { '"', '0' });
                            //        Int32 it = Convert.ToInt32(result);
                            //        people = new User_photo[it];

                            //        Int32 bytesFriend1 = await stream.ReadAsync(data, 0, data.Length);
                            //        //Друзья
                            //        result = System.Text.Encoding.Default.GetString(data, 0, bytesFriend1);
                            //        string rez2 = result.Substring(0, result.IndexOf("}"));

                            //        List<string> tokens = new List<string>(result.Split('}'));

                            //        for (int j = 0; j < tokens.Count - 1; j++)
                            //        {
                            //            string tt = tokens[j] + "}";
                            //            people[j] = JsonSerializer.Deserialize<User_photo>(tt);
                            //        }
                            //    }
                            Chats_main a = new Chats_main();

                            a.Show();
                            a.OpenMes(person3, msgFriends);
                            userpass.Hide();


                            //}
                            //else
                            //{
                            //    MessageBox.Show("Пользователя нет");
                            //}
                        }
                    }
                }

                catch (ArgumentNullException)
                {
                    //Console.WriteLine("ArgumentNullException:{0}", e.Message);
                }
                catch (SocketException)
                {
                    //Console.WriteLine("SocketException: {0}", e.Message);
                }
                catch (Exception)
                {
                    //   Console.WriteLine("SocketException: {0}", e.Message);
                }
                
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                if (textBox2.Text == "Пароль")
                {
                    textBox2.Text = string.Empty;
                    textBox2.UseSystemPasswordChar = true;
                }
                else
                {
                    textBox2.Text = textBox2.Text;           
                    textBox2.PasswordChar = '*';

                }
            }
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.Width = _originalButtonSize.Width + ButtonSizeIncrease;
            button1.Height = _originalButtonSize.Height + ButtonSizeIncrease;
            button1.Location = new Point(_originalButtonLocation.X - ButtonSizeIncrease / 2,
                                          _originalButtonLocation.Y - ButtonSizeIncrease / 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (MemoryStream fs = new MemoryStream())
            {         
                string FileFS = "";
                UserLogin tom = new UserLogin(textBox1.Text, textBox2.Text);
                JsonSerializer.Serialize<UserLogin>(fs, tom);
                FileFS = Encoding.Default.GetString(fs.ToArray());   
                Connect(IP_ADRES.Ip_adress, FileFS, "003", textBox1.Text, this);

            }
        }

        private void Password_Users_Load(object sender, EventArgs e)
        {
            _originalButtonLocation = button1.Location;
            _originalButtonSize = button1.Size;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Size = _originalButtonSize;
            button1.Location = _originalButtonLocation;
        }
    }
}
