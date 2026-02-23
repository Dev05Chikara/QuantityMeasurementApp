namespace QuantityMeasurementApp
{
    /// <summary>
    /// Enumeration of supported length units for conversion and arithmetic operations.
    /// </summary>
    public enum LengthUnit
    {
        // Imperial unit; base for conversions
        FEET,
        // Base unit for internal calculations (1 foot = 12 inches)
        INCHES,
        // Imperial unit; 1 yard = 3 feet
        YARDS,
        // Metric unit; 1 centimeter ≈ 0.3937 inches
        CENTIMETERS
    }
}