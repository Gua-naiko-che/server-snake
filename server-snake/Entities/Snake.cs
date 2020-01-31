namespace server_snake.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class Snake
    {
        public Direction CurrentDirection { get; set; }
        public Direction? NextDirection { get; set; }
        public IEnumerable<Point> Body { get; set; }

        public Snake(Direction direction, IEnumerable<Point> body)
        {
            CurrentDirection = direction;
            Body = body;
        }

        public void Move()
        {
            CurrentDirection = GetNewDirection();
            Body = GetNewBody();
            NextDirection = null;
        }

        private Direction GetNewDirection()
        {
            return NextDirection != null && !NextDirection.Value.IsTheOppositeTo(CurrentDirection)
                ? NextDirection.Value
                : CurrentDirection;
        }

        private IEnumerable<Point> GetNewBody()
        {
            List<Point> newBody = Body.Skip(1).ToList();
            Point? newHead = GetNewHead();
            newBody.Add(newHead.Value);

            return newBody;
        }

        private Point GetNewHead()
        {
            Point oldHead = Body.Last();

            return CurrentDirection switch
            {
                Direction.Rigth => new Point(oldHead.X + 1, oldHead.Y),
                Direction.Left => new Point(oldHead.X - 1, oldHead.Y),
                Direction.Down => new Point(oldHead.X, oldHead.Y + 1),
                Direction.Up => new Point(oldHead.X, oldHead.Y - 1),
                _ => throw new InvalidOperationException($"CurrentDirection has the unhandled value {CurrentDirection.ToString()}")
            };
        }
    }
}