using RobotSpiders.Classes;

namespace RobotSpiders.Services
{
    public interface ISpiderNavigationService
    {
        SpiderPosition? CalculateEndPosition(Wall wall, Spider spider, string directions);
    }
}