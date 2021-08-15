namespace WeQuiz.Services.Categories
{
    using System.Collections.Generic;
    using System.Linq;
    using WeQuiz.Data;

    public class CategoriesService : ICategoriesService
    {
        private readonly WeQuizDbContext data;

        public CategoriesService(WeQuizDbContext data)
            => this.data = data;

        public IEnumerable<AllCategoriesServiceModel> All()
        {
            var allCategories = this.data
                .Subcategories
                .Select(s => new AllCategoriesServiceModel
                {
                    Category = s.Category.Name,
                    Subcategory = s.Name,
                    SchoolId = s.SchoolId,
                })
                .OrderBy(c => c.Category)
                .ThenBy(c => c.Subcategory)
                .ToList();

            foreach (var cat in allCategories)
            {
                if (cat.SchoolId != 0)
                {
                    cat.SchoolName = this.data
                        .Schools
                        .First(s => s.Id == cat.SchoolId).Name;
                }
            }
;
            return allCategories;
        }
    }
}
