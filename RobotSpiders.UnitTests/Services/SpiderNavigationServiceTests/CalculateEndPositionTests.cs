using AutoFixture;
using AutoFixture.AutoMoq;
using RobotSpiders.Classes;
using RobotSpiders.Services;

namespace RobotSpiders.UnitTests.Services.SpiderNavigationServiceTests
{
    public class CalculateEndPositionTests
    {
        private readonly IFixture _fixture;

        public CalculateEndPositionTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization() { ConfigureMembers = true });
        }

        [Fact]
        public void ReturnsEndPosition_When_MovesValid()
        {
            // Arrange
            var wall = new Wall(7, 15);
            var spider = new Spider(new SpiderPosition(2, 4, Enums.Orientation.Left));
            string directions = "FLFLFRFFLF";

            SpiderPosition expectedPosition = new(3, 1, Enums.Orientation.Right);

            var sut = _fixture.Build<SpiderNavigationService>().OmitAutoProperties().Create();

            // Act
            var result = sut.CalculateEndPosition(wall, spider, directions);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedPosition.X, result.X);
            Assert.Equal(expectedPosition.Y, result.Y);
            Assert.Equal(expectedPosition.Orientation, result.Orientation);
        }

        [Fact]
        public void ReturnsNull_When_SpiderStartsOffWall()
        {
            // Arrange
            var wall = new Wall(3, 3);
            var spider = new Spider(new SpiderPosition(2, 4, Enums.Orientation.Left));
            string directions = "FLFLFRFFLF";

            SpiderPosition expectedPosition = new(3, 1, Enums.Orientation.Right);

            var sut = _fixture.Build<SpiderNavigationService>().OmitAutoProperties().Create();

            // Act
            var result = sut.CalculateEndPosition(wall, spider, directions);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ReturnsNull_When_SpiderTravelsOffWall()
        {
            // Arrange
            var wall = new Wall(7, 15);
            var spider = new Spider(new SpiderPosition(2, 4, Enums.Orientation.Left));
            string directions = "FLFLFRFFLFFFFFFFFFFFFs";

            SpiderPosition expectedPosition = new(3, 1, Enums.Orientation.Right);

            var sut = _fixture.Build<SpiderNavigationService>().OmitAutoProperties().Create();

            // Act
            var result = sut.CalculateEndPosition(wall, spider, directions);

            // Assert
            Assert.Null(result);
        }
    }
}
