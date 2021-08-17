namespace WeQuiz.Services.Users
{
    using System.Collections.Generic;

    public interface IUsersService
    {
        public IEnumerable<PedningAdminServiceModel> PendingAdmins();
        public IEnumerable<SchoolAdminServiceModel> AllSchoolAdmins();

        public string Alias(string userId);

        public int SchoolId(string userId);

        public bool HasPhone(string userId);

        void AddPhone(string phone, string userId);

        void RequestAdmin(string userId, int id);

        void ApproveAdmin(string userId);

        public void DenyAdmin(string userId);
    }
}
