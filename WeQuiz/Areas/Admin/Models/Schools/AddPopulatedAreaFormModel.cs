
namespace WeQuiz.Areas.Admin.Models.Schools
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.PopulatedArea;

    public class AddPopulatedAreaFormModel
    {
        [Required(ErrorMessage = "Въведете име на населеното място")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Името трябва да е между {2} и {1} символа")]
        [Display(Name = "Населено място")]
        public string Name { get; init; }


        [Display(Name = "Област")]
        public int DistrictId { get; init; }

        public IEnumerable<DistrictViewModel> Districts { get; set; }
    }
}
