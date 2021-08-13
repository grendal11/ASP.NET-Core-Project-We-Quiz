namespace WeQuiz.Data.Models
{
    public class TrueFalseAnswer
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public bool Answer { get; set; }

        public int Points { get; set; }
    }
}
