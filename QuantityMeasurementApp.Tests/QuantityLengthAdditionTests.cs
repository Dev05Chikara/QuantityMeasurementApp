using NUnit.Framework;
using QuantityMeasurementApp;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Tests verifying QuantityLength works correctly
    /// after UC8 refactoring.
    /// </summary>
    [TestFixture]
    public class QuantityLengthRefactoredTests
    {
        // Comparison tolerance for floating-point assertions
        private const double EPSILON = 1e-6;

        // Test: 1 foot should equal 12 inches
        [Test]
        public void testQuantityLengthRefactored_Equality()
        {
            var a = new QuantityLength(1.0, LengthUnit.FEET);
            var b = new QuantityLength(12.0, LengthUnit.INCHES);

            Assert.That(a.Equals(b), Is.True);
        }

        // Test: Convert 1 foot to inches
        [Test]
        public void testQuantityLengthRefactored_ConvertTo()
        {
            var result = new QuantityLength(1.0, LengthUnit.FEET)
                .ConvertTo(LengthUnit.INCHES);

            Assert.That(result.Value, Is.EqualTo(12).Within(EPSILON));
        }

        // Test: Add 1 foot and 12 inches, result in feet
        [Test]
        public void testQuantityLengthRefactored_Add()
        {
            var result = new QuantityLength(1.0, LengthUnit.FEET)
                .Add(new QuantityLength(12.0, LengthUnit.INCHES), LengthUnit.FEET);

            Assert.That(result.Value, Is.EqualTo(2.0).Within(EPSILON));
        }

        // Test: Add 1 foot and 12 inches, result in yards
        [Test]
        public void testQuantityLengthRefactored_AddWithTargetUnit()
        {
            var result = new QuantityLength(1.0, LengthUnit.FEET)
                .Add(new QuantityLength(12.0, LengthUnit.INCHES), LengthUnit.YARDS);

            Assert.That(result.Value, Is.EqualTo(0.666666).Within(1e-3));
        }
    }
}