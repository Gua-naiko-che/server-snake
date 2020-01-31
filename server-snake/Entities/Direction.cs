namespace server_snake.Entities
{
    public enum Direction
    {
        Up = 1,
        Down = 2,
        Left = 3,
        Rigth = 4,
    }

    public static class DirectionMethods
    {
        public static bool IsTheOppositeTo(this Direction direction1, Direction direction2)
        {
            return direction1 == Direction.Left && direction2 == Direction.Rigth
                   || direction1 == Direction.Rigth && direction2 == Direction.Left
                   || direction1 == Direction.Up && direction2 == Direction.Down
                   || direction1 == Direction.Down && direction2 == Direction.Up;
        }
    }
}