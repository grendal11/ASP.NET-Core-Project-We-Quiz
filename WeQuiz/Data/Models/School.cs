namespace WeQuiz.Data.Models
{
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
    }
}
