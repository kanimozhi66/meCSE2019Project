using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TwoFactor
{
    public partial class LoginType1 : Form
    {
        public LoginType1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Regsistration r = new Regsistration();
            r.method = "single";
            r.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Regsistration r = new Regsistration();
            r.method = "Multiple";
            r.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login_Type ll = new Login_Type();
            ll.Show();
        }
    }
}
