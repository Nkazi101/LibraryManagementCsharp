using System;
using LibrarySystem.Repository;
using LibrarySystem.Models;
namespace LibrarySystem.Services
{
	public class BookService : IBookService
	{

		private readonly IBookRepository _bookRepository;

		public BookService(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}

		public async Task<List<Book>> GetAllBooks()
		{

			List<Book> availableBooks = await _bookRepository.GetAllBooks();

			return availableBooks;
		}

		public async Task<Book> GetBookById(int bookID)
		{
			return await _bookRepository.GetBookById(bookID);
		}

		public async Task<List<Book>> SearchBookByTitle(string searchTitle)
		{
			return await _bookRepository.SearchBookByTitle(searchTitle);
		}

		
	}
}

