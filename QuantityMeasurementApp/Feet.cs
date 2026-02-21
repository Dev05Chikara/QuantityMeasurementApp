namespace QuantityMeasurementApp
{
    // Class to represent a measurement in feet
    public class Feet
    {
        /// <summary>
        /// The Feet class represents a measurement in feet and provides methods for equality comparison and hash code generation.
        /// It includes a tolerance for floating-point comparisons to account for precision issues when comparing double values.
        /// The Equals method is overridden to compare Feet objects based on their value, and the GetHashCode method is overridden to ensure that equal objects produce the same hash code.
        /// The class is designed to be immutable, with a readonly field to store the value of feet and a constructor to initialize it.
        /// The tolerance is set to 0.0001, meaning that two Feet objects are considered equal if their values differ by less than or equal to this tolerance.
        /// This implementation allows for accurate comparisons of Feet objects while accounting for the limitations of floating-point arithmetic.
        /// Overall, the Feet class provides a robust way to represent and compare measurements in feet, making it suitable for use in applications that require precise handling of length measurements.
        /// </summary>
        /// <param name="Value"></param>


        // Readonly field to store the value of feet
        private readonly double value;

        // Tolerance for floating-point comparison
        private const double tolerence = 0.0001;

        //Constructor to initialize the value of feet
        public Feet(double Value)
        {
            // Assign the input value to the readonly field
            value = Value;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            // Check for reference equality
            if (this == obj) return true;

            // Check for null
            if (obj == null) return false;

            //Check for type compatibility
            if (obj.GetType() != this.GetType()) return false;

            // Cast the object to Feet for comparison
            Feet other = (Feet)obj;

            //Check for tolerence-based equality
            return tolerence >= Math.Abs(this.value - other.value);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // Normalize the value to ensure that values within the tolerance range produce the same hash code
            double normalized = Math.Round(value / tolerence) * tolerence;

            // Return the hash code of the normalized value
            return normalized.GetHashCode();
        }
    }
}