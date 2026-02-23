namespace QuantityMeasurementApp
{
    /// <summary>
    /// Defines the units of length measurement supported by the Length class.
    /// This enum includes INCHES, FEET, YARDS, and CENTIMETERS. Each unit can be converted to a common base unit (inches) for comparison purposes in the Length class.
    /// </summary>
    /// <param name="INCHES">Represents inches as a unit of length measurement.</param>
    /// <param name="FEET">Represents feet as a unit of length measurement.</param>
    /// <param name="YARDS">Represents yards as a unit of length measurement.</param>   
    /// <param name="CENTIMETERS">Represents centimeters as a unit of length measurement.</param>
    public enum LengthUnit
    {
        INCHES,
        FEET,
        YARDS,
        CENTIMETERS
    }
}