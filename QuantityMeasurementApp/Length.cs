using System;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Represents a length measurement with a specific unit (Feet or Inch).
    /// Implements equality based on the converted value to a common base unit (Feet).
    /// Handles invalid unit inputs and ensures consistent hash codes for equivalent lengths.
    /// Includes a tolerance for floating-point comparisons to account for precision issues.
    /// This class is designed to be used in a quantity measurement application where different length units need to be compared and validated.
    /// </summary>
    /// <param name="value">The numeric value of the length measurement.</param>
    /// <param name="unit">The unit of the length measurement (Feet or Inch).</param>

    //Class representing a length measurement with unit conversion and equality logic
    public class Length : IEquatable<Length>
    {
        // Private fields to store the value and unit of the length measurement
        private readonly double value;
        private readonly LengthUnit unit;

        // Tolerance for floating-point comparisons to account for precision issues
        private const double TOLERANCE = 0.0001;

        // Constructor to initialize the length measurement with a value and unit
        public Length(double value, LengthUnit unit)
        {
            // Validate the unit input to ensure it is a defined LengthUnit
            if (!Enum.IsDefined(typeof(LengthUnit), unit)) throw new ArgumentException("Invalid Length Unit");
            this.value = value;
            this.unit = unit;
        }

        // Private method to convert the length measurement to a common base unit (Feet) for comparison
        private double ConvertToBaseUnit()
        {
            return unit switch
            {
                LengthUnit.FEET => value,
                LengthUnit.INCH => value / 12.0,
                _ => throw new InvalidOperationException("Unsupported Unit")
            };
        }

        // Implementation of the IEquatable<Length> interface to compare two Length instances for equality
        public bool Equals(Length other)
        {
            if (other is null) return false;
            return Math.Abs(this.ConvertToBaseUnit() - other.ConvertToBaseUnit()) <= TOLERANCE;
        }

        // Override of the Equals method to compare this Length instance with another object for equality
        public override bool Equals(object obj)
        {
            return Equals(obj as Length);
        }

        // Override of the GetHashCode method to provide a consistent hash code for Length instances that are considered equal
        public override int GetHashCode()
        {
            double normalized= Math.Round(ConvertToBaseUnit() /TOLERANCE)*TOLERANCE;
            return normalized.GetHashCode();
        }
    }
}