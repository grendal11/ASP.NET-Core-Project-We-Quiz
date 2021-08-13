namespace WeQuiz.Data.Models
{
    public class ExactAnswer
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int IntAnswer { get; set; }

        public int Points { get; set; }
    }
}
