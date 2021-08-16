namespace WeQuiz.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Category;
    public class CategoryFormModel
    {
        [Required(ErrorMessage = "Въведете име на категория")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Името трябва да е между {2} и {1} символа")]
        [Display(Name = "Наименование на категорията")]
        public string Name { get; init; }

        [Display(Name = "Код по НЕИСПУО")]
        public int SchoolCode { get; init; }
    }
}
