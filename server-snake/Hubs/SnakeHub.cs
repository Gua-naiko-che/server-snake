namespace server_snake.Hubs
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;
    using server_snake.Entities;

    public class SnakeHub : Hub
    {
        private readonly Snake _snake;

        public SnakeHub(Snake snake)
        {
            _snake = snake;
        }

        public async Task SendMessage(CancellationToken stoppingToken)
        {
            if (Clients == null) return;
            await Clients.All.SendAsync("GameState", _snake.Body, stoppingToken);
        }
    }
}