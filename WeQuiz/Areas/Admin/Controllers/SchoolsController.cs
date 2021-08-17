namespace WeQuiz.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WeQuiz.Areas.Admin.Models.Schools;
    using WeQuiz.Services.Schools;

    public class SchoolsController : AdminController
    {

        private readonly ISchoolsService schools;

        public SchoolsController(ISchoolsService schools)
        {
            this.schools = schools;
        }

        public IActionResult All()
        {
            var schools = this.schools.All();

            return View(schools);
        }

        public IActionResult Requested()
        {
            var schools = this.schools.Requested();

            return View(schools);
        }

        public IActionResult Finish(int id) 
        {
            this.schools.FinishSchoolRequest(id);

            return Redirect("/Admin/Schools/Requested");
        }

        public IActionResult Add() => View(new AddSchoolFormModel
        {
            Districts = this.schools.GetDistricts()
        });

        [HttpPost]
        public IActionResult Add(AddSchoolFormModel school)
        {
            if (!this.schools.PopulatedAreaExists(school.PopulatedAreaId))
            {
                this.ModelState.AddModelError(nameof(school.PopulatedAreaId), "Населеното място не съществува.");
            }

            if (!ModelState.IsValid)
            {
                school.Districts = this.schools.GetDistricts();

                return View(school);
            }

            this.schools.Add(school.Name, school.PopulatedAreaId, school.SchoolCode);

            return Redirect("/Admin/Schools/All");
        }

        public IActionResult AddPopulatedArea() => View(new AddPopulatedAreaFormModel
        {
            Districts = this.schools.GetDistricts()
        });

        [HttpPost]
        public IActionResult AddPopulatedArea(AddPopulatedAreaFormModel populatedArea)
        {
            if (!this.schools.DIstrictExists(populatedArea.DistrictId))
            {
                this.ModelState.AddModelError(nameof(populatedArea.DistrictId), "Няма такава област.");
            }

            if (!ModelState.IsValid)
            {
                populatedArea.Districts = this.schools.GetDistricts();

                return View(populatedArea);
            }

            this.schools.AddPopulatedArea(populatedArea.Name, populatedArea.DistrictId);

            return Redirect("/Admin/Schools/Add");
        }

       
        public JsonResult GetPopulatedAreas(int districtId)
        {
            var populatedAreas = this.schools.GetPopulatedAreas(districtId);

            return Json(populatedAreas);
        }
    }
}
