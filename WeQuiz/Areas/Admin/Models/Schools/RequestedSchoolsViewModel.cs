namespace WeQuiz.Areas.Admin.Models.Schools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RequestedSchoolsViewModel
    {
        public int Id { get; set; }

        public string Name { get; init; }

        public string District { get; init; }

        public string PopulatedArea { get; init; }
    }
}
