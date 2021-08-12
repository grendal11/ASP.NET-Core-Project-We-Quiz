namespace WeQuiz.Models.Requests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    using static Data.DataConstants.Category;

    public class SubcategoryRequestFormModel
    {
        [Display(Name = "Категория")]
        public int CategoryId { get; init; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        [Required(ErrorMessage = "Въведете име на подкатегория")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Името трябва да е между {2} и {1} символа")]
        [Display(Name = "Наименование на подкатегорията")]
        public string Name { get; init; }

        [MaxLength(DescriptionMaxLength)]
        [Display(Name = "Основание за предложението")]
        public string Description { get; init; }

        [Display(Name = "Видима само за нашето училище")]
        public bool IsPrivate { get; set; } = false;
    }
}
