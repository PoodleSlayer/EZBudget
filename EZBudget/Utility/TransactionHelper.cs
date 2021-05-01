using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZBudget.Models;

namespace EZBudget.Utility
{
	public class TransactionHelper
	{
		public static readonly string TransactionCollection = "transactions";

		public TransactionHelper()
		{
			// initialize collection if it doesn't exist...?
		}

		public List<TransactionModel> GetTransactionsByAccount(int accountID)
		{
			var transactions = new List<TransactionModel>();
			if (!App.Database.CollectionExists(TransactionCollection))
			{
				return transactions;
			}

			var col = App.Database.GetCollection<TransactionModel>(TransactionCollection);
			transactions = col.Query().Where(x => x.AccountID == accountID).ToList();
			
			return transactions;
		}

		public List<TransactionModel> GetTransactionsByDate(DateTime date)
		{
			var transactions = new List<TransactionModel>();
			
			if (!App.Database.CollectionExists(TransactionCollection))
			{
				return transactions;
			}
			var col = App.Database.GetCollection<TransactionModel>(TransactionCollection);
			
			return transactions;
		}

		public bool AddTransactionToAccount(int accountID, string description, double amount)
		{
			var newTransaction = new TransactionModel()
			{
				AccountID = accountID,
				Description = description,
				Amount = amount
			};

			var col = App.Database.GetCollection<TransactionModel>(TransactionCollection);
			col.Upsert(newTransaction);
			
			return true;
		}

		public bool DeleteTransaction(TransactionModel transaction)
		{
			var col = App.Database.GetCollection<TransactionModel>(TransactionCollection);
			col.Delete(transaction.TransactionID);

			return true;
		}
	}
}
