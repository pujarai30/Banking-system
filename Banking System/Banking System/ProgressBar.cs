using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banking_System
{
    public partial class ProgressBar : Form
    {
        public ProgressBar()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value++;
            if (progressBar1.Value >= 99)
            {
                MainForm m = new MainForm();
                this.Hide();
                m.Show();
                timer1.Enabled = false;
                progressBar1.Value -= 1;
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void ProgressBar_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
