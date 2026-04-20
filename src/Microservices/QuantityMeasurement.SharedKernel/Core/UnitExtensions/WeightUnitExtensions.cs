namespace QuantityMeasurement.SharedKernel.Core.UnitExtensions
{
    using Units;

    /// <summary>Conversion extensions for WeightUnit. Base unit: GRAM.</summary>
    public static class WeightUnitExtensions
    {
        private static readonly Dictionary<WeightUnit, double> ConversionFactors = new()
        {
            { WeightUnit.GRAM,     1.0 },
            { WeightUnit.KILOGRAM, 1000.0 },
            { WeightUnit.TONNE,    1_000_000.0 }
        };

        public static double GetConversionFactor(this WeightUnit unit)
        {
            if (ConversionFactors.TryGetValue(unit, out var factor)) return factor;
            throw new ArgumentException($"Invalid WeightUnit: {unit}");
        }

        public static double ConvertToBaseUnit(this WeightUnit unit, double value)   => value * unit.GetConversionFactor();
        public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue) => baseValue / unit.GetConversionFactor();
        public static string GetUnitName(this WeightUnit unit) => unit.ToString();
        public static string GetMeasurementType(this WeightUnit unit) => "Weight";
    }
}
