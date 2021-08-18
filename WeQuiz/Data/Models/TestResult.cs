namespace WeQuiz.Data.Models
{
    public class TestResult
    {
        public int Id { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int TotalPoints { get; set; }

        public int UserPoints { get; set; }

        public double Percentage { get; set; }
    }
}
