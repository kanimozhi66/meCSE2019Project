using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TwoFactor
{
    public partial class MiniSatement : Form
    {

        public string uname,accno;

        public MiniSatement()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=F:\project\FullProject\MultiFactor\MultiFactor\Privacy.mdf;Integrated Security=True;User Instance=True");
            SqlCommand cmd;
            cmd = new SqlCommand("select * from transtb where Date between '" + dateTimePicker1.Text + "' and '" + dateTimePicker2.Text + "' and username='" + uname + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
        }
    }
}
