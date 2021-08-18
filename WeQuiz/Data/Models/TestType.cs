namespace WeQuiz.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Test;

    public class TestType
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(TestTypeMaxLength)]
        public string Type { get; set; }

        public IEnumerable<Test> Tests { get; init; } = new List<Test>();
    }
}
