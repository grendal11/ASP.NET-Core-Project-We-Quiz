namespace WeQuiz.Services.Schools
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public interface ISchoolsService
    {
        public IEnumerable<SchoolServiceModel> All();

        public IEnumerable<RequestedSchoolServiceModel> Requested();

        public void Add(string name, int populatedAreaId, string schoolCode);

        public void AddPopulatedArea(string name, int districtId);

        public void FinishSchoolRequest(int id);

        public bool DIstrictExists(int id);

        public bool PopulatedAreaExists(int id);

        public IEnumerable<PopulatedAreaServiceModel> GetPopulatedAreas(int id);

        public IEnumerable<DistrictServiceModel> GetDistricts();
    }
}
