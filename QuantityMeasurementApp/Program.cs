using QuantityMeasurementApp.Quantities;
using QuantityMeasurementApp.Units;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Entry point demonstrating generic quantity functionality across multiple measurement categories.
    /// Demonstrates UC1–UC10 functionality using unified, reusable methods.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Length Quantity Operations (UC1–UC8) ===\n");
            DemonstrateLengthOperations();

            Console.WriteLine("\n=== Weight Quantity Operations (UC9) ===\n");
            DemonstrateWeightOperations();

            Console.WriteLine("\n=== Cross-Category Prevention (UC10) ===\n");
            DemonstrateCrossCategoryPrevention();

            Console.WriteLine("\n=== Generic Quantity Interface (UC10) ===\n");
            DemonstrateGenericInterface();
        }

        /// <summary>
        /// Demonstrates length quantity operations using generic Quantity<LengthUnit>.
        /// Shows equality across units, conversion, and addition.
        /// </summary>
        static void DemonstrateLengthOperations()
        {
            // UC1–UC3: Basic length equality
            var feet = new Quantity<LengthUnit>(1, LengthUnit.FEET);
            var inches = new Quantity<LengthUnit>(12, LengthUnit.INCHES);
            DemonstrateEquality(feet, inches, "1 FEET vs 12 INCHES");

            // UC5: Unit conversion
            DemonstrateConversion(feet, LengthUnit.INCHES, "1 FEET to INCHES");

            // UC6–UC7: Addition across units
            DemonstrateAddition(feet, inches, LengthUnit.FEET, "1 FEET + 12 INCHES → FEET");

            // Additional conversions
            var yards = new Quantity<LengthUnit>(1, LengthUnit.YARDS);
            DemonstrateConversion(yards, LengthUnit.FEET, "1 YARDS to FEET");
        }

        /// <summary>
        /// Demonstrates weight quantity operations using generic Quantity<WeightUnit>.
        /// Mirrors length operations to show pattern replicability (UC9 pattern).
        /// </summary>
        static void DemonstrateWeightOperations()
        {
            // Equality across weight units
            var kilograms = new Quantity<WeightUnit>(1, WeightUnit.KILOGRAM);
            var grams = new Quantity<WeightUnit>(1000, WeightUnit.GRAM);
            DemonstrateEquality(kilograms, grams, "1 KILOGRAM vs 1000 GRAMS");

            // Unit conversion
            DemonstrateConversion(kilograms, WeightUnit.GRAM, "1 KILOGRAM to GRAMS");

            // Addition across units
            DemonstrateAddition(kilograms, grams, WeightUnit.KILOGRAM, "1 KILOGRAM + 1000 GRAMS → KILOGRAM");
        }

        /// <summary>
        /// Demonstrates prevention of cross-category comparisons.
        /// Shows that Quantity<LengthUnit> and Quantity<WeightUnit> are type-safe.
        /// </summary>
        static void DemonstrateCrossCategoryPrevention()
        {
            var feet = new Quantity<LengthUnit>(1, LengthUnit.FEET);
            var kilograms = new Quantity<WeightUnit>(1, WeightUnit.KILOGRAM);

            Console.WriteLine("Comparing 1 FEET with 1 KILOGRAM (different categories):");
            Console.WriteLine($"  Are they equal? {feet.Equals(kilograms)}");
            Console.WriteLine("  → Prevented by type-safe Quantity<U> generic class");
        }

        /// <summary>
        /// Demonstrates the unified, generic interface pattern (UC10).
        /// Shows how single methods work with multiple unit types through IMeasurable.
        /// </summary>
        static void DemonstrateGenericInterface()
        {
            Console.WriteLine("Generic demonstration methods handle all measurement categories:");

            var length1 = new Quantity<LengthUnit>(2, LengthUnit.FEET);
            var length2 = new Quantity<LengthUnit>(24, LengthUnit.INCHES);
            Console.WriteLine($"\n  Length: {length1} equals {length2}? {length1.Equals(length2)}");

            var weight1 = new Quantity<WeightUnit>(2, WeightUnit.KILOGRAM);
            var weight2 = new Quantity<WeightUnit>(2000, WeightUnit.GRAM);
            Console.WriteLine($"  Weight: {weight1} equals {weight2}? {weight1.Equals(weight2)}");

            Console.WriteLine("\n  → Single DemonstrateEquality() method works for both categories");
            Console.WriteLine("  → Eliminates method duplication present in UC9");
            Console.WriteLine("  → Scales linearly for new measurement categories (UC10 benefit)");
        }

        /// <summary>
        /// Generic method demonstrating equality across units.
        /// Reusable for any unit type with IMeasurable-compliant extension methods.
        /// </summary>
        /// <typeparam name="U">Unit type (enum)</typeparam>
        /// <param name="q1">First quantity</param>
        /// <param name="q2">Second quantity</param>
        /// <param name="description">Human-readable description of comparison</param>
        static void DemonstrateEquality<U>(Quantity<U> q1, Quantity<U> q2, string description) 
            where U : Enum
        {
            Console.WriteLine($"Equality: {description}");
            Console.WriteLine($"  {q1} equals {q2}? {q1.Equals(q2)}");
        }

        /// <summary>
        /// Generic method demonstrating unit conversion.
        /// Reusable for any unit type with IMeasurable-compliant extension methods.
        /// </summary>
        /// <typeparam name="U">Unit type (enum)</typeparam>
        /// <param name="quantity">Quantity to convert</param>
        /// <param name="targetUnit">Target unit</param>
        /// <param name="description">Human-readable description of conversion</param>
        static void DemonstrateConversion<U>(Quantity<U> quantity, U targetUnit, string description)
            where U : Enum
        {
            var result = quantity.ConvertTo(targetUnit);
            Console.WriteLine($"Conversion: {description}");
            Console.WriteLine($"  {quantity} → {result}");
        }

        /// <summary>
        /// Generic method demonstrating quantity addition.
        /// Reusable for any unit type with IMeasurable-compliant extension methods.
        /// </summary>
        /// <typeparam name="U">Unit type (enum)</typeparam>
        /// <param name="q1">First quantity</param>
        /// <param name="q2">Second quantity</param>
        /// <param name="targetUnit">Target unit for result</param>
        /// <param name="description">Human-readable description of addition</param>
        static void DemonstrateAddition<U>(Quantity<U> q1, Quantity<U> q2, U targetUnit, string description)
            where U : Enum
        {
            var result = q1.Add(q2, targetUnit);
            Console.WriteLine($"Addition: {description}");
            Console.WriteLine($"  {q1} + {q2} = {result}");
        }
    }
}