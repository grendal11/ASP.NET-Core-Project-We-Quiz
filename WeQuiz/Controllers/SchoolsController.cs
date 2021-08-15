namespace WeQuiz.Controllers
{
    using System.Collections.Generic;
    using WeQuiz.Data;
    using WeQuiz.Models.Schools;
    using WeQuiz.Views.Schools;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using WeQuiz.Data.Models;
    using Microsoft.AspNetCore.Authorization;

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
                .ToList();

            return View(schools);
        }

        [Authorize]
        public IActionResult Add() => View(new AddSchoolFormModel
        {
            Districts = this.GetDistricts()
        });

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddSchoolFormModel school)
        {
            if (!this.data.PopulatedAreas.Any(p => p.Id == school.PopulatedAreaId))
            {
                this.ModelState.AddModelError(nameof(school.PopulatedAreaId), "Населеното място не съществува.");
            }

            if (!ModelState.IsValid)
            {
                school.Districts = this.GetDistricts();

                return View(school);
            }

            var newSchool = new School
            {
                Name = school.Name,
                PopulatedAreaId = school.PopulatedAreaId,
                SchoolCode = int.Parse(school.SchoolCode)
            };

            data.Schools.Add(newSchool);
            data.SaveChanges();

            return RedirectToAction("All", "Schools");
        }

        [Authorize]
        public IActionResult AddPopulatedArea() => View(new AddPopulatedAreaFormModel
        {
            Districts = this.GetDistricts()
        });

        [HttpPost]
        [Authorize]
        public IActionResult AddPopulatedArea(AddPopulatedAreaFormModel populatedArea)
        {
            if (!this.data.Districts.Any(d => d.Id == populatedArea.DistrictId))
            {
                this.ModelState.AddModelError(nameof(populatedArea.DistrictId), "Няма такава област.");
            }

            if (!ModelState.IsValid)
            {
                populatedArea.Districts = this.GetDistricts();

                return View(populatedArea);
            }

            var newPopulatedArea = new PopulatedArea
            {
                Name = populatedArea.Name,
                DistrictId = populatedArea.DistrictId
            };

            data.PopulatedAreas.Add(newPopulatedArea);
            data.SaveChanges();

            return RedirectToAction("Add", "Schools");
        }

        private IEnumerable<DistrictViewModel> GetDistricts()
            => this.data
            .Districts
            .Select(d => new DistrictViewModel
            {
                Id = d.Id,
                Name = d.Name
            })
            .ToList();

        public JsonResult GetPopulatedAreas(int districtId)
        {
            var populatedAreas = this.data
                .PopulatedAreas
                .Where(pa => pa.DistrictId == districtId)
                .Select(pa => new PopulatedAreasViewModel
                {
                    Id = pa.Id,
                    Name = pa.Name
                })
                .ToList();

            return Json(populatedAreas);
        }
    }
}
