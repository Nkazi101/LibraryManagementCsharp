using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LibrarySystem.Models
{
	//table
	[Table("Book")]
	public class Book
	{
		public Book()
		{
		}

		
		//fields
		[Key]
		public int BookID { get; set; }
		
		public string Title { get; set; }
      
        public string ISBN { get; set; }
        
        public DateTime PublishedDate { get; set; }
       
        public bool Available { get; set; }
     
        public DateTime DateBorrowed { get; set; }
   
        public string photoUrl { get; set; }

		public double Price { get; set; }



		//foreign key properties
		[ForeignKey("AuthorID")]
		public int AuthorID { get; set; }
        [ForeignKey("PublisherID")]
        public int PublisherID { get; set; }


		public Author Author { get; set; }
		public Publisher Publisher { get; set; }
		public List<BookGenre> BookGenres { get; set; }
        public List<BookCart> booksinCart { get; set; }




    }
}

