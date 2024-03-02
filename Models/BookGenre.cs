using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LibrarySystem.Models
{
	//for a many-to-many relationship
	[Table("BookGenre")]
	public class BookGenre
	{
		public BookGenre()
		{
		}
		//the primary key of the joined table
		[Key]
        public int BookGenreId { get; set; }

		//foreign key properties
		[ForeignKey("bookID")]
        public int BookID { get; set; }
        [ForeignKey("bookID")]
        public int GenreID { get; set; }

        public Book Book { get; set; }
		public Genre Genre { get; set; }
    }
}

