namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WeQuiz.Services.Statistics;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;

        public HomeController(IStatisticsService statistics)
        {
            this.statistics = statistics;
        }

        public IActionResult Index()
        {
            var totals = statistics.Totals();

            return View(totals);
        }

        public IActionResult Error()  => View();
    }
}
