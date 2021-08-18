namespace WeQuiz.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using WeQuiz.Data.Models;

    public class WeQuizDbContext : IdentityDbContext<User>
    {
        public WeQuizDbContext()
        {
        }

        public WeQuizDbContext(DbContextOptions<WeQuizDbContext> options)
            : base(options)
        {
        }

        public DbSet<ActiveStudentTest> ActiveStudentTests { get; init; }
        public DbSet<ActiveTestConfiguration> ActiveTestConfigurations { get; init; }
        public DbSet<Category> Categories { get; init; }
        public DbSet<ChoiceAnswer> ChoiceAnswers { get; init; }
        public DbSet<District> Districts { get; init; }
        public DbSet<ExactAnswer> ExactAnswers { get; init; }
        public DbSet<PopulatedArea> PopulatedAreas { get; init; }
        public DbSet<Question> Questions { get; init; }
        public DbSet<QuestionType> QuestionTypes { get; init; }
        public DbSet<School> Schools { get; init; }
        public DbSet<SchoolAdmin> SchoolAdmins { get; init; }
        public DbSet<SchoolRequest> SchoolRequests { get; init; }
        public DbSet<Student> Students { get; init; }
        public DbSet<StudentAnswer> StudentsAnswers { get; init; }
        public DbSet<Subcategory> Subcategories { get; init; }
        public DbSet<SuggestedCategory> SuggestedCategories { get; init; }
        public DbSet<SuggestedChoiceQuestion> SuggestedChoiceQuestions { get; init; }
        public DbSet<SuggestedExactAnswerQuestion> SuggestedExactAnswerQuestions { get; init; }
        public DbSet<SuggestedSubcategory> SuggestedSubcategories { get; init; }
        public DbSet<SuggestedTrueFalseQuestion> SuggestedTrueFalseQuestions { get; init; }
        public DbSet<Teacher> Teachers { get; init; }
        public DbSet<TeacherCategory> TeachersCategories { get; init; }
        public DbSet<Test> Tests { get; init; }
        public DbSet<TestQuestion> TestsQuestions { get; init; }
        public DbSet<TestResult> TestsResults { get; init; }
        public DbSet<TestType> TestTypes { get; init; }
        public DbSet<TrueFalseAnswer> TrueFalseAnswers { get; init; }


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

            builder
                .Entity<Question>()
                .HasOne(q => q.Subcategory)
                .WithMany(q => q.Questions)
                .HasForeignKey(q => q.SubcategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Test>()
                .HasOne(t => t.TestType)
                .WithMany(t => t.Tests)
                .HasForeignKey(t => t.TestTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<SchoolAdmin>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<SchoolAdmin>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Teacher>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Teacher>(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Student>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Student>(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }

    }
}
