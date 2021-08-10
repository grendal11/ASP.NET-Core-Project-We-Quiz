namespace WeQuiz.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.School;

    public class PopulatedArea
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(PopulatedAreaNameMaxLength)]
        public string Name { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

        public IEnumerable<School> Schools { get; init; } =
            new List<School>();
    }
}
