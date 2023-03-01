namespace Geometry
{
    public record Circle : Shape
    {
        public double Radius { get; init; }

        public static bool TryCreate(double radius, out Circle result)
        {
            result = null;

            if (!ValidateRadius(radius))
            {
                return false;
            }

            result = new Circle() { Radius = radius };
            return true;
        }

        public Circle(double radius)
        {
            ThrowOnInvalidRadius(radius);

            Radius = radius;
        }

        private Circle() { }

        public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }

        private static bool ValidateRadius(double radius)
        {
            return double.IsNormal(radius) && (radius > 0);
        }

        private void ThrowOnInvalidRadius(double radius)
        {
            if (!ValidateRadius(radius))
            {
                throw new ArgumentOutOfRangeException(nameof(radius));
            }
        }
    }
}