namespace QuantityMeasurement.SharedKernel.Core.UnitExtensions
{
    using Units;

    /// <summary>Conversion extensions for VolumeUnit. Base unit: LITRE.</summary>
    public static class VolumeUnitExtensions
    {
        private static readonly Dictionary<VolumeUnit, double> ConversionFactors = new()
        {
            { VolumeUnit.LITRE,      1.0 },
            { VolumeUnit.MILLILITRE, 0.001 },
            { VolumeUnit.GALLON,     3.78541 }
        };

        public static double GetConversionFactor(this VolumeUnit unit)
        {
            if (ConversionFactors.TryGetValue(unit, out var factor)) return factor;
            throw new ArgumentException($"Invalid VolumeUnit: {unit}");
        }

        public static double ConvertToBaseUnit(this VolumeUnit unit, double value)   => value * unit.GetConversionFactor();
        public static double ConvertFromBaseUnit(this VolumeUnit unit, double baseValue) => baseValue / unit.GetConversionFactor();
        public static string GetUnitName(this VolumeUnit unit) => unit.ToString();
        public static string GetMeasurementType(this VolumeUnit unit) => "Volume";
    }
}
