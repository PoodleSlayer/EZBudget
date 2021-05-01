using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace EZBudget.Models
{
	public class TransactionModel
	{
		[BsonId]
		public int TransactionID { get; set; }
		public string Description { get; set; }
		public double Amount { get; set; }
		public string Date { get; set; }
		public int AccountID { get; set; }
	}
}
