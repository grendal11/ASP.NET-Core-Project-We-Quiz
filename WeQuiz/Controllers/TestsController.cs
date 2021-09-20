namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WeQuiz.Infrastructure;
    using WeQuiz.Models.Tests;
    using WeQuiz.Services.Categories;
    using WeQuiz.Services.Questions;

    using static WebConstants;

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

            if (this.questions.ActiveTestTeacher(userId))
            {
                ViewBag.ActiveConfig = true;
            }

            return View(query);
        }


        public IActionResult Auto() => View();

        public IActionResult Results() => View();

        public IActionResult Add(int id)
        {
            string userId = User.Id();

            var result = this.questions.AddQuestionToTest(userId, id);

            if (!result)
            {
                TempData[GlobalMessageKey] = "Вече сте добавили този въпрос";
            }

            return RedirectToAction("Questions", "Tests");
        }

        public IActionResult FinishConfig() => View();

        [HttpPost]
        public IActionResult FinishConfig(ConfigTestFormModel config)
        {
            //Added in 00.01
            var userId = User.Id();

            this.questions.TestConfig(userId, config.Title, config.Class, config.IsActive);

            return RedirectToAction("Home", "Tests");
        }

    }
}
