namespace WeQuiz.Models.Requests
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.School;
    using static Data.DataConstants.PopulatedArea;
    public class SchoolRequestFormModel
    {
        [Required(ErrorMessage = "Въведете име на училище")]
        [StringLength(SchoolNameMaxLength, MinimumLength = SchoolNameMinLength, ErrorMessage = "Името трябва да е между {2} и {1} символа")]
        [Display(Name = "Име на училище")]
        public string Name { get; init; }

        [Required(ErrorMessage = "Въведете област")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Името трябва да е между {2} и {1} символа")]
        [Display(Name = "Област")]
        public string District { get; init; }

        [Required(ErrorMessage = "Въведете име на населеното място")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Името трябва да е между {2} и {1} символа")]
        [Display(Name = "Населено място")]
        public string PopulatedArea { get; init; }
    }
}
