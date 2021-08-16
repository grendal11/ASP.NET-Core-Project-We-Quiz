namespace WeQuiz.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WeQuiz.Areas.Admin.Models;
    using WeQuiz.Services.Categories;
    using WeQuiz.Services.Statistics;

    public class CategoriesController : AdminController
    {
        private readonly IStatisticsService statistics;
        private readonly ICategoriesService categories;

        public CategoriesController(IStatisticsService statistics, ICategoriesService categories)
        {
            this.statistics = statistics;
            this.categories = categories;
        }

        public IActionResult Home()
        {
            var categoryStatistics = this.statistics.CategoriesStatistics();

            return View(categoryStatistics);
        }

        public IActionResult All()
        {
            var mainCategories = this.categories.MainCategories();

            return View(mainCategories);
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(CategoryFormModel category)
        {
            var newCategory = new CategoryServiceModel
            {   
                Name=category.Name,
                SchoolCode = category.SchoolCode
            };

            this.categories.Add(newCategory);

            return Redirect("/Admin/Categories/All");
        }

        public IActionResult Pending()
        {
            var pendingCategories = this.categories.PendingCategories();

            return View(pendingCategories);
        }

        public IActionResult Approve(int id)
        {
            categories.ApproveCategory(id);

            return Redirect("/Admin/Categories/Pending");
        }

        public IActionResult Deny(int id)
        {
            categories.DenyCategory(id);

            return Redirect("/Admin/Categories/Pending");
        }
    }
}
