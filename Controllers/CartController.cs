using Microsoft.AspNetCore.Mvc;
using LibrarySystem.Models;
using Newtonsoft.Json;
using LibrarySystem.Services;

public class CartController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBookService _bookService;
    private readonly CartService _cartService;

    public CartController(IHttpContextAccessor httpContextAccessor, IBookService bookService, CartService cartService)
    {
        _httpContextAccessor = httpContextAccessor;
        _bookService = bookService;
        _cartService = cartService;
    }

    public IActionResult Cart()
    {
        Cart cart = _cartService.getCart();
        List<BookCart> bookCarts = cart.booksinCart;
        return View(bookCarts);
    }

    public async Task<IActionResult> addToCart(int bookID)
    {
        await _cartService.addtoCart(bookID);
        Cart updatedCart = _cartService.getCart();
        List<BookCart> newbooks = updatedCart.booksinCart;
        return View("Cart", newbooks);
    }

    public async Task<IActionResult> removeFromCart(int bookID)
    {
        _cartService.RemoveFromCart(bookID);
        Cart updatedCart = _cartService.getCart();
        return View("Cart", updatedCart.booksinCart);
    }
}