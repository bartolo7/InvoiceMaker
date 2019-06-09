using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker
{
	//David Bartolome 09-04-2017 Assigment Animal Motel

	interface IListManager<T>
	{

		/// <summary>Count items/// </summary>
		int ElementCount { get; }


		/// <summary>Add item/// </summary>
		void AddElement(T item);

		/// <summary>Retrive item/// </summary>
		T RetrieveElementAtPosition(int index);

		/// <summary>Update item/// </summary>
		void ChangeElementAtPosition(T itemIn, int index);


		/// <summary>Delete item/// </summary>
		void DeleteElementAtPosition(int index);


		/// <summary>Delele all items/// </summary>
		void DeleteAllElements();


		/// <summary>/// </summary>
		bool IsIndexValid(int index);


		/// <summary>Print list string/// </summary>
		List<string> ToStringList();

		/// <summary>Print list array/// </summary>
		string[] ToStringArray();


		/// <summary>Read item/// </summary>
		void ReadElementAtPosition(int index);


		/// <summary>
		/// Serialize
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		bool BinarySerialize(string fileName);

		/// <summary>
		/// Deserialize
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		bool BinaryDeSerialize(string fileName);


		/// <summary>
		/// XML Serialize
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		bool XMLSerialize(string fileName);


		/// <summary>
		/// XML Deserialize
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		bool XMLDeSerialize(string fileName);



	}
}
