namespace WeQuiz.Models.Requests
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    using static Data.DataConstants.Category;

    public class CategoryRequestFormModel
    {
        [Required(ErrorMessage = "Въведете име на категория")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Името трябва да е между {2} и {1} символа")]
        [Display(Name = "Наименование на категорията")]
        public string Name { get; init; }

        [MaxLength(DescriptionMaxLength)]
        [Display(Name = "Основание за предложението")]
        public string Description { get; init; }

        [Display(Name = "Видима само за нашето училище")]
        public bool IsPrivate { get; set; } = false;
    }
}
