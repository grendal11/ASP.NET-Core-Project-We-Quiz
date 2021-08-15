namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using WeQuiz.Data;
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
            var allCategories = categories.All();

            return View(allCategories);
        }

        [Authorize]
        public IActionResult Add() => View();
    }
}
