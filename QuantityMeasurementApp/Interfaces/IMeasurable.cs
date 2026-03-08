namespace QuantityMeasurementApp.Interfaces
{
    /// <summary>
    /// Defines a contract for all measurable units.
    /// Ensures consistent conversion behavior across different measurement categories.
    /// </summary>
    public interface IMeasurable
    {
        /// <summary>
        /// Returns conversion factor relative to base unit.
        /// </summary>
        double GetConversionFactor();

        /// <summary>
        /// Converts given value to base unit.
        /// </summary>
        /// <param name="value">Value in current unit</param>
        /// <returns>Value converted to base unit</returns>
        double ConvertToBaseUnit(double value);

        /// <summary>
        /// Converts base unit value into this unit.
        /// </summary>
        /// <param name="baseValue">Value in base unit</param>
        /// <returns>Converted value in current unit</returns>
        double ConvertFromBaseUnit(double baseValue);

        /// <summary>
        /// Returns readable unit name.
        /// </summary>
        string GetUnitName();
    }
}