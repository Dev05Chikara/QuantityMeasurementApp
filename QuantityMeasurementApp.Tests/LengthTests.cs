using NUnit.Framework;
using QuantityMeasurementApp;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Unit tests for the Length class, validating the equality logic, hash code consistency, and handling of invalid inputs.
    /// Tests include comparisons between equivalent lengths in different units (Feet and Inch), as well as checks for non-equivalent lengths, null comparisons, and invalid unit handling.
    /// These tests ensure that the Length class behaves as expected in various scenarios, providing confidence in the correctness of the implementation for use in a quantity measurement application.
    /// </summary>
    
    
    [TestFixture]
    public class LengthTests
    {
        // Test to verify that 1 foot is equal to 12 inches, validating the unit conversion and equality logic in the Length class
        [Test]
        public void testEquality_FeetToInch_Equivalent()
        {
            var l1 = new Length(1.0, LengthUnit.FEET);
            var l2 = new Length(12.0, LengthUnit.INCH);

            Assert.That(l1, Is.EqualTo(l2));
        }

        // Test to verify that different length values are not considered equal, ensuring that the equality logic correctly distinguishes between non-equivalent lengths
        [Test]
        public void testEquality_DifferentValues()
        {
            var l1 = new Length(1.0, LengthUnit.FEET);
            var l2 = new Length(2.0, LengthUnit.FEET);

            Assert.That(l1, Is.Not.EqualTo(l2));
        }

        // Test to verify that the hash codes of equivalent Length instances are consistent, ensuring that the GetHashCode implementation aligns with the equality logic
        [Test]
        public void testHashCode_Consistency()
        {
            var l1 = new Length(1.0, LengthUnit.FEET);
            var l2 = new Length(12.0, LengthUnit.INCH);

            Assert.That(l1.GetHashCode(), Is.EqualTo(l2.GetHashCode()));
        }

        // Test to verify that comparing a Length instance to null returns false, ensuring that the Equals method correctly handles null comparisons
        [Test]
        public void testEquality_Null()
        {
            var l1 = new Length(1.0, LengthUnit.FEET);

            Assert.That(l1.Equals(null), Is.False);
        }

        
        // Test to verify that providing an invalid unit to the Length constructor throws an ArgumentException, ensuring that the class properly validates unit inputs and prevents the creation of Length instances with unsupported units
        [Test]
        public void testInvalidUnit()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Length(1.0, (LengthUnit)999);
            });
        }
    }
}