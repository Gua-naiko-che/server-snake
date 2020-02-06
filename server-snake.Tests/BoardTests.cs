namespace server_snake.Tests
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using FluentAssertions;
    using server_snake.Entities;
    using Xunit;

    public class BoardTests
    {
        [Fact]
        public void Board_StartsWithFoodAtRandomLocation()
        {
            const int BOARD_SIZE = 3;
            var allFoods = new List<Point>();
            for (int i = 0; i < 100; i++)
            {
                var board = new Board(BOARD_SIZE, new Snake(Direction.Down, new List<Point>()));

                board.Food.X.Should().BeGreaterOrEqualTo(0);
                board.Food.Y.Should().BeGreaterOrEqualTo(0);
                board.Food.X.Should().BeLessThan(BOARD_SIZE);
                board.Food.Y.Should().BeLessThan(BOARD_SIZE);

                allFoods.Add(board.Food);
            }

            allFoods.Select(f => f.X).Average().Should().BeInRange(0.75D, 1.25D);
            allFoods.Select(f => f.Y).Average().Should().BeInRange(0.75D, 1.25D);
        }

        [Fact]
        public void Board_FoodDoesNotAppearWhereSnakeIs()
        {
            for (int i = 0; i < 100; i++)
            {
                var snake = new Snake(Direction.Rigth, new[] { new Point(0, 0), new Point(1, 0), new Point(1, 1) });

                var board = new Board(2, snake);

                board.Food.X.Should().Be(0);
                board.Food.Y.Should().Be(1);
            }
        }
    }
}