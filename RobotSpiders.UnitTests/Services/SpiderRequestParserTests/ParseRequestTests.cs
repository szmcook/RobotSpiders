using RobotSpiders.Classes;
using RobotSpiders.Enums;
using RobotSpiders.Services;

namespace RobotSpiders.UnitTests.Services.SpiderRequestParserTests
{
    public class ParseRequestTests
    {
        private SpiderRequestParser sut = new SpiderRequestParser();

        [Fact]
        public void ValidInput_ReturnsExpectedValues()
        {
            // Arrange
            string request = "7 15\n2 4 Left\nFLFLFRFFLF";

            // Act
            var (wall, spider, directions) = sut.ParseRequest(request);

            // Assert
            Assert.IsType<Wall>(wall);
            Assert.Equal(2, spider.Position.X);
            Assert.Equal(4, spider.Position.Y);
            Assert.Equal(Orientation.Left, spider.Position.Orientation);
            Assert.Equal("FLFLFRFFLF", directions);
        }

        [Fact]
        public void InvalidWallDimensions_ThrowsArgumentException()
        {   
            // Arrange
            string request = "invalid input\n1 1 Right\nLRR";

            // Act
            var exception = Assert.Throws<ArgumentException>(() => sut.ParseRequest(request));

            // Assert
            Assert.Equal("Invalid Wall dimensions.", exception.Message);
        }

        [Fact]
        public void InvalidSpiderPosition_ThrowsArgumentException()
        {
            // Arrange
            string request = "5 5\ninvalid input\nLRR";

            // Act
            var exception = Assert.Throws<ArgumentException>(() => sut.ParseRequest(request));

            // Assert
            Assert.Equal("Invalid Spider position.", exception.Message);
        }

        [Fact]
        public void InvalidSpiderOrientation_ThrowsArgumentException()
        {
            // Arrange
            string request = "5 5\n1 1 InvalidOrientation\nLRR";

            // Act
            var exception = Assert.Throws<ArgumentException>(() => sut.ParseRequest(request));

            // Assert
            Assert.Equal("Invalid Spider position.", exception.Message);
        }

        [Fact]
        public void InvalidNumberOfLines_ThrowsArgumentException()
        {
            // Arrange
            string request = "5 5\n1 1 Right";

            // Act
            var exception = Assert.Throws<ArgumentException>(() => sut.ParseRequest(request));

            // Assert
            Assert.Equal("Input format is invalid. Expected 3 lines.", exception.Message);
        }
    }
}
