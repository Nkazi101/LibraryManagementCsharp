using System;
using LibrarySystem.Models;
namespace LibrarySystem.Repository
{
	public interface IBookRepository
	{
	
		//Task<Book> CreateBook(Book book);
		Task<List<Book>> GetAllBooks();
		Task<Book> GetBookById(int bookId);
		Task<List<Book>> SearchBookByTitle(string searchTitle);
		Task<Book> SaveBook(Book book);
		Task<Book> UpdateBook(Book book);
	}
}

