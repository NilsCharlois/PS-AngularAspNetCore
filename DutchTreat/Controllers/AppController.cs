using System;
using DutchTreat.ViewModels;
using DutchTreat.Services; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace DutchTreat.Controllers
{

	public class AppController : Controller
	{
		private readonly IMailService _mailService;
		private readonly DutchTreat.Data.IDutchRepository _repo;

		public AppController(IMailService mailService, DutchTreat.Data.IDutchRepository repo)
		{
			_mailService = mailService;
			_repo = repo;
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

		[HttpGet("shop")]
		[Authorize]
		public IActionResult Shop()
		{			
			var results = _repo.GetAllProducts();
			return View(results.ToList());
		}
	}

}
