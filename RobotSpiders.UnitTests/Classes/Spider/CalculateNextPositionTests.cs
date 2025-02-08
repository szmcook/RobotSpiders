using RobotSpiders.Classes;
using RobotSpiders.Enums;

namespace RobotSpiders.UnitTests.Classes.Spider
{
    public class CalculateNextPositionTests
    {
        [Theory]
        [InlineData(Orientation.Up, 'L', Orientation.Left)]
        [InlineData(Orientation.Left, 'L', Orientation.Down)]
        [InlineData(Orientation.Down, 'L', Orientation.Right)]
        [InlineData(Orientation.Right, 'L', Orientation.Up)]
        public void ReturnsCorrect_When_TurnsLeft(Orientation startOrientation, char direction, Orientation expectedOrientation)
        {
            // Arrange
            var startPosition = new SpiderPosition(2, 3, startOrientation);
            var spider = new RobotSpiders.Classes.Spider(startPosition);

            // Act
            var newPosition = spider.CalculateNextPosition(direction);

            // Assert
            Assert.Equal(startPosition.X, newPosition.X);
            Assert.Equal(startPosition.Y, newPosition.Y);
            Assert.Equal(expectedOrientation, newPosition.Orientation);
        }

        [Theory]
        [InlineData(Orientation.Up, 'R', Orientation.Right)]
        [InlineData(Orientation.Right, 'R', Orientation.Down)]
        [InlineData(Orientation.Down, 'R', Orientation.Left)]
        [InlineData(Orientation.Left, 'R', Orientation.Up)]
        public void ReturnsCorrect_When_TurnsRight(Orientation startOrientation, char direction, Orientation expectedOrientation)
        {
            // Arrange
            var startPosition = new SpiderPosition(2, 3, startOrientation);
            var spider = new RobotSpiders.Classes.Spider(startPosition);

            // Act
            var newPosition = spider.CalculateNextPosition(direction);

            // Assert
            Assert.Equal(startPosition.X, newPosition.X);
            Assert.Equal(startPosition.Y, newPosition.Y);
            Assert.Equal(expectedOrientation, newPosition.Orientation);
        }

        [Theory]
        [InlineData(0, 0, Orientation.Up, 0, 1)]
        [InlineData(0, 0, Orientation.Right, 1, 0)]
        [InlineData(0, 1, Orientation.Down, 0, 0)]
        [InlineData(1, 0, Orientation.Left, 0, 0)]
        public void ReturnsCorrect_When_GoesForward(int startX, int startY, Orientation orientation, int endX, int endY)
        {
            // Arrange
            var startPosition = new SpiderPosition(startX, startY, orientation);
            var spider = new RobotSpiders.Classes.Spider(startPosition);

            // Act
            var newPosition = spider.CalculateNextPosition('F');

            // Assert
            Assert.Equal(endX, newPosition.X);
            Assert.Equal(endY, newPosition.Y);
            Assert.Equal(orientation, newPosition.Orientation);
        }
    }
}
