namespace WeQuiz.Services.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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
    }
}
