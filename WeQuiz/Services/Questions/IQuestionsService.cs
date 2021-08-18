using System.Collections.Generic;
using WeQuiz.Data.Models;

namespace WeQuiz.Services.Questions
{
    public interface IQuestionsService
    {
        QuestionQueryServiceModel All(
            string userId,
            int categoryId = 0,
            string subCategory = null,
            int klas = 0,
            int questionTypeId = 0,
            string text = null,
            int currentPage = 1,
            int questionsPerPage = 10);

        IEnumerable<QuestionTypeServiceModel> QuestionTypes();

        bool AddQuestionToTest(string userId, int questionId);

        bool ActiveTestTeacher(string userId);

        void TestConfig(string userId, string title, int studentClass, bool isActive);
    }

}
