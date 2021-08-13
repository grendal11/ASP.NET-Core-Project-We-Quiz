namespace WeQuiz.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Question;

    public class ChoiceAnswer
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        [Required]
        [MaxLength(AnswerMaxLength)]
        public string TextAnswer { get; set; }

        public int Points { get; set; } 

    }
}
