namespace lab2
{
    public class Polyline
    {
        private List<Point> points;

        public IReadOnlyList<Point> Points => points.AsReadOnly();

        public Polyline()
        {
            points = new List<Point>();
        }

        public Polyline(params Point[] pointsArray)
        {
            Validator.Validate(v => v.ValidatePointsArray(pointsArray));
            points = new List<Point>(pointsArray);
        }

        public Polyline(IEnumerable<Point> pointsCollection)
        {
            var pointsArray = pointsCollection?.ToArray();
            Validator.Validate(v => v.ValidatePointsArray(pointsArray));
            points = new List<Point>(pointsArray);
        }

        public void AddPoint(Point point)
        {
            Validator.Validate(v => v.ValidateNotNull(point, "Точка"));
            points.Add(point);
        }

        public void AddPoints(params Point[] newPoints)
        {
            Validator.Validate(v => v.ValidateCollection(newPoints, "Новые точки", requireNotEmpty: true));
            points.AddRange(newPoints);
        }

        public void AddPoints(IEnumerable<Point> newPoints)
        {
            var pointsArray = newPoints?.ToArray();
            Validator.Validate(v => v.ValidateCollection(pointsArray, "Новые точки", requireNotEmpty: true));
            points.AddRange(pointsArray);
        }

        public double GetLength()
        {
            if (points.Count < 2)
            {
                return 0;
            }

            double length = 0;
            for (int i = 0; i < points.Count - 1; i++)
            {
                double dx = points[i + 1].X - points[i].X;
                double dy = points[i + 1].Y - points[i].Y;
                length += Math.Sqrt(dx * dx + dy * dy);
            }

            {
                return length;
            }
        }

        public void Shift(double deltaX, double deltaY)
        {
            Validator.Validate(v => v.ValidateShift(deltaX, deltaY));

            foreach (var point in points)
            {
                point.X += deltaX;
                point.Y += deltaY;
            }
        }

        public override string ToString()
        {
            var pointsStrings = points.Select(p => p.ToString());
            {
                return $"Линия [{string.Join(",", pointsStrings)}]";
            }
        }
    }
}