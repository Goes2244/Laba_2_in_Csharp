namespace lab2
{
    public class Point : Validator
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            ValidateCoordinate(x, "Координата X")
                .ValidateCoordinate(y, "Координата Y");
            ThrowIfInvalid();

            X = x;
            Y = y;
        }

        public double DistanceTo(Point other)
        {
            ValidateNotNull(other, "Точка для расчета расстояния");
            ThrowIfInvalid();

            return Math.Sqrt(Math.Pow(other.X - X, 2) + Math.Pow(other.Y - Y, 2));
        }

        public override string ToString()
        {
            return $"({X};{Y})";
        }
    }
}