namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WeQuiz.Infrastructure;
    using WeQuiz.Services.Statistics;
    using WeQuiz.Services.Users;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IUsersService users;

        public HomeController(IStatisticsService statistics, IUsersService users)
        {
            this.statistics = statistics;
            this.users = users;
        }

        public IActionResult Index()
        {
            var totals = this.statistics.Totals();

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Id();

                var userAlias = this.users.Alias(userId);

                var userRole = User.RoleName();

                ViewBag.Role = userRole == "" ? "Потребител без училище" : userRole;

                ViewBag.Alias = userAlias;
            }

            return View(totals);
        }

        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 404)
                {
                    var viewName = statusCode.ToString();
                    return View(viewName);
                }
            }

            return View();
        }
    }
}
