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
    public partial class Transfer : Form
    {
        public Transfer()
        {
            InitializeComponent();
        }
        static string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnTransferAcToAc_Click(object sender, EventArgs e)
        {
            int fromAcNo = int.Parse(txtFromAcNo.Text);
            int toAcNo = int.Parse(txtToAcNo.Text);
            decimal amt = decimal.Parse(txtAmout.Text);

            con.Open();
            SqlTransaction transaction;
            transaction = con.BeginTransaction("Deposit");
            SqlCommand cmd = con.CreateCommand();
            cmd.Connection = con;
            cmd.Transaction = transaction;
            try
            {
                string qry = "insert into Transfers (FromAccountId,ToAccountId,Amount) values (@FromAccountId,@ToAccountId,@Amount)";
                cmd.CommandText = qry;
                cmd.Parameters.AddWithValue("@FromAccountId", fromAcNo);
                cmd.Parameters.AddWithValue("@ToAccountId", toAcNo);
                cmd.Parameters.AddWithValue("@Amount", amt);               
                cmd.ExecuteNonQuery();
                //sender
                qry = "update Account set Balance = Balance - @amtDeducted where AccountId = @AccountId2;";
                cmd.CommandText = qry;
                cmd.Parameters.AddWithValue("@AccountId2", fromAcNo);
                cmd.Parameters.AddWithValue("@amtDeducted", amt);
                cmd.ExecuteNonQuery();
                //receiver
                qry = "update Account set Balance = Balance + @amtcredited where AccountId = @AccountId3;";
                cmd.CommandText = qry;
                cmd.Parameters.AddWithValue("@AccountId3", toAcNo);
                cmd.Parameters.AddWithValue("@amtcredited", amt);
                cmd.ExecuteNonQuery();

                transaction.Commit();


                MessageBox.Show("Amount Transferred Successfully!");
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
