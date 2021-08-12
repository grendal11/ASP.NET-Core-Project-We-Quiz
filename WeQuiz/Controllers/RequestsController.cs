namespace WeQuiz.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using WeQuiz.Data;
    using WeQuiz.Data.Models;
    using WeQuiz.Models.Requests;

    public class RequestsController : Controller
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

        //Todo
        public IActionResult Category() => View();

        [HttpPost]
        public IActionResult Category(CategoryRequestFormModel category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            //var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var currentUser = this.data.Users.Find(userId);

            var newCategory = new SuggestedCategory
            {
                Name = category.Name,
                Description = category.Description,
                SchoolCode = category.IsPrivate ? /*currentUser.SchoolCode*/ 500102 : 0
            };

            data.SuggestedCategories.Add(newCategory);
            data.SaveChanges();

            return RedirectToAction("All", "Requests");
        }

        //public IActionResult Subcategory() => View();

        //[HttpPost]
        //public IActionResult Subcategory(SubcategoryRequestFormModel subCategory)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(subCategory);
        //    }

        //    var newSubcategory = new SubcategoryRequest
        //    {

        //    };

        //    data.SubcategoryRequests.Add(newSubcategory);
        //    data.SaveChanges();

        //    return RedirectToAction("All", "Requests");
        //}

    }
}
