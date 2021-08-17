namespace WeQuiz.Services.Categories
{
    using System.Collections.Generic;
    using System.Linq;
    using WeQuiz.Data;
    using WeQuiz.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly WeQuizDbContext data;

        public CategoriesService(WeQuizDbContext data)
            => this.data = data;

        public IEnumerable<MainCategoriesServiceModel> MainCategories()
        {
            var mainCategories = this.data
                .Categories
                .Select(c => new MainCategoriesServiceModel
                {
                    Category = c.Name,
                    SchoolId = c.SchoolId,
                })
                .OrderBy(c => c.Category)
                .ToList();

            foreach (var cat in mainCategories)
            {
                if (cat.SchoolId != 0)
                {
                    cat.SchoolName = this.data
                        .Schools
                        .First(s => s.Id == cat.SchoolId).Name;
                }
            }
;
            return mainCategories;
        }

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

            return allCategories;
        }

        public IEnumerable<CategoryServiceModel> Categories()
        {
            var categories =  this.data
                .Categories
                .Select(c => new CategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    SchoolId = c.SchoolId
                })
                .ToList();

            return categories;
        }

        public IEnumerable<PendingCategoryServiceModel> PendingCategories()
        {
            var pendingCategories = this.data
               .SuggestedCategories
               .Select(c => new PendingCategoryServiceModel
               {
                   Id = c.Id,
                   Name = c.Name,
                   Description = c.Description,
                   SchoolId = c.SchoolId
               })
               .OrderBy(c => c.Name)
               .ToList();

            foreach (var cat in pendingCategories)
            {
                if (cat.SchoolId != 0)
                {
                    var school = data.Schools.FirstOrDefault(s => s.Id == cat.SchoolId);

                    cat.SchoolName = school == null ? "" : school.Name;
                }
            }

            return pendingCategories;
        }

        public void Add(string name, int schoolCode)
        {
            var school = data.Schools
                .FirstOrDefault(s => s.SchoolCode == schoolCode);

            data.Categories.Add(new Category
            {
                Name = name,
                SchoolId = school == null ? 0 : school.Id
            });

            data.SaveChanges();
        }

        public void AddSuggestedCategory(string name, string description, bool isPrivate, string userId)
        {
            var currentUser = data.Users.Find(userId);

            var schoolId = isPrivate ? currentUser.SchoolId : 0;

            var newCategory = new SuggestedCategory
            {
                Name = name,
                Description = description,
                SchoolId = schoolId
            };

            data.SuggestedCategories.Add(newCategory);
            data.SaveChanges();
        }

        public void AddSuggestedSubcategory(string name, string description, int categoryId, bool isPrivate, string userId) 
        {
            var currentUser = data.Users.Find(userId);

            var schoolId = isPrivate ? 
                currentUser.SchoolId : 
                this.data.Categories.Find(categoryId).SchoolId;

            var newSubcategory = new SuggestedSubcategory
            {
                Name = name,
                Description = description,
                CategoryId = categoryId,
                SchoolId = schoolId
            };

            data.SuggestedSubcategories.Add(newSubcategory);
            data.SaveChanges();
        }

        public void ApproveCategory(int id)
        {
            var categoryToApprove = data.SuggestedCategories.Find(id);

            if (categoryToApprove != null)
            {
                var newCategory = new Category
                {
                    Name = categoryToApprove.Name,
                    SchoolId = categoryToApprove.SchoolId
                };

                data.Categories.Add(newCategory);
                data.SuggestedCategories.Remove(categoryToApprove);
                data.SaveChanges();
            }
        }

        public void DenyCategory(int id)
        {
            var categoryToDeny = data.SuggestedCategories.Find(id);

            if (categoryToDeny != null)
            {
                data.SuggestedCategories.Remove(categoryToDeny);
                data.SaveChanges();
            }
        }

        public bool HasParentCategoryById(int id) 
        {
            if (this.data.Categories.Any(c => c.Id == id))
            {
                return true;
            }

            return false;
        }

    }
}
