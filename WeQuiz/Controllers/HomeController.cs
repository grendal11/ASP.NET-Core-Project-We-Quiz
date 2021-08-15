namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Mvc;

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
