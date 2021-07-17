using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banking_System
{
    public partial class Withdraw : Form
    {
        public Withdraw()
        {
            InitializeComponent();
        }
        static string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        SqlDataReader sdr;
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from Account where AccountId = @AccountId;";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AccountId", int.Parse(txtAcNo.Text));
                con.Open();
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    txtBalance.Text = sdr[4].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sdr.Close();
                con.Close();
            }
        }

        private void BtnDeposit_Click(object sender, EventArgs e)
        {

        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void btnWithdraw2_Click(object sender, EventArgs e)
        {

        }

        private void BtnWithdraw2_Click(object sender, EventArgs e)
        {
            int acno = int.Parse(txtAcNo.Text);
            decimal balance = decimal.Parse(txtBalance.Text);
            decimal withdraw = decimal.Parse(txtWithdraw.Text);

            con.Open();
            SqlTransaction transaction;
            transaction = con.BeginTransaction("Deposit");
            SqlCommand cmd = con.CreateCommand();
            cmd.Connection = con;
            cmd.Transaction = transaction;
            try
            {
                string qry = "insert into Transactions (AccountId,balance,deposit,withdraw) values (@AccountId,@balance,@deposit,@withdraw)";
                cmd.CommandText = qry;
                cmd.Parameters.AddWithValue("@AccountId", acno);
                cmd.Parameters.AddWithValue("@balance", balance);
                cmd.Parameters.AddWithValue("@deposit", 0M);
                cmd.Parameters.AddWithValue("@withdraw", withdraw);
                cmd.ExecuteNonQuery();

                qry = "update Account set Balance = @updatedBalance where AccountId = @AccountId2;";
                cmd.CommandText = qry;
                cmd.Parameters.AddWithValue("@AccountId2", acno);
                cmd.Parameters.AddWithValue("@updatedBalance", balance - withdraw);
                cmd.ExecuteNonQuery();
                transaction.Commit();
                MessageBox.Show("Amount withdrawn Successfully!");
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.ToString());
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.ToString());
                }
            }
            finally
            {
                con.Close();
            }
        }
    }
}
