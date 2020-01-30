namespace server_snake.Entities
{
    using System.Collections.Generic;
    using System.Drawing;

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
    }
}