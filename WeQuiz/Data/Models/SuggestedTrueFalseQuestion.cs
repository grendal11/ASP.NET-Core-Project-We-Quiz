namespace WeQuiz.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Question;

    public class SuggestedTrueFalseQuestion
    {
        public int Id { get; init; }

        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

        public int Class { get; set; }

        public int SchoolId { get; set; }

        [Required]
        [MaxLength(TextMaxLength)]
        public string Text { get; set; }
      
        public bool Answer { get; set; }

        public int Points { get; set; }

        public bool? Status { get; set; }

        public string UserId { get; set; }
    }
}
