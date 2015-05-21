using System.Collections.Generic;
using System.Windows.Forms;
using BankAccountWithDatabaseApp.BLL;
using BankAccountWithDatabaseApp.Model;

namespace BankAccountWithDatabaseApp.UI
{
    public partial class CustomerAccountInfoUI : Form
    {
        public CustomerAccountInfoUI()
        {
            InitializeComponent();
        }
        Manager manager = new Manager();
        private void saveButton_Click(object sender, System.EventArgs e)
        {
            Account anAccount = new Account();

            anAccount.CustomerName = customerNameEntryTextBox.Text;
            anAccount.AccountNumber = accountNumberEntryTextBox.Text;
            anAccount.Email = customerEmailEntryTextBox.Text;
            anAccount.OpeningDate = openingDateEntryTextBox.Text;
            anAccount.Balance = 0;

            MessageBox.Show(manager.Save(anAccount));
            LoadAccountInfo();
        }

        private void depositButton_Click(object sender, System.EventArgs e)
        {
            Account account=new Account();
            account.AccountNumber = accountNumberForTransactionTextBox.Text;
            account.Balance =decimal.Parse(amountTextBox.Text) ;
           string message= manager.Deposit(account.AccountNumber, account.Balance);

            MessageBox.Show(message);
            LoadAccountInfo();
        }

        private void withdrawButton_Click(object sender, System.EventArgs e)
        {
            Account account = new Account();
            account.AccountNumber = accountNumberForTransactionTextBox.Text;
            account.Balance = decimal.Parse(amountTextBox.Text);
            string message = manager.Withdraw(account.AccountNumber, account.Balance);

            MessageBox.Show(message);
        LoadAccountInfo();
        }
       
        public void LoadAccountInfo()
        {
            List<Account> accounts =manager.LoadAccountListView();
            showInfoListView.Items.Clear();
            foreach (var account in accounts)
            {
                ListViewItem item = new ListViewItem(account.AccountNumber.ToString());
                item.SubItems.Add(account.CustomerName);
                // item.SubItems.Add(account.email);
                item.SubItems.Add(account.OpeningDate);
                item.SubItems.Add(account.Balance.ToString());


                showInfoListView.Items.Add(item);
                
            }
        }

        private void CustomerAccountInfoUI_Load(object sender, System.EventArgs e)
        {
            LoadAccountInfo();
        }

        private void searchButton_Click(object sender, System.EventArgs e)
        {
            string searchAccountNo = accountNumberToSearchTextBox.Text;
            List<Account> accounts = manager.LoadSearchesAccountInfo(searchAccountNo);
            showInfoListView.Items.Clear();
            foreach (var account in accounts)
            {
                ListViewItem item = new ListViewItem(account.AccountNumber.ToString());
                item.SubItems.Add(account.CustomerName);
                // item.SubItems.Add(account.email);
                item.SubItems.Add(account.OpeningDate);
                item.SubItems.Add(account.Balance.ToString());


                showInfoListView.Items.Add(item);

            }
        }
    }
}
