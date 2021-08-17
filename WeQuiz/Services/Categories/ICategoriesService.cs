namespace WeQuiz.Services.Categories
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<MainCategoriesServiceModel> MainCategories();

        IEnumerable<PendingCategoryServiceModel> PendingCategories();

        IEnumerable<AllCategoriesServiceModel> All();

        public IEnumerable<CategoryServiceModel> Categories();

        void Add(string name, int schoolCode);

        public void AddSuggestedCategory(string name, string description, bool isPrivate, string userId);

        public void AddSuggestedSubcategory(string name, string description, int categoryId, bool isPrivate, string userId);

        void ApproveCategory(int id);

        void DenyCategory(int id);

        bool HasParentCategoryById(int id);
    }
}
