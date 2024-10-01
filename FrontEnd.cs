using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankomatNoStatic
{
    internal class FrontEnd
    {
        public void DisplayAllAccounts(Bank bank)
        {
            Console.Clear();
            bank.BankAccounts.ForEach(account =>
            {
                DisplaySingleAccount(bank.BankAccounts.IndexOf(account), bank);
            });
        }
        public void DisplaySingleAccount(int indexNumber, Bank bank)
        {
            CultureInfo svCulture = new CultureInfo("sv-SE");
            BankAccount singleAccount = bank.BankAccounts.ElementAt(indexNumber); //shortening the "name" for easier use
            Console.WriteLine(singleAccount.AccountName.PadRight(10) + singleAccount.AccountMoney.ToString("C00", svCulture).PadLeft(20));
        }
        public string InputString()
        {
            int nameLength = 3;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please input the account you're looking for.");
                Console.CursorVisible = true;
                string inputString = Console.ReadLine();
                if (inputString.Length >= nameLength)
                {
                    return inputString;
                }
                else
                {
                    Console.WriteLine($"Please make your account-name at least {nameLength} characters long.");
                }
            }
        }
        public int InputInt()
        {
            while (true)
            {
                Console.CursorVisible = true;
                try
                {
                    int accountMoney = Convert.ToInt32(Console.ReadLine());
                    if (accountMoney < 0)
                    {
                        Console.WriteLine("You can't use negative numbers");
                    }
                    else
                    {
                        return accountMoney;
                    }
                }
                catch
                {
                    Console.CursorVisible = false;
                    Console.WriteLine("Please only input numbers.");
                }
            }
        }
        public void CreateNewBankAccount(Bank bank)
        {
            Console.WriteLine("Name for the new account: ");
            Console.CursorVisible = true;
            string accountName = InputString();
            Console.WriteLine("Amount of money on the account: ");
            int accountMoney = InputInt();
            bank.BankAccounts.Add(new BankAccount(accountName, accountMoney));
        }
        public int DeleteCertain()
        {
            Console.CursorVisible = true;
            Console.WriteLine("Are you sure that you want to delete this account?");
            Console.Write("y/n: ");
            string? answer = Console.ReadLine();
            if (answer == "yes" || answer == "y")
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

    }
}
