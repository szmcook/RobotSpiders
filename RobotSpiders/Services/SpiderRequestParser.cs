using RobotSpiders.Classes;
using RobotSpiders.Enums;

namespace RobotSpiders.Services
{
    public class SpiderRequestParser : ISpiderRequestParser
    {
        public SpiderRequestParser() { }

        public (Wall, Spider, string) ParseRequest(string request)
        {
            var lines = request.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (lines.Length < 3) throw new ArgumentException("Input format is invalid. Expected 3 lines.");

            var wallParts = lines[0].Split(' ');
            if (wallParts.Length != 2 ||
                !int.TryParse(wallParts[0], out int wallX) ||
                !int.TryParse(wallParts[1], out int wallY))
            {
                throw new ArgumentException("Invalid Wall dimensions.");
            }
            Wall wall = new(wallX, wallY);

            var spiderParts = lines[1].Split(' ');
            if (spiderParts.Length != 3 ||
                !int.TryParse(spiderParts[0], out int posX) ||
                !int.TryParse(spiderParts[1], out int posY) ||
                !Enum.TryParse(spiderParts[2], true, out Orientation orientation))
            {
                throw new ArgumentException("Invalid Spider position.");
            }
            SpiderPosition spiderPosition = new(posX, posY, orientation);
            Spider spider = new(spiderPosition);

            string directions = lines[2];

            return (wall, spider, directions);
        }
    }
}
