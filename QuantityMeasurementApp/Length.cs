using System;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Represents a length measurement with unit conversion and tolerance-based equality.
    /// Supports multiple units (FEET, INCHES, YARDS, CENTIMETERS) with automatic conversion.
    /// </summary>
    public sealed class Length : IEquatable<Length>
    {
        // Numeric value of the measurement
        private readonly double value;
        // Unit of measurement (FEET, INCHES, YARDS, CENTIMETERS)
        private readonly LengthUnit unit;
        // Tolerance for floating-point comparisons
        private const double TOLERANCE = 0.0001;

        // Initialize with a value and unit; validate value is finite
        public Length(double value, LengthUnit unit)
        {
            if (!double.IsFinite(value)) throw new ArgumentException("Value must be finite");

            this.unit = unit;
            this.value = value;
        }

        // Get the numeric value
        public double Value => value;
        // Get the unit of measurement
        public LengthUnit Unit => unit;

        // Convert a value from one unit to another
        public static double Convert(double value, LengthUnit source, LengthUnit target)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite");

            // Normalize source to base unit (inches), then convert to target
            double baseValue = value * source.GetFactor();
            double result = baseValue / target.GetFactor();

            return result;
        }

        // Return a new Length instance converted to the target unit
        public Length ConvertTo(LengthUnit targetUnit)
        {
            double converted = Convert(this.value, this.unit, targetUnit);
            return new Length(converted, targetUnit);
        }

        // Convert this length to base unit (inches) for comparison
        private double ToBaseUnit()
        {
            return value * unit.GetFactor();
        }

        // Compare two Length instances; equal if base unit difference is within tolerance
        public bool Equals(Length other)
        {
            if (other is null) return false;

            return Math.Abs(this.ToBaseUnit() - other.ToBaseUnit())
                   <= TOLERANCE;
        }

        // Override object.Equals; cast and delegate to IEquatable implementation
        public override bool Equals(object obj)
        {
            return Equals(obj as Length);
        }

        // Compute hash code of normalized base unit for consistency with equality
        public override int GetHashCode()
        {
            double normalized =
                Math.Round(ToBaseUnit() / TOLERANCE) * TOLERANCE;

            return normalized.GetHashCode();
        }

        // Return formatted string representation: value and unit
        public override string ToString()
        {
            return $"{value:F2} {unit}";
        }
    }
}