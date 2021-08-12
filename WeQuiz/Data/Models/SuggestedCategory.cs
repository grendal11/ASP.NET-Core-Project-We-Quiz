namespace WeQuiz.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    using static Data.DataConstants.School;

    public class SuggestedCategory
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(SchoolNameMaxLength)]
        public string Name { get; set; }

        public int SchoolCode { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }
    }
}
