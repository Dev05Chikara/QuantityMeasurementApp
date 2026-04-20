using QuantityMeasurement.SharedKernel.Core.UnitExtensions;
using QuantityMeasurement.SharedKernel.Core.Units;

namespace QuantityMeasurement.SharedKernel.Core.Quantities
{
    /// <summary>
    /// Generic quantity class supporting Length, Weight, Volume, and Temperature.
    /// Ported directly from the monolith's QuantityMeasurementApp.Business.Quantities.Quantity&lt;U&gt;.
    /// </summary>
    public class Quantity<U> where U : Enum
    {
        private enum ArithmeticOperation { ADD, SUBTRACT, DIVIDE }

        public double Value { get; }
        public U Unit { get; }

        public Quantity(double value, U unit)
        {
            if (unit == null) throw new ArgumentException("Unit cannot be null");
            if (double.IsNaN(value) || double.IsInfinity(value)) throw new ArgumentException("Invalid quantity value");
            Value = value;
            Unit = unit;
        }

        // ── Conversion ───────────────────────────────────────────────────────────

        private double ConvertToBase() => ConvertToBase(Unit, Value);

        private static double ConvertToBase(U unit, double value)
        {
            if (unit is LengthUnit l)      return l.ConvertToBaseUnit(value);
            if (unit is WeightUnit w)      return w.ConvertToBaseUnit(value);
            if (unit is VolumeUnit v)      return v.ConvertToBaseUnit(value);
            if (unit is TemperatureUnit t) return t.ConvertToBaseUnit(value);
            throw new ArgumentException("Unsupported unit type");
        }

        private static double ConvertFromBase(U unit, double baseValue)
        {
            if (unit is LengthUnit l)      return l.ConvertFromBaseUnit(baseValue);
            if (unit is WeightUnit w)      return w.ConvertFromBaseUnit(baseValue);
            if (unit is VolumeUnit v)      return v.ConvertFromBaseUnit(baseValue);
            if (unit is TemperatureUnit t) return t.ConvertFromBaseUnit(baseValue);
            throw new ArgumentException("Unsupported unit type");
        }

        public Quantity<U> ConvertTo(U targetUnit)
        {
            double baseValue = ConvertToBase(Unit, Value);
            double result    = ConvertFromBase(targetUnit, baseValue);
            return new Quantity<U>(Math.Round(result, 2), targetUnit);
        }

        // ── Arithmetic helpers ────────────────────────────────────────────────────

        private void ValidateOperands(Quantity<U> other)
        {
            if (other == null) throw new ArgumentException("Cannot perform arithmetic with null quantity");
            if (Unit.GetType() != other.Unit.GetType())
                throw new ArgumentException("Cannot perform arithmetic on quantities of different measurement categories");
            if (double.IsNaN(Value) || double.IsInfinity(Value) ||
                double.IsNaN(other.Value) || double.IsInfinity(other.Value))
                throw new ArgumentException("Invalid quantity value (NaN or infinite)");
        }

        private double PerformBaseArithmetic(Quantity<U> other, ArithmeticOperation operation)
        {
            if (Unit is TemperatureUnit tempUnit)
                tempUnit.ValidateOperationSupport(operation.ToString());

            double base1 = ConvertToBase(Unit, Value);
            double base2 = ConvertToBase(other.Unit, other.Value);

            return operation switch
            {
                ArithmeticOperation.ADD      => base1 + base2,
                ArithmeticOperation.SUBTRACT => base1 - base2,
                ArithmeticOperation.DIVIDE   => base2 == 0.0
                    ? throw new ArithmeticException("Cannot divide by zero quantity")
                    : base1 / base2,
                _ => throw new ArgumentException("Unsupported arithmetic operation")
            };
        }

        // ── Public arithmetic ─────────────────────────────────────────────────────

        public Quantity<U> Add(Quantity<U> other)
        {
            ValidateOperands(other);
            double result = PerformBaseArithmetic(other, ArithmeticOperation.ADD);
            return new Quantity<U>(Math.Round(ConvertFromBase(Unit, result), 2), Unit);
        }

        public Quantity<U> Subtract(Quantity<U> other)
        {
            ValidateOperands(other);
            double result = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);
            return new Quantity<U>(Math.Round(ConvertFromBase(Unit, result), 2), Unit);
        }

        public double Divide(Quantity<U> other)
        {
            ValidateOperands(other);
            return PerformBaseArithmetic(other, ArithmeticOperation.DIVIDE);
        }

        // ── Equality ──────────────────────────────────────────────────────────────

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (Quantity<U>)obj;
            if (Unit.GetType() != other.Unit.GetType()) return false;
            return Math.Round(ConvertToBase(), 5) == Math.Round(other.ConvertToBase(), 5);
        }

        public override int GetHashCode() => ConvertToBase().GetHashCode();

        public override string ToString() => $"Quantity({Value}, {Unit})";
    }
}
