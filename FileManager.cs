using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankomatNoStatic
{
    internal class FileManager
    {
        public string AccountName { get; set; }
        public int AccountMoney { get; set; }

        public BankAccount ToBankAccount()
        {
            return new BankAccount(AccountName, AccountMoney);
        }
        public FileManager FromBankAccount(BankAccount bankAccount)
        {
            return new FileManager
            {
                AccountName = bankAccount.AccountName,
                AccountMoney = bankAccount.AccountMoney
            };
        }
        public string ReturnFileName()
        {
            return "AccountList";
        }
        public void WriteToFile(List<BankAccount> bankAccounts)
        {
            List<FileManager> files = new List<FileManager>();
            foreach (BankAccount bankAccount in bankAccounts)
            {
                FileManager fileManager = FromBankAccount(bankAccount);
                files.Add(fileManager);
            }
            string saveString = JsonSerializer.Serialize(files); //serializing the new list
            File.WriteAllText($@"{ReturnFileName()}.tmp.json", saveString); //write to a temp file
            File.Copy($@"{ReturnFileName()}.tmp.json", $@"{ReturnFileName()}.json", true); //copy temp file to actual file
        }
        public List<BankAccount> ReadFromFile(string fileName)
        {
            string loadString = File.ReadAllText($@"{ReturnFileName()}.json"); //load from file
            List<FileManager> files = JsonSerializer.Deserialize<List<FileManager>>(loadString) ?? new List<FileManager>(); //transform string into filemanager-format

            List<BankAccount> loadBankAccounts = new List<BankAccount>(); //use filemanager-format to create new bankaccount-list
            foreach (var file in files)
            {
                BankAccount bankAccount = file.ToBankAccount();
                loadBankAccounts.Add(bankAccount);
            }
            return loadBankAccounts;
        }
    }
}
