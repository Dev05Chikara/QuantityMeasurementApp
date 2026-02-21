namespace QuantityMeasurementApp
{
    /// <summary>
    /// Inches is an immutable value object implementing IEquatable<Inches> that compares values within a small tolerance (0.0001) to handle floating‑point imprecision.
    /// Overrides Equals and GetHashCode (normalizing by the tolerance) so equal values produce consistent hash codes.
    /// </summary>

    /// <param name="Value">The value of the measurement in inches.</param>
    /// <param name="other">The other Inches object to compare for equality.</param>

    // Class to represent a measurement in inches, implementing IEquatable for equality comparison
    public class Inches
    {
        // Private field to store the value of the measurement in inches
        private readonly double value;

        // Constant to define the tolerance for equality comparison, allowing for minor differences in floating-point values
        private const double tolerence= 0.0001;

        // Constructor to initialize the Inches object with a specific value
        public Inches(double Value)
        {
            // Assign the provided value to the private field
            value= Value;
        }

        // Method to compare this Inches object with another Inches object for equality, considering the defined tolerance
        public bool Equals(Inches other)
        {
            // If the other object is null, they are not equal
            if(other is null) return false;
            // Return true if the absolute difference between the two values is within the defined tolerance
            return tolerence >= Math.Abs(this.value - other.value);
        }

        // Override of the Equals method to allow comparison with any object, using the type-specific Equals method for Inches
        public override bool Equals(object? obj)
        {
            // Attempt to cast the object to an Inches type and compare using the type-specific Equals method
            return Equals(obj as Inches);
        }

        // Override of the GetHashCode method to provide a hash code that is consistent with the Equals method, using a normalized value based on the defined tolerance
        public override int GetHashCode()
        {
            // Normalize the value by rounding it to the nearest multiple of the tolerance, ensuring that values considered equal will have the same hash code
            double normalized= Math.Round(value/tolerence)*tolerence;
            // Return the hash code of the normalized value, ensuring that equal objects have the same hash code
            return normalized.GetHashCode();
        }
    }
}