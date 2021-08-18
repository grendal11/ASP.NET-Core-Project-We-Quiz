namespace WeQuiz.Models.Tests
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Test;

    public class ConfigTestFormModel
    {
        [Required(ErrorMessage = "Въведете име на теста")]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = "Името трябва да е между {2} и {1} символа")]
        [Display(Name = "Име на тест")]
        public string Title { get; set; }

        [Display(Name = "Клас (0 за всички класове)")]
        public int Class { get; init; }

        [Display(Name = "Активиране на теста")]
        public bool IsActive { get; set; } = false;
    }
}
