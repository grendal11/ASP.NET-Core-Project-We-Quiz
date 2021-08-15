namespace WeQuiz.Services.Statistics
{
    using System.Linq;
    using WeQuiz.Data;

    public class StatisticsService : IStatisticsService
    {
        private readonly WeQuizDbContext data;

        public StatisticsService(WeQuizDbContext data)
            => this.data = data;


        public TotalsServiceModel Totals()
        {
            var totalSchools = this.data.Schools.Count();
            var totalUsers = this.data.Users.Count();
            var totalQuestions = this.data.Questions.Count();

            return new TotalsServiceModel
            {
                TotalSchools = totalSchools,
                TotalUsers = totalUsers,
                TotalQuestions = totalQuestions
            };
        }

        public UserStaticsicsServiceModel UserStaticsics(string userId)
        {
            var suggestedQuestions =
                this.data.SuggestedChoiceQuestions.Count(q => q.UserId == userId) + this.data.SuggestedExactAnswerQuestions.Count(q => q.UserId == userId) + this.data.SuggestedTrueFalseQuestions.Count(q => q.UserId == userId);

            var approvedQuestions =
                this.data.SuggestedChoiceQuestions
                .Count(q => q.UserId == userId && q.Status == true) + this.data.SuggestedExactAnswerQuestions
                .Count(q => q.UserId == userId && q.Status == true) + this.data.SuggestedTrueFalseQuestions
                .Count(q => q.UserId == userId && q.Status == true);

            return new UserStaticsicsServiceModel
            {
                SugestedQuestions = suggestedQuestions,
                ApprovedQuestions = approvedQuestions,
                AverageTestsResult = 0,
                AverageInstantTestsResult = 0
            };
        }

        public TotalCategoriesServiceModel CategoriesStatistics()
        {
            var totalCategories = data.Categories.Count();
            var publicCategories = data.Categories.Count(c=>c.SchoolId==0);
            var pendingCategories = data.SuggestedCategories.Count();
            var totalSubcategories = data.Subcategories.Count();
            var publicSubcategories = data.Subcategories.Count(c => c.SchoolId == 0);
            var pendingSubcategories = data.SuggestedSubcategories.Count();

            return new TotalCategoriesServiceModel
            {
                TotalCategories = totalCategories,
                PublicCategories = publicCategories,
                PendingCategories = pendingCategories,
                TotalSubcategories = totalSubcategories,
                PublicSubcategories = publicSubcategories,
                PendingSubcategories = pendingSubcategories
            };
        }
    }
}
