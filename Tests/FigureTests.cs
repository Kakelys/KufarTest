using System.Net.Http.Headers;
using Api.Utils;

namespace Tests
{
    public class FigureTests
    {
        public static IEnumerable<object[]> InvalidPointsData =>
            new List<object[]>
            {
                new object[]
                {
                    new List<Point>(){new (0, 0), new (1, 0), new (1,0), new (1,0)},
                },
                new object[]
                {
                    new List<Point>(){new(0, 0), new(0, 0), new(0,0), new(0,0)},
                },
                new object[]
                {
                    new List<Point>(){new (0, 0), new (0, 1), new (0, 1), new (0, 1)},
                }
            };

        [Fact]
        public void BuildFigure_Returns_SamePoints_When_LessThan3Points()
        {
            // Arrange
            var points = new List<Point>
            {
                new Point(0, 0),
                new Point(1, 1)
            };

            // Act
            var result = Figure.BuildFigure(points);

            // Assert
            Assert.Equal(points, result);
        }

        [Fact]
        public void BuildFigure_Returns_MoreThan2Points_When_CorrectPoints()
        {
            // Arrange
            var points = new List<Point>
            {
                new Point(0, 0),
                new Point(1, 1),
                new Point(2, 0),
                new Point(1, -1)
            };

            // Act
            var result = Figure.BuildFigure(points);

            // Assert
            Assert.True(result.Count > 2);
        }

        [Theory]
        [MemberData(nameof(InvalidPointsData))]
        public void BuildFigure_Returns_LessThan3Points_When_IncorrectPoints(List<Point> points)
        {
            // Act
            var result = Figure.BuildFigure(points);

            // Assert
            Assert.True(result.Count < 3);
        }
    }
}