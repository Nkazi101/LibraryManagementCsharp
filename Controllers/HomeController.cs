using System;
using Microsoft.AspNetCore.Mvc;
namespace LibrarySystem.Controllers
{
	public class HomeController : Controller
	{
		public HomeController()
		{
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}

