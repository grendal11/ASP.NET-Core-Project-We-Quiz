namespace WeQuiz.Data.Models
{
    public class ActiveStudentTest
    {
        public int Id { get; set; }

        public int TestId { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public string TextAnswer { get; set; }

        public int Points { get; set; }
    }
}
