namespace WeQuiz.Controllers
{
    using System.Diagnostics;
    using WeQuiz.Models;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Error()  => View();
    }
}
