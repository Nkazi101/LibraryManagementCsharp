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

            if (cartJson == null)
            {
                var newCart = new Cart();
                newCart.booksinCart = new List<BookCart>(); // Initialize booksinCart
                var serCart = JsonConvert.SerializeObject(newCart);
                _httpContextAccessor.HttpContext.Session.SetString("currentCart", serCart);

                return newCart;
            }
            else
            {
                Cart cart = JsonConvert.DeserializeObject<Cart>(cartJson);
                if (cart.booksinCart == null)
                {
                    cart.booksinCart = new List<BookCart>(); // Initialize booksinCart
                }
                return cart;
            }
        }



        public async Task<Cart> addtoCart(int bookID)
        {

            Cart cart = getCart();
            Book bookToAdd = await _bookService.GetBookById(bookID);
            if (bookToAdd != null)
            {
                BookCart bookCart = new BookCart();

                bookCart.CartID = cart.CartID;
                bookCart.BookID = bookToAdd.BookID;

                bookCart.Book = bookToAdd;

                cart.booksinCart.Add(bookCart);
                
                UpdateSessionCart(cart);
            }

            return cart;
        }

        public void RemoveFromCart(int id)
        {
            Cart cart = getCart();
            List<BookCart> itemsToRemove = new List<BookCart>();

            foreach (var bookCart in cart.booksinCart)
            {
                if (bookCart.BookID == id)
                {
                    itemsToRemove.Add(bookCart);
                }
            }

            foreach (var itemToRemove in itemsToRemove)
            {
                cart.booksinCart.Remove(itemToRemove);
            }

            UpdateSessionCart(cart);
        }

        private void UpdateSessionCart(Cart cart)
        {
            
            var serializedCart = JsonConvert.SerializeObject(cart);
            _httpContextAccessor.HttpContext.Session.SetString("currentCart", serializedCart);
        }
    }

	}


