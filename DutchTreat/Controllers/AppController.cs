using System;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{

	public class AppController : Controller
	{
		public IActionResult Index()
		{
			ViewBag.Title = "Home Page";
			return View();
		}

		[HttpGet("contact")]
		public IActionResult Contact()
		{
			ViewBag.Title = "Contact Us";

			throw new InvalidOperationException("Oh Jesus");
			return View();
		}

		[HttpGet("about")]
		public IActionResult About()
		{
			ViewBag.Title = "About Us";
			return View();
		}
	}

}
