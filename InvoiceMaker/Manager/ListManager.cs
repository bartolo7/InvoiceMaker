using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker
{//David Bartolome 14-05-2017 Assigment 6

	[Serializable]
	public class ListManager<T> : IListManager<T>//implementation of interphase 
	{

		/// <summary> Class variable list with generic value /// </summary>
		private List<T> m_list; //Generic List 

		/// <summary>
		/// Path + File Name for the Binary Serialization
		/// </summary>
		private string binary_FileName = String.Empty; //Initiated variable

		/// <summary>
		/// Path + File Name for the XML Serialization
		/// </summary>
		private string xml_FileName;

		/// <summary>
		/// Property to get/set Path + File name for Binary Serialization
		/// </summary>
		public string BinaryFileName
		{
			get { return binary_FileName; }
			set { binary_FileName = value; }
		}


		/// <summary>
		/// Properoty to get/set Path + File name for XML Serialization
		/// </summary>
		public string XMLFileName
		{
			get { return xml_FileName; }
			set { xml_FileName = value; }
		}
		/// <summary>Constractor/// </summary>
		public ListManager()
		{
			m_list = new List<T>(); //variable list with generic is initiated
		}


		/// <summary>Method count item in the generic list/// </summary>
		public int ElementCount
		{
			get { return m_list.Count(); }
		}

		/// <summary>Method add element to generic list/// </summary>/// 
		/// <param name="item"></param>
		public virtual void AddElement(T item)
		{
			//the pass parameter is copied 
			T copyObje = item;

			//nothing check and add object to list 
			if (copyObje != null)
				m_list.Add(copyObje);
		}


		/// <summary>
		/// Method to Serialize to binary animal registry list 
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public bool BinaryDeSerialize(string fileName)
		{
			bool proceed = true;//local variable 

			//DeleteAllElements();//All the element are deleted because the objec will be populated during the Deserialization

			//m_list = BinSerializerUtility.DeserializeListOfProductBin<List<T>>(fileName);

			return proceed;
		}


		/// <summary>
		/// Method to Deserialize
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public bool BinarySerialize(string fileName)
		{
			bool proceed = true;//local variable 

		//	BinSerializerUtility.SerializeListOfOjbectsBin<List<T>>(fileName, m_list);

			return proceed;
		}

		/// <summary>Method update item in the generic list/// </summary>
		public void ChangeElementAtPosition(T itemIn, int index)
		{
			if (IsIndexValid(index))//check index 
			{
				//is it recommended to use a copy constructor here??
				// Something like Object newObject = new Object (itemIN);

				m_list[index] = itemIn;
			}
		}

		/// <summary>Method delete all the items in the generic list/// </summary>
		public void DeleteAllElements()
		{
			m_list.Clear();
		}

		/// <summary>Method delete item in the generic list /// </summary>
		public void DeleteElementAtPosition(int index)
		{
			if (IsIndexValid(index))// check index 
				m_list.RemoveAt(index);
		}

		/// <summary>Method to validate index/// </summary>
		public bool IsIndexValid(int index)
		{
			return ((index >= 0) && (index < m_list.Count));
		}

		/// <summary>Method to read element at positoin in the generic list/// </summary>
		public void ReadElementAtPosition(int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>Method to retrieve element at positoin in the generic list/// </summary>
		public T RetrieveElementAtPosition(int index)
		{
			if (IsIndexValid(index))//check index
			{

				return m_list[index];
			}

			return default(T);//? 
		}

		/// <summary>Method print string array/// </summary>
		public string[] ToStringArray()
		{
			throw new NotImplementedException();
		}

		/// <summary>Method print list of strings/// </summary>
		public List<string> ToStringList()
		{
			throw new NotImplementedException();
		}

		public bool XMLSerialize(string fileName)
		{
			throw new NotImplementedException();
		}

		public bool XMLDeSerialize(string fileName)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Method to XML Serialization
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		//public bool XMLSerialize(string fileName)
		//{
		//	bool proceed = true;

		//	XMLSerializerUtility.XMLSerializer(fileName, m_list);//This works fine

		//	return proceed;
		//}

		/// <summary>
		/// Method to deserialize animal registry list
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		//public bool XMLDeSerialize(string fileName)
		//{
		//	bool proceed = true;

		//	DeleteAllElements();

		//	m_list = XMLSerializerUtility.XMLDeserializer<List<T>>(fileName);

		//	return proceed;
		//}
	}
}
