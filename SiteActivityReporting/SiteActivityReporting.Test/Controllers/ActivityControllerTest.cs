using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiteActivityReporting.Controllers;
using SiteActivityReporting.DTO;
using SiteActivityReporting.Service;
using System;
using Xunit;

namespace SiteActivityReporting.Test.Controllers
{
    public class ActivityControllerTest
    {
        private readonly ActivityController _controller;
        private readonly IActivityEventService _activityEventService;
        private readonly ILogger<ActivityController> _logger;

        public ActivityControllerTest()
        {
            _activityEventService = new ActivityEventServiceFake();
            _controller = new ActivityController(_activityEventService, _logger);
        }

        [Fact]
        public void Post_EmptyKey_ReturnsBadRequest()
        {
            //Arrange
            var activityEventItem = new ActivityEventDTO()
            {
                Value = 20
            };

            //Act
            var badResponse = _controller.Post(null, activityEventItem);
            
            //Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Post_EmptyObject_ReturnsBadRequest()
        {
            //Arrange
            ActivityEventDTO activityEventItem = null;
            var key = string.Empty;

            //Act
            var badResponse = _controller.Post(key, activityEventItem);

            //Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Post_CorrectObject_ReturnsOkRequest()
        {
            //Arrange
            var activityEventItem = new ActivityEventDTO()
            {
                Value = 20
            };

            //Act
            var okResponse = _controller.Post("learn_more_page", activityEventItem) ;

            //Assert
            Assert.IsType<OkResult>(okResponse);
        }

        [Fact]
        public void Get_EmptyKey_ReturnsBadRequest()
        {
            //Arrange
            string key = string.Empty;

            //Act
            var badResponse = _controller.Get(key);

            //Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Get_CorrectKey_ReturnsOkRequest()
        {
            //Arrange
            var key = "learn_more_page";

            //Act
            var okResponse = _controller.Get(key);

            //Assert
            Assert.IsType<OkObjectResult>(okResponse);
        }

        [Fact]
        public void Get_Total_KeyExistInPrev12Hour_ReturnsRightItem()
        {
            //Arrange
            var activityEventItem = new ActivityEventDTO()
            {
                Value = 4
            };
            var key = "learn_more_page";

            //Act
            _controller.Post(key, activityEventItem);
            var fetchedOkResponse = _controller.Get(key) as OkObjectResult;
            
            //Assert
            Assert.NotNull(fetchedOkResponse);
            var item = Convert.ToInt32(fetchedOkResponse.Value);

            Assert.IsType<int>(item);
            Assert.Equal(41, item);
        }

        [Fact]
        public void Get_Total_KeyNotExistInPrev12Hour_ReturnsZero()
        {
            //Arrange
            var key = "learn_more";

            //Act
            var fetchedOkResponse = _controller.Get(key) as OkObjectResult;

            //Assert
            Assert.NotNull(fetchedOkResponse);
            var item = Convert.ToInt32(fetchedOkResponse.Value);

            Assert.IsType<int>(item);
            Assert.Equal(0, item);
        }
    }
}
