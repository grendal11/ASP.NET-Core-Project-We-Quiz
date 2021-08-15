namespace WeQuiz.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WeQuiz.Services.Statistics;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statistics;

        public StatisticsApiController(IStatisticsService statistics)
           => this.statistics = statistics;

        [HttpGet]
        [Route("{id}")]
        public UserStaticsicsServiceModel UserStatistics(string userId)
         => this.statistics.UserStaticsics(userId);
    }
}
