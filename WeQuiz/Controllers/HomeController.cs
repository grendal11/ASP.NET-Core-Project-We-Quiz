namespace WeQuiz.Controllers
{
    using System.Diagnostics;
    using WeQuiz.Models;
    using Microsoft.AspNetCore.Mvc;
    using WeQuiz.Infrastructure;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //if (User.Identity.IsAuthenticated && User.IsAdmin())
            //{
            //    return RedirectToAction("All", "Requests");
            //}

            return View();
        }

        public IActionResult Error()  => View();
    }
}
