using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Name Spaces
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using ChinookSystem.ViewModels;
using System.ComponentModel; // Becuase we are using the ODS (Object Data Source) Wizard
#endregion

namespace ChinookSystem.BLL
{
	[DataObject]
	public class EmployeeController
	{
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public List<EmployeeItem> Employee_EmployeeCustomers()
		{
			using (var context = new ChinookSystemContext())
			{
				//I need to alterthe from to indicate where the Entity 
				// DbSets are located
				IEnumerable<EmployeeItem> results = from x in context.Employees //Notice we had to add context when moving from linqpad
													where x.Title.Contains("Sales Support")
													orderby x.LastName, x.FirstName
													select new EmployeeItem
													{
														FullName = x.LastName + ", " + x.FirstName,
														// the $() method of concatenantion doesnt work here
														Title = x.Title,
														NumberOfCustomers = x.Customers.Count(), //Notice the name change
														CustomerList = from y in x.Customers //Nested Query
																	   select new CustomerItem
																	   {
																		   FullName = y.LastName + ", " + y.FirstName,
																		   Phone = y.Phone,
																		   City = y.City,
																		   State = y.State
																	   } //no semi colon
													}; //semi colon
				return results.ToList();
			}

		}
	}
}


