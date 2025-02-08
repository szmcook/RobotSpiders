using RobotSpiders.Classes;

namespace RobotSpiders.Services
{
    public interface ISpiderRequestParser
    {
        (Wall, Spider, string) ParseRequest(string request);
    }
}