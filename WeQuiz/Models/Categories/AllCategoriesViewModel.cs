namespace WeQuiz.Models.Categories
{
    public class AllCategoriesViewModel
    {
        public int Id { get; init; }

        public string Category { get; init; }

        public string Subcategory { get; init; }

        public string SchoolName { get; set; }

        public int SchoolCode { get; init; }
    }
}
