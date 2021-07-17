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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            AccountCreation ac = new AccountCreation();
            ac.Show();
        }

        private void BtnTransfer_Click(object sender, EventArgs e)
        {
            Transfer tr = new Transfer();
            tr.Show();
        }

        private void BtnWithdraw_Click(object sender, EventArgs e)
        {
            Withdraw wd = new Withdraw();
            wd.Show();
        }

        private void BtnDeposit_Click(object sender, EventArgs e)
        {
            Deposit dep = new Deposit();
            dep.Show();
        }

        //private void BtnAcReview_Click(object sender, EventArgs e)
        //{
        //    AccountReview ar = new AccountReview();
        //    ar.Show();
        //}

        private void BtnExitApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //private void btnLoan_Click(object sender, EventArgs e)
        //{
        //    Loan ln = new Loan();
        //    ln.Show();
        //}

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
