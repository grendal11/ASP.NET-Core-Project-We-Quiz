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

        public ProfileServiceModel EditableInfo(string userId);

        public void EditProfile(string name, string alias, string phone, string userId);

        void RequestAdmin(string userId, int id);

        void ApproveAdmin(string userId);

        public void DenyAdmin(string userId);

        public void RemoveAdmin(string userId);

        IEnumerable<PendingTeacherServiceModel> PendingTeachers(int schoolId);

        IEnumerable<TeacherServiceModel> Teachers(int schoolId);

        void ApproveTeacher(string userId);

        void DenyTeacher(string userId);

        void RemoveTeacher(string userId);

        void RequestTeacher(string userId, int id);

        IEnumerable<PendingStudentServiceModel> PendingStudents(int schoolId);

        IEnumerable<StudentServiceModel> Students(int schoolId);

        void ApproveStudent(string userId);

        void DenyStudent(string userId);

        void RemoveStudent(string userId);

        void RequestStudent(string userId, int id);



    }
}
