using NUnit.Framework;
using System;
using QuantityMeasurementApp;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Tests for `QuantityLength.Add(..., targetUnit)` covering explicit target-unit addition,
    /// commutativity and edge cases.
    /// </summary>
    /// <param name="EPSILON">Comparison tolerance used in assertions.</param>
    [TestFixture]
    public class QuantityLengthExplicitTargetTests
    {
        private const double EPSILON = 1e-6;

        // Test: 1 ft + 12 in => result in FEET
        [Test]
        public void testAddition_ExplicitTargetUnit_Feet()
        {
            var result = new QuantityLength(1.0, LengthUnit.FEET)
                .Add(new QuantityLength(12.0, LengthUnit.INCHES), LengthUnit.FEET);

            Assert.That(result.Value, Is.EqualTo(2.0).Within(EPSILON));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.FEET));
        }

        // Test: 1 ft + 12 in => result in INCHES
        [Test]
        public void testAddition_ExplicitTargetUnit_Inches()
        {
            var result = new QuantityLength(1.0, LengthUnit.FEET)
                .Add(new QuantityLength(12.0, LengthUnit.INCHES), LengthUnit.INCHES);

            Assert.That(result.Value, Is.EqualTo(24.0).Within(EPSILON));
        }

        // Test: 1 ft + 12 in => result in YARDS
        [Test]
        public void testAddition_ExplicitTargetUnit_Yards()
        {
            var result = new QuantityLength(1.0, LengthUnit.FEET)
                .Add(new QuantityLength(12.0, LengthUnit.INCHES), LengthUnit.YARDS);

            Assert.That(result.Value, Is.EqualTo(0.666666).Within(1e-3));
        }

        // Test: 1 in + 1 in => result in CENTIMETERS
        [Test]
        public void testAddition_ExplicitTargetUnit_Centimeters()
        {
            var result = new QuantityLength(1.0, LengthUnit.INCHES)
                .Add(new QuantityLength(1.0, LengthUnit.INCHES), LengthUnit.CENTIMETERS);

            Assert.That(result.Value, Is.EqualTo(5.08).Within(EPSILON));
        }

        // Test: Addition is commutative when result converted to same target unit
        [Test]
        public void testAddition_Commutativity_WithTargetUnit()
        {
            var a = new QuantityLength(1.0, LengthUnit.FEET);
            var b = new QuantityLength(12.0, LengthUnit.INCHES);

            var r1 = a.Add(b, LengthUnit.YARDS);
            var r2 = b.Add(a, LengthUnit.YARDS);

            Assert.That(r1.Value, Is.EqualTo(r2.Value).Within(EPSILON));
        }

        // Test: Adding zero affects result as expected (explicit target)
        [Test]
        public void testAddition_WithZero_ExplicitTarget()
        {
            var result = new QuantityLength(5.0, LengthUnit.FEET)
                .Add(new QuantityLength(0.0, LengthUnit.INCHES), LengthUnit.YARDS);

            Assert.That(result.Value, Is.EqualTo(1.666666).Within(1e-3));
        }

        // Test: Negative values are handled correctly (explicit target)
        [Test]
        public void testAddition_NegativeValues_ExplicitTarget()
        {
            var result = new QuantityLength(5.0, LengthUnit.FEET)
                .Add(new QuantityLength(-2.0, LengthUnit.FEET), LengthUnit.INCHES);

            Assert.That(result.Value, Is.EqualTo(36.0).Within(EPSILON));
        }

        // Test: Invalid target unit throws ArgumentException
        [Test]
        public void testAddition_NullTargetUnit_Invalid()
        {
            var a = new QuantityLength(1.0, LengthUnit.FEET);
            var b = new QuantityLength(12.0, LengthUnit.INCHES);

            Assert.Throws<ArgumentException>(() =>
                a.Add(b, (LengthUnit)999));
        }

        // Test: Large-to-small scale addition returns expected small-unit value
        [Test]
        public void testAddition_LargeToSmallScale()
        {
            var result = new QuantityLength(1000.0, LengthUnit.FEET)
                .Add(new QuantityLength(500.0, LengthUnit.FEET), LengthUnit.INCHES);

            Assert.That(result.Value, Is.EqualTo(18000.0).Within(EPSILON));
        }

        // Test: Small-to-large scale addition returns expected large-unit value
        [Test]
        public void testAddition_SmallToLargeScale()
        {
            var result = new QuantityLength(12.0, LengthUnit.INCHES)
                .Add(new QuantityLength(12.0, LengthUnit.INCHES), LengthUnit.YARDS);

            Assert.That(result.Value, Is.EqualTo(0.666666).Within(1e-3));
        }
    }
}