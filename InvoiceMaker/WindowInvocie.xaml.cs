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
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace InvoiceMaker
{//David Bartolomé 14-05-2017 Assigment 6
 /// <summary>
 /// Interaction logic for WindowInvocie.xaml
 /// </summary>
	public partial class WindowInvocie : Window
	{
		Invoice documentInvoice;//object 

		public EventHandler<InvoiceEventInfo> UpdateInvoice;//publisher 

		
		
		/// <summary>
		/// Constractor 
		/// </summary>
		/// <param name="obj"></param>
		public WindowInvocie(Invoice obj)
		{

			documentInvoice = obj;
			InitializeComponent();
			InitializeGUI();
		}

		/// <summary>
		/// Method to Initialize and populate the invoice with all the data
		/// </summary>
		public void InitializeGUI()
		{
			lblInvoiceNumber.Content = documentInvoice.InvoiceNumber;
			lblCustomerAddress.Content = documentInvoice.DebtorAddress;
			lblAddress.Content = "Address: " + Environment.NewLine + documentInvoice.CompanyAddress;
			lblPhone.Content = "Phone: " + Environment.NewLine + documentInvoice.PhoneNumber;
			lblHomePage.Content = "Home Page: " + Environment.NewLine + documentInvoice.HomePageURL;
			PopulateListArticles();
			lblTotal.Content = documentInvoice.TotalInvoice().ToString("F");
			txtDiscount.Text = documentInvoice.Discount.ToString();

			

			daPDueDate.BorderBrush = Brushes.White;
			daPCreatedDate.BorderBrush = Brushes.White;
			daPCreatedDate.SelectedDate = documentInvoice.CreateDate;
			daPDueDate.SelectedDate = documentInvoice.DueDate;


			txtDiscount.MaxLength = 2;
			imgLogo.Source = documentInvoice.Logo;
			
		}

		

		


		

		
		/// <summary>
		/// Method to populate list view number of producst in the invoice 
		/// </summary>
		public void PopulateListArticles()
		{
			List <Products> articles = documentInvoice.ItemPerInvoice;

			for (int index = 0; index < articles.Count; index++)
			{
				Products d_item = articles.ElementAt(index);
				lstArticles.Items.Add(new MyItem { Item = d_item.item, Description = d_item.Description, Quantity = d_item.Quantity, UnitPrice = d_item.Price, TaxPercentage = d_item.Tax, TaxTotal = documentInvoice.TotalTax(index).ToString("F"), Total = documentInvoice.TotalPrice(index).ToString("F")});

			}

		}

		public class MyItem
		{
			/// <summary>
			/// XAML property to set/get flight number
			/// </summary>
			public int Item { get; set; }

			/// <summary>
			/// XAML property to set/get flight status 
			/// </summary>
			public string Description { get; set; }

			/// <summary>
			/// XAML property to set/get flight status 
			/// </summary>
			public double Quantity { get; set; }

			/// <summary>
			/// XAML property to set/get flight status 
			/// </summary>
			public decimal UnitPrice { get; set; }


			/// <summary>
			/// XAML property to set/get date time
			/// </summary>
			public double TaxPercentage { get; set; }


			/// <summary>
			/// XAML property to set/get total tax 
			/// </summary>
			public string TaxTotal { get; set; }

			/// <summary>
			/// XMAL properoty to set/get total invoice price
			/// </summary>
			public string Total { get; set; }
		}


		/// <summary>
		/// Method to limit the invoice text box to 2 digits 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtDiscount_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
		}

		
		/// <summary>
		/// Method to validate discount input 
		/// </summary>
		/// <returns></returns>
		public bool ValidateInput()
		{
			decimal discount = Convert.ToDecimal(txtDiscount.Text);

			bool proceed = true;

			if (discount > 50) //Discount can NOT be higher than 50%
			{
				txtDiscount.BorderBrush = System.Windows.Media.Brushes.Red;
				txtDiscount.Text = "";
				lblErrorDiscount.Foreground = System.Windows.Media.Brushes.Red;
				lblErrorDiscount.Content = "Warning! Discount can not be higher than 50%";
				proceed = false;
			}

			return proceed;
		}

		/// <summary>
		/// Method to calculate discount and new total invoice price
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDiscount_Click(object sender, RoutedEventArgs e)
		{
			if (ValidateInput())
			{
				documentInvoice.Discount = Convert.ToDecimal(txtDiscount.Text);

				lblTotal.Content = documentInvoice.TotalInvoice().ToString("F");
				UpdateGUI();
				InvoiceEventInfo updateTotal = new InvoiceEventInfo(documentInvoice);
				OnUpdateInvoice(updateTotal);
			}
			
		} 

		/// <summary>
		/// Method to UpdateGUI
		/// </summary>
		public void UpdateGUI()
		{
			lblErrorDiscount.Content = "";
			txtDiscount.BorderBrush = System.Windows.Media.Brushes.White;
			btnPrint.Visibility = Visibility.Visible;
			btnAddLogo.Visibility = Visibility.Visible;
		}

	
		/// <summary>
		/// Method to handle changes by the user in the Due date data timepicker
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void daPDueDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			bool proceed = documentInvoice.ValidateDueDate(daPDueDate.SelectedDate.Value);

			if (proceed)
			{
				documentInvoice.DueDate = daPDueDate.SelectedDate.Value;
				InvoiceEventInfo updateTotal = new InvoiceEventInfo(documentInvoice);
				OnUpdateInvoice(updateTotal);
				daPDueDate.BorderBrush = Brushes.White;
				lblErrorDueDate.Content = string.Empty;
			}
			else
			{
				lblErrorDueDate.Foreground = Brushes.Red;
				lblErrorDueDate.Content = "Error! Due Date < Created Date";
				daPDueDate.BorderBrush = Brushes.Red;

			}
				
		}

		/// <summary>
		/// Method to add a logo in the invoice 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddLogo_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog op = new OpenFileDialog();
			op.Title = "Select a picture";
			op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
			  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
			  "Portable Network Graphic (*.png)|*.png";
			if (op.ShowDialog() == true)
			{
				imgLogo.Source = new BitmapImage(new Uri(op.FileName));

				documentInvoice.Logo = (BitmapImage)imgLogo.Source;
			}
		}

		/// <summary>
		/// Publisher event handler method to invoice updated variables 
		/// </summary>
		/// <param name="e"></param>
		public void OnUpdateInvoice(InvoiceEventInfo e)
		{
			if (UpdateInvoice != null)
				UpdateInvoice(this, e); // call event handler 


		}

		/// <summary>
		/// Method to print the window invoice
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			btnAddLogo.Visibility = Visibility.Collapsed;
			btnPrint.Visibility = Visibility.Collapsed;
			PrintDialog printDlg = new PrintDialog();
			if (printDlg.ShowDialog() == true)
			{
				printDlg.PrintVisual(this, "WPF Print");
			}
			UpdateGUI();
		}

		/// <summary>
		/// Method to handle if the user changes the Created date in the data timepicker 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void daPCreatedDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{

			bool proceed = documentInvoice.ValidateCreateDate(daPCreatedDate.SelectedDate.Value);

			if (proceed)
			{
				documentInvoice.CreateDate = daPCreatedDate.SelectedDate.Value;
				daPCreatedDate.BorderBrush = Brushes.White;
				lblErrorCreatedDate.Content = string.Empty;

			}
			else
			{
				lblErrorCreatedDate.Foreground = Brushes.Red;
				lblErrorCreatedDate.Content = "Error! Created Date > Due Date";
				daPCreatedDate.BorderBrush = Brushes.Red;
			}
		}
	}
}
