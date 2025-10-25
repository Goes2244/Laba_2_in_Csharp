namespace lab2
{
    public class Point
    {
        private double _x;
        private double _y;

        public double X 
        { 
            get => _x; 
            set
            {
                Validator.Validate(v => v.ValidateCoordinate(value, "X"));
                _x = value;
            }
        }

        public double Y 
        { 
            get => _y; 
            set
            {
                Validator.Validate(v => v.ValidateCoordinate(value, "Y"));
                _y = value;
            }
        }

        public Point() : this(0, 0) { }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{{{X};{Y}}}";
        }
    }
}