using RobotSpiders.Enums;

namespace RobotSpiders.Classes
{
    public class SpiderPosition(int posX, int posY, Orientation orientation)
    {
        public int X { get; } = posX;
        public int Y { get; } = posY;
        public Orientation Orientation { get; } = orientation;
    }
}
