using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Banking_System
{
    public partial class AccountCreation : Form
    {
        public AccountCreation()
        {
            InitializeComponent();
        }

        public void GetCustomerId()
        {
            //string cs = "Data Source=LENOVO-PC\\SQLEXPRESS;Initial Catalog=IndiaBank;Integrated Security=True";
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "select max(CustomerId) from Customer;";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                string val = sdr[0].ToString();
                if (val == "")
                {
                    lblCustomerId.Text = "10001";
                }
                else
                {
                    int a = int.Parse(val) + 1;
                    lblCustomerId.Text = a.ToString();
                }
            }
            sdr.Close();
            con.Close();
            //Get A/c No
            query = "select max(AccountId) from Account;";
            cmd = new SqlCommand(query, con);
            con.Open();
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                string val = sdr[0].ToString();
                if (val == "")
                {
                    lblAcNo.Text = "20001";
                }
                else
                {
                    int a = int.Parse(val) + 1;
                    lblAcNo.Text = a.ToString();
                }
            }
            sdr.Close();
            con.Close();
        }

        private void AccountCreation_Load(object sender, EventArgs e)
        {
            GetCustomerId();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string custId, fName, lName, street, city, stateName, phone, email, acno, accountType, acDescription, balance;
            custId = lblCustomerId.Text;
            fName = txtFirstName.Text;
            lName = txtLastName.Text;
            street = txtStreet.Text;
            city = txtCity.Text;
            stateName = txtState.Text;
            phone = txtPhone.Text;
            email = txtEmail.Text;
            acno = lblAcNo.Text;
            accountType = txtAccountType.Text;
            acDescription = txtDescription.Text;
            balance = txtBalance.Text;

            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlTransaction transaction;
            transaction = con.BeginTransaction("AcCreate");
            SqlCommand cmd = con.CreateCommand();
            cmd.Connection = con;
            cmd.Transaction = transaction;
            try
            {   
                string qry = "insert into Customer (CustomerId,Firstname,Lastname,Street,City,StateName,Phone,Email) values (@CustomerId,@Firstname,@Lastname,@Street,@City,@StateName,@Phone,@Email)";
                cmd.CommandText = qry;
                cmd.Parameters.AddWithValue("@CustomerId", int.Parse(custId));
                cmd.Parameters.AddWithValue("@Firstname", fName);
                cmd.Parameters.AddWithValue("@Lastname", lName);
                cmd.Parameters.AddWithValue("@Street", street);
                cmd.Parameters.AddWithValue("@City", city);
                cmd.Parameters.AddWithValue("@StateName", stateName);
                cmd.Parameters.AddWithValue("@Phone", decimal.Parse(phone));
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.ExecuteNonQuery();

                qry = "insert into Account (AccountId,CustomerId,AccountType,AcDescription,Balance) values (@AccountId,@CustomerId2,@AccountType,@AcDescription,@Balance)";
                cmd.CommandText = qry;
                cmd.Parameters.AddWithValue("@AccountId", int.Parse(acno));
                cmd.Parameters.AddWithValue("@CustomerId2", int.Parse(custId));
                cmd.Parameters.AddWithValue("@AccountType", accountType);
                cmd.Parameters.AddWithValue("@AcDescription", acDescription);
                cmd.Parameters.AddWithValue("@Balance", decimal.Parse(balance));
                cmd.ExecuteNonQuery();
                transaction.Commit();
                MessageBox.Show("Records Added Successfully!");
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



        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnTabCustomer_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void BtnTabAccount_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }
    }
}
