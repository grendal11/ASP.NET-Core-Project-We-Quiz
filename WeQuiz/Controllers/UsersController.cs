namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WeQuiz.Infrastructure;
    using WeQuiz.Services.Statistics;
    using WeQuiz.Services.Users;

    using static WebConstants;

    [Authorize(Roles = SchoolAdminRoleName)]
    public class UsersController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IUsersService users;

        public UsersController(IStatisticsService statistics, IUsersService users)
        {
            this.statistics = statistics;
            this.users = users;
        }

        public IActionResult Home()
        {
            var userId = User.Id();

            var usersStatistics = this.statistics.TotalSchoolUsers(userId);

            return View(usersStatistics);
        }

        public IActionResult PendingTeachers()
        {
            var schoolId = this.users.SchoolId(User.Id());
            
            var pendingAdmins = this.users.PendingTeachers(schoolId);

            return View(pendingAdmins);
        }

        public IActionResult Teachers()
        {
            var schoolId = this.users.SchoolId(User.Id());

            var allSchoolAdmins = this.users.Teachers(schoolId);

            return View(allSchoolAdmins);
        }

        public IActionResult ApproveTeacher(string id)
        {
            this.users.ApproveTeacher(id);

            return RedirectToAction("PendingTeachers", "Users");
        }

        public IActionResult DenyTeacher(string id)
        {
            this.users.DenyTeacher(id);

            return RedirectToAction("PendingTeachers", "Users");
        }

        public IActionResult RemoveTeacher(string id)
        {
            this.users.RemoveTeacher(id);

            return RedirectToAction("Teachers", "Users");
        }

        public IActionResult PendingStudents()
        {
            var schoolId = this.users.SchoolId(User.Id());

            var pendingAdmins = this.users.PendingStudents(schoolId);

            return View(pendingAdmins);
        }

        public IActionResult Students()
        {
            var schoolId = this.users.SchoolId(User.Id());

            var allSchoolAdmins = this.users.Students(schoolId);

            return View(allSchoolAdmins);
        }
                
        public IActionResult ApproveStudent(string id)
        {
            this.users.ApproveStudent(id);

            return RedirectToAction("PendingStudents", "Users");
        }

        public IActionResult DenyStudent(string id)
        {
            this.users.DenyStudent(id);

            return RedirectToAction("PendingStudents", "Users");
        }

        public IActionResult RemoveStudent(string id)
        {
            this.users.RemoveStudent(id);

            return RedirectToAction("Students", "Users");
        }
    }
}
