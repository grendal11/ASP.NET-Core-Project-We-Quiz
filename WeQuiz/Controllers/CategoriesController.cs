namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WeQuiz.Services.Categories;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categories;

        public CategoriesController(ICategoriesService categories)
        {
            this.categories = categories;
        }

        public IActionResult All()
        {
            var allCategories = this.categories.All();

            return View(allCategories);
        }

        [Authorize]
        public IActionResult Add() => View();
    }
}
