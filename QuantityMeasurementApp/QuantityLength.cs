using System;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Represents a quantity of length with a numeric value and unit.
    /// 
    /// This class delegates unit conversions to LengthUnit,
    /// focusing only on comparison and arithmetic logic.
    /// </summary>
    public class QuantityLength
    {
        private const double EPSILON = 1e-6;

        /// <summary>
        /// Numeric value of the length.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Unit of the length.
        /// </summary>
        public LengthUnit Unit { get; }

        /// <summary>
        /// Constructor for QuantityLength.
        /// </summary>
        /// <param name="value">Length value</param>
        /// <param name="unit">Unit of measurement</param>
        public QuantityLength(double value, LengthUnit unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value.");

            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Converts this quantity to a target unit.
        /// </summary>
        /// <param name="targetUnit">Target unit</param>
        /// <returns>Converted QuantityLength</returns>
        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            double baseValue = Unit.ConvertToBaseUnit(Value);
            double converted = targetUnit.ConvertFromBaseUnit(baseValue);

            return new QuantityLength(converted, targetUnit);
        }

        /// <summary>
        /// Adds another QuantityLength and returns result in target unit.
        /// </summary>
        /// <param name="other">Second quantity</param>
        /// <param name="targetUnit">Target unit for result</param>
        /// <returns>Result quantity</returns>
        public QuantityLength Add(QuantityLength other, LengthUnit targetUnit)
        {
            if (other == null)
                throw new ArgumentException("Other quantity cannot be null.");

            double base1 = Unit.ConvertToBaseUnit(Value);
            double base2 = other.Unit.ConvertToBaseUnit(other.Value);

            double sum = base1 + base2;

            double result = targetUnit.ConvertFromBaseUnit(sum);

            return new QuantityLength(result, targetUnit);
        }

        /// <summary>
        /// Determines equality across units using base-unit comparison.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is not QuantityLength other)
                return false;

            double base1 = Unit.ConvertToBaseUnit(Value);
            double base2 = other.Unit.ConvertToBaseUnit(other.Value);

            return Math.Abs(base1 - base2) < EPSILON;
        }

        /// <summary>
        /// Generates hash code based on normalized base-unit value.
        /// </summary>
        public override int GetHashCode()
        {
            double baseValue = Unit.ConvertToBaseUnit(Value);
            return Math.Round(baseValue / EPSILON).GetHashCode();
        }
    }
}