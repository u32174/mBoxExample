using Geometry;

namespace GeometryTests
{

    [TestClass]
    public class CircleTests
    {
        [TestMethod]
        public void CompareTwoCircleHashCodes_CirclesHaveDifferentRadius_HashCodesAreDifferent()
        {
            Circle a = new Circle(1d);
            Circle b = new Circle(1.1d);

            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod]
        public void CompareTwoCircleHashCodes_CirclesHaveSameRadius_HashCodesAreEqual()
        {
            Circle a = new Circle(1d);
            Circle b = new Circle(1d);

            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod]
        public void CompareTwoCircleHashCodes_CirclesHaveDifferenceLessThanPrecision_HashCodesAreEqual()
        {
            Circle a = new Circle(1d);
            Circle b = new Circle(1.000000000001d);

            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod]
        public void TestEquals_OtherIsNull_ReturnsFalse() 
        {
            Circle c = new(123d);
            Assert.IsFalse(c.Equals(null));
        }

        [TestMethod]
        public void TestEquals_OtherIsDifferent_ReturnsFalse() 
        {
            Circle smaller = new(1d);
            object bigger = new Circle(4d);

            Assert.IsFalse(smaller.Equals(bigger));
        }

        [TestMethod]
        public void TestEquals_otherIsEqual_returnsTrue() 
        {
            Circle left = new(1d);
            object right = new Circle(1d);

            Assert.IsTrue(left.Equals(right));
        }

        [TestMethod]
        public void TestEquatable_otherIsEqual_returnsTrue() 
        {
            IEquatable<Circle> left = new Circle(1d);
            Circle right = new(1d);

            Assert.IsTrue(left.Equals(right));
        }

        [TestMethod]
        public void TryCircleCreateMethod_ValidRadius_ReturnsTrue() 
        {
            Assert.IsTrue(Circle.TryCreate(1d, out _));
        }

        [TestMethod]
        public void TryCircleCreateMethod_ValidRadius_ReturnsNewCircle() 
        {
            Circle result = null;

            Circle.TryCreate(1d, out result);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TryCircleCreateMethod_NegativeRadius_ReturnsFalse() 
        {
            Assert.IsFalse(Circle.TryCreate(-1d, out _));
        }

        [TestMethod]
        public void TryCircleCreateMethod_InvalidRadius_ReturnsNull() 
        {
            Circle result = null;

            Circle.TryCreate(-1d, out result);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CircleCreationValidation_ProvidesPositiveRadius_CreatesCircleWithoutThrowingExceptions()
        {
            var circle = new Circle(1.23d);
        }

        [TestMethod]
        public void CircleCreationValidation_ProvidesNegativeRadius_ConstructorThrowsArgumentException()
        {
            Action createCircleDelegate = () =>
                {
                    new Circle(-7d);
                };

            Assert.ThrowsException<ArgumentOutOfRangeException>(createCircleDelegate);
        }

        [TestMethod]
        public void CircleCreationValidation_ProvidesNaNRadius_ConstructorThrowsArgumentException()
        {
            Action createCircleDelegate = () =>
                {
                    new Circle(double.NaN);
                };

            Assert.ThrowsException<ArgumentOutOfRangeException>(createCircleDelegate);
        }

        [TestMethod]
        public void CircleCreationValidation_ProvidesZeroRadius_ConstructorThrowsArgumentException()
        {
            Action createCircleDelegate = () =>
                {
                    new Circle(0d);
                };

            Assert.ThrowsException<ArgumentOutOfRangeException>(createCircleDelegate);
        }

        [TestMethod]
        public void CircleAreaCalc_CorrectValues_ReturnsCorrectArea()
        {
            Circle circle = new Circle(1.381976597885342d);
            double expectedArea = 6d;

            double calculatedArea = circle.GetArea();

            Assert.IsTrue(calculatedArea.EqualWithPrecision(expectedArea));
        }
    }
}