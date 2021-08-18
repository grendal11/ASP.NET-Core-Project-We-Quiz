namespace WeQuiz.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WeQuiz.Data;
    using WeQuiz.Infrastructure;
    using WeQuiz.Models.Profile;
    using WeQuiz.Services.Categories;
    using WeQuiz.Services.Users;
    using WeQuiz.Views.Profile;

    using static WebConstants;

    [Authorize]
    public class ProfileController : Controller
    {
        private readonly WeQuizDbContext data;
        private readonly IUsersService users;
        private readonly ICategoriesService categories;

        public ProfileController(
            WeQuizDbContext data, 
            IUsersService users, 
            ICategoriesService categories)
        {
            this.data = data;
            this.users = users;
            this.categories = categories;
        }


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
                SchoolName = user.SchoolId!=0 ? 
                    this.data.Schools.Find(user.SchoolId).Name : "",
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
;
            return View(userInfo);
        }


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

        [HttpPost]
        public IActionResult Edit(ProfileFormModel profile)
        {
            var userId = User.Id();                      

            this.users.EditProfile(profile.FullName, profile.Alias, profile.PhoneNumber, userId);

            return RedirectToAction("Info", "Profile");
        }

        [Authorize(Roles = StudentRoleName)]
        public IActionResult StudentClass() => View();

        [Authorize(Roles = StudentRoleName)]
        [HttpPost]
        public IActionResult StudentClass(StudentClassServiceModel student)
        {
            var userId = User.Id();

            this.users.AddStudentClass(student.Class, userId);

            return RedirectToAction("Info", "Profile");
        }

        [Authorize(Roles = TeacherRoleName)]
        public IActionResult TeacherCategories()
        {
            var userId = User.Id();

            var categories = this.categories.TeacherCategories(userId);

            return View(categories);
        }

        [Authorize(Roles = TeacherRoleName)]
        public IActionResult AddCategory(int id)
        {
            string userId = User.Id();

            this.categories.AddToTeacher(userId, id);

            return RedirectToAction("TeacherCategories", "Profile");
        }

        [Authorize(Roles = TeacherRoleName)]
        public IActionResult RemoveCategory(int id)
        {
            string userId = User.Id();

            this.categories.RemoveFromTeacher(userId, id);

            return RedirectToAction("TeacherCategories", "Profile");
        }
    }
}
