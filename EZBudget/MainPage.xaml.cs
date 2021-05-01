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
using EZBudget.ViewModels;
using EZBudget.Views;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EZBudget
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel;

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModel();

            MainNavView.ItemInvoked += MainNavView_ItemInvoked;

            MainFrame.Navigate(typeof(HomePage));
        }

        private void MainNavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string selectedPage = args.InvokedItem as string;

            if (selectedPage == ViewModel.SelectedItem)
			{
                return;
			}                

            if (selectedPage == "Home")
            {
                MainFrame.Navigate(typeof(HomePage));
            }
            else if (selectedPage == "Transactions")
            {
                MainFrame.Navigate(typeof(TransactionsPage));
            }
            else
            {
                MainFrame.Navigate(typeof(HomePage));
            }
        }
    }
}
