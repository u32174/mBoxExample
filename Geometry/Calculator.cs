namespace Geometry
{
    public static class Calculator
    {
        public static int PRECISION = 6;

        public static double GetArea(Shape shape)
        {
            return shape.GetArea();
        }
    }
}