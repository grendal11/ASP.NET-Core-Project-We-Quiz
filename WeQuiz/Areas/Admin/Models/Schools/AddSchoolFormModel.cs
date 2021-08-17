namespace WeQuiz.Areas.Admin.Models.Schools
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WeQuiz.Services.Schools;
    using static Data.DataConstants.School;

    public class AddSchoolFormModel
    {
        [Required(ErrorMessage = "Въведете име на училище")]
        [StringLength(SchoolNameMaxLength, MinimumLength = SchoolNameMinLength, ErrorMessage = "Името трябва да е между 4 и 50 символа")]
        [Display(Name = "Име на училище")]
        public string Name { get; init; }

        [Required(ErrorMessage = "Въведете код по НЕИСПУО")]
        [RegularExpression("[0-9]{6,7}", ErrorMessage = "Кодът е поне 6-цифрен")]
        [Display(Name = "Код по НЕИСПУО")]
        public string SchoolCode { get; init; }

        [Display(Name = "Област")]
        public int DistrictId { get; init; }

        [Required(ErrorMessage = "Изберете населено място")]
        [Display(Name = "Населено място")]
        public int PopulatedAreaId { get; init; }

        public IEnumerable<DistrictServiceModel> Districts { get; set; }
    }
}
