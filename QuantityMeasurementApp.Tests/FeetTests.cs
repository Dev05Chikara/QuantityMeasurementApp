// NUnit test class for testing the Feet class in the QuantityMeasurementApp
using NUnit.Framework;
namespace QuantityMeasurementApp.Tests;

// Test fixture for Feet class
[TestFixture]
// Class to contain unit tests for the Feet class
public class FeetTests
{
    //Test for equality of two Feet objects with the same value
    [Test]
    public void testEquality_SameValue()
    {
        //Arrange
        var value1 = new Feet(1.0);
        var value2 = new Feet(1.0);

        //Assert that the two objects are considered equal based on the Equals method
        Assert.That(value1.Equals(value2), Is.True);
    }

    //Test for equality of two Feet objects with different values
    [Test]
    public void testEquality_DifferentValue()
    {
        //Arrange
        var value1 = new Feet(1.0);
        var value2 = new Feet(2.0);

        //Assert that the two objects are not considered equal based on the Equals method
        Assert.That(value1.Equals(value2), Is.False);
    }

    //Test for equality of a Feet object with null
    [Test]
    public void testEquality_NullComparison()
    {
        //Arrange
        var value = new Feet(1.0);

        //Assert that a Feet object is not equal to null
        Assert.That(value.Equals(null), Is.False);
    }

    //Test for equality of a Feet object with an object of a different type
    [Test]
    public void testEquality_NonNumericInput()
    {
        //Arrange
        var value = new Feet(1.0);
        object nonNumeric = "NotANumber";

        //Assert that a Feet object is not equal to an object of a different type
        Assert.That(value.Equals(nonNumeric), Is.False);
    }

    //Test for equality of a Feet object with itself(Reference equality)
    [Test]
    public void testEquality_SameReference()
    {
        //Arrange
        var value = new Feet(1.0);

        //Assert that a Feet object is equal to itself based on reference equality
        Assert.That(value.Equals(value), Is.True);
    }

    //Test for equality of two Feet objects with values within the tolerance range: Hash code consistency for equal objects
    [Test]
    public void testHashCode_Consistency_ForEqualObjects()
    {
        //Arrange
        var value1 = new Feet(1.0000);
        var value2 = new Feet(1.00005);

        //Assert that the two objects are considered equal based on the Equals method
        Assert.That(value1.Equals(value2), Is.True);
        // Assert that the hash codes of equal objects are the same
        Assert.That(value1.GetHashCode(), Is.EqualTo(value2.GetHashCode()));
    }
}
