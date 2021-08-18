namespace WeQuiz.Services.Questions
{
    using System.Collections.Generic;

    public class QuestionQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int QuestionsPerPage { get; init; }

        public int TotalQuestions { get; init; }

        public IEnumerable<QuestionServiceModel> Questions { get; init; }
    }
}
