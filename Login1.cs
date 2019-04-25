using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Net;
using System.Web;
using System.Net.Mail;


namespace TwoFactor
{
    public partial class Login1 : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=F:\project\FullProject\MultiFactor\MultiFactor\Privacy.mdf;Integrated Security=True;User Instance=True");
        SqlCommand cmd;
        public string uname, accno;

        public string mailid1;

        public Login1()
        {
            InitializeComponent();
        }
        int coun;

        int i;
        public string mobile;
        string pass;
        public void sendmessage(string targetno, string message)
        {

            String query = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=fantasy5535&password=1163974702&sendername=Sample&mobileno=" + targetno + "&message=" + message;
            WebClient client = new WebClient();
            Stream sin = client.OpenRead(query);
            MessageBox.Show("Message Send To '" + targetno + "'");
        }

        string mail;

        private void button1_Click(object sender, EventArgs e)
        {

            if (System.IO.Directory.Exists(@"D:\"))
            {



                con.Open();
                cmd = new SqlCommand("select * from regtb1 where UserName='" + label3.Text + "' and PAssword2='" + textBox2.Text + "'  ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {





                    Random r = new Random();
                    i = r.Next(1111, 9999);
                    //  mobile = dr["mobileno"].ToString();

                    pass = "" + i.ToString();


                    const string sPath = "D:\\newdataset.txt";

                    System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);

                    SaveFile.WriteLine(Encrypt(i.ToString()));


                    SaveFile.Close();

                    mobile = dr["Mobileno"].ToString();
                    sendmessage(dr["Mobileno"].ToString(), "Next Time Your OTP:" + pass);

                    mail = dr["mailid"].ToString(); ;
                    string to = dr["mailid"].ToString();

                    string from = "sampletest685@gmail.com";
                    // string subject = "Key";
                    // string body = TextBox1.Text;
                    string password = "mailtest2";
                    using (MailMessage mm = new MailMessage(from, to))
                    {
                        mm.Subject = "Keys";
                        mm.Body = "YNext Time Your OTP:" + pass;
                        //if (fuAttachment.HasFile)
                        //{
                        //    string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
                        //    mm.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
                        //}
                        mm.IsBodyHtml = false;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(from, password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                        MessageBox.Show("Mail Send!");

                    }










                    dr.Close();

                    cmd = new SqlCommand("update regtb1 set  Password2='4235436234' where Accno='" + accno + "'  ", con);

                    //con.Open();
                    cmd.ExecuteReader();
                    // con.Close();


                    OTP_Password h = new OTP_Password();
                    h.uname = uname;
                    h.accno = accno;
                    h.Show();

                    this.Close();



                }
                else
                {



                    string text;
                    var fileStream = new FileStream(@"D:\\newdataset.txt", FileMode.Open, FileAccess.Read);
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        text = streamReader.ReadToEnd();
                    }


                    label5.Text = text;
                    label5.Text = Decrypt(text);


                    if (label5.Text == textBox2.Text)
                    {


                        Random r = new Random();
                        i = r.Next(1111, 9999);
                        //mobile = dr["mobileno"].ToString();

                        pass = "" + i.ToString();


                        const string sPath = "D:\\newdataset.txt";

                        System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);

                        SaveFile.WriteLine(Encrypt(i.ToString()));


                        SaveFile.Close();


                        sendmessage(mobile, "Next Time Your OTP:" + pass);

                        string to = mailid1;

                        string from = "sampletest685@gmail.com";
                        // string subject = "Key";
                        // string body = TextBox1.Text;
                        string password = "mailtest2";
                        using (MailMessage mm = new MailMessage(from, to))
                        {
                            mm.Subject = "Keys";
                            mm.Body = "YNext Time Your OTP:" + pass;
                            //if (fuAttachment.HasFile)
                            //{
                            //    string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
                            //    mm.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
                            //}
                            mm.IsBodyHtml = false;
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "smtp.gmail.com";
                            smtp.EnableSsl = true;
                            NetworkCredential NetworkCred = new NetworkCredential(from, password);
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(mm);

                            MessageBox.Show("Mail Send!");
                        }



                        OTP_Password h = new OTP_Password();
                        h.uname = uname;
                        h.accno = accno;
                        h.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("OTP Incorrect!");
                    }


                }


                con.Close();



            }
            else
            {
                MessageBox.Show("Please Insert Your Pendrive!");
            }





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


        private void Login1_Load(object sender, EventArgs e)
        {
            label3.Text = uname;


        }


        int ii;


        public string mob, em;

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("select * from regtb1 where UserName='" + label3.Text + "'   ", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Random rr = new Random();
                 ii = rr.Next(1111, 9999);

                 mob = dr["mobileno"].ToString();
                 em = dr["mailid"].ToString();

                sendmessage(dr["mobileno"].ToString(), "Your Forgot Password OTP:" + ii.ToString());

                string to = dr["mailid"].ToString();

                string from = "sampletest685@gmail.com";
                // string subject = "Key";
                // string body = TextBox1.Text;
                string password = "mailtest2";
                using (MailMessage mm = new MailMessage(from, to))
                {
                    mm.Subject = "Keys";
                    mm.Body = "Your Forgot Password : " + ii;
                    //if (fuAttachment.HasFile)
                    //{
                    //    string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
                    //    mm.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
                    //}
                    mm.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(from, password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);

                    MessageBox.Show("Mail Send!");
                }
             


            }
            else
            {
            }
            con.Close();








        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == ii.ToString())
            {


                if (System.IO.Directory.Exists(@"D:\"))
                {

                    Random r = new Random();
                    i = r.Next(1111, 9999);
                    //mobile = dr["mobileno"].ToString();

                    pass = "" + i.ToString();


                    const string sPath = "D:\\newdataset.txt";

                    System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);

                    SaveFile.WriteLine(Encrypt(i.ToString()));


                    SaveFile.Close();


                    sendmessage(mob, "Next Time Your OTP:" + pass);

                    string to = em;

                    string from = "sampletest685@gmail.com";
                    // string subject = "Key";
                    // string body = TextBox1.Text;
                    string password = "mailtest2";
                    using (MailMessage mm = new MailMessage(from, to))
                    {
                        mm.Subject = "Keys";
                        mm.Body = "YNext Time Your OTP:" + pass;
                        //if (fuAttachment.HasFile)
                        //{
                        //    string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
                        //    mm.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
                        //}
                        mm.IsBodyHtml = false;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(from, password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);

                        MessageBox.Show("Mail Send!");
                    }

                    
                }
                else
                {
                    MessageBox.Show("Please Insert Your Pendrive!");
                }




            }
            else
            {
                MessageBox.Show("OTP Incorrcet!");
            }
        }
    }
}
