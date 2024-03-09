using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using net_test_api.Services;

namespace net_test_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;

        public StatusController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }
        [HttpGet("/status")]
        public async Task<IActionResult> GetStatus()
        {
            // Get database status
            bool databaseStatus = await _mongoDBService.GetDatabaseStatus();

            // Construct response
            string response = $"App Status: Active\nDatabase Status: {(databaseStatus ? "Connected" : "Disconnected")}";

            return Ok(response);
        }
    }
}