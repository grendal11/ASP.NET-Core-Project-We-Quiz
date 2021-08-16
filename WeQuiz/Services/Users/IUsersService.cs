namespace WeQuiz.Services.Users
{
    using System.Collections.Generic;

    public interface IUsersService
    {
        public IEnumerable<PedningAdminServiceModel> PendingAdmins();
        public IEnumerable<SchoolAdminServiceModel> AllSchoolAdmins();

        void AddPhone(string phone, string userId);

        void RequestAdmin(string userId, int id);

        void ApproveAdmin(string userId);

        public void DenyAdmin(string userId);
    }
}
