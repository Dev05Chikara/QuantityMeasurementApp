using System;
using NUnit.Framework;
using QuantityMeasurementApp;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Unit tests for the Length.Convert static method.
    /// Validates conversions between all supported units and edge cases.
    /// </summary>
    [TestFixture]
    public class LengthConversionTests
    {
        private const double EPSILON = 1e-6;

        // Test: 1 foot = 12 inches
        [Test]
        public void testConversion_FeetToInches()
        {
            double result = Length.Convert(1.0, LengthUnit.FEET, LengthUnit.INCHES);
            Assert.That(result, Is.EqualTo(12.0).Within(EPSILON));
        }

        // Test: 24 inches = 2 feet
        [Test]
        public void testConversion_InchesToFeet()
        {
            double result = Length.Convert(24.0,LengthUnit.INCHES,LengthUnit.FEET);
            Assert.That(result, Is.EqualTo(2.0).Within(EPSILON));
        }

        // Test: 1 yard = 36 inches
        [Test]
        public void testConversion_YardsToInches()
        {
            double result = Length.Convert(1.0, LengthUnit.YARDS, LengthUnit.INCHES);

            Assert.That(result, Is.EqualTo(36.0).Within(EPSILON));
        }

        // Test: 72 inches = 2 yards
        [Test]
        public void testConversion_InchesToYards()
        {
            double result = Length.Convert(72.0, LengthUnit.INCHES, LengthUnit.YARDS);

            Assert.That(result, Is.EqualTo(2.0).Within(EPSILON));
        }

        // Test: 2.54 cm ≈ 1 inch
        [Test]
        public void testConversion_CentimetersToInches()
        {
            double result = Length.Convert(2.54,LengthUnit.CENTIMETERS,LengthUnit.INCHES);

            Assert.That(result, Is.EqualTo(1.0).Within(EPSILON));
        }

        // Test: 6 feet = 2 yards
        [Test]
        public void testConversion_FeetToYards()
        {
            double result = Length.Convert(6.0, LengthUnit.FEET, LengthUnit.YARDS);

            Assert.That(result, Is.EqualTo(2.0).Within(EPSILON));
        }

        // Test: Convert FEET → CENTIMETERS → FEET returns original value
        [Test]
        public void testConversion_RoundTrip_PreservesValue()
        {
            double original = 5.75;

            double converted =
                Length.Convert(original, LengthUnit.FEET, LengthUnit.CENTIMETERS);

            double back =
                Length.Convert(converted, LengthUnit.CENTIMETERS, LengthUnit.FEET);

            Assert.That(back, Is.EqualTo(original).Within(EPSILON));
        }

        // Test: Zero converts to zero in any unit
        [Test]
        public void testConversion_ZeroValue()
        {
            double result = Length.Convert(0.0, LengthUnit.FEET, LengthUnit.INCHES);

            Assert.That(result, Is.EqualTo(0.0).Within(EPSILON));
        }

        // Test: Negative values convert correctly
        [Test]
        public void testConversion_NegativeValue()
        {
            double result = Length.Convert(-1.0, LengthUnit.FEET, LengthUnit.INCHES);

            Assert.That(result, Is.EqualTo(-12.0).Within(EPSILON));
        }

        // Test: Converting to same unit returns same value
        [Test]
        public void testConversion_SameUnit_ReturnsSameValue()
        {
            double result = Length.Convert(5.0, LengthUnit.FEET, LengthUnit.FEET);

            Assert.That(result, Is.EqualTo(5.0).Within(EPSILON));
        }

        // Test: NaN value throws ArgumentException
        [Test]
        public void testConversion_NaN_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                Length.Convert(double.NaN, LengthUnit.FEET, LengthUnit.INCHES));
        }

        // Test: Positive infinity throws ArgumentException
        [Test]
        public void testConversion_PositiveInfinity_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                Length.Convert(double.PositiveInfinity, LengthUnit.FEET, LengthUnit.INCHES));
        }

        // Test: Negative infinity throws ArgumentException
        [Test]
        public void testConversion_NegativeInfinity_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                Length.Convert(double.NegativeInfinity, LengthUnit.FEET, LengthUnit.INCHES));
        }

        // Test: Conversion maintains precision within tolerance
        [Test]
        public void testConversion_PrecisionTolerance()
        {
            double result = Length.Convert(1.0, LengthUnit.CENTIMETERS, LengthUnit.FEET);

            // 1 cm ≈ 0.0328084 feet
            Assert.That(result, Is.EqualTo(0.0328084).Within(EPSILON));
        }

        // Test: Large value conversions work correctly
        [Test]
        public void testConversion_LargeValue()
        {
            double large = 1_000_000.0;

            double result = Length.Convert(large, LengthUnit.FEET, LengthUnit.INCHES);

            Assert.That(result, Is.EqualTo(12_000_000.0).Within(EPSILON));
        }

        // Test: Small value conversions work correctly
        [Test]
        public void testConversion_SmallValue()
        {
            double small = 0.000001;

            double result = Length.Convert(small, LengthUnit.YARDS, LengthUnit.INCHES);

            Assert.That(result, Is.EqualTo(0.000036).Within(EPSILON));
        }
    }
}