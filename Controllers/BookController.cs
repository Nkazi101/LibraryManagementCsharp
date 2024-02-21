using System;
using Microsoft.AspNetCore.Mvc;
using LibrarySystem.Services;
using LibrarySystem.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LibrarySystem.Controllers
{
	public class BookController : Controller
	{

		private readonly IBookService _bookService;
		private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookController(IBookService bookService, IUserService userService, IHttpContextAccessor httpContextProcessor)
		{
			_bookService = bookService;
			_userService = userService;
			_httpContextAccessor = httpContextProcessor;
		}

		[HttpGet]
		public async Task<IActionResult> BookList()
		{
			List<Book> books = await _bookService.GetAllBooks();



			//pass the books to the view
			return View(books);
		}

		[HttpGet]
		public async Task<IActionResult> BookDetails(int bookID)
		{

			Book bookDetails = await _bookService.GetBookById(bookID);

			//if (bookDetails == null)
			//{
			//	return NotFound();
			//}
			return View("BookDetails", bookDetails);

		}

		
		public async Task<IActionResult> BookSearch(string searchTitle)
		{
			List<Book> foundBooks = await _bookService.SearchBookByTitle(searchTitle);
			return View("BookList", foundBooks);
		}

		public async Task<IActionResult> borrowingBook(int bookId)
		{

			string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


			if (userId == null)
			{
				return RedirectToAction("Login", "User");
			}

			int parseduserID;

			if(!int.TryParse(userId, out parseduserID))
			{
				return BadRequest("Invalid ID");
			}
			User user = await _userService.BorrowBook(parseduserID, bookId);

			//List<Book> updatedBooks = await _bookService.GetAllBooks();

			//return View("BookList", updatedBooks);

			return RedirectToAction("BookList", "Book");
		}

		
	}
}

