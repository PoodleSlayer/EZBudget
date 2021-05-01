using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using EZBudget.Utility;
using EZBudget.ViewModels;

namespace EZBudget.IoC
{
	public class ContainerSetup
	{
		public IContainer CreateContainer()
		{
			var containerBuilder = new ContainerBuilder();
			RegisterDependencies(containerBuilder);
			return containerBuilder.Build();
		}

		/// <summary>
		/// Method for setting up the initial dependencies of the container. Marked virtual in case
		/// we added other frontends or projects in the future that will need to register their own
		/// dependencies by extending this class.
		/// </summary>
		/// <param name="cb"></param>
		protected virtual void RegisterDependencies(ContainerBuilder cb)
		{
			cb.RegisterType<TransactionsViewModel>().SingleInstance();
			cb.RegisterType<TransactionHelper>().SingleInstance();
			cb.RegisterType<AccountHelper>().SingleInstance();
		}
	}
}
