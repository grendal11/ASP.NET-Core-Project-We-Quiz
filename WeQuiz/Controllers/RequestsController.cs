namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WeQuiz.Data;
    using WeQuiz.Data.Models;
    using WeQuiz.Models.Requests;

    public class RequestsController: Controller
    {
        private readonly WeQuizDbContext data;

        public RequestsController(WeQuizDbContext data)
        {
            this.data = data;
        }

        public IActionResult All() => View();

        public IActionResult School() => View();

        [HttpPost]
        public IActionResult School(SchoolRequestFormModel school)
        {
            if (!ModelState.IsValid)
            {
                return View(school);
            }

            var newSchool = new SchoolRequest
            {
                Name = school.Name,
                District = school.District,
                PopulatedArea = school.PopulatedArea
            };

            data.SchoolRequests.Add(newSchool);
            data.SaveChanges();

            return RedirectToAction("All", "Schools");
        }
    }
}
