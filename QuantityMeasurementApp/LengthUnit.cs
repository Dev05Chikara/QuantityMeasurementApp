namespace QuantityMeasurementApp
{
    /// <summary>
    /// Enumeration of supported length units for measurement conversion.
    /// </summary>
    public enum LengthUnit
    {
        // Base unit; conversion factor = 1.0
        INCHES,
        // 12 inches
        FEET,
        // 36 inches
        YARDS,
        // Metric unit; approximately 0.393701 inches
        CENTIMETERS
    }

    /// <summary>
    /// Extension methods for LengthUnit to support unit conversion.
    /// </summary>
    public static class LengthUnitExtensions
    {
        /// <summary>
        /// Get the conversion factor relative to the base unit (inches).
        /// </summary>
        public static double GetFactor(this LengthUnit unit)
        {
            // Return factor to convert unit to inches
            return unit switch
            {
                LengthUnit.INCHES => 1.0,
                LengthUnit.FEET => 12.0,
                LengthUnit.YARDS => 36.0,
                LengthUnit.CENTIMETERS => 0.393701,
                _ => throw new ArgumentException("Unsupported LengthUnit")
            };
        }
    }
}