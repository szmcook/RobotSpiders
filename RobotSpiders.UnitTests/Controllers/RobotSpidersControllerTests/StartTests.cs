using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using RobotSpiders.Controllers;
using RobotSpiders.DTO;
using RobotSpiders.Services;
using RobotSpiders.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RobotSpiders.Enums;
using System.Text;

namespace RobotSpiders.UnitTests.Controllers.RobotSpidersControllerTests
{
    public class StartTests
    {

        private readonly IFixture _fixture;

        public StartTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization() { ConfigureMembers = true });
        }

        [Fact]
        public void ReturnsOK_When_ConditionsValid()
        {
            // Arrange
            var request = new SpiderNavigationRequestDto();
            request.Wall = [7, 15];
            request.SpiderPosition = [2, 4, "Left"];
            request.Directions = "FLFLFRFFLF";

            var expectedResult = new SpiderPosition(3, 1, Enums.Orientation.Right);

            _fixture.Freeze<Mock<ISpiderNavigationService>>()
                .Setup(s => s.CalculateEndPosition(It.IsAny<Wall>(), It.IsAny<Spider>(), It.IsAny<string>()))
                .Returns(expectedResult);

            var sut = _fixture.Build<RobotSpidersController>().OmitAutoProperties().Create();

            // Act
            var result = sut.JSONStart(request);

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultPos = Assert.IsType<SpiderPosition>(okResult.Value);
            Assert.Equal(expectedResult, resultPos);
        }

        [Fact]
        public void ReturnsBadRequest_When_SpiderOffWall()
        {
            // Arrange
            var request = new SpiderNavigationRequestDto();
            request.Wall = [7, 15];
            request.SpiderPosition = [0, 0, "Left"];
            request.Directions = "FLFLFRFFLF";

            var expectedResult = new SpiderPosition(3, 1, Enums.Orientation.Right);

            _fixture.Freeze<Mock<ISpiderNavigationService>>()
                .Setup(s => s.CalculateEndPosition(It.IsAny<Wall>(), It.IsAny<Spider>(), It.IsAny<string>()))
                .Returns(null as SpiderPosition);

            var sut = _fixture.Build<RobotSpidersController>().OmitAutoProperties().Create();

            // Act
            var result = sut.JSONStart(request);

            // Assert
            Assert.NotNull(result);
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ReturnsBadRequest_When_InvalidInput()
        {
            // Arrange
            var request = new SpiderNavigationRequestDto();
            request.Wall = [7, 15];
            request.SpiderPosition = [0, 0, "NotADirection"];
            request.Directions = "FLFLFRFFLF";

            var expectedResult = new SpiderPosition(3, 1, Enums.Orientation.Right);

            _fixture.Freeze<Mock<ISpiderNavigationService>>()
                .Setup(s => s.CalculateEndPosition(It.IsAny<Wall>(), It.IsAny<Spider>(), It.IsAny<string>()))
                .Returns(null as SpiderPosition);

            var sut = _fixture.Build<RobotSpidersController>().OmitAutoProperties().Create();

            // Act
            var result = sut.JSONStart(request);

            // Assert
            Assert.NotNull(result);
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public async Task ReturnsOK_When_StringConditionsValid()
        {
            // Arrange
            string requestText = "7 15\n2 4 Left\nFLFLFRFFLF";
            var expectedResult = new SpiderPosition(3, 1, Orientation.Right);
            var stringResult = "3 1 Right";

            var spiderNavigationServiceMock = _fixture.Freeze<Mock<ISpiderNavigationService>>();
            spiderNavigationServiceMock
                .Setup(s => s.CalculateEndPosition(It.IsAny<Wall>(), It.IsAny<Spider>(), It.IsAny<string>()))
                .Returns(expectedResult);

            var sut = _fixture.Build<RobotSpidersController>().OmitAutoProperties().Create();

            var context = new DefaultHttpContext();
            context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(requestText));
            sut.ControllerContext = new ControllerContext { HttpContext = context };

            // Act
            var result = await sut.Start();

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultPos = Assert.IsType<string>(okResult.Value);
            Assert.Equal(stringResult, resultPos);
        }
    }
}
