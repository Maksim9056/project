using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
using System.Net.Sockets;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Class_chat;
//using ServersAccept;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
//using System.Runtime.InteropServices.ComTypes;
//using System.Runtime.CompilerServices;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using System.Runtime.InteropServices;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Client_chat
{
    public partial class Password_Users : Form
    {
        public Password_Users()
        {
            InitializeComponent();
        }

        public  User_regis[] List_Friend { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            User_create Регестрироваться = new User_create();
            Регестрироваться.Show();
            Hide();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                if (textBox2.Text == "Пароль")
                {
                    textBox2.Text = string.Empty;
                    textBox2.UseSystemPasswordChar = true;
                    textBox2.PasswordChar = '*';
                }
                else
                {

                    textBox2.Text = textBox2.Text;
                }
            }
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

        private void button1_Click(object sender, EventArgs e)
        {    
             string FileFS = "";
            using (MemoryStream fs = new MemoryStream())
            {            UserLogin tom = new UserLogin(textBox1.Text, textBox2.Text);

                    JsonSerializer.Serialize<UserLogin>(fs, tom);
                    FileFS = Encoding.Default.GetString(fs.ToArray());
             }        
          Connect(IP_ADRES.Ip_adress, FileFS, "003", textBox1.Text, this);
        }

        async  void Connect(String server, string fs, string command, string user, Form userpass)
        {     Int32 port = 9595;
            using (TcpClient client = new TcpClient(server, port))
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

                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    //    int cnt = 0;
                    //    Byte[] locbuffer = new byte[1024];
                    //    do
                    //    {
                    //        cnt = await stream.ReadAsync(locbuffer, 0, locbuffer.Length);
                    //        ms.Write(locbuffer, 0, cnt);
                    //    } while (client.Available > 0);

                    //    responseDat = Encoding.Default.GetString(ms.ToArray());
                    //}
                    //MsgInfo person32 = JsonSerializer.Deserialize<MsgInfo>(responseDat);
                    Int32 bytess = await stream.ReadAsync(data, 0, data.Length);

                    responseData = System.Text.Encoding.Default.GetString(data, 0, bytess);
                    User_photo person3 = JsonSerializer.Deserialize<User_photo>(responseData);

                    if (person3.Name == user)
                    {
                        //MessageBox.Show("Подключение пользователя разрешено");
                        //получить перечень друзей
                        Int32 bytesFriend = await stream.ReadAsync(data, 0, 5);
                        responseDat = System.Text.Encoding.Default.GetString(data, 0, bytesFriend);
                        User_photo[] people = null;
                        if (responseDat == "false")
                        {



                        }
                        else
                        {
                            string result = responseDat.Trim(new char[] { '"', '0' });
                            Int32 it = Convert.ToInt32(result);
                            people = new User_photo[it];
                            // int i =0;
                            Int32 bytesFriend1 = await stream.ReadAsync(data, 0, data.Length);
                            //Друзья
                            result = System.Text.Encoding.Default.GetString(data, 0, bytesFriend1);
                            string rez2 = result.Substring(0, result.IndexOf("}"));
                            //string rez2 = result.Insert(result.IndexOf("}") + 1, ",");
                            List<string> tokens = new List<string>(result.Split('}'));

                            for (int j = 0; j < tokens.Count - 1; j++)
                            {
                                string tt = tokens[j] + "}";
                                people[j] = JsonSerializer.Deserialize<User_photo>(tt);
                            }
                        }
                        Chats_main a = new Chats_main();

                        a.Show();
                        a.OpenMes(person3, people);
                        userpass.Hide();



                    }
                    else
                    {
                        MessageBox.Show("Пользователя нет");

                    }
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("ArgumentNullException:{0}", e.Message);
                }
                catch (SocketException e)
                {
                    Console.WriteLine("SocketException: {0}", e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("SocketException: {0}", e.Message);
                }
            }
        }
    }
}
