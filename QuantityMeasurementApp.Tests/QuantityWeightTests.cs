using NUnit.Framework;
using QuantityMeasurementApp;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class QuantityWeightTests
    {
        private const double EPSILON = 1e-6;

        [Test]
        public void testEquality_KilogramToGram()
        {
            var a = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            var b = new QuantityWeight(1000.0, WeightUnit.GRAM);

            Assert.That(a.Equals(b), Is.True);
        }

        [Test]
        public void testConversion_KilogramToPound()
        {
            var result = new QuantityWeight(1.0, WeightUnit.KILOGRAM)
                .ConvertTo(WeightUnit.POUND);

            Assert.That(result.Value, Is.EqualTo(2.20462).Within(1e-3));
        }

        [Test]
        public void testAddition_KgPlusGram()
        {
            var result = new QuantityWeight(1.0, WeightUnit.KILOGRAM)
                .Add(new QuantityWeight(1000.0, WeightUnit.GRAM));

            Assert.That(result.Value, Is.EqualTo(2.0).Within(EPSILON));
        }

        [Test]
        public void testAddition_TargetUnit()
        {
            var result = new QuantityWeight(1.0, WeightUnit.KILOGRAM)
                .Add(new QuantityWeight(1000.0, WeightUnit.GRAM), WeightUnit.GRAM);

            Assert.That(result.Value, Is.EqualTo(2000).Within(EPSILON));
        }
    }
}