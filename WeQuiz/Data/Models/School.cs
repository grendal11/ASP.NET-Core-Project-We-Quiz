namespace WeQuiz.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.School;

    public class School
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(SchoolNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public int SchoolCode { get; set; }

        public int PopulatedAreaId { get; set; }
        public PopulatedArea PopulatedArea { get; set; }

        public IEnumerable<SchoolAdmin> SchoolAdmins { get; init; } = new List<SchoolAdmin>();

        public IEnumerable<Teacher> Teachers { get; init; } = new List<Teacher>();

        public IEnumerable<Student> Students { get; init; } = new List<Student>();

    }
}
