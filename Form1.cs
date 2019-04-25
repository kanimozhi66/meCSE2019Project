using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TwoFactor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Timer MyTimer = new Timer();
        private void Form1_Load(object sender, EventArgs e)
        {


            MyTimer.Interval = (1 * 60 * 1000); // 1 mint
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            // MessageBox.Show("The form will now be closed.", "Time Elapsed");
            DialogResult = MessageBox.Show("Session Time Out \n Please select one option Time Increase Yes/No)", "Conditional", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DialogResult == DialogResult.Yes)
            {
                MyTimer.Start();

                MessageBox.Show("The form will now be closed.", "Time Elapsed");
            }
            else
            {
                MyTimer.Stop();

                MessageBox.Show("The form will now be closed.", "Time Elapsed");

                this.Close();

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
