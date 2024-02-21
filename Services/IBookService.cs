using System;
using LibrarySystem.Models;
namespace LibrarySystem.Services
{
	public interface IBookService
	{

		Task<List<Book>> GetAllBooks();
        Task<Book> GetBookById(int bookId);
		Task<List<Book>> SearchBookByTitle(string searchTitle);

    }
}

