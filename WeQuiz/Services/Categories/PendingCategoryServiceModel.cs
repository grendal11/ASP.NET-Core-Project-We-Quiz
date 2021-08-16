namespace WeQuiz.Services.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PendingCategoryServiceModel
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public int SchoolId { get; set; }

        public string SchoolName { get; set; }

        public string Description { get; set; }
    }
}
