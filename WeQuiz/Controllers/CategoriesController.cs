namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using WeQuiz.Data;
    using WeQuiz.Models.Categories;

    public class CategoriesController : Controller
    {
        private readonly WeQuizDbContext data;

        public CategoriesController(WeQuizDbContext data)
        {
            this.data = data;
        }

        public IActionResult All()
        {
            var allCategories = this.data
                .Subcategories
                .Select(s => new AllCategoriesViewModel
                {
                    Category = s.Category.Name,
                    Subcategory = s.Name,
                    SchoolCode = s.SchoolCode,
                })
                .OrderBy(c => c.Category)
                .ThenBy(c => c.Subcategory)
                .ToList();

            foreach (var cat in allCategories)
            {
                if (cat.SchoolCode != 0)
                {
                    cat.SchoolName = this.data
                        .Schools
                        .First(s => s.SchoolCode == cat.SchoolCode).Name;
                }
            }

            return View(allCategories);
        }
    }
}
