namespace WeQuiz.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    using static Data.DataConstants.Category;

    public class SuggestedSubcategory
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public int SchoolCode { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
