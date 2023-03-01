using Geometry;

namespace GeometryTests
{
    [TestClass]
    public class UtilsTest
    {
        [TestMethod]
        public void EquationWithPrecisionDefaultPrecision_ArgumentsAreEqual_ReturnsTrue()
        {
            Assert.IsTrue(Utils.EqualWithPrecision(3.1415d, 3.1415d));
        }

        [TestMethod]
        public void EquationWithPrecisionDefaultPrecision_ArgumentsAreUnequal_ReturnsFalse()
        {
            Assert.IsFalse(Utils.EqualWithPrecision(3.1415d, 3.1416d));
        }

        [TestMethod]
        public void EquationWithArbitraryPrecision_ArgumentsAreUnequal_ReturnsTrue()
        {
            Assert.IsTrue(Utils.EqualWithPrecision(3.1415d, 3.1415d, 0.00001d));
        }

        [TestMethod]
        public void EquationWithArbitraryPrecision_ArgumentsAreUnequal_ReturnsFalse()
        {
            Assert.IsFalse(Utils.EqualWithPrecision(3.1416d, 3.1415d, 0.00001d));
        }

    }
}