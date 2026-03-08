using System;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Represents a quantity of weight with a numeric value and unit.
    /// This class handles equality comparison, conversion, and arithmetic
    /// operations for weight measurements.
    /// </summary>
    public class QuantityWeight
    {
        private const double EPSILON = 1e-6;

        /// <summary>
        /// Numeric value of the weight.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Unit of the weight measurement.
        /// </summary>
        public WeightUnit Unit { get; }

        /// <summary>
        /// Creates a new QuantityWeight instance.
        /// </summary>
        /// <param name="value">Weight value</param>
        /// <param name="unit">Weight unit</param>
        public QuantityWeight(double value, WeightUnit unit)
        {
            if (unit == null)
                throw new ArgumentException("Unit cannot be null");

            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value");

            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Converts the current weight to the specified target unit.
        /// </summary>
        /// <param name="targetUnit">Target weight unit</param>
        /// <returns>New QuantityWeight in target unit</returns>
        public QuantityWeight ConvertTo(WeightUnit targetUnit)
        {
            double baseValue = Unit.ConvertToBaseUnit(Value);
            double converted = targetUnit.ConvertFromBaseUnit(baseValue);

            return new QuantityWeight(converted, targetUnit);
        }

        /// <summary>
        /// Adds another QuantityWeight and returns the result
        /// in the current object's unit.
        /// </summary>
        /// <param name="other">Other weight</param>
        /// <returns>Resulting QuantityWeight</returns>
        public QuantityWeight Add(QuantityWeight other)
        {
            return Add(other, this.Unit);
        }

        /// <summary>
        /// Adds another QuantityWeight and returns result
        /// in a specified target unit.
        /// </summary>
        /// <param name="other">Other weight</param>
        /// <param name="targetUnit">Target unit</param>
        /// <returns>Sum of both weights</returns>
        public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit)
        {
            if (other == null)
                throw new ArgumentException("Other quantity cannot be null");

            double base1 = Unit.ConvertToBaseUnit(Value);
            double base2 = other.Unit.ConvertToBaseUnit(other.Value);

            double sumBase = base1 + base2;

            double result = targetUnit.ConvertFromBaseUnit(sumBase);

            return new QuantityWeight(result, targetUnit);
        }

        /// <summary>
        /// Determines equality between two weight quantities.
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True if equal</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(QuantityWeight))
                return false;

            var other = (QuantityWeight)obj;

            double base1 = Unit.ConvertToBaseUnit(Value);
            double base2 = other.Unit.ConvertToBaseUnit(other.Value);

            return Math.Abs(base1 - base2) < EPSILON;
        }

        /// <summary>
        /// Generates hash code using normalized base unit value.
        /// </summary>
        public override int GetHashCode()
        {
            double baseValue = Unit.ConvertToBaseUnit(Value);
            return Math.Round(baseValue / EPSILON).GetHashCode();
        }

        /// <summary>
        /// Returns readable string representation.
        /// </summary>
        public override string ToString()
        {
            return $"{Value} {Unit}";
        }
    }
}