using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace EZBudget.Models
{
	public class AccountModel
	{
		[BsonId]
		public int AccountID { get; set; }
		public string AccountName { get; set; }
		public Double Balance { get; set; }
	}
}
