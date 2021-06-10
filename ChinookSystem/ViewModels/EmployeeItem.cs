﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.ViewModels
{
	public class EmployeeItem
	{
		public string FullName { get; set; }
		public string Title { get; set; }
		public int NumberOfCustomers { get; set; }
		// the list of customers associated witht he employee
		public IEnumerable<CustomerItem> CustomerList { get; set; }

	}
}
