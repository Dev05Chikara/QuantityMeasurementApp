using System;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Represents supported weight units.
    /// Kilogram is treated as the base unit for all conversions.
    /// </summary>
    public enum WeightUnit
    {
        KILOGRAM,
        GRAM,
        POUND
    }

    /// <summary>
    /// Extension methods for WeightUnit to handle conversions
    /// to and from the base unit (Kilogram).
    /// </summary>
    public static class WeightUnitExtensions
    {
        /// <summary>
        /// Conversion factors relative to base unit (Kilogram).
        /// </summary>
        private const double GRAM_TO_KILOGRAM = 0.001;
        private const double POUND_TO_KILOGRAM = 0.453592;

        /// <summary>
        /// Converts a value from a given unit to the base unit (Kilogram).
        /// </summary>
        /// <param name="unit">Source weight unit</param>
        /// <param name="value">Value in the source unit</param>
        /// <returns>Equivalent value in kilograms</returns>
        public static double ConvertToBaseUnit(this WeightUnit unit, double value)
        {
            return unit switch
            {
                WeightUnit.KILOGRAM => value,
                WeightUnit.GRAM => value * GRAM_TO_KILOGRAM,
                WeightUnit.POUND => value * POUND_TO_KILOGRAM,
                _ => throw new ArgumentException("Unsupported weight unit")
            };
        }

        /// <summary>
        /// Converts a value from the base unit (Kilogram)
        /// to the specified weight unit.
        /// </summary>
        /// <param name="unit">Target weight unit</param>
        /// <param name="baseValue">Value in kilograms</param>
        /// <returns>Converted value</returns>
        public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue)
        {
            return unit switch
            {
                WeightUnit.KILOGRAM => baseValue,
                WeightUnit.GRAM => baseValue / GRAM_TO_KILOGRAM,
                WeightUnit.POUND => baseValue / POUND_TO_KILOGRAM,
                _ => throw new ArgumentException("Unsupported weight unit")
            };
        }
    }
}