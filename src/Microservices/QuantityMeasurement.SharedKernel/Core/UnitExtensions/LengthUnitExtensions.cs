namespace QuantityMeasurement.SharedKernel.Core.UnitExtensions
{
    using Units;

    /// <summary>Conversion extensions for LengthUnit. Base unit: INCHES.</summary>
    public static class LengthUnitExtensions
    {
        private static readonly Dictionary<LengthUnit, double> ConversionFactors = new()
        {
            { LengthUnit.FEET,        12.0 },
            { LengthUnit.INCHES,       1.0 },
            { LengthUnit.YARDS,       36.0 },
            { LengthUnit.CENTIMETERS,  0.393701 },
            { LengthUnit.MILLIMETER,   0.0393701 }
        };

        public static double GetConversionFactor(this LengthUnit unit)
        {
            if (ConversionFactors.TryGetValue(unit, out var factor)) return factor;
            throw new ArgumentException($"Invalid LengthUnit: {unit}");
        }

        public static double ConvertToBaseUnit(this LengthUnit unit, double value)  => value * unit.GetConversionFactor();
        public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue) => baseValue / unit.GetConversionFactor();
        public static string GetUnitName(this LengthUnit unit) => unit.ToString();
        public static string GetMeasurementType(this LengthUnit unit) => "Length";
    }
}
