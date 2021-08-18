namespace WeQuiz.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class StudentAnswer
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
