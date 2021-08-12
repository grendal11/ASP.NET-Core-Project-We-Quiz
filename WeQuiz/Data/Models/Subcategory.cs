﻿namespace WeQuiz.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Category;

    public class Subcategory
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public int SchoolCode { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
