namespace RobotSpiders.Classes
{
    public class Wall(int x, int y)
    {
        private readonly int x = x;
        private readonly int y = y;

        public bool ValidatePosition(SpiderPosition spiderPosition)
        {
            return 0 <= spiderPosition.X && spiderPosition.X < x && 0 <= spiderPosition.Y && spiderPosition.Y < y;
        }
    }
}
