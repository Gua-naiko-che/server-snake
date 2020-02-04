namespace server_snake
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using server_snake.Entities;
    using server_snake.Hubs;

    public class GameLoop : BackgroundService
    {
        private readonly SnakeHub _snakeHub;
        private readonly Snake _snake;

        public GameLoop(SnakeHub snakeHub, Snake snake)
        {
            _snakeHub = snakeHub;
            _snake = snake;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _snake.Move();
                await _snakeHub.SendMessage(stoppingToken);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}