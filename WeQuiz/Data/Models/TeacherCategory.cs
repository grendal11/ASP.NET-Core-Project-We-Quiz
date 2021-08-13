namespace WeQuiz.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class TeacherCategory
    {
        public int Id { get; set; }

        [Required]
        public string TeacherId { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public bool IsApproved { get; set; }
    }
}
