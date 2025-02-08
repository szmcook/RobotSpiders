using RobotSpiders.Classes;

namespace RobotSpiders.Services
{
    public class SpiderNavigationService : ISpiderNavigationService
    {
        private readonly ILogger<ISpiderNavigationService> _logger;

        public SpiderNavigationService(ILogger<ISpiderNavigationService> logger) {
            _logger = logger;
        }

        public SpiderPosition? CalculateEndPosition(Wall wall, Spider spider, string directions)
        {
            if (!wall.ValidatePosition(spider.Position))
            {
                _logger.LogWarning("Path took spider off the wall.");
                return null;
            }

            char[] dirs = directions.ToCharArray();

            foreach (char dir in dirs)
            {
                SpiderPosition nextPosition = spider.CalculateNextPosition(dir);
                if (wall.ValidatePosition(nextPosition))
                {
                    spider.Position = nextPosition;
                } else
                {
                    _logger.LogWarning("Path took spider off the wall.");
                    return null;
                }

            }
            return spider.Position;
        }
    }
}
