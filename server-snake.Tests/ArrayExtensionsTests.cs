namespace server_snake.Tests
{
    using System.Drawing;
    using FluentAssertions;
    using server_snake.Utils;
    using Xunit;

    public class ArrayExtensionsTests
    {
        [Fact]
        public void ArrayExtensions_Contains_PointInArray_ReturnTrue()
        {
            bool result = new[] { new Point(2, 2) }.Contains(new Point(2, 2));
            result.Should().BeTrue();
        }

        [Fact]
        public void ArrayExtensions_Contains_PointNotInArray_ReturnFalse()
        {
            bool result = new[] { new Point(1, 2) }.Contains(new Point(2, 2));
            result.Should().BeFalse();
        }
    }
}