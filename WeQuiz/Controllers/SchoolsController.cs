namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using WeQuiz.Infrastructure;
    using WeQuiz.Services.Schools;
    using WeQuiz.Services.Users;

    [Authorize]
    public class SchoolsController : Controller
    {
        private readonly IUsersService users;
        private readonly ISchoolsService schools;

        public SchoolsController(ISchoolsService schools, IUsersService users)
        {
            this.schools = schools;
            this.users = users;
        }

        public IActionResult All()
        {
            var schools = this.schools.All();

            var userId = User.Id();

            ViewBag.Role = User.RoleName();
            ViewBag.SchoolId = this.users.SchoolId(userId);

            return View(schools);
        }


    }
}
