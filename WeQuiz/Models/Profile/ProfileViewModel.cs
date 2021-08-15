namespace WeQuiz.Models.Profile
{
    using System.Collections.Generic;
    using WeQuiz.Views.Profile;

    public class ProfileViewModel
    {
        public string UserId { get; set; }

        public string Email { get; init; }
                
        public string FullName { get; init; }

        public string Alias { get; init; }

        public string SchoolName { get; init; }

        public string RoleName { get; init; }

        public int Class { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } 
    }
}
