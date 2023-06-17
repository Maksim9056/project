using Class_chat;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_chat
{
    public partial class User_create : Form
    {
        public  OpenFileDialog open_dialog;
        public  byte[] buf;

        public User_create()
        {
            InitializeComponent();
        }


        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Имя")
            { textBox1.Text = string.Empty; }
            else
            { textBox1.Text = textBox1.Text; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

                if (textBox2.Text == textBox3.Text)
                {
                
                     using (MemoryStream Reg_user_Dispons = new MemoryStream())
                     {
                        CommandCL command = new CommandCL();
                        string FileFS = "";
                        using (MemoryStream fs = new MemoryStream())
                        {
                     
                         User_regis tom = new User_regis(textBox1.Text, textBox2.Text, textBox4.Text, buf, 0, 0);
                            JsonSerializer.Serialize<User_regis>(fs, tom);
                            FileFS = Encoding.Default.GetString(fs.ToArray());
                        }

                        //command.Reg_User(IP_ADRES.Ip_adress, FileFS, "002");
                        Task.Run(async () => await command.Reg_User(IP_ADRES.Ip_adress, FileFS, "002")).Wait();
                        if (User_reg.UserName == null)
                        {
                            MessageBox.Show("Выберите картинку  с меньшим размером!");

                        }
                        else
                        {
                            if (User_reg.UserName == textBox1.Text)
                            {
                                MessageBox.Show("Пользователь уже есть");
                            }
                            else
                            {
                                if(string.IsNullOrWhiteSpace(User_reg.UserName))
                                {

                                }
                                else
                                {
                                  MessageBox.Show("Добавление пользователя разрешено");
                                  this.Close();
                                }
                                
                            }

                            //using (Password_Users a = new Password_Users())
                            //{
                            //Chats_main parent = (Chats_main)this.Owner;
                            //parent.Show(); 
                            //a.ShowDialog();
                            //}

                        }
                        User_reg.UserName = null;
                    }
                }

                else
                {
                    MessageBox.Show($"Пароли не совпадают !");
                }
            }             
            //  Reg_User(IP_ADRES.Ip_adress, FileFS, "002", textBox1.Text, this);
        


        /*
        // Процедура отправки регистрации пользователей 002
        //async  void Reg_User(String server, string fs, string command, string user, Form userpass)
        //{
        //    try
        //    {
           

        //        using (TcpClient client = new TcpClient(server, ConnectSettings.port))
        //        {
        //            Byte[] data = System.Text.Encoding.Default.GetBytes(command + fs);
        //            NetworkStream stream = client.GetStream();

        //            await stream.WriteAsync(data, 0, data.Length);

        //            String responseData = String.Empty;

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
        //            }

        //            /*
        //            data = new Byte[999999990];
        //            Int32 bytess = await stream.ReadAsync(data, 0, data.Length);
        //            responseData = System.Text.Encoding.Default.GetString(data, 0, bytess);
        //            */
        /*
         * 
        //            if (responseData == user)
        //            {
        //                MessageBox.Show("Пользователь уже есть");
        //            }
        //            else
        //            {
        //                MessageBox.Show("Добавление пользователя разрешено");
        //                //using (Password_Users a = new Password_Users())
        //                //{
        //                   //Chats_main parent = (Chats_main)this.Owner;
        //                   //parent.Show();
        //                   userpass.Close();
                            
        //                    //a.ShowDialog();
        //                //}
        //            }
        //        }
        //    }
        */
    /*    //    catch (ArgumentNullException e)
        //    {
        //        Console.WriteLine("ArgumentNullException:{0}", e.Message);
        //    }
        //    catch (SocketException e)
        //    {
        //        Console.WriteLine("SocketException: {0}", e.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("SocketException: {0}", e.Message);
        //    }
        //}*/


        private void pictureBox2_Click(object sender, EventArgs e)
        {

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(openFileDialog2.FileName);
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox2.Image.Save(ms, ImageFormat.Png);
                    buf = ms.ToArray();
                    pictureBox2.BorderStyle = BorderStyle.None;
                }
            }
        }


        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58 || e.KeyChar == 127) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox2.Text == "Повторите пароль ")
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox3.Text == "Повторите пароль ")
            {
                textBox3.Text = string.Empty;
                textBox3.UseSystemPasswordChar = true;
            }
            else
            {
                textBox3.Text = textBox3.Text;
                textBox3.PasswordChar = '*';
            }
        }
    }
}
    