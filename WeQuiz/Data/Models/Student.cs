using System.ComponentModel.DataAnnotations;

namespace WeQuiz.Data.Models
{
    public class Student
    {
        public int Id { get; init; }

        [Required]
        public int SchoolId { get; set; }
        public School School { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int Class { get; set; }

        public bool IsApproved { get; set; }
    }
}
