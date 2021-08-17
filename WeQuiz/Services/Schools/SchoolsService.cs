namespace WeQuiz.Services.Schools
{
    using System.Collections.Generic;
    using System.Linq;
    using WeQuiz.Data;
    using WeQuiz.Data.Models;

    public class SchoolsService : ISchoolsService
    {
        private readonly WeQuizDbContext data;

        public SchoolsService(WeQuizDbContext data)
            => this.data = data;

        public IEnumerable<SchoolServiceModel> All()
        {
            var schools = this.data
                .Schools
                .Select(s => new SchoolServiceModel
                {
                    Id = s.Id,
                    District = s.PopulatedArea.District.Name,
                    PopulatedArea = s.PopulatedArea.Name,
                    Name = s.Name,
                    SchoolCode = s.SchoolCode
                })
                .OrderBy(s => s.Name)
                .ToList();

            return schools;
        }

        public IEnumerable<RequestedSchoolServiceModel> Requested() 
        {
            var schools = this.data
                    .SchoolRequests
                    .Select(s => new RequestedSchoolServiceModel
                    {
                        Id = s.Id,
                        District = s.District,
                        PopulatedArea = s.PopulatedArea,
                        Name = s.Name
                    })
                    .OrderBy(s => s.Name)
                    .ToList();

            return schools;
        }

        public void Add(string name, int populatedAreaId, string schoolCode) 
        {
            var newSchool = new School
            {
                Name = name,
                PopulatedAreaId = populatedAreaId,
                SchoolCode = int.Parse(schoolCode)
            };

            this.data.Schools.Add(newSchool);
            this.data.SaveChanges();
        }

        public void AddPopulatedArea(string name, int districtId)
        {
            var newPopulatedArea = new PopulatedArea
            {
                Name = name,
                DistrictId = districtId
            };

            this.data.PopulatedAreas.Add(newPopulatedArea);
            this.data.SaveChanges();
        }

        public void AddSchoolRequest(string name, string district, string populatedArea) 
        {
            var newSchool = new SchoolRequest
            {
                Name = name,
                District = district,
                PopulatedArea = populatedArea
            };

            data.SchoolRequests.Add(newSchool);
            data.SaveChanges();
        }

        public void FinishSchoolRequest(int id) 
        {
            var requestToRemove = this.data.SchoolRequests.Find(id);

            this.data.SchoolRequests.Remove(requestToRemove);

            this.data.SaveChanges();
        }

        public bool PopulatedAreaExists(int id) 
        {
            if (this.data.PopulatedAreas.Any(p => p.Id == id)) 
            {
                return true;
            }

            return false;
        }   
        
        public bool DIstrictExists(int id) 
        {
            if (this.data.Districts.Any(p => p.Id == id)) 
            {                
                return true;
            }

            return false;
        }

        public IEnumerable<DistrictServiceModel> GetDistricts()
            => this.data
            .Districts
            .Select(d => new DistrictServiceModel
            {
                Id = d.Id,
                Name = d.Name
            })
            .ToList();

        public IEnumerable<PopulatedAreaServiceModel> GetPopulatedAreas(int id)
        {
            var populatedAreas = this.data
                .PopulatedAreas
                .Where(pa => pa.DistrictId == id)
                .Select(pa => new PopulatedAreaServiceModel
                {
                    Id = pa.Id,
                    Name = pa.Name
                })
                .ToList();

            return populatedAreas;
        }

    }
}
