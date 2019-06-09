using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker
{// David Bartolomé 14-05-2017 Assigment 6
	public class InvoiceEventInfo : EventArgs
	{

		private Invoice updateInvoice; // object variable 

		/// <summary>
		/// Constructor 
		/// </summary>
		/// <param name="obj"></param>
		public InvoiceEventInfo(Invoice obj)
		{

			updateInvoice = obj; //pass object

		}

		/// <summary>
		/// Property Invoice 
		/// </summary>
		public Invoice UpdateInvoice
		{
			get{return updateInvoice;}
			set{ updateInvoice = value;}
		}

	}
}
