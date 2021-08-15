namespace WeQuiz.Services.Categories
{
    public class AllCategoriesServiceModel
    {
        public int Id { get; init; }

        public string Category { get; init; }

        public string Subcategory { get; init; }

        public string SchoolName { get; set; }

        public int SchoolId { get; init; }
    }
}
