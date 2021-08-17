namespace WeQuiz.Services.Statistics
{
    public interface IStatisticsService
    {
        TotalsServiceModel Totals();

        UserStaticsicsServiceModel UserStaticsics(string userId);

        TotalCategoriesServiceModel CategoriesStatistics();

        TotalUsersServiceModel TotalUsersStatistics();

        TotalSchoolUsersServiceModel TotalSchoolUsers(string userId);
    }
}
