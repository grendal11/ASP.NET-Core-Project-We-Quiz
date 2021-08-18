namespace WeQuiz.Models.Tests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WeQuiz.Services.Categories;
    using WeQuiz.Services.Questions;

    public class AllQuestionsQueryModel
    {
        public const int QuestionsPerPage = 10;

        [Display(Name = "Търсене по име на основен предмет")]
        public int CategoryId { get; init; }

        [Display(Name = "Подкатегория")]
        public string Subcategory { get; init; }

        [Display(Name = "Клас")]
        public int Klas { get; init; }

        [Display(Name = "Тип на въпроса")]
        public int QuestionTypeId { get; init; }

        [Display(Name = "Текст на въпроса")]
        public string Text { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalQuestions { get; set; }

        public IEnumerable<CategoryServiceModel> Categories { get; set; }

        public IEnumerable<QuestionTypeServiceModel> QuestionTypes { get; set; }

        public IEnumerable<QuestionServiceModel> Questions { get; set; }
    }
}
