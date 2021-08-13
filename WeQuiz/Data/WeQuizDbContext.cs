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


        protected override void OnModelCreating(ModelBuilder builder)
        {         
            builder
               .Entity<PopulatedArea>()
               .HasOne(p => p.District)
               .WithMany(p => p.PopulatedAreas)
               .HasForeignKey(p => p.DistrictId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
             .Entity<School>()
             .HasOne(s => s.PopulatedArea)
             .WithMany(s => s.Schools)
             .HasForeignKey(s => s.PopulatedAreaId)
             .OnDelete(DeleteBehavior.Restrict);
                      
            builder
             .Entity<Subcategory>()
             .HasOne(s => s.Category)
             .WithMany(s => s.Subcategories)
             .HasForeignKey(s => s.CategoryId)
             .OnDelete(DeleteBehavior.Restrict);

            
            builder
             .Entity<SuggestedSubcategory>()
             .HasOne(s => s.Category)
             .WithMany(s => s.SuggestedSubcategories)
             .HasForeignKey(s => s.CategoryId)
             .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }

    }
}
