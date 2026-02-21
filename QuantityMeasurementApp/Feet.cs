namespace QuantityMeasurementApp
{
    /// <summary>
    /// Feet is an immutable value object implementing IEquatable<Feet> that compares values within a small tolerance (0.0001) to handle floating‑point imprecision.
    /// Overrides Equals and GetHashCode (normalizing by the tolerance) so equal values produce consistent hash codes.
    /// </summary>

    /// <param name="Value">The value of the measurement in feet.</param>
    /// <param name="other">The other Feet object to compare for equality.</param>

    // Class to represent a measurement in feet, implementing IEquatable for equality comparison
    public class Feet : IEquatable<Feet>
    {
        // Private field to store the value of the measurement in feet
        private readonly double value;

        // Constant to define the tolerance for equality comparison, allowing for minor differences in floating-point values
        private const double tolerence= 0.0001;

        // Constructor to initialize the Feet object with a specific value
        public Feet(double Value)
        {
            value= Value;
        }

        // Method to compare this Feet object with another Feet object for equality, considering the defined tolerance
        public bool Equals(Feet other)
        {
            if(other is null) return false;
            return tolerence>= Math.Abs(this.value-other.value);
        }

        // Override of the Equals method to allow comparison with any object, using the type-specific Equals method for Feet
        public override bool Equals(object? obj)
        {
            return Equals(obj as Feet);
        }

        // Override of the GetHashCode method to provide a hash code that is consistent with the Equals method, using a normalized value based on the defined tolerance
        public override int GetHashCode()
        {
            double normalized= Math.Round(value/tolerence)*tolerence;
            return normalized.GetHashCode();
        }
    }
}