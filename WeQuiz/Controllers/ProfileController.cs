namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using WeQuiz.Data;
    using WeQuiz.Infrastructure;
    using WeQuiz.Models.Profile;

    public class ProfileController : Controller
    {
        private readonly WeQuizDbContext data;

        public ProfileController(WeQuizDbContext data)
        {
            this.data = data;
        }

        public IActionResult Info()
        {
            var userId = User.Id();

            var user = data.Users.Find(userId);

            if (User.IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            var roleName = "";
            if (User.IsSchoolAdmin())
            {
                roleName = "Училищен администратор";
            }
            else if (User.IsTeacher())
            {
                roleName = "Учител";
            }
            else if (User.IsSchoolAdmin())
            {
                roleName = "Ученик";
            }


            var userInfo = new ProfileViewModel
            {
                UserId = userId,
                Email = user.Email,
                FullName = user.FullName,
                Alias = user.Alias,
                SchoolName = this.data.Schools.Find(user.SchoolId).Name,
                RoleName = roleName
            };

            return View(userInfo);
        }
    }
}
