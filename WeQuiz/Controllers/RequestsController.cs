namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WeQuiz.Infrastructure;
    using WeQuiz.Models.Requests;
    using WeQuiz.Services.Categories;
    using WeQuiz.Services.Schools;
    using WeQuiz.Services.Users;

    public class RequestsController : Controller
    {
        private readonly IUsersService users;
        private readonly ISchoolsService schools;
        private readonly ICategoriesService categories;

        public RequestsController(IUsersService users, ISchoolsService schools, ICategoriesService categories)
        {
            this.users = users;
            this.schools = schools;
            this.categories = categories;
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

            this.schools.AddSchoolRequest(school.Name, school.District, school.PopulatedArea);

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

            if (!ModelState.IsValid)
            {
                return View(category);
            }                

            this.categories.AddSuggestedCategory(category.Name, category.Description, category.IsPrivate, userId);

            return RedirectToAction("All", "Requests");
        }

        [Authorize]
        public IActionResult Subcategory() => View(new SubcategoryRequestFormModel
        {
            Categories = this.categories.Categories()
        });

        [HttpPost]
        [Authorize]
        public IActionResult Subcategory(SubcategoryRequestFormModel subCategory)
        {
            if (!this.categories.HasParentCategoryById(subCategory.CategoryId))
            {
                this.ModelState.AddModelError(nameof(subCategory.CategoryId), "Категорията не съществува.");
            }

            if (!ModelState.IsValid)
            {
                subCategory.Categories = this.categories.Categories();

                return View(subCategory);
            }

            var userId = User.Id();

            this.categories.AddSuggestedSubcategory(
                subCategory.Name, 
                subCategory.Description, 
                subCategory.CategoryId, 
                subCategory.IsPrivate, 
                userId);

            return RedirectToAction("All", "Requests");
        }


        [Authorize]
        public IActionResult SchoolAdmin(int id)
        {
            var userId = User.Id();

            if (User.IsSchoolAdmin())
            {
                return RedirectToAction("All", "Requests");
            }

            if (!this.users.HasPhone(userId))
            {
                return RedirectToAction("AdminPhone", "Requests");
            }

            this.users.RequestAdmin(userId, id);

            return RedirectToAction("All", "Requests");
        }

        [Authorize]
        public IActionResult AdminPhone() => View();

        [Authorize]
        [HttpPost]
        public IActionResult AdminPhone(UserPhoneServiceModel phone)
        {
            var userId = User.Id();

            this.users.AddPhone(phone.PhoneNumber, userId);

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
