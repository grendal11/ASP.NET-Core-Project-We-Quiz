﻿namespace WeQuiz.Services.Categories
{
    public class MainCategoriesServiceModel
    {
        public int Id { get; init; }

        public string Category { get; init; }

        public string SchoolName { get; set; }

        public int SchoolId { get; init; }
    }
}
