using QuantityMeasurementApp.Interfaces;

namespace QuantityMeasurementApp.Units
{
    /// <summary>
    /// Enumeration representing supported volume units.
    /// Effectively implements IMeasurable through extension methods.
    /// </summary>
    public enum VolumeUnit
    {
        LITRE,
        MILLILITRE,
        GALLON
    }

    /// <summary>
    /// Extension methods for VolumeUnit that implement IMeasurable interface contract.
    /// Provides consistent conversion behavior across all volume units.
    /// </summary>
    public static class VolumeUnitExtensions
    {
        /// <summary>
        /// Conversion factors relative to base unit (LITRE).
        /// </summary>
        private static readonly Dictionary<VolumeUnit, double> ConversionFactors = new()
        {
            { VolumeUnit.LITRE, 1.0 },
            { VolumeUnit.MILLILITRE, 0.001 },
            { VolumeUnit.GALLON, 3.78541 }
        };

        /// <summary>
        /// Gets conversion factor for the unit relative to base unit (LITRE).
        /// Implements IMeasurable interface method.
        /// </summary>
        public static double GetConversionFactor(this VolumeUnit unit)
        {
            if (ConversionFactors.TryGetValue(unit, out var factor))
                return factor;
            throw new ArgumentException("Invalid unit");
        }

        /// <summary>
        /// Converts a value from this unit to the base unit (LITRE).
        /// Implements IMeasurable interface method.
        /// </summary>
        public static double ConvertToBaseUnit(this VolumeUnit unit, double value)
        {
            return value * unit.GetConversionFactor();
        }

        /// <summary>
        /// Converts a value from the base unit (LITRE) to this unit.
        /// Implements IMeasurable interface method.
        /// </summary>
        public static double ConvertFromBaseUnit(this VolumeUnit unit, double baseValue)
        {
            return baseValue / unit.GetConversionFactor();
        }

        /// <summary>
        /// Returns the readable unit name.
        /// Implements IMeasurable interface method.
        /// </summary>
        public static string GetUnitName(this VolumeUnit unit)
        {
            return unit.ToString();
        }

        /// <summary>
        /// Returns the measurement type.
        /// Implements IMeasurable interface method.
        /// </summary>
        public static string GetMeasurementType(this VolumeUnit unit)
        {
            return "Volume";
        }
    }
}