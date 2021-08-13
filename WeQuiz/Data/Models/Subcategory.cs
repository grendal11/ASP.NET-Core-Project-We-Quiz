namespace WeQuiz.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Category;

    public class Subcategory
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public int SchoolId { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public IEnumerable<Question> Questions { get; init; } = new List<Question>();

        public IEnumerable<SuggestedChoiceQuestion> SuggestedChoiceQuestions { get; init; } = new List<SuggestedChoiceQuestion>();    
        
        public IEnumerable<SuggestedTrueFalseQuestion> SuggestedTrueFalseQuestions { get; init; } = new List<SuggestedTrueFalseQuestion>();

        public IEnumerable<SuggestedExactAnswerQuestion> SuggestedExactAnswerQuestions { get; init; } = new List<SuggestedExactAnswerQuestion>();
    }
}
