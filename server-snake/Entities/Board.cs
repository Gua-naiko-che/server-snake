namespace server_snake.Entities
{
    using System;
    using System.Drawing;
    using server_snake.Utils;

    public class Board
    {
        private readonly int _size;
        private readonly Snake _snake;
        private readonly Random _random;
        public Point Food { get; set; }

        public Board(int size, Snake snake)
        {
            _size = size;
            _snake = snake;
            _random = new Random();
            Food = GetRandomFood();
        }

        private Point GetRandomFood()
        {
            var randomFood = new Point(_random.Next(_size), _random.Next(_size));

            return _snake.Body.Contains(randomFood) ? GetRandomFood() : randomFood;
        }
    }
}