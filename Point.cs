using System;

namespace lab2
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            Validator.Validate(v => v
                .ValidateCoordinate(x, "Координата X")
                .ValidateCoordinate(y, "Координата Y"));

            X = x;
            Y = y;
        }

        public double DistanceTo(Point other)
        {
            Validator.Validate(v => v.ValidateNotNull(other, "Точка для расчета расстояния"));
            return Math.Sqrt(Math.Pow(other.X - X, 2) + Math.Pow(other.Y - Y, 2));
        }

        public override string ToString()
        {
            return $"({X};{Y})";
        }
    }
}
