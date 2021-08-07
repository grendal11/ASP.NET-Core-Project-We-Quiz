namespace WeQuiz.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class QuestionType
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(QuestionTypeMaxLength)]
        public string Type { get; set; }
    }
}
