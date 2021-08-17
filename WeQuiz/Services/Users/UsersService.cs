namespace WeQuiz.Services.Users
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        public void RequestAdmin(string userId, int id)
        {
            if (this.data.SchoolAdmins.Any(u=>u.UserId == userId))
            {
                return;
            }

            var school = this.data.Schools.First(s => s.Id==id);

            this.data.SchoolAdmins.Add(new SchoolAdmin
            {
                School=school,
                UserId=userId,
                IsApproved = false
            });
           
            this.data.SaveChanges();
        }
    }
}
