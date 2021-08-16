namespace WeQuiz.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WeQuiz.Services.Statistics;
    using WeQuiz.Services.Users;

    public class UsersController : AdminController
    {
        private readonly IStatisticsService statistics;
        private readonly IUsersService usersService;

        public UsersController(IStatisticsService statistics, IUsersService usersService)
        {
            this.statistics = statistics;
            this.usersService = usersService;
        }
        public IActionResult Pending()
        {
            var pendingAdmins = this.usersService.PendingAdmins();

            return View(pendingAdmins);
        }

        public IActionResult SchoolAdmins()
        {
            var allSchoolAdmins = this.usersService.AllSchoolAdmins();

            return View(allSchoolAdmins);
        }

        public IActionResult Home()
        {
            var usersStatistics = this.statistics.TotalUsersStatistics();

            return View(usersStatistics);
        }

        public IActionResult Approve(string id)
        {
            usersService.ApproveAdmin(id);

            return Redirect("/Admin/Users/Pending");
        }

        public IActionResult Deny(string id)
        {
            usersService.DenyAdmin(id);

            return Redirect("/Admin/Users/Pending");
        }
    }
}
