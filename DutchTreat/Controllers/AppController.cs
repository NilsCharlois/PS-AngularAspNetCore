using System;
using DutchTreat.ViewModels;
using DutchTreat.Services; 
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{

	public class AppController : Controller
	{
		private readonly IMailService _mailService;

		public AppController(IMailService mailService)
		{
			_mailService = mailService;
		}

		public IActionResult Index()
		{
			ViewBag.Title = "Home Page";
			return View();
		}

		[HttpGet("contact")]
		public IActionResult Contact()
		{
			ViewBag.Title = "Contact Us";

			//throw new InvalidOperationException("Oh Jesus");

			return View();
		}

		// from the contact form
		[HttpPost("contact")]
		public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
				// send email
				_mailService.SendEMail(
					"test@test.com",
					model.Subject,
					$"From:{model.Name} - {model.Email}",
					$"Message: ${model.Message}"
				);

				ViewBag.UserMessage = "Mail sent";
				ModelState.Clear();
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
