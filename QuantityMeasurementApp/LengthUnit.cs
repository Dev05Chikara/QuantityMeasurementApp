using System;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Represents supported length units and provides conversion
    /// functionality to and from the base unit (Feet).
    /// 
    /// Each enum constant stores its conversion factor relative
    /// to the base unit (Feet).
    /// </summary>
    public enum LengthUnit
    {
        FEET,
        INCHES,
        YARDS,
        CENTIMETERS
    }

    /// <summary>
    /// Extension methods for LengthUnit to handle conversions.
    /// Keeps conversion responsibility inside the unit itself.
    /// </summary>
    public static class LengthUnitExtensions
    {
        /// <summary>
        /// Conversion factors relative to base unit (Feet).
        /// </summary>
        private static readonly double FEET_TO_INCHES = 12.0;
        private static readonly double FEET_TO_YARDS = 1.0 / 3.0;
        private static readonly double FEET_TO_CENTIMETERS = 30.48;

        /// <summary>
        /// Converts a value in the given unit to base unit (Feet).
        /// </summary>
        /// <param name="unit">Source unit</param>
        /// <param name="value">Value in the source unit</param>
        /// <returns>Equivalent value in Feet</returns>
        public static double ConvertToBaseUnit(this LengthUnit unit, double value)
        {
            return unit switch
            {
                LengthUnit.FEET => value,
                LengthUnit.INCHES => value / FEET_TO_INCHES,
                LengthUnit.YARDS => value * 3.0,
                LengthUnit.CENTIMETERS => value / FEET_TO_CENTIMETERS,
                _ => throw new ArgumentException("Unsupported unit")
            };
        }

        /// <summary>
        /// Converts a base unit (Feet) value into the given unit.
        /// </summary>
        /// <param name="unit">Target unit</param>
        /// <param name="baseValue">Value in Feet</param>
        /// <returns>Converted value</returns>
        public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue)
        {
            return unit switch
            {
                LengthUnit.FEET => baseValue,
                LengthUnit.INCHES => baseValue * FEET_TO_INCHES,
                LengthUnit.YARDS => baseValue / 3.0,
                LengthUnit.CENTIMETERS => baseValue * FEET_TO_CENTIMETERS,
                _ => throw new ArgumentException("Unsupported unit")
            };
        }
    }
}