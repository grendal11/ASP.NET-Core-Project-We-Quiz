namespace WeQuiz.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.User;
    public class User : IdentityUser
    {
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

        [MaxLength(AliasMaxLength)]
        public string Alias { get; set; }

        public int SchoolId { get; set; }

    }
}
