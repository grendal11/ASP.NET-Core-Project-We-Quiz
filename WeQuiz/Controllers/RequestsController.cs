namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using WeQuiz.Data;
    using WeQuiz.Data.Models;
    using WeQuiz.Models.Requests;

    public class RequestsController : Controller
    {
        private readonly WeQuizDbContext data;

        public RequestsController(WeQuizDbContext data)
        {
            this.data = data;
        }

        public IActionResult All() => View();

        public IActionResult School() => View();

        [HttpPost]
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


        public IActionResult Category() => View();

        [HttpPost]
        public IActionResult Category(CategoryRequestFormModel category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            //var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var currentUser = this.data.Users.Find(userId);

            var newCategory = new SuggestedCategory
            {
                Name = category.Name,
                Description = category.Description,
                SchoolId = category.IsPrivate ? /*currentUser.SchoolId*/ 1 : 0
            };

            data.SuggestedCategories.Add(newCategory);
            data.SaveChanges();

            return RedirectToAction("All", "Requests");
        }

        public IActionResult Subcategory() => View(new SubcategoryRequestFormModel
        {
            Categories = this.GetCategories()
        });

        [HttpPost]
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

    }
}
