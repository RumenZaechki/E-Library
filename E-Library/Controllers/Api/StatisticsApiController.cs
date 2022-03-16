using E_Library.Models.Api.Statistics;
using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;
        public StatisticsApiController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }
        [HttpGet]
        public StatisticsResponseModel GetStatistics()
        {
            return new StatisticsResponseModel
            {
                TotalBooks = statisticsService.GetTotalBooks(),
                TotalUsers = statisticsService.GetTotalUsers(),
            };
        }
    }
}