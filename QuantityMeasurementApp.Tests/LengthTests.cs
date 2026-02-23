using NUnit.Framework;
using QuantityMeasurementApp;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Unit tests for the Length class to verify equality comparison across different units of length measurement.
    /// These tests cover various scenarios, including comparisons between yards, feet, inches, and centimeters, as well as edge cases such as null comparisons and reference equality.
    /// The tests also ensure that the GetHashCode method produces consistent results for equivalent Length objects, which is crucial for the correct behavior of hash-based collections.
    /// </summary>
    [TestFixture]
    public class LengthTests
    {
        // Test cases for equality comparison between Length objects with different units and values.
        [Test]
        public void testEquality_YardToYard_SameValue()
        {
            var l1 = new Length(1.0, LengthUnit.YARDS);
            var l2 = new Length(1.0, LengthUnit.YARDS);

            Assert.That(l1, Is.EqualTo(l2));
        }

        // Test cases for equality comparison between Length objects with different units but equivalent values.
        [Test]
        public void testEquality_YardToFeet_EquivalentValue()
        {
            var yard = new Length(1.0, LengthUnit.YARDS);
            var feet = new Length(3.0, LengthUnit.FEET);

            Assert.That(yard, Is.EqualTo(feet));
        }

        // Test cases for equality comparison between Length objects with different units but equivalent values.
        [Test]
        public void testEquality_YardToInches_EquivalentValue()
        {
            var yard = new Length(1.0, LengthUnit.YARDS);
            var inches = new Length(36.0, LengthUnit.INCHES);

            Assert.That(yard, Is.EqualTo(inches));
        }

        // Test cases for equality comparison between Length objects with different units and non-equivalent values.
        [Test]
        public void testEquality_centimetersToInches_EquivalentValue()
        {
            var cm = new Length(1.0, LengthUnit.CENTIMETERS);
            var inches = new Length(0.393701, LengthUnit.INCHES);

            Assert.That(cm, Is.EqualTo(inches));
        }

        // Test cases for equality comparison between Length objects with different units and non-equivalent values.
        [Test]
        public void testEquality_centimetersToFeet_NonEquivalentValue()
        {
            var cm = new Length(1.0, LengthUnit.CENTIMETERS);
            var feet = new Length(1.0, LengthUnit.FEET);

            Assert.That(cm, Is.Not.EqualTo(feet));
        }

        // Test cases for transitive property of equality across multiple units of length measurement.
        [Test]
        public void testEquality_MultiUnit_TransitiveProperty()
        {
            var yard = new Length(1.0, LengthUnit.YARDS);
            var feet = new Length(3.0, LengthUnit.FEET);
            var inches = new Length(36.0, LengthUnit.INCHES);

            Assert.That(yard, Is.EqualTo(feet));
            Assert.That(feet, Is.EqualTo(inches));
            Assert.That(yard, Is.EqualTo(inches));
        }

        // Test cases for equality comparison when both Length objects are null.
        [Test]
        public void testEquality_SameReference()
        {
            var yard = new Length(2.0, LengthUnit.YARDS);

            Assert.That(yard.Equals(yard), Is.True);
        }

        // Test cases for equality comparison when one of the Length objects is null.
        [Test]
        public void testEquality_NullComparison()
        {
            var yard = new Length(1.0, LengthUnit.YARDS);

            Assert.That(yard.Equals(null), Is.False);
        }

        
        // Test cases for consistency of GetHashCode method for equivalent Length objects.
        [Test]
        public void testHashCode_Consistency()
        {
            var yard = new Length(1.0, LengthUnit.YARDS);
            var inches = new Length(36.0, LengthUnit.INCHES);

            Assert.That(yard.GetHashCode(),
                        Is.EqualTo(inches.GetHashCode()));
        }
    }
}