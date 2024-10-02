using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BankomatNoStatic
{
    internal class ATM
    {
        public Bank Bank { get; set; }
        public FrontEnd FrontEnd { get; set; }
        public Menu Menu { get; set; }
        public ATM(Bank bank)
        {
            Bank = bank;
        }

        public void MainMenu()
        {
            FrontEnd = new FrontEnd();
            Menu = new Menu(new List<string> { "" });
            Boolean runProgram = true;
            while (runProgram)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Menu.StartingMenu();
                int menuSelected = Menu.MenuSelection();
                int indexNumber = 0;

                switch (menuSelected)
                {
                    case 0: //Display all accounts
                        FrontEnd.DisplayAllAccounts(Bank);
                        Console.ReadLine();
                        break;
                    case 1: //View account
                        indexNumber = Menu.AllAccountsMenu(Bank.BankAccounts); //select the account from menu-list
                        menuSelected = Menu.SingleAccountMenuSelector(menuSelected, Bank); //do something?
                        EditSelectedAccount(indexNumber, menuSelected); //does the thing
                        break;
                    case 2: //Find account
                        string searchedName = FrontEnd.InputString();//input method
                        int searchResult = SearchForAccount(searchedName); //search method
                        if (searchResult > 0) //if it exists -> display, otherwise return to menu
                        {
                            menuSelected = Menu.SingleAccountMenuSelector(menuSelected, Bank); //do something?
                            EditSelectedAccount(searchResult, menuSelected);
                        }
                        break;
                    case 3: //Add to account ->
                        indexNumber = Menu.AllAccountsMenu(Bank.BankAccounts); //select the account from menu-list
                        EditSelectedAccount(indexNumber, 1);
                        break;
                    case 4: //Remove from account ->
                        indexNumber = Menu.AllAccountsMenu(Bank.BankAccounts); //select the account from menu-list
                        EditSelectedAccount(indexNumber, 2);
                        break;
                    case 5: //Sort accounts
                        SortBankAccounts();
                        break;
                    case 6: //Add new account -> 
                        FrontEnd.CreateNewBankAccount(Bank);
                        break;
                    case 7: //Delete account ->
                        indexNumber = Menu.AllAccountsMenu(Bank.BankAccounts); //select the account from menu-list
                        EditSelectedAccount(indexNumber, 3);
                        break;
                    case 8: //Quit -> 
                        Bank.UpdateBank();
                        runProgram = false;
                        break;
                }
            }
        }
        public int SearchForAccount(string searchedName)
        {
            foreach (BankAccount bankAccount in Bank.BankAccounts)
            {
                if (bankAccount.AccountName == searchedName)
                {
                    return Bank.BankAccounts.IndexOf(bankAccount);
                }
            }
            Console.WriteLine("There is no account by that name.");
            Console.ReadLine();
            return -1;
        }
        public void EditSelectedAccount(int indexNumber, int decision)
        {
            switch (decision)
            {
                case 0: //return
                    break;
                case 1: //deposit -> 
                    Console.Clear();
                    Console.WriteLine("How much money do you want to deposit?");
                    Bank.BankAccounts.ElementAt(indexNumber).SetMoney(FrontEnd.InputInt());
                    break;
                case 2: //withdraw -> 
                    ChangeAccountMoney(Bank.BankAccounts, indexNumber);

                    break;
                case 3: //delete account
                    decision = FrontEnd.DeleteCertain();
                    if (decision > 0)
                    {
                        string accountName = Bank.BankAccounts.ElementAt(indexNumber).AccountName;
                        Bank.DeleteAccount(indexNumber);
                        Console.WriteLine($"The account called {accountName} has been deleted.");
                        Console.ReadLine();
                        break;
                    }
                    else
                    {
                        break;
                    }
            }
        }
        public void ChangeAccountMoney(List<BankAccount> bankAccounts, int whichAccount)
        {
            Console.Clear();
            Console.WriteLine($"How much money do you want to withdraw?");
            int moneyEdit = FrontEnd.InputInt();

            //turning a "normal number" into a negative (remove)
            moneyEdit = (moneyEdit * -1);

            if (moneyEdit + bankAccounts[whichAccount].AccountMoney < 0)
            {
                Console.CursorVisible = false;
                Console.WriteLine("You don't have enough money for that.");
                Console.ReadLine();
                return;
            }
            //after checking to make sure that nothing goes wrong -> use method for changing account-money
            Bank.BankAccounts.ElementAt(whichAccount).SetMoney(moneyEdit);
        }
        public void SortBankAccounts()
        {
            CultureInfo svCulture = new CultureInfo("sv-SE"); //set the culture (for sorting å-ä-ö)

            //create a new BankAccount-list -> Fill it with the sorted list
            Bank.BankAccounts.OrderBy(x => x.AccountName, StringComparer.Create(svCulture, false)).ToList();
            Console.WriteLine("The list of accounts is now sorted from (a -> z)");
            Console.ReadLine();
        }
    }
}
