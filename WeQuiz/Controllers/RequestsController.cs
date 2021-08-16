namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using WeQuiz.Data;
    using WeQuiz.Data.Models;
    using WeQuiz.Infrastructure;
    using WeQuiz.Models.Requests;
    using WeQuiz.Services.Users;

    public class RequestsController : Controller
    {
        private readonly WeQuizDbContext data;
        private readonly IUsersService userService;

        public RequestsController(WeQuizDbContext data, IUsersService usersService)
        {
            this.data = data;
            this.userService = usersService;
        }

        [Authorize]
        public IActionResult All() => View();

        [Authorize]
        public IActionResult School() => View();

        [HttpPost]
        [Authorize]
        public IActionResult School(SchoolRequestFormModel school)
        {
            if (!ModelState.IsValid)
            {
                return View(school);
            }

            var newSchool = new SchoolRequest
            {
                Name = school.Name,
                District = school.District,
                PopulatedArea = school.PopulatedArea
            };

            data.SchoolRequests.Add(newSchool);
            data.SaveChanges();

            return RedirectToAction("All", "Schools");
        }

        [Authorize]
        public IActionResult Category()
        {
            if (!User.IsSchoolAdmin() && !User.IsTeacher())
            {
                return RedirectToAction("All", "Requests");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Category(CategoryRequestFormModel category)
        {
            var userId = User.Id();
            var currentUser = data.Users.Find(userId);

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var newCategory = new SuggestedCategory
            {
                Name = category.Name,
                Description = category.Description,
                SchoolId = category.IsPrivate ? currentUser.SchoolId : 0
            };

            data.SuggestedCategories.Add(newCategory);
            data.SaveChanges();

            return RedirectToAction("All", "Requests");
        }

        [Authorize]
        public IActionResult Subcategory() => View(new SubcategoryRequestFormModel
        {
            Categories = this.GetCategories()
        });

        [HttpPost]
        [Authorize]
        public IActionResult Subcategory(SubcategoryRequestFormModel subCategory)
        {
            if (!this.data.Categories.Any(c => c.Id == subCategory.CategoryId))
            {
                this.ModelState.AddModelError(nameof(subCategory.CategoryId), "Категорията не съществува.");
            }

            if (!ModelState.IsValid)
            {
                subCategory.Categories= this.GetCategories();

                return View(subCategory);
            }

            var newSubcategory = new SuggestedSubcategory
            {
                Name = subCategory.Name,
                Description = subCategory.Description,
                CategoryId = subCategory.CategoryId,
                SchoolId = subCategory.IsPrivate ? /*currentUser.SchoolId*/ 1 : this.data.Categories.Find(subCategory.CategoryId).SchoolId
            };

            data.SuggestedSubcategories.Add(newSubcategory);
            data.SaveChanges();

            return RedirectToAction("All", "Requests");
        }


        private IEnumerable<CategoryViewModel> GetCategories()
            => this.data
            .Categories
            .Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                SchoolId = c.SchoolId
            })
            .ToList();

        [Authorize]
        public IActionResult SchoolAdmin(int id)
        {
            var user = data.Users.Find(User.Id());

            if (User.IsSchoolAdmin())
            {
                return RedirectToAction("All", "Requests");
            }

            if (string.IsNullOrEmpty(user.PhoneNumber))
            {
                return RedirectToAction("AdminPhone", "Requests");
            }

            this.userService.RequestAdmin(user.Id, id);

            return RedirectToAction("All", "Requests");
        }

        [Authorize]
        public IActionResult AdminPhone() => View();

        [Authorize]
        [HttpPost]
        public IActionResult AdminPhone(UserPhoneServiceModel phone)
        {
            this.userService.AddPhone(phone.PhoneNumber, User.Id());

            return RedirectToAction("All", "Requests");
        }

        [Authorize]
        public IActionResult Teacher() => View();

        [Authorize]
        public IActionResult Student() => View();


        [Authorize]
        public IActionResult ChoiceQuestion() => View();

        //[Authorize]
        //[HttpPost]
        //public IActionResult ChoiceQuestion() => View();

        [Authorize]
        public IActionResult TrueFalseQuestion() => View();

        //[Authorize]
        //[HttpPost]
        //public IActionResult TrueFalseQuestion() => View();

        [Authorize]
        public IActionResult ExactAnswerQuestion() => View();

        //[Authorize]
        //[HttpPost]
        //public IActionResult ExactAnswerQuestion() => View();

    }
}
