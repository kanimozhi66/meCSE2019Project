using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SmsClient;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.IO;
using System.Text;


namespace TwoFactor
{
    public partial class Login_Type : Form
    {

        SqlConnection mycon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=F:\project\FullProject\MultiFactor\MultiFactor\Privacy.mdf;Integrated Security=True;User Instance=True");
        SqlCommand mycmd;
        double tot, l1, l2, l3, tot1 = 0;
        static int cnt = 1;
        String password = "";
        DateTime keyUpTime, keyDownTime;
        byte[] asciibyte;
        private List<double> keyDownKeyUpMeasurements = new List<double>();
        private List<double> keyDownKeyDownMeasurements = new List<double>();
        private List<double> keyUpKeyDownMeasurements = new List<double>();

        public Login_Type()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //textBox1.Text = "";
            //textBox2.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {

          




        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           //Form1   f = new Form1();
           // f.Show();

        }



       

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            string text = label17.Text;
            asciibyte = Encoding.ASCII.GetBytes(text);
            foreach (byte b in asciibyte)
            {
                label18.Text = b.ToString();
                tot1 += Convert.ToDouble(label18.Text);
                label18.Text = tot1.ToString();
            }


            l1 = Convert.ToDouble(label14.Text);
            l2 = Convert.ToDouble(label15.Text);
            l3 = Convert.ToDouble(label10.Text);

            tot = Convert.ToDouble(label14.Text) + Convert.ToDouble(label15.Text) + Convert.ToDouble(label10.Text);

        }

        private void keydown(object sender, KeyEventArgs e)
        {
            DateTime prevKeyDownTime = keyDownTime;
            keyDownTime = DateTime.Now;

            if (prevKeyDownTime != null)
            {
                keyDownKeyDownMeasurements
                    .Add(keyDownTime.Subtract(prevKeyDownTime).TotalMilliseconds);
                label14.Text = keyDownTime.Subtract(prevKeyDownTime).Milliseconds.ToString();

            }

            if (keyUpTime != null)
            {
                keyUpKeyDownMeasurements
                    .Add(keyDownTime.Subtract(keyUpTime).TotalMilliseconds);
                label15.Text = keyDownTime.Subtract(keyUpTime).Milliseconds.ToString();
            }
        }

        private void keypress(object sender, KeyPressEventArgs e)
        {
            string test = e.KeyChar.ToString();
            label17.Text = test.ToString();
        }


        private void keyup(object sender, KeyEventArgs e)
        {
            keyUpTime = DateTime.Now;
            keyDownKeyUpMeasurements
                .Add(keyUpTime.Subtract(keyDownTime).TotalMilliseconds);

            label10.Text = keyUpTime.Subtract(keyDownTime).TotalMilliseconds.ToString();
        }


        string mobile,pass;
        int i;
        private void button3_Click(object sender, EventArgs e)
        {

            if (comboBox1.Text == "Admin")
            {

                if (textBox7.Text == "admin" & textBox8.Text == "admin")
                {
                    Admin_Home ah = new Admin_Home();
                    ah.Show();

                }
            }
            else
            {




                mycon.Open();
                mycmd = new SqlCommand("select * from regtb1 where  UserName='" + textBox7.Text + "' and Password='" + textBox8.Text + "' and (passac='" + label17.Text + "' and  pass1='" + label14.Text + "' or pass2='" + label15.Text + "' or pass3='" + label18.Text + "'  or total='" + label10.Text + "')", mycon);
                SqlDataReader dr = mycmd.ExecuteReader();

                if (dr.Read())
                {

                    MessageBox.Show("Loging step1 process is completed");

                    if (dr["keys"].ToString() == "Single key")
                    {
                        UserHome h = new UserHome();
                        h.uname = textBox7.Text;
                        h.accno = dr["Accno"].ToString();
                        h.Show();

                    }

                    else
                    {

                        Login1 ll = new Login1();
                        ll.accno = dr["Accno"].ToString();
                        ll.uname = textBox7.Text;
                        ll.mobile = dr["mobileno"].ToString();
                        ll.mailid1 = dr["mailid"].ToString();
                        ll.Show();

                        this.Close();

                    }

                }

                else
                {
                    MessageBox.Show("plz given correct Password");


                    dr.Close();

                   // mycon.Open();
                    mycmd = new SqlCommand("select * from regtb1 where  UserName='" + textBox7.Text + "' ", mycon);
                    SqlDataReader dr1 = mycmd.ExecuteReader();

                    if (dr1.Read())
                    {


                        sendmessage(dr1["mobileno"].ToString(), "Unknow User Acces Your Acoount");

                    }


                }
                mycon.Close();

            }


        }

      

        public void sendmessage(string targetno, string message)
        {

            String query = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=fantasy5535&password=1163974702&sendername=Sample&mobileno=" + targetno + "&message=" + message;
            WebClient client = new WebClient();
            Stream sin = client.OpenRead(query);
            MessageBox.Show("Message Send To '" + targetno + "'");
        }
        private void button5_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
            textBox8.Text = "";
            label15.Text = "0";
            label17.Text = "0";
            label14.Text = "0";
            label10.Text = "0";
            label18.Text = "0";
        }


        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }


        private string Encrypt(string clearText)
        {
            string ECC = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(ECC, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }


       

    }
}
