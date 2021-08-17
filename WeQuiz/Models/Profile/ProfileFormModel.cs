using System.ComponentModel.DataAnnotations;

namespace WeQuiz.Models.Profile
{
    public class ProfileFormModel
    {
        [Display(Name = "Пълно име")]
        public string FullName { get; init; }

        [Display(Name = "Име в системата (nickname)")]
        public string Alias { get; init; }

        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; init; }
    }
}
