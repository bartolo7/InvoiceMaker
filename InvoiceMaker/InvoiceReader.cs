using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace InvoiceMaker
{ //David Bartolomé 14-05-2017 Assigment 6 
	public class InvoiceReader
	{
		private string filePath;//import file path
		private Invoice newInvoice; //object
		private const int minNumberLines = 16; // Invoice without products has max 16 lines 
		private int numberLines; //total number line in the import file


		/// <summary>
		/// Constractor 
		/// </summary>
		/// <param name="file"></param>
		/// <param name="obj"></param>
		public InvoiceReader(string file, Invoice obj)
		{

			filePath = file;//pass path
			newInvoice = obj;//pass object
			numberLines = File.ReadAllLines(filePath).Length; //total number lines

		}
		
		/// <summary>
		/// Method to extra 2 from the total of lines which are added by C#
		/// </summary>
		/// <returns></returns>
		public int NumberLinesInvoice()
		{
			int numberlines = File.ReadAllLines(filePath).Length;
			return numberLines - 2;
		}


		/// <summary>
		/// Method to Calculate total number of items
		/// </summary>
		/// <returns></returns>
		public int TotalNumberItems()
		{
			return (NumberLinesInvoice() - 16)/4;

		}

		/// <summary>
		/// Method to read the invoice line by line and populate invoice object
		/// </summary>
		public void ReadInvoice()
		{

            using (TextReader tr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {

                

                    List<Products> articlesInvoice = new List<Products>(); //list for the articles in the invoice

                    int items = TotalNumberItems();

                    //the order of the lines below is relevant 
                    newInvoice.InvoiceNumber = tr.ReadLine();
                    newInvoice.CreateDate = DateTime.Parse(tr.ReadLine());
                    newInvoice.DueDate = DateTime.Parse(tr.ReadLine());
                    newInvoice.CompanyDebtorName = tr.ReadLine();
                    newInvoice.DebtorContactPerson = tr.ReadLine();
                    newInvoice.DebtorAddress = tr.ReadLine() + Environment.NewLine + tr.ReadLine() + Environment.NewLine + tr.ReadLine() + Environment.NewLine + tr.ReadLine();

                    for (int index = 0; index < items; index++)
                    {
                        //populate list with articles
                        articlesInvoice.Add(new Products(index + 1, tr.ReadLine(), Convert.ToDouble(ParseNumber(tr.ReadLine())), Convert.ToDecimal(ParseNumber(tr.ReadLine())), Convert.ToDouble(ParseNumber(tr.ReadLine()))));

                    }

                    newInvoice.ItemPerInvoice = articlesInvoice; // pass the list to Invoice class 

                    newInvoice.CompanyAddress = tr.ReadLine() + Environment.NewLine + tr.ReadLine() + Environment.NewLine + tr.ReadLine() + Environment.NewLine + tr.ReadLine() + Environment.NewLine + tr.ReadLine();
                    newInvoice.PhoneNumber = tr.ReadLine();
                    newInvoice.HomePageURL = tr.ReadLine();
               
               }
            
		}

        /// <summary>
        /// Method to replace , for .
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string ParseNumber(string item)
        {
            
            if (CultureInfo.CurrentCulture.Name == "sv-SE")
            {

                item = item.Replace(".", ",");

            }
            if (CultureInfo.CurrentCulture.Name == "en-US")
            {
                item = item.Replace(",", ".");
            }
            return item;

        }
    }
}









