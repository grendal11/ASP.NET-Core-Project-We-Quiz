namespace WeQuiz.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Question;
    public class Question
    {
        public int Id { get; init; }

        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

        public int Class { get; set; }

        public int SchoolId { get; set; }

        public int QuestionTypeId { get; set; }

        [Required]
        [MaxLength(TextMaxLength)]
        public string Text { get; set; }

        public IEnumerable<ChoiceAnswer> ChoiceAnswers { get; init; } =
           new List<ChoiceAnswer>();

        public IEnumerable<TestQuestion> TestsQuestions { get; init; } = 
            new List<TestQuestion>();

    }
}
