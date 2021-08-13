using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeQuiz.Data.Models
{
    public class Teacher
    {
        public int Id { get; init; }

        [Required]
        public int SchoolId { get; set; }
        public School School { get; set; }

        [Required]
        public string UserId { get; set; }

        public bool IsApproved { get; set; }

        public IEnumerable<TeacherCategory> TeachersCategories { get; init; } = new List<TeacherCategory>();
    }
}
