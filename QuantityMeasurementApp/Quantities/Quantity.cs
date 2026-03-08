using QuantityMeasurementApp.Interfaces;
using QuantityMeasurementApp.Units;

namespace QuantityMeasurementApp.Quantities
{
    /// <summary>
    /// Generic quantity class supporting multiple measurement categories.
    /// Works with any enum type that implements IMeasurable methods through extension methods.
    /// </summary>
    /// <typeparam name="U">Unit type (enum with IMeasurable-compliant extension methods)</typeparam>
    public class Quantity<U> where U : Enum
    {
        /// <summary>
        /// Quantity value
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Unit type
        /// </summary>
        public U Unit { get; }

        /// <summary>
        /// Initializes a quantity instance.
        /// </summary>
        /// <param name="value">Quantity value</param>
        /// <param name="unit">Measurement unit</param>
        /// <exception cref="ArgumentException">Thrown when unit is null or value is non-finite</exception>
        public Quantity(double value, U unit)
        {
            if (unit == null)
                throw new ArgumentException("Unit cannot be null");

            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid quantity value");

            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Converts quantity value to base unit.
        /// </summary>
        /// <returns>Value expressed in base unit</returns>
        private double ConvertToBase()
        {
            return ConvertToBase(Unit, Value);
        }

        /// <summary>
        /// Helper method performing conversion for any supported unit enum.
        /// Pattern-matches on the concrete type to call the appropriate extension method.
        /// Centralizes the logic so dynamic binding is not needed.
        /// </summary>
        private static double ConvertToBase(U unit, double value)
        {
            // note: the cast via pattern matching ensures compile-time knowledge of the type
            if (unit is LengthUnit l)
                return l.ConvertToBaseUnit(value);
            if (unit is WeightUnit w)
                return w.ConvertToBaseUnit(value);

            throw new ArgumentException("Unsupported unit type");
        }

        /// <summary>
        /// Converts quantity to target unit.
        /// </summary>
        /// <param name="targetUnit">Target unit for conversion</param>
        /// <returns>New quantity with converted value in target unit</returns>
        public Quantity<U> ConvertTo(U targetUnit)
        {
            double baseValue = ConvertToBase(Unit, Value);
            double result = ConvertFromBase(targetUnit, baseValue);

            return new Quantity<U>(Math.Round(result, 2), targetUnit);
        }

        private static double ConvertFromBase(U unit, double baseValue)
        {
            if (unit is LengthUnit l)
                return l.ConvertFromBaseUnit(baseValue);
            if (unit is WeightUnit w)
                return w.ConvertFromBaseUnit(baseValue);

            throw new ArgumentException("Unsupported unit type");
        }

        /// <summary>
        /// Adds two quantities in the first operand's unit.
        /// </summary>
        /// <param name="other">Quantity to add</param>
        /// <returns>New quantity with sum in first operand's unit</returns>
        public Quantity<U> Add(Quantity<U> other)
        {
            return Add(other, Unit);
        }

        /// <summary>
        /// Adds two quantities with explicit target unit specification.
        /// </summary>
        /// <param name="other">Quantity to add</param>
        /// <param name="targetUnit">Target unit for result</param>
        /// <returns>New quantity with sum in target unit</returns>
        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            double base1 = ConvertToBase(Unit, Value);
            double base2 = ConvertToBase(other.Unit, other.Value);

            double sum = base1 + base2;
            double result = ConvertFromBase(targetUnit, sum);

            return new Quantity<U>(Math.Round(result, 2), targetUnit);
        }

        /// <summary>
        /// Compares two quantities for equality using base unit normalization.
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True if quantities represent equal measurements, false otherwise</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Quantity<U> other = (Quantity<U>)obj;

            // Cross-category protection: ensure units are from same category
            if (Unit.GetType() != other.Unit.GetType())
                return false;

            return Math.Round(ConvertToBase(), 5) == Math.Round(other.ConvertToBase(), 5);
        }

        /// <summary>
        /// Returns hash code based on base unit value for consistency with Equals().
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            return ConvertToBase().GetHashCode();
        }

        /// <summary>
        /// Returns string representation of quantity.
        /// </summary>
        /// <returns>Formatted string showing value and unit</returns>
        public override string ToString()
        {
            var name = GetUnitName(Unit);
            return $"Quantity({Value}, {name})";
        }

        private static string GetUnitName(U unit)
        {
            if (unit is LengthUnit l)
                return l.GetUnitName();
            if (unit is WeightUnit w)
                return w.GetUnitName();

            throw new ArgumentException("Unsupported unit type");
        }
    }
}