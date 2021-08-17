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
            var publicCategories = data.Categories.Count(c => c.SchoolId == 0);
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

        public TotalUsersServiceModel TotalUsersStatistics()
        {
            var totalUsers = this.data.Users.Count();
            var totalSchoolAdmins = this.data.SchoolAdmins.Count(sa => sa.IsApproved == true);
            var pendingSchoolAdmins = this.data
                .SchoolAdmins.Count(sa => sa.IsApproved == false);
            var totalTeachers = this.data.Teachers.Count(t => t.IsApproved == true);
            var totalStudents = this.data.Students.Count(s => s.IsApproved == true);
            var totalFreeUsers = totalUsers -
                (totalSchoolAdmins + totalTeachers + totalStudents) - 1;

            return new TotalUsersServiceModel
            {
                TotalUsers = totalUsers,
                TotalSchoolAdmins = totalSchoolAdmins,
                PendingSchoolAdmins = pendingSchoolAdmins,
                TotalTeachers = totalTeachers,
                TotalStudents = totalStudents,
                TotalFreeUsers = totalFreeUsers
            };
        }

        public TotalSchoolUsersServiceModel TotalSchoolUsers(string userId)
        {
            var schoolId = this.data.Users.Find(userId).SchoolId;

            var totalSchoolAdmins = this.data.SchoolAdmins
                .Count(sa => sa.IsApproved == true && sa.SchoolId == schoolId);

            var pendingSchoolAdmins = this.data.SchoolAdmins
                .Count(sa => sa.IsApproved == false && sa.SchoolId == schoolId);

            var totalTeachers = this.data.Teachers
                .Count(t => t.IsApproved == true && t.SchoolId == schoolId);

            var pendingTeachers = this.data.Teachers
                .Count(t => t.IsApproved == false && t.SchoolId == schoolId);

            var totalStudents = this.data.Students
                .Count(s => s.IsApproved == true && s.SchoolId == schoolId);

            var pendingStudents = this.data.Students
                .Count(s => s.IsApproved == false && s.SchoolId == schoolId);


            return new TotalSchoolUsersServiceModel
            {
                TotalSchoolAdmins = totalSchoolAdmins,
                PendingSchoolAdmins = pendingSchoolAdmins,
                TotalTeachers = totalTeachers,
                PendingTeachers = pendingTeachers,
                TotalStudents = totalStudents,
                PendingStudents = pendingStudents

            };
        }
    }
}
