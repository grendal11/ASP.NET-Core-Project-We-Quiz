namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WeQuiz.Services.Categories;
    using WeQuiz.Infrastructure;
    using WeQuiz.Services.Users;
    using WeQuiz.Models.Categories;

    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categories;
        private readonly IUsersService users;

        public CategoriesController(ICategoriesService categories, IUsersService users)
        {
            this.categories = categories;
            this.users = users;
        }

        public IActionResult All()
        {
            var allCategories = this.categories.All();

            return View(allCategories);
        }

        public IActionResult Add() => View();

        public IActionResult Teacher()
        {
            var schoolId = this.users.SchoolId(User.Id());

            var pending = this.categories.PendingSubcategories(schoolId);

            return View(pending);
        }

        public IActionResult AddSubcategory() => View(new AddSubCategoryFormModel
        {
            Categories = this.categories.Categories()
        });

        [HttpPost]
        public IActionResult AddSubcategory(AddSubCategoryFormModel subCategory)
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

            var schoolId = this.users.SchoolId(userId);

            this.categories.AddSubcategory(subCategory.Name, subCategory.CategoryId, schoolId);

            return RedirectToAction("Teacher", "Categories");
        }

        public IActionResult ApproveSubcategory(int id) 
        {
            this.categories.ApproveSubcategory(id);

            return RedirectToAction("Teacher", "Categories");
        }

        public IActionResult DenySubcategory(int id)
        {
            this.categories.DenySubcategory(id);

            return RedirectToAction("Teacher", "Categories");
        }
    }
}
