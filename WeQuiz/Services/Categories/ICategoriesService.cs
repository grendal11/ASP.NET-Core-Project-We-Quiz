namespace WeQuiz.Services.Categories
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<MainCategoriesServiceModel> MainCategories();

        IEnumerable<PendingCategoryServiceModel> PendingCategories();

        public IEnumerable<PendingSubcategoryServiceModel> PendingSubcategories(int schoolId);

        IEnumerable<AllCategoriesServiceModel> All();

        IEnumerable<CategoryServiceModel> Categories();

        void Add(string name, int schoolCode);

        public void AddSubcategory(string name, int categoryId, int schoolId);

        public void AddSuggestedCategory(string name, string description, bool isPrivate, string userId);

        public void AddSuggestedSubcategory(string name, string description, int categoryId, bool isPrivate, string userId);

        void ApproveCategory(int id);

        void DenyCategory(int id);

        void ApproveSubcategory(int id);

        void DenySubcategory(int id);

        bool HasParentCategoryById(int id);
    }
}
