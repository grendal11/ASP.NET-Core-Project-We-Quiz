namespace WeQuiz.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.PopulatedArea;

    public class PopulatedArea
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

        public IEnumerable<School> Schools { get; init; } =
            new List<School>();
    }
}
