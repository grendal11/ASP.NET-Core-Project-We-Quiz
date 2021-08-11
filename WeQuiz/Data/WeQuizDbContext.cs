namespace WeQuiz.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using WeQuiz.Data.Models;

    public class WeQuizDbContext : IdentityDbContext<User>
    {
        public WeQuizDbContext(DbContextOptions<WeQuizDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; init; }
        public DbSet<District> Districts { get; init; }
        public DbSet<PopulatedArea> PopulatedAreas { get; init; }
        public DbSet<QuestionType> QuestionTypes { get; init; }
        public DbSet<School> Schools { get; init; }
        public DbSet<SchoolRequest> SchoolRequests { get; init; }
        public DbSet<Subcategory> Subcategories { get; init; }
        public DbSet<SuggestedCategory> SuggestedCategories { get; init; }
        public DbSet<SuggestedSubcategory> SuggestedSubcategories { get; init; }


    }
}
