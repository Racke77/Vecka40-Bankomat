using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatNoStatic
{
    internal class BankAccount
    {
        #region Properties 
        private string accountName;
		public string AccountName
		{
			get { return accountName; }
		}

		private int accountMoney;
		public int AccountMoney
		{
			get { return accountMoney; }
		}
        #endregion

        public BankAccount(string accountName, int accountMoney)
        {
            this.accountName = accountName;
            this.accountMoney = accountMoney;
        }

        public void SetMoney(int value)
        {
            this.accountMoney += value;
        }
    }
}
