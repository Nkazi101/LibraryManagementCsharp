using System;
using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Repository
{
	public class BookRepository : IBookRepository
	{

		private readonly LibraryDBContext _context;

		public BookRepository(LibraryDBContext context)

		{
			_context = context;
		}


		public async Task<List<Book>> GetAllBooks()
		{
			return await _context.Books.Include(b => b.Author).Include(b => b.Publisher).ToListAsync();
		}

		public async Task<Book> GetBookById(int bookID)
		{
			return await _context.Books.Where(book => book.BookID == bookID).FirstAsync();
		}

		public async Task<List<Book>> SearchBookByTitle(string searchTitle)
		{
			return await _context.Books.Where(book => book.Title.Contains(searchTitle, StringComparison.OrdinalIgnoreCase)).ToListAsync();
		}

        public async Task<Book> SaveBook(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateBook(Book book)
        {
			_context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return book;
        }
    }
}

