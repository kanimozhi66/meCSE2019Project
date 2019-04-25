using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace TwoFactor
{
    public partial class Admin_Home : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=F:\project\FullProject\MultiFactor\MultiFactor\Privacy.mdf;Integrated Security=True;User Instance=True");
        SqlCommand cmd;

        public Admin_Home()
        {
            InitializeComponent();
        }

        private void userDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label3.Text = "User Transaction";
            
            cmd = new SqlCommand("select Name,Age,Address,MobileNo,UserName,Accno from regtb1 ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
        }

        private void userRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label3.Text = "Account Transaction";
            cmd = new SqlCommand("select* from transtb ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
        }

        private void fileUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //File_Upload fu = new File_Upload();
            //fu.Show();
        }

        private void sendProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Send_Project sp = new Send_Project();
            //sp.Show();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
