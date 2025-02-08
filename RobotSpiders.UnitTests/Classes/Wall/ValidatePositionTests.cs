using RobotSpiders.Classes;
using RobotSpiders.Enums;

namespace RobotSpiders.UnitTests.Classes.Wall
{
    public class ValidatePositionTests
    {
        public ValidatePositionTests() {}

        private readonly RobotSpiders.Classes.Wall sut = new(7, 15);

        [Theory]
        [InlineData(6, 14, Orientation.Left, true)]  // Inside the wall
        [InlineData(-1, 0, Orientation.Left, false)] // Off to the left
        [InlineData(7, 0, Orientation.Left, false)]  // Off to the right
        [InlineData(0, 15, Orientation.Left, false)] // Off to the top
        [InlineData(0, -1, Orientation.Left, false)] // Off to the bottom]
        public void ValidatePosition_ReturnsExpectedResult(int x, int y, Orientation orientation, bool expectedResult)
        {
            // Arrange
            var position = new SpiderPosition(x, y, orientation);

            // Act
            var result = sut.ValidatePosition(position);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
