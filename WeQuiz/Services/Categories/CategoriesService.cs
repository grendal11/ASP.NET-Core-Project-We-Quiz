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
            var categories = this.data
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
        
        public IEnumerable<CategoryServiceModel> OwnCategories(string userId)
        {
            var schoolId = this.data.Users.Find(userId).SchoolId;
                        
            var categories = this.data
                .Categories
                .Where(c=> c.SchoolId == 0 || c.SchoolId == schoolId)
                .Select(c => new CategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    SchoolId = c.SchoolId
                })
                .ToList();

            return categories;
        }
        
        public IEnumerable<TeacherCategoryServiceModel> TeacherCategories(string userId)
        {
            var schoolId = this.data.Users.Find(userId).SchoolId;

            var categories = this.data.Categories
                .Select(c => new TeacherCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    HasIt = this.data.TeachersCategories
                    .Any(tc => tc.Teacher.UserId == userId && tc.CategoryId == c.Id) ? true : false,
                    IsPrivate = c.SchoolId == schoolId ? true : false
                })
                .OrderBy(c => c.Name)
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
                    var school = this.data.Schools.FirstOrDefault(s => s.Id == cat.SchoolId);

                    cat.SchoolName = school == null ? "" : school.Name;
                }
            }

            return pendingCategories;
        }

        public IEnumerable<PendingSubcategoryServiceModel> PendingSubcategories(int schoolId)
        {
            var pendingSubCategories = this.data.SuggestedSubcategories
                .Where(sc => sc.SchoolId == schoolId || sc.SchoolId == 0)
                .Select(sc => new PendingSubcategoryServiceModel
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    Description = sc.Description,
                    CategoryName = sc.Category.Name,
                    SchoolId = sc.SchoolId
                })
                .OrderBy(sc => sc.Name)
                .ToList();

            return pendingSubCategories;
        }

        public void Add(string name, int schoolCode)
        {
            var school = this.data.Schools
                .FirstOrDefault(s => s.SchoolCode == schoolCode);

            this.data.Categories.Add(new Category
            {
                Name = name,
                SchoolId = school == null ? 0 : school.Id
            });

            this.data.SaveChanges();
        }

        public void AddSubcategory(string name, int categoryId, int schoolId)
        {
            this.data.Subcategories.Add(new Subcategory
            {
                Name = name,
                CategoryId = categoryId,
                SchoolId = schoolId
            });

            this.data.SaveChanges();
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

            this.data.SuggestedCategories.Add(newCategory);
            this.data.SaveChanges();
        }

        public void AddSuggestedSubcategory(string name, string description, int categoryId, bool isPrivate, string userId)
        {
            var currentUser = this.data.Users.Find(userId);

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

            this.data.SuggestedSubcategories.Add(newSubcategory);
            this.data.SaveChanges();
        }

        public void ApproveCategory(int id)
        {
            var categoryToApprove = this.data.SuggestedCategories.Find(id);

            if (categoryToApprove != null)
            {
                var newCategory = new Category
                {
                    Name = categoryToApprove.Name,
                    SchoolId = categoryToApprove.SchoolId
                };

                this.data.Categories.Add(newCategory);
                this.data.SuggestedCategories.Remove(categoryToApprove);
                this.data.SaveChanges();
            }
        }

        public void DenyCategory(int id)
        {
            var categoryToDeny = this.data.SuggestedCategories.Find(id);

            if (categoryToDeny != null)
            {
                this.data.SuggestedCategories.Remove(categoryToDeny);
                this.data.SaveChanges();
            }
        }

        public void ApproveSubcategory(int id)
        {
            var subCategoryToApprove = this.data.SuggestedSubcategories.Find(id);

            if (subCategoryToApprove != null)
            {
                var newSubcategory = new Subcategory
                {
                    Name = subCategoryToApprove.Name,
                    CategoryId = subCategoryToApprove.CategoryId,
                    SchoolId = subCategoryToApprove.SchoolId
                };

                this.data.Subcategories.Add(newSubcategory);
                this.data.SuggestedSubcategories.Remove(subCategoryToApprove);
                this.data.SaveChanges();
            }
        }

        public void DenySubcategory(int id)
        {
            var subCategoryToDeny = data.SuggestedSubcategories.Find(id);

            if (subCategoryToDeny != null)
            {
                this.data.SuggestedSubcategories.Remove(subCategoryToDeny);
                this.data.SaveChanges();
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

        public void AddToTeacher(string userId, int categoryId)
        {
            var teacherId = this.data.Teachers.FirstOrDefault(t => t.UserId == userId).Id;

            var categoryToAdd = new TeacherCategory
            {
                CategoryId = categoryId,
                TeacherId = teacherId,
                IsApproved = true
            };

            this.data.TeachersCategories.Add(categoryToAdd);

            this.data.SaveChanges();
        }

        public void RemoveFromTeacher(string userId, int categoryId)
        {
            var teacherId = this.data.Teachers.FirstOrDefault(t => t.UserId == userId).Id;

            var categoryToRemove = this.data.TeachersCategories
                .First(c => c.CategoryId == categoryId && c.TeacherId == teacherId);

            if (categoryToRemove != null)
            {
                this.data.TeachersCategories.Remove(categoryToRemove);
                this.data.SaveChanges();
            }
        }
    }
}
