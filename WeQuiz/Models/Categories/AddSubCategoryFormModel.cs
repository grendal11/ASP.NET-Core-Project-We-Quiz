namespace WeQuiz.Models.Categories
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WeQuiz.Services.Categories;

    using static Data.DataConstants.Category;

    public class AddSubCategoryFormModel
    {
        [Display(Name = "Категория")]
        public int CategoryId { get; init; }
        public IEnumerable<CategoryServiceModel> Categories { get; set; }

        [Required(ErrorMessage = "Въведете име на подкатегория")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Името трябва да е между {2} и {1} символа")]
        [Display(Name = "Наименование на подкатегорията")]
        public string Name { get; init; }

        [Display(Name = "Видима само за нашето училище")]
        public bool IsPrivate { get; set; } = false;
    }
}
