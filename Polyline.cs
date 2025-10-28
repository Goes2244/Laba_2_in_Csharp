namespace lab2
{
    public class Polyline : Validator
    {
        public List<Point> Points { get; set; }

        public Polyline()
        {
            Points = new List<Point>();
        }

        public Polyline(params Point[] points)
        {
            ValidatePointsArray(points);
            ThrowIfInvalid();

            Points = new List<Point>(points);
        }

        public void AddPoint(Point point)
        {
            ValidateNotNull(point, "Точка ломаной");
            ThrowIfInvalid();

            Points.Add(point);
        }

        public void AddPoints(params Point[] points)
        {
            ValidatePointsArray(points);
            ThrowIfInvalid();

            Points.AddRange(points);
        }

        public double CalculateLength()
        {
            if (Points.Count < 2) return 0;

            double length = 0;
            for (int i = 0; i < Points.Count - 1; i++)
            {
                length += Points[i].DistanceTo(Points[i + 1]);
            }
            return length;
        }

        public override string ToString()
        {
            return $"Линия [{string.Join(", ", Points)}]";
        }
    }
}