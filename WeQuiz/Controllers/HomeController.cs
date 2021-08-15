namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WeQuiz.Data;
    using WeQuiz.Infrastructure;
    using WeQuiz.Services.Statistics;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly WeQuizDbContext data;

        public HomeController(IStatisticsService statistics, WeQuizDbContext data)
        {
            this.statistics = statistics;
            this.data = data;
        }

        public IActionResult Index()
        {
            var totals = statistics.Totals();

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Id();

                var userAlias = this.data.Users.Find(userId).Alias;

                ViewBag.Alias = userAlias;
            }

            return View(totals);
        }

        public IActionResult Error() => View();
    }
}
