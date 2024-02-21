using System;
namespace LibrarySystem.Models
{
	//for a many-to-many relationship
	public class BookGenre
	{
		public BookGenre()
		{
		}
		//the primary key of the joined table
        public int BookGenreId { get; set; }

		//foreign key properties
        public int BookID { get; set; }
		public int GenreID { get; set; }

        public Book Book { get; set; }
		public Genre Genre { get; set; }
    }
}

