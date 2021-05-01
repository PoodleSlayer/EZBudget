using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Autofac;
using EZBudget.IoC;
using EZBudget.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EZBudget.Views
{
	/// <summary>
	/// This page holds the Account List, which the user can then use to view the
	/// transactions for each account.
	/// </summary>
	public sealed partial class TransactionsPage : Page
	{
		public TransactionsPage()
		{
			this.InitializeComponent();
			DataContext = AppContainer.Container.Resolve<TransactionsViewModel>();

			ViewModel.DismissPopups += ViewModel_DismissPopups;
		}

		private void ViewModel_DismissPopups(object sender, EventArgs e)
		{
			DismissPopups();
		}

		private void ClosePopupClick(object sender, RoutedEventArgs e)
		{
			DismissPopups();
		}

		private void DismissPopups()
		{
			if (AccountPopup.IsOpen)
			{
				AccountPopup.IsOpen = false;
			}
			if (TransactionPopup.IsOpen)
			{
				TransactionPopup.IsOpen = false;
			}
			if (DeleteAccountPopup.IsOpen)
			{
				DeleteAccountPopup.IsOpen = false;
			}
			if (DeleteTransactionPopup.IsOpen)
			{
				DeleteTransactionPopup.IsOpen = false;
			}
		}

		private void MenuFlyout_Account(object sender, RoutedEventArgs e)
		{
			AccountPopup.IsOpen = true;
		}

		private void MenuFlyout_Transaction(object sender, RoutedEventArgs e)
		{
			TransactionPopup.IsOpen = true;
		}

		private void DeleteAccount_Click(object sender, RoutedEventArgs e)
		{
			DeleteAccountPopup.IsOpen = true;
		}

		private void DeleteTransaction_Click(object sender, RoutedEventArgs e)
		{
			DeleteTransactionPopup.IsOpen = true;
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);
			ViewModel.DismissPopups -= ViewModel_DismissPopups;
		}

		private TransactionsViewModel ViewModel
		{
			get => DataContext as TransactionsViewModel;
		}
	}
}
