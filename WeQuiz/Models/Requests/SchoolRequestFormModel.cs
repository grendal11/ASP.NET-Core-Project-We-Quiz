namespace WeQuiz.Models.Requests
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.School;
    public class SchoolRequestFormModel
    {
        [Required(ErrorMessage = "Въведете име на училище")]
        [StringLength(SchoolNameMaxLength, MinimumLength = SchoolNameMinLength, ErrorMessage = "Името трябва да е между 4 и 50 символа")]
        [Display(Name = "Име на училище")]
        public string Name { get; init; }

        [Required(ErrorMessage = "Въведете област")]
        [Display(Name = "Област")]
        public string District { get; init; }

        [Required(ErrorMessage = "Въведете име на населеното място")]
        [Display(Name = "Населено място")]
        public string PopulatedArea { get; init; }
    }
}
