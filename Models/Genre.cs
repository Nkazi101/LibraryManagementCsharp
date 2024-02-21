using System;
namespace LibrarySystem.Models
{
	public class Genre
	{
		public Genre()
		{
		}

		public int GenreID { get; set; }
		public string Name { get; set; }

		public List<BookGenre> BookGenres { get; set; }
    }
}

