using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatNoStatic
{
    internal class Bank
    {
        public List<BankAccount> BankAccounts { get; set; }
        public FileManager FileManager { get; set; }

        public Bank()
        {
            BankAccounts = new List<BankAccount>();
            FileManager = new FileManager();
            if (File.Exists(@$"{FileManager.ReturnFileName()}.json")) //check if the file exists
            {
                FileManager fileManager = new FileManager();
                BankAccounts = fileManager.ReadFromFile(FileManager.ReturnFileName());
            }
            else
            {
                InitialBankAccounts();
            }
        }
        public List<BankAccount> InitialBankAccounts()
        {
            Random randomMoney = new Random();
            List<string> accountNames = new List<string>()
            {
                "100010", "100011", "100012", "100013", "100014", "100015", "100016", "100017", "100018", "100019"
            };
            foreach (string accountName in accountNames)
            {
                BankAccounts.Add(new BankAccount(accountName, randomMoney.Next(100, 10001)));
            }
            return BankAccounts;
        }
        public void CreateAccount(string accountName, int accountMoney)
        {
            BankAccounts.Add(new BankAccount(accountName, accountMoney));
        }
        public void DeleteAccount(int indexNumber)
        {
            BankAccounts.RemoveAt(indexNumber);
        }
        public void UpdateBank()
        {
            FileManager.WriteToFile(BankAccounts);
        }
    }
}
