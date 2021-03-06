namespace WeQuiz.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Category;

    public class Category
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public int SchoolId { get; set; }

        public IEnumerable<Subcategory> Subcategories { get; init; } = new List<Subcategory>();

        public IEnumerable<SuggestedSubcategory> SuggestedSubcategories { get; init; } = new List<SuggestedSubcategory>();
    }
}
