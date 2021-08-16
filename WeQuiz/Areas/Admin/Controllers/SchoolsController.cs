namespace WeQuiz.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using WeQuiz.Data;
    using WeQuiz.Areas.Admin.Models.Schools;
    using WeQuiz.Data.Models;

    public class SchoolsController : AdminController
    {

        private readonly WeQuizDbContext data;

        public SchoolsController(WeQuizDbContext data)
        {
            this.data = data;
        }

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
            ViewBag.Count = schools.Count;

            return View(schools);
        }

        public IActionResult Requested()
        {
            var schools = this.data
                .SchoolRequests
                .Select(s => new RequestedSchoolsViewModel
                {
                    Id = s.Id,
                    District = s.District,
                    PopulatedArea = s.PopulatedArea,
                    Name = s.Name
                })
                .OrderBy(s => s.Name)
                .ToList();

            ViewBag.Count = schools.Count;

            return View(schools);
        }

        public IActionResult Finish(int id) 
        {
            var requestToRemove = this.data.SchoolRequests.Find(id);

            this.data.SchoolRequests.Remove(requestToRemove);

            this.data.SaveChanges();

            return Redirect("/Admin/Schools/Requested");
        }

        public IActionResult Add() => View(new AddSchoolFormModel
        {
            Districts = this.GetDistricts()
        });

        [HttpPost]
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

            this.data.Schools.Add(newSchool);
            this.data.SaveChanges();

            return Redirect("/Admin/Schools/All");
        }

        public IActionResult AddPopulatedArea() => View(new AddPopulatedAreaFormModel
        {
            Districts = this.GetDistricts()
        });

        [HttpPost]
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

            this.data.PopulatedAreas.Add(newPopulatedArea);
            this.data.SaveChanges();

            return Redirect("/Admin/Schools/Add");
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
