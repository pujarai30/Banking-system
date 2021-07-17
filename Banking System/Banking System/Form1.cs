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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int attempts;
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string userName, passWord;
            userName = txtUserName.Text;
            passWord = txtPassWord.Text;
            attempts++;
            if (attempts > 3)
            {
                MessageBox.Show("System Blocked!");
                Application.Exit();
            }
            if (userName == "" && passWord == "")
            {
                lblLoginStat.Text = "UserName & Password can't be left Blank!";
            }
            else if (userName.Length > 10 && passWord.Length > 10)
            {
                lblLoginStat.Text = "UserName or Password Are too Long! Max 10 characters allowed";
            }
            else
            {
                if (userName == "admin" && passWord == "admin")
                {
                    //lblLoginStat.Text = "Login Successful!";
                    ProgressBar pr = new ProgressBar();
                    this.Hide();
                    pr.Show();
                }
                else
                {
                    lblLoginStat.Text = "Invalid Login and Password!";
                    txtUserName.Clear();
                    txtPassWord.Clear();
                    txtUserName.Focus();
                }
            }            
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            lblLoginStat.Text = "";
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
