namespace WeQuiz.Services.Categories
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<MainCategoriesServiceModel> MainCategories();

        IEnumerable<PendingCategoryServiceModel> PendingCategories();

        IEnumerable<AllCategoriesServiceModel> All();

        void Add(CategoryServiceModel category);

        void ApproveCategory(int id);

        void DenyCategory(int id);
    }
}
