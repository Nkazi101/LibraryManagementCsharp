using System;
namespace LibrarySystem.Models
{
	//table 
	public class Book
	{
		public Book()
		{
		}

		//fields
		public int BookID { get; set; }
		public string Title { get; set; }
		public string ISBN { get; set; }
		public DateTime PublishedDate { get; set; }
		public bool Available { get; set; }
		public DateTime DateBorrowed { get; set; }
		public string photoUrl { get; set; }
		


		//foreign key properties
		public int AuthorID { get; set; }
		public int PublisherID { get; set; }


		public Author Author { get; set; }
		public Publisher Publisher { get; set; }
		public List<BookGenre> BookGenres { get; set; }
		 

		
	
    }
}

