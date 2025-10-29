using System.Collections.Generic;

namespace lab2
{
    public class Polyline
    {
        public List<Point> Points { get; set; }

        public Polyline()
        {
            Points = new List<Point>();
        }

        public Polyline(params Point[] points)
        {
            Validator.Validate(v => v.ValidatePointsArray(points));
            Points = new List<Point>(points);
        }

        public void AddPoint(Point point)
        {
            Validator.Validate(v => v.ValidateNotNull(point, "Точка ломаной"));
            Points.Add(point);
        }

        public void AddPoints(params Point[] points)
        {
            Validator.Validate(v => v.ValidatePointsArray(points));
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
