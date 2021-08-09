﻿namespace WeQuiz.Models.Schools
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WeQuiz.Views.Schools;

    using static Data.DataConstants;

    public class AddSchoolFormModel
    {
        [Required(ErrorMessage = "Въведете име на училище")]
        [StringLength(SchoolNameMaxLength, MinimumLength = SchoolNameMinLength, ErrorMessage = "Името трябва да е между 4 и 50 символа")]
        [Display(Name = "Име на училище")]
        public string Name { get; init; }

        [Required]
        [RegularExpression("[0-9]{6,7}", ErrorMessage = "Кодът е поне 6-цифрен")]
        [Display(Name = "Код по НЕИСПУО")]
        public string SchoolCode { get; init; }

        [Display(Name = "Област")]
        public int DistrictId { get; init; }

        [Display(Name = "Населено място")]
        public int PopulatedAreaId { get; init; }

        public IEnumerable<DistrictViewModel> Districts { get; set; }
    }
}