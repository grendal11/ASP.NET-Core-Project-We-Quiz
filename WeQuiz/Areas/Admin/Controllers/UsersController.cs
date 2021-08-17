namespace WeQuiz.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WeQuiz.Services.Statistics;
    using WeQuiz.Services.Users;

    public class UsersController : AdminController
    {
        private readonly IStatisticsService statistics;
        private readonly IUsersService users;

        public UsersController(IStatisticsService statistics, IUsersService users)
        {
            this.statistics = statistics;
            this.users = users;
        }
        public IActionResult Pending()
        {
            var pendingAdmins = this.users.PendingAdmins();

            return View(pendingAdmins);
        }

        public IActionResult SchoolAdmins()
        {
            var allSchoolAdmins = this.users.AllSchoolAdmins();

            return View(allSchoolAdmins);
        }

        public IActionResult Home()
        {
            var usersStatistics = this.statistics.TotalUsersStatistics();

            return View(usersStatistics);
        }

        public IActionResult Approve(string id)
        {
            this.users.ApproveAdmin(id);

            return Redirect("/Admin/Users/Pending");
        }

        public IActionResult Deny(string id)
        {
            this.users.DenyAdmin(id);

            return Redirect("/Admin/Users/Pending");
        }

        public IActionResult Remove(string id)
        {
            this.users.RemoveAdmin(id);

            return Redirect("/Admin/Users/SchoolAdmins");
        }
    }
}
