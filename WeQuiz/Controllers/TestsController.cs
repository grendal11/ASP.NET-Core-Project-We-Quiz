namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WeQuiz.Infrastructure;
    using WeQuiz.Models.Tests;
    using WeQuiz.Services.Categories;
    using WeQuiz.Services.Questions;

    [Authorize]

    public class TestsController : Controller
    {
        private readonly IQuestionsService questions;
        private readonly ICategoriesService categories;

        public TestsController(IQuestionsService questions, ICategoriesService categories)
        {
            this.questions = questions;
            this.categories = categories;
        }

        public IActionResult Home() => View();

        public IActionResult Questions([FromQuery] AllQuestionsQueryModel query)
        {
            var userId = User.Id();

            var queryResult = this.questions.All(
                userId, 
                query.CategoryId, 
                query.Subcategory, 
                query.Klas, 
                query.QuestionTypeId, 
                query.Text, 
                query.CurrentPage, 
                AllQuestionsQueryModel.QuestionsPerPage);

            var categories = this.categories.OwnCategories(userId);
            var questionTypes = this.questions.QuestionTypes();

            query.QuestionTypes = questionTypes;
            query.Categories = categories;
            query.TotalQuestions = queryResult.TotalQuestions;
            query.Questions = queryResult.Questions;
            
            return View(query);
        }

        public IActionResult Auto() => View();

        public IActionResult Results() => View();


    }
}
