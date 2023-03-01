using Geometry;

namespace GeometryTests
{
    [TestClass]
    public class TriangleTests
    {
        [TestMethod]
        public void TryCreateTriangleMethod_ProvidingCorrectArguments_ReturnsTrue()
        {
            bool isCreated = Triangle.TryCreate(3d, 4d, 5d, out _);
            Assert.IsTrue(isCreated);
        }

        [TestMethod]
        public void TryCreateTriangleMethod_ProvidingCorrectArguments_ReturnsNewObject()
        {
            Triangle result;
            Triangle.TryCreate(3d, 4d, 5d, out result);
            Console.WriteLine(result);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TryCreateTriangleMethod_ProvidingArgumentsForInvalidTriangle_ReturnsFalse()
        {
            bool isCreated = Triangle.TryCreate(1d, 2d, 3d, out _);
            Assert.IsFalse(isCreated);
        }

        [TestMethod]
        public void TryCreateTriangleMethod_ProvidingArgumentsForInvalidTriangle_ReturnsNull()
        {
            Triangle result;
            Triangle.TryCreate(1d, 2d, 3d, out result);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TriangleCreationValidation_ProvidingValidSidesToConstructor_CreatesTriangleWithoutExceptions()
        {
            Triangle _ = new Triangle(7d, 10d, 5d);
        }


        [TestMethod]
        public void TriangleCreationValidation_ProvidingNegativeSidesToConstructor_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                {
                    Triangle triangle = new Triangle(-1d, 2d, 3d);
                });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                {
                    Triangle triangle = new Triangle(1d, -2d, 3d);
                });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                {
                    Triangle triangle = new Triangle(1d, 2d, -3d);
                });
        }

        [TestMethod]
        public void TriangleCreationValidation_ProvidingInvalidTriangle_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                {
                    Triangle _ = new Triangle(1d,2d,3d);
                });
        }

        [TestMethod]
        public void TriangleCreationValidation_ProvidingNaNSidesToConstructor_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                Triangle triangle = new Triangle(double.NaN, 2d, 3d);
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                Triangle triangle = new Triangle(1d, double.NaN, 3d);
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                Triangle triangle = new Triangle(1d, 2d, double.NaN);
            });
        }
        
        [TestMethod]
        public void TriangleAreaCalc_TriangleWithCorrectDimensions_CalculatesCorrectArea()
        {
            Triangle triangle = new Triangle(3d, 4d, 5d);
            double expectedArea = 6d;
            double calculatedArea = triangle.GetArea();

            Assert.IsTrue(calculatedArea.EqualWithPrecision(expectedArea));
        }

        [TestMethod]
        public void CheckingForRightAngledness_TriangleIsRightAngled_ReturnsTrue()
        {
            var triangle = new Triangle(3d, 4d, 5d);
            Assert.IsTrue(triangle.CheckIfRightAngled());
        }

        [TestMethod]
        public void CheckingForRightAngledness_TriangleIsNotRightAngled_ReturnsFalse()
        {
            var triangle = new Triangle(7d, 10d, 5d);
            Assert.IsFalse(triangle.CheckIfRightAngled());
        }
    }
}