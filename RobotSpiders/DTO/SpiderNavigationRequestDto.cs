using System.Text.Json.Serialization;
using RobotSpiders.Classes;
using RobotSpiders.Enums;

namespace RobotSpiders.DTO
{
    public class SpiderNavigationRequestDto
    {
        [JsonPropertyName("Wall")]
        public int[] Wall { get; set; }

        [JsonPropertyName("SpiderPosition")]
        public object[] SpiderPosition { get; set; }
        
        [JsonPropertyName("Directions")]
        public string Directions { get; set; }

        public SpiderNavigationRequestDto()
        {
            Wall = [0, 0];
            SpiderPosition = [0, 0, Orientation.Up.ToString()];
            Directions = "";
        }

        public Wall GetWall() => new Wall(Wall[0], Wall[1]);

        public Spider GetSpider()
        {
            if (SpiderPosition.Length != 3)
            {
                throw new ArgumentException("SpiderPosition should contain [x, y, \"Orientation\"].");
            }

            if (!int.TryParse(SpiderPosition[0].ToString(), out int x) ||
                !int.TryParse(SpiderPosition[1].ToString(), out int y) ||
                !Enum.TryParse(SpiderPosition[2].ToString(), true, out Orientation orientation))
            {
                throw new ArgumentException("Invalid SpiderPosition format.");
            }

            SpiderPosition sp = new (x, y, orientation);

            return new Spider(sp);
        }
    }
}
