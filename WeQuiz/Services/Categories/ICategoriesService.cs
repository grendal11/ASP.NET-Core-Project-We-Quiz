namespace WeQuiz.Services.Categories
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<MainCategoriesServiceModel> MainCategories();

        IEnumerable<AllCategoriesServiceModel> All();

        void Add(CategoryServiceModel category);
    }
}
