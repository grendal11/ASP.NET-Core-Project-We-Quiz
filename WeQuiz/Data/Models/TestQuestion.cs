namespace WeQuiz.Data.Models
{
    public class TestQuestion
    {
        public int Id { get; set; }

        public int TestId { get; set; }

        public Test Test { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
