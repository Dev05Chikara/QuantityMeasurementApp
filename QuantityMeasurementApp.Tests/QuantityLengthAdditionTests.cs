using NUnit.Framework;
using System;
using QuantityMeasurementApp;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Unit tests for QuantityLength addition across same and different units.
    /// Validates correct conversion and addition logic, edge cases, and error handling.
    /// </summary>
    [TestFixture]
    public class QuantityLengthAdditionTests
    {
        private const double EPSILON = 1e-6;

        // Test: 1 ft + 2 ft = 3 ft
        [Test]
        public void testAddition_SameUnit_FeetPlusFeet()
        {
            var result = new QuantityLength(1.0, LengthUnit.FEET)
                .Add(new QuantityLength(2.0, LengthUnit.FEET));

            Assert.That(result.Value, Is.EqualTo(3.0).Within(EPSILON));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.FEET));
        }

        // Test: 6 in + 6 in = 12 in
        [Test]
        public void testAddition_SameUnit_InchPlusInch()
        {
            var result = new QuantityLength(6.0, LengthUnit.INCHES)
                .Add(new QuantityLength(6.0, LengthUnit.INCHES));

            Assert.That(result.Value, Is.EqualTo(12.0).Within(EPSILON));
        }


        // Test: 1 ft + 12 in = 2 ft
        [Test]
        public void testAddition_CrossUnit_FeetPlusInches()
        {
            var result = new QuantityLength(1.0, LengthUnit.FEET)
                .Add(new QuantityLength(12.0, LengthUnit.INCHES));

            Assert.That(result.Value, Is.EqualTo(2.0).Within(EPSILON));
        }

        // Test: 12 in + 1 ft = 24 in (result in inches)
        [Test]
        public void testAddition_CrossUnit_InchPlusFeet()
        {
            var result = new QuantityLength(12.0, LengthUnit.INCHES)
                .Add(new QuantityLength(1.0, LengthUnit.FEET));

            Assert.That(result.Value, Is.EqualTo(24.0).Within(EPSILON));
        }

        // Test: 1 yd + 3 ft = 2 yd
        [Test]
        public void testAddition_CrossUnit_YardPlusFeet()
        {
            var result = new QuantityLength(1.0, LengthUnit.YARDS)
                .Add(new QuantityLength(3.0, LengthUnit.FEET));

            Assert.That(result.Value, Is.EqualTo(2.0).Within(EPSILON));
        }

        // Test: 2.54 cm + 1 in = 5.08 cm
        [Test]
        public void testAddition_CrossUnit_CentimeterPlusInch()
        {
            var result = new QuantityLength(2.54, LengthUnit.CENTIMETERS)
                .Add(new QuantityLength(1.0, LengthUnit.INCHES));

            Assert.That(result.Value, Is.EqualTo(5.08).Within(EPSILON));
        }

        // Test: Addition is commutative (a + b = b + a)
        [Test]
        public void testAddition_Commutativity()
        {
            var a = new QuantityLength(1.0, LengthUnit.FEET);
            var b = new QuantityLength(12.0, LengthUnit.INCHES);

            var result1 = a.Add(b);
            var result2 = b.Add(a);

            Assert.That(result1.Equals(result2), Is.True);
        }

        // Test: Adding zero returns original value
        [Test]
        public void testAddition_WithZero()
        {
            var result = new QuantityLength(5.0, LengthUnit.FEET)
                .Add(new QuantityLength(0.0, LengthUnit.INCHES));

            Assert.That(result.Value, Is.EqualTo(5.0).Within(EPSILON));
        }

        // Test: Adding negative quantities works correctly
        [Test]
        public void testAddition_NegativeValues()
        {
            var result = new QuantityLength(5.0, LengthUnit.FEET)
                .Add(new QuantityLength(-2.0, LengthUnit.FEET));

            Assert.That(result.Value, Is.EqualTo(3.0).Within(EPSILON));
        }


        // Test: Adding null throws ArgumentException
        [Test]
        public void testAddition_NullSecondOperand()
        {
            var first = new QuantityLength(1.0, LengthUnit.FEET);

            Assert.Throws<ArgumentException>(() =>
                first.Add(null));
        }

        // Test: Large values add correctly
        [Test]
        public void testAddition_LargeValues()
        {
            var result = new QuantityLength(1e6, LengthUnit.FEET)
                .Add(new QuantityLength(1e6, LengthUnit.FEET));

            Assert.That(result.Value, Is.EqualTo(2e6).Within(EPSILON));
        }

        // Test: Small values add correctly within tolerance
        [Test]
        public void testAddition_SmallValues()
        {
            var result = new QuantityLength(0.001, LengthUnit.FEET)
                .Add(new QuantityLength(0.002, LengthUnit.FEET));

            Assert.That(result.Value, Is.EqualTo(0.003).Within(EPSILON));
        }
    }
}