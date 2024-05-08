namespace Api.Utils
{
    public static class Figure
    {
        public static List<Point> BuildFigure(List<Point> points)
        {
            int n = points.Count;
            if (n < 3)
                return points;

            points = points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            List<Point> upper = new List<Point>();
            List<Point> lower = new List<Point>();

            foreach (Point p in points)
            {
                while (upper.Count >= 2 && VectorProduct(upper[upper.Count - 2], upper[upper.Count - 1], p) <= 0)
                    upper.RemoveAt(upper.Count - 1);

                upper.Add(p);
            }

            for (int i = points.Count - 1; i >= 0; i--)
            {
                Point p = points[i];
                while (lower.Count >= 2 && VectorProduct(lower[lower.Count - 2], lower[lower.Count - 1], p) <= 0)
                    lower.RemoveAt(lower.Count - 1);

                lower.Add(p);
            }

            upper.RemoveAt(upper.Count - 1);
            lower.RemoveAt(lower.Count - 1);

            upper.AddRange(lower);

            return upper;
        }

        public static bool IsPointInside(List<Point> polygon, Point point)
        {
            int n = polygon.Count;
            if (n < 3)
                return false;

            bool rotate = VectorProduct(polygon[0], polygon[1], polygon[2]) < 0;

            for (int i = 0; i < n; i++)
            {
                int j = (i + 1) % n;

                if ((VectorProduct(polygon[i], polygon[j], point) < 0) != rotate)
                    return false;
            }

            return true;
        }

        private static double VectorProduct(Point O, Point A, Point B)
        {
            return (A.X - O.X) * (B.Y - O.Y) - (A.Y - O.Y) * (B.X - O.X);
        }
    }
}