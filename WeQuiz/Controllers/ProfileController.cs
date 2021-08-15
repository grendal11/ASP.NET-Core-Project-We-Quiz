namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using WeQuiz.Data;
    using WeQuiz.Infrastructure;
    using WeQuiz.Models.Profile;
    using WeQuiz.Views.Profile;

    public class ProfileController : Controller
    {
        private readonly WeQuizDbContext data;

        public ProfileController(WeQuizDbContext data)
        {
            this.data = data;
        }

        [Authorize]
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
            else if (User.IsStudent())
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
                RoleName = roleName,
                Class = User.IsStudent() ?
                    data.Students.First(s => s.UserId == userId).Class : 0
            };

            if (User.IsTeacher())
            {
                var teacherId = data.Teachers.First(t => t.UserId == userId).Id;
                var categories = data.TeachersCategories
                    .Where(t=>t.TeacherId == teacherId)
                    .Select(c => new CategoryViewModel
                    { 
                       Name = c.Category.Name
                    })
                    .ToList();
                userInfo.Categories = categories;
            }

            return View(userInfo);
        }
    }
}
