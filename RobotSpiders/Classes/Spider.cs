using RobotSpiders.Enums;

namespace RobotSpiders.Classes
{
    public class Spider(SpiderPosition position)
    {
        public SpiderPosition Position { get; set; } = position;

        public SpiderPosition CalculateNextPosition(char direction)
        {
            return direction switch
            {
                'L' => new SpiderPosition(Position.X, Position.Y, (Orientation)(((int)Position.Orientation + 3) % 4)),
                'R' => new SpiderPosition(Position.X, Position.Y, (Orientation)(((int)Position.Orientation + 1) % 4)),
                _ => CalculateForwardStep(),
            };
        }

        private SpiderPosition CalculateForwardStep()
        {
            return Position.Orientation switch
            {
                Orientation.Left => new SpiderPosition(Position.X - 1, Position.Y, Position.Orientation),
                Orientation.Right => new SpiderPosition(Position.X + 1, Position.Y, Position.Orientation),
                Orientation.Down => new SpiderPosition(Position.X, Position.Y - 1, Position.Orientation),
                Orientation.Up => new SpiderPosition(Position.X, Position.Y + 1, Position.Orientation),
                _ => Position,
            };
        }
    }
}
