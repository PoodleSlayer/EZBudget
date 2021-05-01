using EZBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZBudget.Utility
{
	public class AccountHelper
	{
		public static readonly string AccountCollection = "accounts";

		public AccountHelper()
		{
			// initialize collection if it doesn't exist?
		}

		public List<AccountModel> GetAllAccounts()
		{
			var accountList = new List<AccountModel>();

			var col = App.Database.GetCollection<AccountModel>(AccountCollection);
			accountList = col.FindAll().ToList();

			return accountList;
		}

		public bool AddAccount(string accountName, double balance)
		{
			var newAccount = new AccountModel()
			{
				AccountName = accountName,
				Balance = balance
			};

			var col = App.Database.GetCollection<AccountModel>(AccountCollection);
			col.Insert(newAccount);

			return true;
		}

		public bool DeleteAccount(int accountID)
		{
			var col = App.Database.GetCollection<AccountModel>(AccountCollection);
			col.Delete(accountID);

			return true;
		}
	}
}
