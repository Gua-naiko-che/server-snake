namespace server_snake.Utils
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public static class ArrayExtensions
    {
        public static bool Contains(this IEnumerable<Point> array, Point point) => array.Any(p => p == point);
    }
}