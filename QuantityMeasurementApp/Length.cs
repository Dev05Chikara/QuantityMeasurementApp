using System;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Represents a length measurement with a specific unit. Supports equality comparison across different units by converting to a common base unit (inches) for comparison.
    /// The class implements IEquatable<Length> to provide a type-specific equality method, and overrides Equals and GetHashCode to ensure consistent behavior in collections and when comparing objects.
    /// The equality comparison uses a tolerance to account for floating-point precision issues when converting between units. The GetHashCode method normalizes the value to the base unit and rounds it to ensure that equivalent lengths produce the same hash code, which is important for the correct behavior of hash-based collections.
    /// </summary>
    /// <param name="value">The numeric value of the length measurement.</param>
    /// <param name="unit">The unit of the length measurement, defined by the LengthUnit enum.</param>
    public class Length : IEquatable<Length>
    {
        // The numeric value of the length measurement.
        private readonly double value;
        private readonly LengthUnit unit;

        // Tolerance for comparing lengths, to account for floating-point precision issues when converting between units.
        private const double TOLERANCE = 0.0001;

        public Length(double value, LengthUnit unit)
        {
            this.value = value;
            this.unit = unit;
        }

        // Converts the length measurement to a common base unit (inches) for comparison. This method uses a switch expression to determine the conversion factor based on the unit of the length measurement.
        private double ConvertToBaseUnit()
        {
            return unit switch
            {
                LengthUnit.INCHES => value,
                LengthUnit.FEET => value * 12.0,
                LengthUnit.YARDS => value * 36.0,
                LengthUnit.CENTIMETERS => value * 0.393701,
                _ => throw new InvalidOperationException("Unsupported unit")
            };
        }

        //Equals method for IEquatable<Length> interface
        //This method checks if the other Length object is null and then compares the two Length objects by converting them to the base unit (inches) and checking if the absolute difference is within the defined tolerance.
        public bool Equals(Length other)
        {
            if (other is null) return false;

            return Math.Abs(
                this.ConvertToBaseUnit() - 
                other.ConvertToBaseUnit()
            ) <= TOLERANCE;
        }

        // Overrides the default Equals method to provide a consistent equality comparison for Length objects.
        // It first checks if the reference of the current object and the object being compared are the same, in which case it returns true.
        // If not, it attempts to cast the object to a Length type and calls the type-specific Equals method.
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as Length);
        }

        //Overrides GetHashCode to ensure that equivalent Length objects produce the same hash code,
        //which is important for the correct behavior of hash-based collections.
        public override int GetHashCode()
        {
            double normalized =
                Math.Round(ConvertToBaseUnit() / TOLERANCE) * TOLERANCE;

            return normalized.GetHashCode();
        }
    }
}