namespace WeQuiz.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TeacherCategory
    {
        public int Id { get; set; }

        [Required]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public bool IsApproved { get; set; }
    }
}
