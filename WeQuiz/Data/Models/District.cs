namespace WeQuiz.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.PopulatedArea;

    public class District
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<PopulatedArea> PopulatedAreas { get; init; } = 
            new List<PopulatedArea>();
    }
}
