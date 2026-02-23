using System;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Represents a length quantity with multi-unit arithmetic and conversion support.
    /// Enables addition with explicit target unit specification and cross-unit comparisons.
    /// </summary>
    /// <param name="value">The numeric magnitude of the length.</param>
    /// <param name="unit">The unit of measurement (FEET, INCHES, YARDS, CENTIMETERS).</param>
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
            // Check value is not NaN or Infinity
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            Unit = unit; // enum can't be null
            Value = value;
        }

        // Convert a value in given unit to feet (base unit for internal calculation)
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
            double feet = ToFeet(Value, Unit);
            double converted = FromFeet(feet, targetUnit);
            return new QuantityLength(converted, targetUnit);
        }

        // Add another quantity to this one, returning result in this unit
        public QuantityLength Add(QuantityLength other)
        {
            return Add(other, this.Unit);
        }

        // Add another quantity with explicit target unit specification
        public QuantityLength Add(QuantityLength other, LengthUnit targetUnit)
        {
            // Validate second operand is not null
            if (other == null)
                throw new ArgumentException("Second operand cannot be null.");

            // Validate operand value is finite
            if (!double.IsFinite(other.Value))
                throw new ArgumentException("Operand value must be finite.");

            // Validate target unit is defined in enum
            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
                throw new ArgumentException("Invalid target unit.");

            // Perform addition in base unit (feet), then convert to target
            double resultInFeet = AddInFeet(this, other);
            double resultInTarget = FromFeet(resultInFeet, targetUnit);

            return new QuantityLength(resultInTarget, targetUnit);
        }

        // Static overload for consistent API with multiple entry points
        public static QuantityLength Add(
            QuantityLength a,
            QuantityLength b,
            LengthUnit targetUnit)
        {
            // Validate both operands are not null
            if (a == null || b == null)
                throw new ArgumentException("Operands cannot be null.");

            return a.Add(b, targetUnit);
        }

        // Helper: convert two quantities to feet and sum them
        private static double AddInFeet(QuantityLength a, QuantityLength b)
        {
            double aFeet = ToFeet(a.Value, a.Unit);
            double bFeet = ToFeet(b.Value, b.Unit);

            return aFeet + bFeet;
        }

        // Compare two quantities based on their base unit (feet) value with tolerance
        public override bool Equals(object obj)
        {
            // Type check; reject non-QuantityLength objects
            if (obj is not QuantityLength other)
                return false;

            // Convert both to feet, compare within tolerance
            double thisFeet = ToFeet(Value, Unit);
            double otherFeet = ToFeet(other.Value, other.Unit);

            return Math.Abs(thisFeet - otherFeet) < EPSILON;
        }

        // Return hash code based on normalized base unit (feet) value
        public override int GetHashCode()
        {
            // Hash based on feet to ensure equal quantities produce equal hashes
            return ToFeet(Value, Unit).GetHashCode();
        }

        // Return formatted string representation
        public override string ToString()
        {
            return $"Quantity({Value}, {Unit})";
        }
    }
}