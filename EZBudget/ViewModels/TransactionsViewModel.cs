using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using EZBudget.IoC;
using EZBudget.Models;
using EZBudget.Utility;
using GalaSoft.MvvmLight.Command;

namespace EZBudget.ViewModels
{
	public class TransactionsViewModel : EZBViewModel
	{
		private TransactionHelper transactionHelper;
		private AccountHelper accountHelper;
		private int currentAccountID;

		public RelayCommand AddAccountCommand { get; set; }
		public RelayCommand AddTransactionCommand { get; set; }
		public RelayCommand DeleteAccountCommand { get; set; }
		public RelayCommand DeleteTransactionCommand { get; set; }

		public event EventHandler DismissPopups;

		public TransactionsViewModel()
		{
			transactionHelper = AppContainer.Container.Resolve<TransactionHelper>();
			accountHelper = AppContainer.Container.Resolve<AccountHelper>();

			InitializeAccounts();
			InitializeTransactions();
			SetupCommands();

			if (Accounts.Count > 0)
			{
				// if they have some accounts on file, just default to the first
				AccountIndex = 0;
			}
		}

		private void InitializeTransactions()
		{
			Transactions = new ObservableCollection<TransactionModel>();
			updateTransactions(currentAccountID);
			//loadDebugTransactions();
		}

		private void InitializeAccounts()
		{
			Accounts = new ObservableCollection<AccountModel>();
			updateAccounts();
			//loadDebugAccounts();
		}

		private void SetupCommands()
		{
			AddAccountCommand = new RelayCommand(() =>
			{
				AddNewAccount();
			});
			AddTransactionCommand = new RelayCommand(() =>
			{
				AddNewTransaction();
			});
			DeleteAccountCommand = new RelayCommand(() =>
			{
				DeleteAccount();
			});
			DeleteTransactionCommand = new RelayCommand(() =>
			{
				DeleteTransaction();
			});
		}

		public ObservableCollection<TransactionModel> Transactions { get; set; }
		public ObservableCollection<AccountModel> Accounts { get; set; }

		private string accountName;
		public string AccountName { get => accountName; set => Set(ref accountName, value); }
		
		private double accountBalance;
		public double AccountBalance { get => accountBalance; set => Set(ref accountBalance, value); }
		
		private string transactionDescription;
		public string TransactionDescription { get => transactionDescription; set => Set(ref transactionDescription, value); }
		
		private double transactionAmount;
		public double TransactionAmount { get => transactionAmount; set => Set(ref transactionAmount, value); }

		public string CurrentAccount { get; set; }
		public string TransactionsLabel
		{
			get
			{
				if (Accounts == null || Accounts.Count == 0)
				{
					return "No Accounts on file. Add one!";
				}

				return $"Transactions for {Accounts[accountIndex].AccountName}";
			}
		}

		private int accountIndex = -1;
		public int AccountIndex
		{
			get => accountIndex;
			set
			{
				if (value < 0)
				{
					return;
				}
				accountIndex = value;
				CurrentAccount = Accounts[accountIndex].AccountName;
				currentAccountID = Accounts[accountIndex].AccountID;
				updateTransactions(currentAccountID);
				RaisePropertyChanged("AccountIndex");
				RaisePropertyChanged("TransactionsLabel");
			}
		}

		private void updateTransactions(int accountID)
		{
			var tList = transactionHelper.GetTransactionsByAccount(accountID);
			Transactions.Clear();
			foreach (TransactionModel t in tList)
			{
				Transactions.Add(t);
			}
		}

		private void updateAccounts()
		{
			var aList = accountHelper.GetAllAccounts();
			
			// add any accounts that are new from the database
			foreach (AccountModel a in aList)
			{
				if (!Accounts.Any(acc => acc.AccountID == a.AccountID))
				{
					Accounts.Add(a);
				}
			}

			// find any accounts that are NOT in the database...
			var accountsToRemove = new List<AccountModel>();
			foreach (AccountModel a in Accounts)
			{
				if (!aList.Any(acc => acc.AccountID == a.AccountID))
				{
					accountsToRemove.Add(a);
				}
			}

			// this is a nice one-liner but not very readable...
			//var accountsToRemove = Accounts.Where(a1 => aList.All(a2 => a1.AccountID != a2.AccountID)).ToList();

			// ... and remove them!
			foreach (AccountModel a in accountsToRemove)
			{
				Accounts.Remove(a);
			}

		}

		private void AddNewAccount()
		{
			accountHelper.AddAccount(AccountName, AccountBalance);
			updateAccounts();
			if (Accounts.Count == 1)
			{
				// added our first account, so set selected index
				AccountIndex = 0;
			}
			AccountName = "";
			AccountBalance = 0;
			DismissPopups?.Invoke(this, null);
		}

		private void AddNewTransaction()
		{
			if (CurrentAccount == null)
			{
				// no accounts added so don't even bother
				return;
			}
			transactionHelper.AddTransactionToAccount(currentAccountID, TransactionDescription, TransactionAmount);
			updateTransactions(Accounts[accountIndex].AccountID);
			TransactionDescription = "";
			TransactionAmount = 0;
			DismissPopups?.Invoke(this, null);
		}

		private void DeleteAccount()
		{
			// delete the currently selected account
			if (CurrentAccount == null)
			{
				// no account selected so just bail
				return;
			}
			int currentSelection = accountIndex;
			accountHelper.DeleteAccount(currentAccountID);
			// ideally this would delete all transactions for this AccountID as well
			updateAccounts();
			if (Accounts.Count > 1)
			{
				AccountIndex = currentSelection - 1;
			}
			else
			{
				AccountIndex = 0;
			}
			DismissPopups?.Invoke(this, null);
		}

		private void DeleteTransaction()
		{
			// delete the currently selected transaction
			;
		}

		private void loadDebugAccounts()
		{
			Accounts.Add(new AccountModel()
			{
				AccountName = "SECU",
				AccountID = 0,
				Balance = 100
			});
			Accounts.Add(new AccountModel()
			{
				AccountName = "Wells Fargo",
				AccountID = 1,
				Balance = 250.27
			});
			Accounts.Add(new AccountModel()
			{
				AccountName = "Arapahoe Credit Union",
				AccountID = 2,
				Balance = 100
			});
			Accounts.Add(new AccountModel()
			{
				AccountName = "Doge Coin",
				AccountID = 3,
				Balance = 3000
			});
			Accounts.Add(new AccountModel()
			{
				AccountName = "Coinbase",
				AccountID = 4,
				Balance = 283.91
			});
		}

		private void loadDebugTransactions()
		{
			Transactions.Add(new TransactionModel()
			{
				Description = "Bought vidya game",
				Amount = 59.99,
				Date = "03/12/21"
			});
			Transactions.Add(new TransactionModel()
			{
				Description = "Taco Bell lol",
				Amount = 10.72,
				Date = "03/19/21"
			});
			Transactions.Add(new TransactionModel()
			{
				Description = "Partied",
				Amount = 200.00,
				Date = "02/20/21"
			});
		}

	}
}
