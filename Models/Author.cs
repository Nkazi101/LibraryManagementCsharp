using System;
namespace LibrarySystem.Models
{
	//table, class, entity, model
	public class Author
	{

		public Author()
		{

		}
		//fields, columns, instance variables, properties
		public int AuthorID { get; set;  }
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
		public string Country { get; set; }
		public string Biography { get; set; }

		//property for the one-to-many relationship
		public List<Book> Books { get; set; }
    }


}

