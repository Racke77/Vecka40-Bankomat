using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BankomatNoStatic
{
    internal class Menu
    {
        public List<string>? MenuList;
        public int MenuSelect;
        public Boolean SelectedOption;
        public Menu(List<string> menuList)
        {
            MenuList = menuList;
        }
        public void StartingMenu()
        {
            MenuList.Clear();
            MenuList = new List<string>()
            {
                  "Display all", "View account", "Find account" ,"Deposit to account", "Withdraw from account", "Sort accounts", "Add new account", "Delete account", "Quit"
            };
        }
        public void FoundAccountMenu()
        {
            MenuList = new List<string>()
            {
                "Return", "Deposit", "Withdraw", "Delete account"
            };
        }
        public int AllAccountsMenu(List<BankAccount> bankAccounts)
        {
            MenuList.Clear();
            foreach (BankAccount bankAccount in bankAccounts)
            {
                MenuList.Add(bankAccount.AccountName);
            }
            return MenuSelection();
        }
        public int MenuSelection()
        {
            MenuSelect = 0;
            SelectedOption = false;
            while (SelectedOption != true)
            {
                Console.Clear();
                Console.CursorVisible = false;
                //printing out the full menu
                MenuDisplayBase(MenuSelect);
            }
            return MenuSelect;
        }
        public int SingleAccountMenuSelector(int indexNumber, Bank bank)
        {
            MenuSelect = 0;
            SelectedOption = false;
            while (SelectedOption != true)
            {
                Console.Clear();
                FoundAccountMenu();
                FrontEnd frontEnd = new FrontEnd();
                frontEnd.DisplaySingleAccount(indexNumber, bank);
                Console.WriteLine();
                Console.CursorVisible = false;
                //printing out the full menu
                MenuDisplayBase(MenuSelect);
            }
            return MenuSelect;
        }
        public void MenuDisplayBase(int menuSelect)
        {
            foreach (string menuOption in MenuList)
            {
                if (MenuList.IndexOf(menuOption) == menuSelect)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(menuOption);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(menuOption);
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            var keyPressed = Console.ReadKey();

            //going down, one smaller than the full menu
            if (keyPressed.Key == ConsoleKey.DownArrow && menuSelect != MenuList.Count - 1)
            {
                MenuSelect++;
            }
            else if (keyPressed.Key == ConsoleKey.DownArrow && menuSelect == MenuList.Count - 1) //loop around
            {
                MenuSelect = 0;
            }
            //going up, no going higher than the starting-option
            else if (keyPressed.Key == ConsoleKey.UpArrow && menuSelect >= 1)
            {
                MenuSelect--;
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow && menuSelect == 0) //loop around
            {
                MenuSelect = MenuList.Count - 1;
            }
            //press ENTER to send back info about which menu-option was selected
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                SelectedOption = true;
            }
        }
    }
}
