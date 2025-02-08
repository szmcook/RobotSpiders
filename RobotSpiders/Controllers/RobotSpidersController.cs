using Microsoft.AspNetCore.Mvc;
using RobotSpiders.Classes;
using RobotSpiders.DTO;
using RobotSpiders.Services;

namespace RobotSpiders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RobotSpidersController(
        ILogger<RobotSpidersController> logger,
        ISpiderNavigationService spiderNavigationService,
        ISpiderRequestParser spiderRequestParser
        ) : ControllerBase
    {
        private readonly ILogger<RobotSpidersController> _logger = logger;
        private readonly ISpiderNavigationService _spiderNavigationService = spiderNavigationService;
        private readonly ISpiderRequestParser _spiderRequestParser = spiderRequestParser;

        [HttpPost("JSONStart")]
        public IActionResult JSONStart([FromBody] SpiderNavigationRequestDto requestDto)
        {
            _logger.LogInformation("Received JSON request to Start spider navigation: {@requestDto}", requestDto);
            try
            {
                Wall wall = requestDto.GetWall();
                Spider spider = requestDto.GetSpider();
                SpiderPosition? endPosition = _spiderNavigationService.CalculateEndPosition(wall, spider, requestDto.Directions);

                return endPosition != null ? Ok(endPosition) : BadRequest("Spider path was invalid.");

            }
            catch (ArgumentException ex)
            {
                _logger.LogError("Argument exception");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Start")]
        public async Task<IActionResult> Start()
        {
            using var reader = new StreamReader(Request.Body);
            string request = await reader.ReadToEndAsync();

            _logger.LogInformation("Received string request to Start spider navigation: {@request}", request);
            try
            {
                (Wall wall, Spider spider, string directions) = _spiderRequestParser.ParseRequest(request);
                SpiderPosition? endPosition = _spiderNavigationService.CalculateEndPosition(wall, spider, directions);

                return endPosition != null ? Ok($"{endPosition.X} {endPosition.Y} {endPosition.Orientation}") : BadRequest("Spider path was invalid");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError("Argument exception");
                return BadRequest(ex.Message);
            }
        }
    }
}
