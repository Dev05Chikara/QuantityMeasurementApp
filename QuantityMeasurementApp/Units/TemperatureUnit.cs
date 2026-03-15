using QuantityMeasurementApp.Interfaces;

namespace QuantityMeasurementApp.Units
{
    /// <summary>
    /// Temperature measurement units supporting Celsius, Fahrenheit, and Kelvin.
    /// Temperature units support conversion and equality but not arithmetic operations.
    /// </summary>
    public enum TemperatureUnit
    {
        /// <summary>
        /// Celsius temperature scale (base unit)
        /// </summary>
        CELSIUS,

        /// <summary>
        /// Fahrenheit temperature scale
        /// </summary>
        FAHRENHEIT,

        /// <summary>
        /// Kelvin absolute temperature scale
        /// </summary>
        KELVIN
    }

    /// <summary>
    /// Extension methods for TemperatureUnit to implement IMeasurable interface.
    /// </summary>
    public static class TemperatureUnitExtensions
    {
        /// <summary>
        /// Indicates that temperature units do not support arithmetic operations.
        /// </summary>
        private static readonly SupportsArithmetic supportsArithmetic = () => false;

        /// <summary>
        /// Celsius to Celsius identity conversion
        /// </summary>
        private static readonly Func<double, double> CELSIUS_TO_CELSIUS = (celsius) => celsius;

        /// <summary>
        /// Celsius to Fahrenheit conversion: °F = (°C × 9/5) + 32
        /// </summary>
        private static readonly Func<double, double> CELSIUS_TO_FAHRENHEIT = (celsius) => (celsius * 9.0 / 5.0) + 32.0;

        /// <summary>
        /// Celsius to Kelvin conversion: K = °C + 273.15
        /// </summary>
        private static readonly Func<double, double> CELSIUS_TO_KELVIN = (celsius) => celsius + 273.15;

        /// <summary>
        /// Fahrenheit to Celsius conversion: °C = (°F - 32) × 5/9
        /// </summary>
        private static readonly Func<double, double> FAHRENHEIT_TO_CELSIUS = (fahrenheit) => (fahrenheit - 32.0) * 5.0 / 9.0;

        /// <summary>
        /// Fahrenheit to Fahrenheit identity conversion
        /// </summary>
        private static readonly Func<double, double> FAHRENHEIT_TO_FAHRENHEIT = (fahrenheit) => fahrenheit;

        /// <summary>
        /// Fahrenheit to Kelvin conversion: K = (°F - 32) × 5/9 + 273.15
        /// </summary>
        private static readonly Func<double, double> FAHRENHEIT_TO_KELVIN = (fahrenheit) => (fahrenheit - 32.0) * 5.0 / 9.0 + 273.15;

        /// <summary>
        /// Kelvin to Celsius conversion: °C = K - 273.15
        /// </summary>
        private static readonly Func<double, double> KELVIN_TO_CELSIUS = (kelvin) => kelvin - 273.15;

        /// <summary>
        /// Kelvin to Fahrenheit conversion: °F = (K - 273.15) × 9/5 + 32
        /// </summary>
        private static readonly Func<double, double> KELVIN_TO_FAHRENHEIT = (kelvin) => (kelvin - 273.15) * 9.0 / 5.0 + 32.0;

        /// <summary>
        /// Kelvin to Kelvin identity conversion
        /// </summary>
        private static readonly Func<double, double> KELVIN_TO_KELVIN = (kelvin) => kelvin;

        /// <summary>
        /// Gets the conversion factor for temperature units.
        /// Temperature conversions are non-linear, so this returns 1.0.
        /// </summary>
        public static double GetConversionFactor(this TemperatureUnit unit) => 1.0;

        /// <summary>
        /// Converts a temperature value to Celsius (base unit).
        /// </summary>
        public static double ConvertToBaseUnit(this TemperatureUnit unit, double value)
        {
            return unit switch
            {
                TemperatureUnit.CELSIUS => CELSIUS_TO_CELSIUS(value),
                TemperatureUnit.FAHRENHEIT => FAHRENHEIT_TO_CELSIUS(value),
                TemperatureUnit.KELVIN => KELVIN_TO_CELSIUS(value),
                _ => throw new ArgumentException("Unsupported temperature unit")
            };
        }

        /// <summary>
        /// Converts a temperature value from Celsius (base unit) to the target unit.
        /// </summary>
        public static double ConvertFromBaseUnit(this TemperatureUnit unit, double baseValue)
        {
            return unit switch
            {
                TemperatureUnit.CELSIUS => CELSIUS_TO_CELSIUS(baseValue),
                TemperatureUnit.FAHRENHEIT => CELSIUS_TO_FAHRENHEIT(baseValue),
                TemperatureUnit.KELVIN => CELSIUS_TO_KELVIN(baseValue),
                _ => throw new ArgumentException("Unsupported temperature unit")
            };
        }

        /// <summary>
        /// Gets the readable name for the temperature unit.
        /// </summary>
        public static string GetUnitName(this TemperatureUnit unit)
        {
            return unit switch
            {
                TemperatureUnit.CELSIUS => "Celsius",
                TemperatureUnit.FAHRENHEIT => "Fahrenheit",
                TemperatureUnit.KELVIN => "Kelvin",
                _ => "Unknown"
            };
        }

        /// <summary>
        /// Indicates whether temperature units support arithmetic operations.
        /// Temperature units do not support arithmetic operations.
        /// </summary>
        public static bool SupportsArithmeticOperations(this TemperatureUnit unit) => supportsArithmetic();

        /// <summary>
        /// Validates that the specified operation is supported by temperature units.
        /// Temperature units do not support arithmetic operations.
        /// </summary>
        /// <param name="unit">The temperature unit</param>
        /// <param name="operation">Name of the operation being attempted</param>
        /// <exception cref="NotSupportedException">Always thrown for arithmetic operations</exception>
        public static void ValidateOperationSupport(this TemperatureUnit unit, string operation)
        {
            if (operation == "ADD" || operation == "SUBTRACT" || operation == "DIVIDE")
            {
                throw new NotSupportedException($"Temperature does not support {operation.ToLower()} operation. " +
                    "Temperature arithmetic is not meaningful in most practical contexts.");
            }
        }

        /// <summary>
        /// Returns the measurement type.
        /// Implements IMeasurable interface method.
        /// </summary>
        public static string GetMeasurementType(this TemperatureUnit unit)
        {
            return "Temperature";
        }
    }
}