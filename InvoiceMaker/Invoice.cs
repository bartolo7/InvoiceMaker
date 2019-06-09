using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace InvoiceMaker
{ //David Bartolomé 14-05-2017 Assigment 6
	public class Invoice
	{
		private string invoiceNumber;
		public DateTime createDate;
		public DateTime dueDate;
		private string companyDebtorName;
		private string debtorContactPerson;
		private string debtorAddress;
		private int numberOfItems;
		private List<Products> itemPerInvoice;
		private string companyAddress;
		private string phoneNumber;
		private string homePageURL;
		private BitmapImage logo;
		private decimal discount;
		private decimal d_finalTotal;// final price with VAT for the invoice


		/// <summary>
		/// Constractor
		/// </summary>
		public void InvoiceMaker()
		{
			discount = 0;
		}

		/// <summary>
		/// Property Discount
		/// </summary>
		public Decimal Discount
		{
			get { return discount; }
			set { discount = value; }

		}

		/// <summary>
		/// Property Invoice Logo
		/// </summary>
		public BitmapImage Logo
		{
			get { return logo; }
			set { logo = value; }
		}

		/// <summary>
		/// Property list of items 
		/// </summary>
		public List<Products> ItemPerInvoice
		{
			get { return itemPerInvoice; }
			set { itemPerInvoice = value; }
		}

		/// <summary>
		/// Property Final Price 
		/// </summary>
		public Decimal FinalTotal
		{
			get { return d_finalTotal;  }
			set { d_finalTotal = value; }
		}

		
		/// <summary>
		/// Property Company Web
		/// </summary>
		public string HomePageURL
		{
			get { return homePageURL; }
			set { homePageURL = value; }
		}
		
		/// <summary>
		/// Property phone
		/// </summary>
		public string PhoneNumber
		{
			get { return phoneNumber; }
			set { phoneNumber = value; }
		}

		/// <summary>
		/// Property Address
		/// </summary>
		public string CompanyAddress
		{
			get { return companyAddress; }
			set { companyAddress = value; }
		}

		/// <summary>
		/// Property Number of items 
		/// </summary>
		public int NumberOfItems
		{
			get { return numberOfItems; }
			set { numberOfItems = value; }
		}

		/// <summary>
		/// Property Address
		/// </summary>
		public string DebtorAddress
		{
			get { return debtorAddress; }
			set { debtorAddress = value; }
		}

		/// <summary>
		/// Property contact person 
		/// </summary>
		public string DebtorContactPerson
		{
			get { return debtorContactPerson; }
			set { debtorContactPerson = value; }
		}

		/// <summary>
		/// Property debtor company name
		/// </summary>
		public string CompanyDebtorName
		{
			get { return companyDebtorName; }
			set { companyDebtorName = value; }
		}

		/// <summary>
		/// Property due date 
		/// </summary>
		public DateTime DueDate
		{
			get { return dueDate; }
			set { dueDate = value; }
		}

		/// <summary>
		/// Property Created Date 
		/// </summary>
		public DateTime CreateDate
		{
			get { return createDate; }
			set { createDate = value; }
		}

		/// <summary>
		/// Property Invoice Number 
		/// </summary>
		public string InvoiceNumber
		{
			get { return invoiceNumber; }
			set { invoiceNumber = value; }
		}

		/// <summary>
		/// Methoed to validate Due Date vs Created Date 
		/// </summary>
		/// <param name="due"></param>
		/// <returns></returns>
		public bool ValidateDueDate(DateTime due)
		{
			bool proceed = true;


			if (createDate > due)
			{
				proceed = false;
			}
			return proceed;
		}

		/// <summary>
		/// Method to validate Created Date Vs Due Date 
		/// </summary>
		/// <param name="create"></param>
		/// <returns></returns>
		public bool ValidateCreateDate(DateTime create)
		{
			bool proceed = true;


			if (create > dueDate)
			{
				proceed = false;
			}
			return proceed;
		}

		/// <summary>
		/// Method to calculate total price without VAT
		/// </summary>
		/// <returns></returns>
		public decimal TotalPriceWithoutVAT()
		{
			Decimal price = 0;

			for (int index = 0; index < itemPerInvoice.Count(); index++)
			{
				Products item = itemPerInvoice.ElementAt<Products>(index);

				price = price + item.Price;
			}
			return price;
		}

		/// <summary>
		/// Method to calculate the VAT value per item 
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public decimal TotalTax(int index)
		{
			Decimal price = 0;

			Products item = itemPerInvoice.ElementAt<Products>(index);

			price = item.Price * (((decimal)item.Tax)/100);
			
			return price;
		}

		/// <summary>
		/// Method to calculate the total price per item 
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public decimal TotalPrice(int index)
		{
			Decimal valueTax = 0;
			Decimal totalPrice = 0;

			Products item = itemPerInvoice.ElementAt<Products>(index);

			valueTax = item.Price * (((decimal)item.Tax) / 100);

			totalPrice = (valueTax + item.Price)* (decimal)item.Quantity;

			return totalPrice;
		}

		/// <summary>
		/// Method to calculate the total price of the invoice with VAT
		/// </summary>
		/// <returns></returns>
		private decimal CalculateTotal()
		{
			Decimal finalTotal = 0;

			for (int index = 0; index < itemPerInvoice.Count; index++)
			{
				Decimal valueTax = 0;
				Decimal totalPrice = 0;

				Products item = itemPerInvoice.ElementAt<Products>(index);

				valueTax = item.Price * (((decimal)item.Tax) / 100);
				totalPrice = (valueTax + item.Price) * (decimal)item.Quantity;

				finalTotal = finalTotal + totalPrice;
			}

			return finalTotal;
		}

		/// <summary>
		/// Method to calculate total price with VAT 
		/// </summary>
		/// <returns></returns>
		public decimal TotalInvoice()
		{

			return CalculateTotal() - CalculateDiscount();
		}


		/// <summary>
		/// Method to calculate the discount amount 
		/// </summary>
		/// <returns></returns>
		public decimal CalculateDiscount()
		{

			return CalculateTotal() * ((discount) / 100); 
		}
		

	}
}
