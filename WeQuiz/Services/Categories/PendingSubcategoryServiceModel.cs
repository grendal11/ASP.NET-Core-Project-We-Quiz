namespace WeQuiz.Services.Categories
{
    public class PendingSubcategoryServiceModel
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int SchoolId { get; set; }

        public string CategoryName { get; set; }

    }
}
