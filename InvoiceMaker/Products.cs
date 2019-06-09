using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker
{//David Bartolomé 14-05-2017 Assigment 6

	public class Products
	{
		// Variable for the  CSV file 
		private string description;
		private double quantity;
		private decimal price;
		private double tax;

		//Variables for the GUI 
		public int item;


		/// <summary>
		/// Constructor 
		/// </summary>
		public Products(int item, string description, double quantity, decimal price, double tax)
		{
			this.item = item;
			this.description = description;
			this.quantity = quantity;
			this.price = price;
			this.tax = tax;

		}

		/// <summary>
		/// Property Item Description
		/// </summary>
		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		/// <summary>
		/// Property quantity of items
		/// </summary>
		public double Quantity
		{
			get { return quantity; }
			set { quantity = value; }
		}

		/// <summary>
		/// Property price per item 
		/// </summary>
		public decimal Price
		{
			get { return price; }
			set { price = value; }
		}

		/// <summary>
		/// Property tax per item 
		/// </summary>
		public double Tax
		{
			get { return tax; }
			set { tax = value; }
		}


	}
}
