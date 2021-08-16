namespace WeQuiz.Controllers
{
    using WeQuiz.Data;
    using WeQuiz.Models.Schools;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using WeQuiz.Infrastructure;

    public class SchoolsController : Controller
    {
        private readonly WeQuizDbContext data;

        public SchoolsController(WeQuizDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult All()
        {
            var schools = this.data
                .Schools
                .Select(s => new SchoolViewModel
                {
                    Id = s.Id,
                    District = s.PopulatedArea.District.Name,
                    PopulatedArea = s.PopulatedArea.Name,
                    Name = s.Name,
                    SchoolCode = s.SchoolCode
                })
                .OrderBy(s => s.Name)
                .ToList();

            var user = data.Users.Find(User.Id());

            var role = "";
            if (User.IsSchoolAdmin())
            {
                role = "Училищен администратор";
            }
            if (User.IsTeacher())
            {
                role = "Учител";
            }
            if (User.IsStudent())
            {
                role = "Ученик";
            }

            ViewBag.Role = role;
            ViewBag.SchoolId = user.SchoolId;
            ViewBag.Count = schools.Count;

            return View(schools);
        }

       
    }
}
