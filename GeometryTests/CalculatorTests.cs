using Geometry;

namespace GeometryTests
{
    [TestClass]
    public class CalculatorTests
    {

        [TestMethod]
        public void CalculateTriangleArea_ValidArguments_ReturnsExpectedArea()
        {
            double expectedArea = 6d;
            Triangle t = new Triangle(3d, 4d, 5d);

            double calculatedArea = Calculator.GetArea(t);

            Assert.IsTrue(calculatedArea.EqualWithPrecision(expectedArea));
        }

        [TestMethod]
        public void CalculateCircleArea_ValidArguments_ReturnsExpectedArea()
        {
            Circle circle = new Circle(1.381976597885342d);
            double expectedArea = 6d;

            double calculatedArea = Calculator.GetArea(circle);

            Assert.IsTrue(calculatedArea.EqualWithPrecision(expectedArea));
        }
    }
}