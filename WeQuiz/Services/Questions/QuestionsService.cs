using System.Collections.Generic;
using System.Linq;
using WeQuiz.Data;
using WeQuiz.Data.Models;

namespace WeQuiz.Services.Questions
{
    public class QuestionsService : IQuestionsService
    {
        private readonly WeQuizDbContext data;

        public QuestionsService(WeQuizDbContext data)
            => this.data = data;

        public QuestionQueryServiceModel All(
            string userId,
            int categoryId = 0,
            string subCategory = null,
            int klas = 0,
            int questionTypeId = 0,
            string text = null,
            int currentPage = 1,
            int questionsPerPage = 10)
        {
            var schoolId = this.data.Users.Find(userId).SchoolId;

            var questionsQuery = this.data.Questions
                   .Where(q => q.SchoolId == 0 || q.SchoolId == schoolId);

            if (categoryId > 0)
            {
                questionsQuery = questionsQuery
                    .Where(q => q.Subcategory.CategoryId == categoryId);
            }

            if (klas > 0)
            {
                questionsQuery = questionsQuery
                   .Where(q => q.Class == klas);
            }

            if (questionTypeId > 0)
            {
                questionsQuery = questionsQuery
                   .Where(q => q.QuestionTypeId == questionTypeId);
            }

            if (text != null)
            {
                questionsQuery = questionsQuery
                   .Where(q => q.Text.ToLower().Contains(text.ToLower()));
            }

            if (subCategory != null)
            {
                questionsQuery = questionsQuery
                   .Where(q => q.Subcategory.Name.ToLower()
                        .Contains(subCategory.ToLower()));
            }

            var totalQuestions = questionsQuery.Count();

            var questions = questionsQuery
                .Skip((currentPage - 1) * questionsPerPage)
                .Take(questionsPerPage)
                .Select(q => new QuestionServiceModel
                {
                    Id = q.Id,
                    SubcategoryId = q.SubcategoryId,
                    SubcategoryName = q.Subcategory.Name,
                    CategoryName = q.Subcategory.Category.Name,
                    Class = q.Class,
                    SchoolId = q.SchoolId,
                    QuestionTypeId = q.QuestionTypeId,
                     Text = q.Text
                })
                .ToList();

            return new QuestionQueryServiceModel
            {
                CurrentPage = currentPage,
                QuestionsPerPage = questionsPerPage,
                TotalQuestions = totalQuestions,
                Questions = questions
            };
        }

        IEnumerable<QuestionTypeServiceModel> IQuestionsService.QuestionTypes()
        {
            var questionTypes = this.data.QuestionTypes
                .Select(q => new QuestionTypeServiceModel
                {
                    Id = q.Id,
                    QuestionType = q.Type
                })
                .ToList();

            return questionTypes;
        }

        public bool AddQuestionToTest(string userId, int questionId)
        {
            var exists = this.data.ActiveTestConfigurations
                .Any(q => q.QuestionId == questionId && q.UserId == userId);

            if (exists)
            {
                return false;
            }

            this.data.ActiveTestConfigurations
                .Add(new ActiveTestConfiguration 
                { 
                    UserId = userId,
                    QuestionId = questionId
                });

            this.data.SaveChanges();

            return true;
        }

        public bool ActiveTestTeacher(string userId)
        {
            var active = this.data.ActiveTestConfigurations.Any(t => t.UserId == userId);

            if (active)
            {
                return true;
            }

            return false;
        }

        public void TestConfig(string userId, string title, int studentClass, bool isActive)
        {
            var questions = this.data.ActiveStudentTests
                .Where(q => q.UserId == userId)
                .ToList();

            var isChoiceType = questions.All(q => q.Question.QuestionTypeId == 3);
            var isTrueFalseType = questions.All(q => q.Question.QuestionTypeId == 2);
            var isExactType = questions.All(q => q.Question.QuestionTypeId == 1);

            var typeTest = 4;

            if (isChoiceType)
            {
                typeTest = 3;
            }

            if (isTrueFalseType)
            {
                typeTest = 2;
            }

            if (isExactType)
            {
                typeTest = 1;
            }

            var testType = this.data.TestTypes.First(tt => tt.Id == typeTest);

            this.data.Tests.Add(new Test
            {
                 OwnerId = userId,
                 Title = title,
                 Class=studentClass,
                 IsActive = isActive,
                 TestTypeId= testType.Id
            });

            this.data.SaveChanges();
        }
    }
}
