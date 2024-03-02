using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using LibrarySystem.Models;
using LibrarySystem.Services;

namespace LibrarySystem.Services
{
	public class CartService
	{

		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IBookService _bookService;

		public CartService(IHttpContextAccessor httpContextAccessor, IBookService bookService)
		{
			_httpContextAccessor = httpContextAccessor;
			_bookService = bookService;
		}

        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public Cart getCart()
		{
		   var cartJson = _httpContextAccessor.HttpContext.Session.GetString("currentCart");

           Cart cart = JsonConvert.DeserializeObject<Cart>(cartJson);

            if (cart == null)
			{
				cart = new Cart();
				var serCart = JsonConvert.SerializeObject(cart);

				_httpContextAccessor.HttpContext.Session.SetString("newCart", serCart);
			}
			return cart;
        }


		public async void addtoCart(int bookID)
		{

			Cart cart = getCart();
			Book bookToAdd = await _bookService.GetBookById(bookID);
			if(bookToAdd != null)
			{
				BookCart bookCart = new BookCart();

				bookCart.CartID = cart.CartID;
				bookCart.BookID = bookToAdd.BookID;

				
				cart.booksinCart.Add(bookCart);
			}


			
		}

	}
}

