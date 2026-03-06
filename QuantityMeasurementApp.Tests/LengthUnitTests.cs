using NUnit.Framework;
using QuantityMeasurementApp;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Tests for LengthUnit standalone enum conversion behavior.
    /// </summary>
    [TestFixture]
    public class LengthUnitTests
    {
        // Comparison tolerance for floating-point assertions
        private const double EPSILON = 1e-6;

        // Test: Convert 1 foot to inches
        [Test]
        public void testConvertToBaseUnit_InchesToFeet()
        {
            double result = LengthUnit.INCHES.ConvertToBaseUnit(12);

            Assert.That(result, Is.EqualTo(1).Within(EPSILON));
        }

        // Test: Convert 1 yard to feet
        [Test]
        public void testConvertToBaseUnit_YardsToFeet()
        {
            double result = LengthUnit.YARDS.ConvertToBaseUnit(1);

            Assert.That(result, Is.EqualTo(3).Within(EPSILON));
        }

        // Test: Convert 30.48 centimeters to feet
        [Test]
        public void testConvertFromBaseUnit_FeetToInches()
        {
            double result = LengthUnit.INCHES.ConvertFromBaseUnit(1);

            Assert.That(result, Is.EqualTo(12).Within(EPSILON));
        }

        // Test: Convert 1 foot to centimeters
        [Test]
        public void testConvertFromBaseUnit_FeetToCentimeters()
        {
            double result = LengthUnit.CENTIMETERS.ConvertFromBaseUnit(1);

            Assert.That(result, Is.EqualTo(30.48).Within(EPSILON));
        }
    }
}