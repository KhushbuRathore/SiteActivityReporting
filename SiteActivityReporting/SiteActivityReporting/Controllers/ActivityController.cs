using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiteActivityReporting.DTO;
using SiteActivityReporting.Helper.Mapper;
using SiteActivityReporting.Service;
using System;

namespace SiteActivityReporting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {

        private readonly IActivityEventService _activityEventService;
        private readonly ILogger<ActivityController> _logger;

        public ActivityController(IActivityEventService activityEventService, ILogger<ActivityController> logger)
        {
            _activityEventService = activityEventService;
            _logger = logger;
        }

        [HttpPost]
        [Route("{key}")]
        public  IActionResult Post(string key, [FromBody] ActivityEventDTO activityEvent)
        {
            try
            {
                if (string.IsNullOrEmpty(key) || activityEvent == null)
                {
                    return BadRequest("Key or ActivityEvent object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model ActivityEvent");
                }

                // Convert ActivityEventDTO to ActivityEventEntity
                var activityEventEntity = activityEvent.ToEntity(key);

                // Insert ActivityEvent Record
                _activityEventService.InsertActivityEvent(activityEventEntity);

                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Exception in Activity Post");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("{key}/total")]
        public IActionResult Get(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    return BadRequest("key is null");
                }

                // Get Total Activity Event Count.
                var activityEvent = _activityEventService.GetActivityEventTotal(key);                
                return Ok(new ActivityEventDTO { Value = activityEvent });
            }
            catch (Exception ex)
            {                
                _logger.LogError(ex, "Exception in Activity Get Count by key");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
