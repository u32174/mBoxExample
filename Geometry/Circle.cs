namespace Geometry
{
    public class Circle : Shape, IEquatable<Circle>
    {
        public double Radius { get; }

        public static bool TryCreate(double radius, out Circle result)
        {
            result = null;

            if (!ValidateRadius(radius))
            {
                return false;
            }

            result = new Circle(radius);
            return true;
        }

        public Circle(double radius)
        {
            radius = Math.Round(radius, Calculator.PRECISION);
            ThrowOnInvalidRadius(radius);

            Radius = radius;
        }

        private Circle() { }

        public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }

        public override bool Equals(object other)
        {
            return other is Circle && this.Equals((Circle)other);
        }

        public bool Equals(Circle other)
        {
            if (other == null)
            {
                return false;
            }

            return Radius.EqualWithPrecision(other.Radius);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Radius, Calculator.PRECISION);
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