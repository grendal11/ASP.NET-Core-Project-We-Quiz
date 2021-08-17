namespace WeQuiz.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WeQuiz.Data;
    using WeQuiz.Infrastructure;
    using WeQuiz.Models.Profile;
    using WeQuiz.Services.Users;
    using WeQuiz.Views.Profile;

    public class ProfileController : Controller
    {
        private readonly WeQuizDbContext data;
        private readonly IUsersService users;

        public ProfileController(WeQuizDbContext data, IUsersService users)
        {
            this.data = data;
            this.users = users;
        }

        [Authorize]
        public IActionResult Info()
        {
            var userId = User.Id();

            if (User.IsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }
            
            var roleName = User.RoleName();
                        
            var user = data.Users.Find(userId);
   
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

        [Authorize]
        public IActionResult Edit()
        {
            var userId = User.Id();

            var info = this.users.EditableInfo(userId);

            var displayInfo = new ProfileFormModel
            { 
                FullName = info.FullName,
                Alias = info.Alias,
                PhoneNumber = info.PhoneNumber
            };

            return View(displayInfo);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(ProfileFormModel profile)
        {
            var userId = User.Id();                      

            this.users.EditProfile(profile.FullName, profile.Alias, profile.PhoneNumber, userId);

            return RedirectToAction("Info", "Profile");
        }
    }
}
