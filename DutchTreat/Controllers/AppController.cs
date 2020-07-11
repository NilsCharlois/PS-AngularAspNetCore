using System;
using DutchTreat.ViewModels;
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
			return View();
		}

		// from the contact form
		[HttpPost("contact")]
		public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
				// good
            }
            else
            {
				// bad
            }
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
