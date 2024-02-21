using System;
namespace LibrarySystem.Models
{
	public class Publisher
	{
		public Publisher()
		{
		}

		public int PublisherID { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }

		//navigating property for the one-to-many relationship
		public List<Book> Books { get; set; }
	}
}

