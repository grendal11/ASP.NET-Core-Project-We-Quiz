namespace WeQuiz.Models.Profile
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProfileViewModel
    {
        public string UserId { get; set; }

        public string Email { get; init; }
                
        public string FullName { get; init; }

        public string Alias { get; init; }

        public string SchoolName { get; set; }

        public string RoleName { get; set; }
    }
}
