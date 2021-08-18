namespace WeQuiz.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    using static DataConstants.Test;

    public class Test
    {
        public int Id { get; init; }

        [Required]
        public string OwnerId { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        public int Class { get; set; }

        public bool IsActive { get; set; }

        public int TestTypeId { get; set; }

        public TestType TestType { get; set; }

        public IEnumerable<TestQuestion> TestsQuestions { get; init; } = 
            new List<TestQuestion>();

    }
}
