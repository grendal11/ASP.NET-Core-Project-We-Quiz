namespace WeQuiz.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.PopulatedArea;
    using static Data.DataConstants.School;

    public class SchoolRequest
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(SchoolNameMaxLength)]
        public string Name { get; set; }


        [Required]
        [MaxLength(NameMaxLength)]
        public string District { get; set; }


        [Required]
        [MaxLength(NameMaxLength)]
        public string PopulatedArea { get; set; }

    }
}
