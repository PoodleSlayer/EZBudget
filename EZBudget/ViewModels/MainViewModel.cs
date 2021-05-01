using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZBudget.ViewModels
{
	public class MainViewModel : EZBViewModel
	{
		private List<String> menuItems = new List<String> { "Home", "Transactions", "Income", "Budget", "Taxes", "Projection" };
		public List<String> MenuItems
		{
			get => menuItems;
		}

		private String selectedItem;
		public String SelectedItem
		{
			get
			{
				if (String.IsNullOrEmpty(selectedItem))
				{
					return "";
				}
				else
				{
					return selectedItem;
				}
			}
			set
			{
				if (value != selectedItem)
				{
					selectedItem = value;
					RaisePropertyChanged("SelectedItem");
				}
			}
		}

		public MainViewModel()
		{
			SelectedItem = MenuItems[0];
		}
	}
}
