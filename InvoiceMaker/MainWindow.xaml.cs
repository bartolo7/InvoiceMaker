using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;



namespace InvoiceMaker
{ //David Bartolomé 14-05-2017 Assigment 6
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//Initialize object
		InvoiceManager addInvoiceToLibrary = new InvoiceManager();

		/// <summary>
		/// Constructor 
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			InitializeGUI();
		}

		/// <summary>
		/// Method to InitializeGUI and UpdateGUI for other methods 
		/// </summary>
		public void InitializeGUI()
		{

			btnCreateInvoice.IsEnabled = false;
			lstInvoices.SelectedIndex = -1;


		}

	
		/// <summary>
		/// Property in the MainForm to set get values in the XAML mark-up
		/// </summary>
		public class MyItem
		{
			/// <summary>
			/// XAML property to set/get flight number
			/// </summary>
			public string ID { get; set; }

			/// <summary>
			/// XAML property to set/get flight status 
			/// </summary>
			public string Company { get; set; }

			/// <summary>
			/// XAML property to set/get flight status 
			/// </summary>
			public string NumberOfItems { get; set; }

			/// <summary>
			/// XAML property to set/get flight status 
			/// </summary>
			public string ContactPerson { get; set; }


			public string TotalAmount { get; set; }


			/// <summary>
			/// XAML property to set/get date time
			/// </summary>
			public string DueDate { get; set; }
		}

		


		/// <summary>
		/// Method to import a text file and generate list view row 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnImport_Click(object sender, RoutedEventArgs e)
		{
			string fileName;// local variable 

			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			openFileDialog1.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension
			Nullable<bool> result = openFileDialog1.ShowDialog();

			try
			{
				if ( result == true) //Dialog box to open the xml file 
				{
					fileName = openFileDialog1.FileName; //retrieve filename and path

					Invoice newInvoice = new Invoice();//initiate object 

					InvoiceReader newDocument = new InvoiceReader(fileName, newInvoice);

					newDocument.ReadInvoice();

					newInvoice.NumberOfItems = newDocument.TotalNumberItems();

					lstInvoices.Items.Add(new MyItem { ID = newInvoice.InvoiceNumber.ToString(), Company = newInvoice.CompanyDebtorName, NumberOfItems = newInvoice.NumberOfItems.ToString(), ContactPerson = newInvoice.DebtorContactPerson, DueDate = newInvoice.DueDate.ToString("yyyy-MM-dd"), TotalAmount = newInvoice.TotalInvoice().ToString("F")});

					lstInvoices.ScrollIntoView(lstInvoices.Items[lstInvoices.Items.Count - 1]);

					addInvoiceToLibrary.AddElement(newInvoice);
				}
			}
			catch (Exception error)
			{
				MessageBox.Show(error.Message + "Environment.NewLine" + error.StackTrace, "Error");
			}
			finally
			{

			}
		}

		/// <summary>
		/// Method to generate an invoice 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
			private void btnCreateInvoice_Click(object sender, RoutedEventArgs e)
			{
				if (lstInvoices.SelectedIndex != -1) //check that an item is selected in list view 
				{

				Invoice documentInvoice = addInvoiceToLibrary.RetrieveElementAtPosition(lstInvoices.SelectedIndex);

				WindowInvocie newInvoice = new WindowInvocie(documentInvoice);
				newInvoice.Show();

				newInvoice.UpdateInvoice += OnUpdateInvoiceSent;// subscribe to event handler 
				}

				InitializeGUI();

			}

		/// <summary>
		/// Method subscribe to event handler which re-populate list view items 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnUpdateInvoiceSent(object sender, InvoiceEventInfo e)
		{
			Invoice updateInvoice = e.UpdateInvoice;
			addInvoiceToLibrary.ChangeElementAtPosition(updateInvoice, lstInvoices.SelectedIndex);
			UpdateListViewInvoices();
		}

		/// <summary>
		/// Method to update list view 
		/// </summary>
		public void UpdateListViewInvoices()
		{

			
			lstInvoices.Items.Clear(); //clear list view 


			 
			for (int index = 0; index < addInvoiceToLibrary.ElementCount; index++) //loop the list view
			{
				//New invoice object get assigned the different objects from manager
				Invoice allInvoices = addInvoiceToLibrary.RetrieveElementAtPosition(index);// method to retrieve objects form the list 
				
				lstInvoices.Items.Add(new MyItem { ID = allInvoices.InvoiceNumber.ToString(), Company = allInvoices.CompanyDebtorName, NumberOfItems = allInvoices.NumberOfItems.ToString(), ContactPerson = allInvoices.DebtorContactPerson, DueDate = allInvoices.DueDate.ToString("yyyy-MM-dd"), TotalAmount = allInvoices.TotalInvoice().ToString("F") });

				
			}
			//focus in the last item in the list view 
			lstInvoices.ScrollIntoView(lstInvoices.Items[lstInvoices.Items.Count - 1]);
		}

		/// <summary>
		/// Method to enable button create invoice if item is selected in list view 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lstInvoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (lstInvoices.SelectedIndex > -1)
				btnCreateInvoice.IsEnabled = true;
		
		}

		
	}
}

