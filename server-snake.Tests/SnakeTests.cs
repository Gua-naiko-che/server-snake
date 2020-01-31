namespace server_snake.Tests
{
    using System.Drawing;
    using FluentAssertions;
    using server_snake.Entities;
    using Xunit;

    public class SnakeTests
    {
        [Fact]
        public void Snake_Move_DoesNotHaveNextDirection_KeepsMovingInTheSameDirection()
        {
            const Direction DIRECTION = Direction.Rigth;
            var snake = new Snake(DIRECTION, new[] { new Point(0, 1) });

            snake.Move();

            snake.CurrentDirection.Should().Be(DIRECTION);
        }

        [Fact]
        public void Snake_Move_HasNextDirection_NextDirectionIsResetAndDirectionIsChanged()
        {
            const Direction NEXT_DIRECTION = Direction.Down;
            var snake = new Snake(Direction.Rigth, new[] { new Point(0, 1) }) { NextDirection = NEXT_DIRECTION };

            snake.Move();

            snake.NextDirection.Should().BeNull();
            snake.CurrentDirection.Should().Be(NEXT_DIRECTION);
        }

        [Fact]
        public void Snake_Move_MovingRight_MoveRight()
        {
            var snake = new Snake(Direction.Rigth, new[] { new Point(0, 1), new Point(1, 1) });

            snake.Move();

            snake.Body.Should().BeEquivalentTo(new[] { new Point(1, 1), new Point(2, 1) });
        }

        [Fact]
        public void Snake_Move_MovingDownAndGoRight_MoveRight()
        {
            var snake = new Snake(Direction.Down, new[] { new Point(0, 1), new Point(1, 1) }) { NextDirection = Direction.Rigth };

            snake.Move();

            snake.Body.Should().BeEquivalentTo(new[] { new Point(1, 1), new Point(2, 1) });
        }

        [Fact]
        public void Snake_Move_MovingLeft_MoveLeft()
        {
            var snake = new Snake(Direction.Left, new[] { new Point(1, 1), new Point(1, 2) });

            snake.Move();

            snake.Body.Should().BeEquivalentTo(new[] { new Point(1, 2), new Point(0, 2) });
        }

        [Fact]
        public void Snake_Move_MovingDownAndGoLeft_MoveLeft()
        {
            var snake = new Snake(Direction.Down, new[] { new Point(1, 1), new Point(1, 2) }) { NextDirection = Direction.Left };

            snake.Move();

            snake.Body.Should().BeEquivalentTo(new[] { new Point(1, 2), new Point(0, 2) });
        }

        [Fact]
        public void Snake_Move_MovingDown_MoveDown()
        {
            var snake = new Snake(Direction.Down, new[] { new Point(1, 1), new Point(1, 2) });

            snake.Move();

            snake.Body.Should().BeEquivalentTo(new[] { new Point(1, 2), new Point(1, 3) });
        }

        [Fact]
        public void Snake_Move_MovingRightAndGoDown_MoveDown()
        {
            var snake = new Snake(Direction.Rigth, new[] { new Point(1, 1), new Point(2, 1) }) { NextDirection = Direction.Down };

            snake.Move();

            snake.Body.Should().BeEquivalentTo(new[] { new Point(2, 1), new Point(2, 2) });
        }

        [Fact]
        public void Snake_Move_MovingUp_MoveUp()
        {
            var snake = new Snake(Direction.Up, new[] { new Point(1, 1), new Point(1, 2) });

            snake.Move();

            snake.Body.Should().BeEquivalentTo(new[] { new Point(1, 2), new Point(1, 1) });
        }

        [Fact]
        public void Snake_Move_MovingRightAndGoUp_MoveUp()
        {
            var snake = new Snake(Direction.Rigth, new[] { new Point(1, 1), new Point(2, 1) }) { NextDirection = Direction.Up };

            snake.Move();

            snake.Body.Should().BeEquivalentTo(new[] { new Point(2, 1), new Point(2, 0) });
        }

        [Fact]
        public void Snake_Move_MovingRightAndTryToGoLeft_KeepsMovingRight()
        {
            var snake = new Snake(Direction.Rigth, new[] { new Point(1, 1), new Point(2, 1) }) { NextDirection = Direction.Left };

            snake.Move();

            snake.Body.Should().BeEquivalentTo(new[] { new Point(2, 1), new Point(3, 1) });
            snake.CurrentDirection.Should().Be(Direction.Rigth);
        }

        [Fact]
        public void Snake_Move_MovingLeftAndTryToGoRight_KeepsMovingLeft()
        {
            var snake = new Snake(Direction.Left, new[] { new Point(1, 1), new Point(2, 1) }) { NextDirection = Direction.Rigth };

            snake.Move();

            snake.Body.Should().BeEquivalentTo(new[] { new Point(2, 1), new Point(1, 1) });
            snake.CurrentDirection.Should().Be(Direction.Left);
        }

        [Fact]
        public void Snake_Move_MovingUpAndTryToGoDown_KeepsMovingUp()
        {
            var snake = new Snake(Direction.Up, new[] { new Point(1, 1), new Point(2, 1) }) { NextDirection = Direction.Down };

            snake.Move();

            snake.Body.Should().BeEquivalentTo(new[] { new Point(2, 1), new Point(2, 0) });
            snake.CurrentDirection.Should().Be(Direction.Up);
        }

        [Fact]
        public void Snake_Move_MovingDownAndTryToGoUp_KeepsMovingDown()
        {
            var snake = new Snake(Direction.Down, new[] { new Point(1, 1), new Point(2, 1) }) { NextDirection = Direction.Up };

            snake.Move();

            snake.Body.Should().BeEquivalentTo(new[] { new Point(2, 1), new Point(2, 2) });
            snake.CurrentDirection.Should().Be(Direction.Down);
        }
    }
}