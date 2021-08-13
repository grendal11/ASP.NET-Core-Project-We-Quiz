namespace WeQuiz.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Question;

    public class SuggestedChoiceQuestion
    {
        public int Id { get; init; }

        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

        public int Class { get; set; }

        public int SchoolId { get; set; }

        [Required]
        [MaxLength(TextMaxLength)]
        public string Text { get; set; }

        [Required]
        [MaxLength(AnswerMaxLength)]
        public string TextA { get; set; }

        public bool AnswerA { get; set; }

        [Required]
        [MaxLength(AnswerMaxLength)]
        public string TextB { get; set; }

        public bool AnswerB { get; set; }

        [Required]
        [MaxLength(AnswerMaxLength)]
        public string TextC { get; set; }

        public bool AnswerC { get; set; }

        [Required]
        [MaxLength(AnswerMaxLength)]
        public string TextD { get; set; }

        public bool AnswerD { get; set; }

        public int Points { get; set; }

        public bool? Status { get; set; }

        public string UserId { get; set; }
    }
}
