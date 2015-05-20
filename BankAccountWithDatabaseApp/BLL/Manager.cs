using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankAccountWithDatabaseApp.DAL;
using BankAccountWithDatabaseApp.Model;

namespace BankAccountWithDatabaseApp.BLL
{
    class Manager
    {
        Gateway gateway = new Gateway();

        public string Save(Account anAccount)
        {
            if (anAccount.AccountNumber.Length>7)
            {
                int value = gateway.Save(anAccount);

                if (value > 0)
                    return "Saved successfully";
                else
                {
                    return "Save failed";
                }
            }
            else
            {
                return "Account Number Should be atleast 8 character long.";
            }
           
        }

        public string Deposit(string accountNo,decimal amount)
        {
            decimal balane = gateway.GetBalanceByAccountNumber(accountNo);
            balane += amount;
            int value=gateway.UpdateBalance(accountNo,balane);

            if (amount>0)
            {
                
           

            if (value>0)
            {
                return "Deposite successful";
            }
            else
            {
                return "Deposit Fail.";
            }
            }
            else
            {
                return "Amount should be positive ";
            }
        }
        public string Withdraw(string accountNo, decimal amount)
        {
            decimal balane = gateway.GetBalanceByAccountNumber(accountNo);

            if (amount > 0)
            {


                if (amount < balane)
                {


                    balane -= amount;
                    int value = gateway.UpdateBalance(accountNo, balane);

                    if (value > 0)
                    {
                        return "Withdraw successful";
                    }
                    else
                    {
                        return "withdraw Fail.";
                    }
                }
                else
                {
                    return "Not enough balanc.";
                }
            }

            else
            {
                return "amount should be positive value.";
            }
        }

        public List<Account> LoadAccountListView()
        {
            return gateway.ShowAllAccountInfo();
        }

    }
}
