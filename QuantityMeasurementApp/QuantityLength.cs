using System;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Represents a length quantity with support for addition and conversion across multiple units.
    /// Performs arithmetic on lengths in different units by converting to a common base (feet) and back.
    /// </summary>
    public sealed class QuantityLength
    {
        private const double EPSILON = 1e-6;

        // Numeric value of the quantity
        public double Value { get; }
        // Unit of measurement
        public LengthUnit Unit { get; }

        // Initialize with value and unit; validate value is finite
        public QuantityLength(double value, LengthUnit unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite (not NaN or Infinity).");

            Value = value;
            Unit = unit;
        }

        // Convert a value in given unit to feet (base unit)
        private static double ToFeet(double value, LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.FEET => value,
                LengthUnit.INCHES => value / 12.0,
                LengthUnit.YARDS => value * 3.0,
                LengthUnit.CENTIMETERS => value / 30.48,
                _ => throw new ArgumentException("Unsupported unit.")
            };
        }

        // Convert feet back to target unit
        private static double FromFeet(double feet, LengthUnit targetUnit)
        {
            return targetUnit switch
            {
                LengthUnit.FEET => feet,
                LengthUnit.INCHES => feet * 12.0,
                LengthUnit.YARDS => feet / 3.0,
                LengthUnit.CENTIMETERS => feet * 30.48,
                _ => throw new ArgumentException("Unsupported unit.")
            };
        }

        // Convert this quantity to a different unit
        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            double feet = ToFeet(this.Value, this.Unit);
            double converted = FromFeet(feet, targetUnit);
            return new QuantityLength(converted, targetUnit);
        }

        // Add another quantity to this one, returning result in this unit
        public QuantityLength Add(QuantityLength other)
        {
            if (other == null)
                throw new ArgumentException("Second operand cannot be null.");

            double thisInFeet = ToFeet(this.Value, this.Unit);
            double otherInFeet = ToFeet(other.Value, other.Unit);

            double sumFeet = thisInFeet + otherInFeet;

            double resultValue = FromFeet(sumFeet, this.Unit);

            return new QuantityLength(resultValue, this.Unit);
        }
        // Static overload for adding two quantities
        public static QuantityLength Add(QuantityLength a, QuantityLength b)
        {
            if (a == null || b == null)
                throw new ArgumentException("Operands cannot be null.");

            return a.Add(b);
        }

        // Compare two quantities based on their base unit (feet) value
        public override bool Equals(object obj)
        {
            if (obj is not QuantityLength other)
                return false;

            double thisFeet = ToFeet(this.Value, this.Unit);
            double otherFeet = ToFeet(other.Value, other.Unit);

            return Math.Abs(thisFeet - otherFeet) < EPSILON;
        }

        // Return hash code based on normalized base unit (feet) value
        public override int GetHashCode()
        {
            double feet = ToFeet(Value, Unit);
            return feet.GetHashCode();
        }

        // Return formatted string representation
        public override string ToString()
        {
            return $"Quantity({Value}, {Unit})";
        }
    }
}