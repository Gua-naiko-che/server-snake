namespace server_snake.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using server_snake.Entities;

    [ApiController]
    [Route("[controller]")]
    public class SnakeController : ControllerBase
    {
        private readonly Snake _snake;
        private readonly ILogger<SnakeController> _logger;

        public SnakeController(Snake snake, ILogger<SnakeController> logger)
        {
            _snake = snake;
            _logger = logger;
        }

        [HttpPut]
        public void SetSnakeNextDirection([FromBody]Direction nextDirection)
        {
            _logger.LogInformation($"Next direction: {nextDirection}");
            _snake.NextDirection = nextDirection;
        }
    }
}