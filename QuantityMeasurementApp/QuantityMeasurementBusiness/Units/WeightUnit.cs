namespace QuantityMeasurementApp.QuantityMeasurementBusiness.Units
{
    /// <summary>
    /// Enumeration representing supported weight units.
    /// Effectively implements IMeasurable through extension methods.
    /// </summary>
    public enum WeightUnit
    {
        KILOGRAM,
        GRAM,
        TONNE
    }

    /// <summary>
    /// Extension methods for WeightUnit that implement IMeasurable interface contract.
    /// Provides consistent conversion behavior across all weight units.
    /// </summary>
    public static class WeightUnitExtensions
    {
        /// <summary>
        /// Conversion factors relative to base unit (GRAM).
        /// </summary>
        private static readonly Dictionary<WeightUnit, double> ConversionFactors = new()
        {
            { WeightUnit.KILOGRAM, 1000.0 },
            { WeightUnit.GRAM, 1.0 },
            { WeightUnit.TONNE, 1000000.0 }
        };

        /// <summary>
        /// Gets conversion factor for the unit relative to base unit (GRAM).
        /// Implements IMeasurable interface method.
        /// </summary>
        public static double GetConversionFactor(this WeightUnit unit)
        {
            if (ConversionFactors.TryGetValue(unit, out var factor))
                return factor;
            throw new ArgumentException("Invalid unit");
        }

        /// <summary>
        /// Converts a value from this unit to the base unit (GRAM).
        /// Implements IMeasurable interface method.
        /// </summary>
        public static double ConvertToBaseUnit(this WeightUnit unit, double value)
        {
            return value * unit.GetConversionFactor();
        }

        /// <summary>
        /// Converts a value from the base unit (GRAM) to this unit.
        /// Implements IMeasurable interface method.
        /// </summary>
        public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue)
        {
            return baseValue / unit.GetConversionFactor();
        }

        /// <summary>
        /// Returns the readable unit name.
        /// Implements IMeasurable interface method.
        /// </summary>
        public static string GetUnitName(this WeightUnit unit)
        {
            return unit.ToString();
        }

        /// <summary>
        /// Returns the measurement type.
        /// Implements IMeasurable interface method.
        /// </summary>
        public static string GetMeasurementType(this WeightUnit unit)
        {
            return "Weight";
        }
    }
}