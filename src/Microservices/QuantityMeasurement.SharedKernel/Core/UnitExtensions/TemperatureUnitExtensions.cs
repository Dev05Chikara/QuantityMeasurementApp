namespace QuantityMeasurement.SharedKernel.Core.UnitExtensions
{
    using Units;

    /// <summary>Conversion extensions for TemperatureUnit. Base unit: CELSIUS.</summary>
    public static class TemperatureUnitExtensions
    {
        public static double ConvertToBaseUnit(this TemperatureUnit unit, double value) => unit switch
        {
            TemperatureUnit.CELSIUS    => value,
            TemperatureUnit.FAHRENHEIT => (value - 32.0) * 5.0 / 9.0,
            TemperatureUnit.KELVIN     => value - 273.15,
            _ => throw new ArgumentException("Unsupported temperature unit")
        };

        public static double ConvertFromBaseUnit(this TemperatureUnit unit, double baseValue) => unit switch
        {
            TemperatureUnit.CELSIUS    => baseValue,
            TemperatureUnit.FAHRENHEIT => (baseValue * 9.0 / 5.0) + 32.0,
            TemperatureUnit.KELVIN     => baseValue + 273.15,
            _ => throw new ArgumentException("Unsupported temperature unit")
        };

        public static string GetUnitName(this TemperatureUnit unit) => unit.ToString();
        public static string GetMeasurementType(this TemperatureUnit unit) => "Temperature";

        public static void ValidateOperationSupport(this TemperatureUnit unit, string operation)
        {
            if (operation is "ADD" or "SUBTRACT" or "DIVIDE")
                throw new NotSupportedException(
                    $"Temperature does not support {operation.ToLower()} operation. " +
                    "Temperature arithmetic is not meaningful in most practical contexts.");
        }
    }
}
