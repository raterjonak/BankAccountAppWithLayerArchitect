using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountWithDatabaseApp.Model;

namespace BankAccountWithDatabaseApp.DAL
{
    class Gateway
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["connectionDb"].ConnectionString;

        public int Save(Account anAccount)
        {
            string query = "INSERT INTO tbl_Account_Info VALUES ('" + anAccount.CustomerName + "', '" + anAccount.AccountNumber + "', '" +anAccount.Email +"', '" + anAccount.OpeningDate + "', '" + anAccount.Balance + "')";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }

        public decimal GetBalanceByAccountNumber(string acNO )
        {
            string query = "SELECT balanch FROM tbl_Account_Info WHERE accountNumber='"+acNO+"'";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();


            Account account = new Account();

            while (reader.Read())
            {

               
                account.Balance = decimal.Parse(reader["balanch"].ToString());

            }


            reader.Close();
            connection.Close();

            return account.Balance;

        }

        public int UpdateBalance(string accNo,decimal newBalance)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "Update  tbl_Account_Info Set balanch=" + newBalance + " Where accountNumber='" + accNo + "'";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;

        }

        public List<Account> ShowAllAccountInfo()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT AccountNumber,customerName,openingDate,balanch FROM tbl_Account_Info";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Account> accountList = new List<Account>();
            while (reader.Read())
            {
                Account account = new Account();
                account.AccountNumber = reader["AccountNumber"].ToString();
                account.CustomerName = reader["customerName"].ToString();
                account.OpeningDate = reader["openingDate"].ToString();
                account.Balance = decimal.Parse(reader["balanch"].ToString());
                //account.Email = reader["email"].ToString();

                accountList.Add(account);
            }

            reader.Close();
            connection.Close();

            return accountList;
        }
    }
}
