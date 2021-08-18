using System.Collections.Generic;
using WeQuiz.Data.Models;

namespace WeQuiz.Services.Questions
{
    public class QuestionServiceModel
    {
        public int Id { get; init; }

        public int SubcategoryId { get; set; }

        public string SubcategoryName { get; set; }

        public string CategoryName { get; set; }

        public int Class { get; set; }

        public int SchoolId { get; set; }

        public int QuestionTypeId { get; set; }

        public string Text { get; set; }

    }
}
