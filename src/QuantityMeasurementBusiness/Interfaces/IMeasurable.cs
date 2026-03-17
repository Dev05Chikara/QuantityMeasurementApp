namespace QuantityMeasurementApp.QuantityMeasurementBusiness.Interfaces
{
    /// <summary>
    /// Functional interface to indicate whether a measurable unit supports arithmetic operations.
    /// </summary>
    public delegate bool SupportsArithmetic();

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

        /// <summary>
        /// Indicates whether this unit supports arithmetic operations.
        /// Default implementation returns true for backward compatibility.
        /// </summary>
        public bool SupportsArithmeticOperations() => true;

        /// <summary>
        /// Validates that the specified operation is supported by this unit.
        /// Default implementation does nothing (allows all operations).
        /// Units can override to throw exceptions for unsupported operations.
        /// </summary>
        /// <param name="operation">Name of the operation being attempted</param>
        /// <exception cref="NotSupportedException">Thrown when operation is not supported</exception>
        public void ValidateOperationSupport(string operation) { }

        /// <summary>
        /// Returns the measurement type (e.g., "Length", "Weight").
        /// </summary>
        string GetMeasurementType();
    }
}
