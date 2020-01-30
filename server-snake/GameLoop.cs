namespace server_snake
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using server_snake.Hubs;

    public class GameLoop : BackgroundService
    {
        private readonly SnakeHub _snakeHub;

        public GameLoop(SnakeHub snakeHub)
        {
            _snakeHub = snakeHub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _snakeHub.SendMessage(stoppingToken);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}