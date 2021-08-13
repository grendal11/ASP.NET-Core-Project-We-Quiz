namespace WeQuiz.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SchoolAdmin
    {
        public int Id { get; init; }

        [Required]
        public int SchoolId { get; set; }
        public School School { get; set; }

        [Required]
        public string UserId { get; set; }

        public bool IsApproved { get; set; }

    }
}
