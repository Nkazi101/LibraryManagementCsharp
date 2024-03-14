using System;
using Microsoft.AspNetCore.Mvc;
using LibrarySystem.StripeConfig;
using LibrarySystem.Services;
using Microsoft.Extensions.Options;
using Stripe;

namespace LibrarySystem.Controllers
{
	public class StripeController : Controller
	{

		private readonly CartService _cartService;
		private readonly StripeSettings _stripeSettings;

		public StripeController(CartService cartService, IOptions<StripeSettings> stripeSettings)
		{
			_cartService = cartService;
			_stripeSettings = stripeSettings.Value;
		}


		public async Task<IActionResult> CreateCheckOut()
		{

			var cart = _cartService.getCart();
			var lineItems = new List<Stripe.Checkout.SessionLineItemOptions>();

			foreach (var bookCart in cart.booksinCart)
			{
				lineItems.Add(new Stripe.Checkout.SessionLineItemOptions
				{
					PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
					{
						UnitAmount = (long)(bookCart.Book.Price * 100),
						Currency = "usd",

						ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
						{
							Name = bookCart.Book.Title,

						}

					},
					Quantity = 1


				});
			}

			StripeConfiguration.ApiKey = _stripeSettings.Secretkey;
			var options = new Stripe.Checkout.SessionCreateOptions
			{
				SuccessUrl = "https://localhost:7729/Stripe/success",
				CancelUrl = "https://localhost:7729/Stripe/success",
                LineItems = lineItems,
				Mode = "payment",
			};
			var service = new Stripe.Checkout.SessionService();
			var session = service.Create(options);

			return Redirect(session.Url);


		}
	}

	//public async Task<IActionResult> Success()
	//{
	//	ViewData["Message"] = "Payment successful! Thank you for your purchase.";
		
	//}

}