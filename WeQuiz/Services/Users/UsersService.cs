namespace WeQuiz.Services.Users
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WeQuiz.Data;
    using WeQuiz.Data.Models;

    using static WeQuiz.WebConstants;

    public class UsersService : IUsersService
    {
        private readonly WeQuizDbContext data;
        private readonly UserManager<User> userManager;

        public UsersService(WeQuizDbContext data, UserManager<User> userManager)
        {
            this.data = data;
            this.userManager = userManager;
        }

        public string Alias(string userId)
        {
            return this.data.Users.Find(userId).Alias;
        }

        public int SchoolId(string userId)
        {
            return this.data.Users.Find(userId).SchoolId;
        }

        public bool HasPhone(string userId)
        {
            var user = this.data.Users.Find(userId);

            if (string.IsNullOrEmpty(user.PhoneNumber))
            {
                return false;
            }

            return true;
        }

        public ProfileServiceModel EditableInfo(string userId)
        {
            var user = this.data.Users.Find(userId);

            return new ProfileServiceModel
            {
                FullName = user.FullName,
                Alias = user.Alias,
                PhoneNumber = user.PhoneNumber
            };
        }

        public void EditProfile(string name, string alias, string phone, string userId)
        {
            var user = this.data.Users.Find(userId);

            if (!string.IsNullOrWhiteSpace(name))
            {
                user.FullName = name;
            }

            if (!string.IsNullOrWhiteSpace(alias))
            {
                user.Alias = alias;
            }

            if (!string.IsNullOrWhiteSpace(phone))
            {
                user.PhoneNumber = phone;
            }

            this.data.SaveChanges();
        }

        public void AddStudentClass(int studentClass, string userId)
        {
            var student = this.data.Students.FirstOrDefault(s=>s.UserId==userId);

            if (student == null)
            {
                return;
            }

            if (studentClass <= 12 && studentClass >= 1)
            {
                student.Class = studentClass;
            }

            this.data.SaveChanges();

        }

        public void AddTeacherCategory(int subjectId, string userId)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveTeacherCategory(int subjectId, string userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PedningAdminServiceModel> PendingAdmins()
        {
            var pendingAdmins = this.data.SchoolAdmins
                .Where(sa => sa.IsApproved == false)
                .Select(sa => new PedningAdminServiceModel
                {
                    Id = sa.UserId,
                    SchoolName = sa.School.Name
                })
                .ToList(); ;

            foreach (var adm in pendingAdmins)
            {
                var user = data.Users.Find(adm.Id);
                adm.FullName = user.FullName;
                adm.UserEmail = user.Email;
            }

            return pendingAdmins;
        }

        public IEnumerable<SchoolAdminServiceModel> AllSchoolAdmins()
        {
            var allSchoolAdmins = this.data.SchoolAdmins
                .Select(sa => new SchoolAdminServiceModel
                {
                    Id = sa.UserId,
                    SchoolName = sa.School.Name,
                    Status = sa.IsApproved
                })
                .ToList();

            foreach (var adm in allSchoolAdmins)
            {
                var user = data.Users.Find(adm.Id);
                adm.FullName = user.FullName;
                adm.UserEmail = user.Email;
            }

            return allSchoolAdmins;
        }

        public void AddPhone(string phone, string userId)
        {
            this.data.Users.Find(userId).PhoneNumber = phone;

            this.data.SaveChanges();
        }

        public void ApproveAdmin(string userId)
        {
            var user = this.data.Users.Find(userId);

            Task.Run(async () =>
                {
                    await userManager.AddToRoleAsync(user, SchoolAdminRoleName);
                })
                .GetAwaiter()
                .GetResult();


            this.data.SchoolAdmins.First(u => u.UserId == userId).IsApproved = true;

            this.data.SaveChanges();
        }

        public void DenyAdmin(string userId)
        {
            var adminToDeny = this.data.SchoolAdmins
                .First(u => u.UserId == userId);

            this.data.SchoolAdmins.Remove(adminToDeny);

            this.data.SaveChanges();
        }

        public void RemoveAdmin(string userId)
        {

            var user = this.data.Users.Find(userId);

            Task.Run(async () =>
            {
                await userManager.RemoveFromRoleAsync(user, SchoolAdminRoleName);
            })
                .GetAwaiter()
                .GetResult();

            var adminToRemove = this.data.SchoolAdmins
                .First(u => u.UserId == userId);

            user.SchoolId = 0;

            this.data.SchoolAdmins.Remove(adminToRemove);

            this.data.SaveChanges();
        }

        public void RequestAdmin(string userId, int id)
        {
            if (this.data.SchoolAdmins.Any(u => u.UserId == userId))
            {
                return;
            }

            var school = this.data.Schools.First(s => s.Id == id);

            this.data.SchoolAdmins.Add(new SchoolAdmin
            {
                SchoolId = id,
                UserId = userId,
                IsApproved = false
            });

            var user = this.data.Users.Find(userId);

            user.SchoolId = id;

            this.data.SaveChanges();
        }
  
        public IEnumerable<PendingTeacherServiceModel> PendingTeachers(int schoolId)
        {
            var pendingTeachers = this.data.Teachers
                .Where(t => t.IsApproved == false && t.SchoolId == schoolId)
                .Select(t => new PendingTeacherServiceModel
                {
                    Id = t.UserId,
                })
                .ToList();

            foreach (var teacher in pendingTeachers)
            {
                var user = data.Users.Find(teacher.Id);
                teacher.FullName = user.FullName;
                teacher.UserEmail = user.Email;
            }

            return pendingTeachers;
        }

        public IEnumerable<TeacherServiceModel> Teachers(int schoolId)
        {
            var teachers = this.data.Teachers
                .Where(t => t.SchoolId == schoolId)
                .Select(t => new TeacherServiceModel
                {
                    Id = t.UserId,
                    Status = t.IsApproved
                })
                .ToList();

            foreach (var teacher in teachers)
            {
                var user = data.Users.Find(teacher.Id);
                teacher.FullName = user.FullName;
                teacher.UserEmail = user.Email;
            }

            return teachers;
        }
               
        public void ApproveTeacher(string userId)
        {
            var user = this.data.Users.Find(userId);

            Task.Run(async () =>
            {
                await userManager.AddToRoleAsync(user, TeacherRoleName);
            })
                .GetAwaiter()
                .GetResult();


            this.data.Teachers.First(u => u.UserId == userId).IsApproved = true;

            this.data.SaveChanges();
        }

        public void DenyTeacher(string userId)
        {
            var teacherToDeny = this.data.Teachers
                .First(u => u.UserId == userId);

            this.data.Teachers.Remove(teacherToDeny);

            this.data.SaveChanges();
        }

        public void RemoveTeacher(string userId)
        {

            var user = this.data.Users.Find(userId);

            Task.Run(async () =>
            {
                await userManager.RemoveFromRoleAsync(user, TeacherRoleName);
            })
                .GetAwaiter()
                .GetResult();

            var teacherToRemove = this.data.Teachers
                .First(u => u.UserId == userId);

            var categories = this.data
                .TeachersCategories.Where(c => c.TeacherId == teacherToRemove.Id);

            if (categories.Count() > 0)
            {
                this.data.TeachersCategories.RemoveRange(categories);
            }

            user.SchoolId = 0;

            this.data.Teachers.Remove(teacherToRemove);

            this.data.SaveChanges();
        }

        public void RequestTeacher(string userId, int id)
        {
            if (this.data.Teachers.Any(u => u.UserId == userId))
            {
                return;
            }

            var school = this.data.Schools.First(s => s.Id == id);

            this.data.Teachers.Add(new Teacher
            {
                SchoolId = id,
                UserId = userId,
                IsApproved = false
            });

            var user = this.data.Users.Find(userId);

            user.SchoolId = id;

            this.data.SaveChanges();
        }

        public IEnumerable<PendingStudentServiceModel> PendingStudents(int schoolId)
        {
            var pendingStudents = this.data.Students
                .Where(s => s.IsApproved == false)
                .Select(s => new PendingStudentServiceModel
                {
                    Id = s.UserId,
                })
                .ToList();

            foreach (var student in pendingStudents)
            {
                var user = data.Users.Find(student.Id);
                student.FullName = user.FullName;
                student.UserEmail = user.Email;
            }

            return pendingStudents;
        }

        public IEnumerable<StudentServiceModel> Students(int schoolId)
        {
            var students = this.data.Students
                .Select(s => new StudentServiceModel
                {
                    Id = s.UserId,
                    Status = s.IsApproved
                })
                .ToList();

            foreach (var student in students)
            {
                var user = data.Users.Find(student.Id);
                student.FullName = user.FullName;
                student.UserEmail = user.Email;
            }

            return students;
        }

        public void ApproveStudent(string userId)
        {
            var user = this.data.Users.Find(userId);

            Task.Run(async () =>
            {
                await userManager.AddToRoleAsync(user, StudentRoleName);
            })
                .GetAwaiter()
                .GetResult();


            this.data.Students.First(u => u.UserId == userId).IsApproved = true;

            this.data.SaveChanges();
        }

        public void DenyStudent(string userId)
        {
            var studentToDeny = this.data.Students
                .First(u => u.UserId == userId);

            this.data.Students.Remove(studentToDeny);

            this.data.SaveChanges();
        }

        public void RemoveStudent(string userId)
        {

            var user = this.data.Users.Find(userId);

            Task.Run(async () =>
            { 
                await userManager.RemoveFromRoleAsync(user, StudentRoleName);
            })
                .GetAwaiter()
                .GetResult();

            var studentToRemove = this.data.Students
                .First(u => u.UserId == userId);

            user.SchoolId = 0;

            this.data.Students.Remove(studentToRemove);

            this.data.SaveChanges();
        }

        public void RequestStudent(string userId, int id)
        {
            if (this.data.Students.Any(u => u.UserId == userId))
            {
                return;
            }

            var school = this.data.Schools.First(s => s.Id == id);

            this.data.Students.Add(new Student
            {
                SchoolId = id,
                UserId = userId,
                IsApproved = false
            });

            var user = this.data.Users.Find(userId);

            user.SchoolId = id;

            this.data.SaveChanges();
        }

                       
    }
}
