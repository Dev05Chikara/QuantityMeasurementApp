using QuantityMeasurementApp.Quantities;
using QuantityMeasurementApp.Units;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Entry point demonstrating generic quantity functionality across multiple measurement categories.
    /// Demonstrates UC1–UC11 functionality using unified, reusable methods.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Length Quantity Operations (UC1–UC8) ===\n");
            DemonstrateLengthOperations();

            Console.WriteLine("\n=== Weight Quantity Operations (UC9) ===\n");
            DemonstrateWeightOperations();

            Console.WriteLine("\n=== Volume Quantity Operations (UC11) ===\n");
            DemonstrateVolumeOperations();

            Console.WriteLine("\n=== Subtraction Operations (UC12) ===\n");
            DemonstrateSubtractionOperations();

            Console.WriteLine("\n=== Division Operations (UC12) ===\n");
            DemonstrateDivisionOperations();

            Console.WriteLine("\n=== Cross-Category Prevention (UC10) ===\n");
            DemonstrateCrossCategoryPrevention();

            Console.WriteLine("\n=== Generic Quantity Interface (UC10) ===\n");
            DemonstrateGenericInterface();

            Console.WriteLine("\n=== Temperature Operations (UC14) ===\n");
            DemonstrateTemperatureOperations();
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
        /// Demonstrates volume quantity operations using generic Quantity<VolumeUnit>.
        /// Shows seamless integration of new measurement category (UC11).
        /// </summary>
        static void DemonstrateVolumeOperations()
        {
            // Equality across volume units
            var litres = new Quantity<VolumeUnit>(1, VolumeUnit.LITRE);
            var millilitres = new Quantity<VolumeUnit>(1000, VolumeUnit.MILLILITRE);
            DemonstrateEquality(litres, millilitres, "1 LITRE vs 1000 MILLILITRES");

            // Unit conversion
            DemonstrateConversion(litres, VolumeUnit.MILLILITRE, "1 LITRE to MILLILITRES");

            // Addition across units
            DemonstrateAddition(litres, millilitres, VolumeUnit.LITRE, "1 LITRE + 1000 MILLILITRES → LITRE");

            // Additional conversions
            var gallons = new Quantity<VolumeUnit>(1, VolumeUnit.GALLON);
            DemonstrateConversion(gallons, VolumeUnit.LITRE, "1 GALLON to LITRES");
        }

        /// <summary>
        /// Demonstrates subtraction quantity operations using generic Quantity<U>.
        /// Shows implicit and explicit target unit subtraction, negative results, and cross-unit handling.
        /// </summary>
        static void DemonstrateSubtractionOperations()
        {
            // Subtraction with implicit target unit (first operand's unit)
            var feet1 = new Quantity<LengthUnit>(10, LengthUnit.FEET);
            var inches1 = new Quantity<LengthUnit>(6, LengthUnit.INCHES);
            DemonstrateSubtraction(feet1, inches1, "10 FEET - 6 INCHES → FEET (implicit)");

            // Subtraction with explicit target unit
            DemonstrateSubtractionWithTargetUnit(feet1, inches1, LengthUnit.INCHES, "10 FEET - 6 INCHES → INCHES (explicit)");

            // Subtraction resulting in negative
            var feet2 = new Quantity<LengthUnit>(5, LengthUnit.FEET);
            var feet3 = new Quantity<LengthUnit>(10, LengthUnit.FEET);
            DemonstrateSubtraction(feet2, feet3, "5 FEET - 10 FEET → negative result");

            // Subtraction resulting in zero
            var feet4 = new Quantity<LengthUnit>(10, LengthUnit.FEET);
            var inches2 = new Quantity<LengthUnit>(120, LengthUnit.INCHES);
            DemonstrateSubtraction(feet4, inches2, "10 FEET - 120 INCHES → 0 (equivalent quantities)");

            // Weight subtraction
            var kg1 = new Quantity<WeightUnit>(10, WeightUnit.KILOGRAM);
            var grams1 = new Quantity<WeightUnit>(5000, WeightUnit.GRAM);
            DemonstrateSubtraction(kg1, grams1, "10 KILOGRAM - 5000 GRAM → KILOGRAM");

            // Volume subtraction
            var litre1 = new Quantity<VolumeUnit>(5, VolumeUnit.LITRE);
            var ml1 = new Quantity<VolumeUnit>(500, VolumeUnit.MILLILITRE);
            DemonstrateSubtraction(litre1, ml1, "5 LITRE - 500 MILLILITRE → LITRE");
        }

        /// <summary>
        /// Demonstrates division quantity operations using generic Quantity<U>.
        /// Shows same-unit and cross-unit division with dimensionless scalar results.
        /// </summary>
        static void DemonstrateDivisionOperations()
        {
            // Division with same units
            var feet1 = new Quantity<LengthUnit>(10, LengthUnit.FEET);
            var feet2 = new Quantity<LengthUnit>(2, LengthUnit.FEET);
            DemonstrateDivision(feet1, feet2, "10 FEET ÷ 2 FEET");

            // Division with different units (same category)
            var inches1 = new Quantity<LengthUnit>(24, LengthUnit.INCHES);
            var feet3 = new Quantity<LengthUnit>(2, LengthUnit.FEET);
            DemonstrateDivision(inches1, feet3, "24 INCHES ÷ 2 FEET");

            // Division resulting in ratio < 1
            var feet4 = new Quantity<LengthUnit>(5, LengthUnit.FEET);
            var feet5 = new Quantity<LengthUnit>(10, LengthUnit.FEET);
            DemonstrateDivision(feet4, feet5, "5 FEET ÷ 10 FEET (ratio < 1)");

            // Weight division
            var kg1 = new Quantity<WeightUnit>(10, WeightUnit.KILOGRAM);
            var gramsDivisor = new Quantity<WeightUnit>(5000, WeightUnit.GRAM);
            DemonstrateDivision(kg1, gramsDivisor, "10 KILOGRAM ÷ 5000 GRAM");

            // Volume division
            var litre1 = new Quantity<VolumeUnit>(10, VolumeUnit.LITRE);
            var litre2 = new Quantity<VolumeUnit>(5, VolumeUnit.LITRE);
            DemonstrateDivision(litre1, litre2, "10 LITRE ÷ 5 LITRE");

            // Division with small result
            var gramSmall = new Quantity<WeightUnit>(2000, WeightUnit.GRAM);
            var kgLarge = new Quantity<WeightUnit>(1, WeightUnit.KILOGRAM);
            DemonstrateDivision(gramSmall, kgLarge, "2000 GRAM ÷ 1 KILOGRAM (ratio < 1)");
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

            // Cross-category arithmetic is prevented at compile-time by generics
            Console.WriteLine("\nCross-category subtraction and division are compile-time errors:");
            Console.WriteLine("  feet.Subtract(kilograms) → Compile-time error (type mismatch)");
            Console.WriteLine("  feet.Divide(kilograms) → Compile-time error (type mismatch)");
            Console.WriteLine("  → Generic type parameters ensure category safety");

            // Demonstrate arithmetic within same category works fine
            var feet2 = new Quantity<LengthUnit>(10, LengthUnit.FEET);
            var feet3 = new Quantity<LengthUnit>(5, LengthUnit.FEET);
            Console.WriteLine("\nWithin-category subtraction and division work correctly:");
            Console.WriteLine($"  10 FEET - 5 FEET = {feet2.Subtract(feet3)}");
            Console.WriteLine($"  10 FEET ÷ 5 FEET = {feet2.Divide(feet3)}");
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

        /// <summary>
        /// Generic method demonstrating quantity subtraction.
        /// Reusable for any unit type with IMeasurable-compliant extension methods.
        /// </summary>
        /// <typeparam name="U">Unit type (enum)</typeparam>
        /// <param name="q1">First quantity (minuend)</param>
        /// <param name="q2">Second quantity (subtrahend)</param>
        /// <param name="description">Human-readable description of subtraction</param>
        static void DemonstrateSubtraction<U>(Quantity<U> q1, Quantity<U> q2, string description)
            where U : Enum
        {
            var result = q1.Subtract(q2);
            Console.WriteLine($"Subtraction: {description}");
            Console.WriteLine($"  {q1} - {q2} = {result}");
        }

        /// <summary>
        /// Generic method demonstrating quantity subtraction with explicit target unit.
        /// Reusable for any unit type with IMeasurable-compliant extension methods.
        /// </summary>
        /// <typeparam name="U">Unit type (enum)</typeparam>
        /// <param name="q1">First quantity (minuend)</param>
        /// <param name="q2">Second quantity (subtrahend)</param>
        /// <param name="targetUnit">Target unit for result</param>
        /// <param name="description">Human-readable description of subtraction</param>
        static void DemonstrateSubtractionWithTargetUnit<U>(Quantity<U> q1, Quantity<U> q2, U targetUnit, string description)
            where U : Enum
        {
            var result = q1.Subtract(q2, targetUnit);
            Console.WriteLine($"Subtraction: {description}");
            Console.WriteLine($"  {q1} - {q2} = {result}");
        }

        /// <summary>
        /// Generic method demonstrating quantity division.
        /// Returns a dimensionless scalar ratio.
        /// Reusable for any unit type with IMeasurable-compliant extension methods.
        /// </summary>
        /// <typeparam name="U">Unit type (enum)</typeparam>
        /// <param name="q1">First quantity (dividend)</param>
        /// <param name="q2">Second quantity (divisor)</param>
        /// <param name="description">Human-readable description of division</param>
        static void DemonstrateDivision<U>(Quantity<U> q1, Quantity<U> q2, string description)
            where U : Enum
        {
            try
            {
                var result = q1.Divide(q2);
                Console.WriteLine($"Division: {description}");
                Console.WriteLine($"  {q1} ÷ {q2} = {result}");
            }
            catch (ArithmeticException ex)
            {
                Console.WriteLine($"Division: {description}");
                Console.WriteLine($"  ERROR: {ex.Message}");
            }
        }

        /// <summary>
        /// Demonstrates temperature quantity operations using generic Quantity<TemperatureUnit>.
        /// Shows equality across temperature units, conversion, and selective arithmetic constraints (UC14).
        /// </summary>
        static void DemonstrateTemperatureOperations()
        {
            // UC14: Temperature equality across units (0°C = 32°F = 273.15K)
            var celsius = new Quantity<TemperatureUnit>(0, TemperatureUnit.CELSIUS);
            var fahrenheit = new Quantity<TemperatureUnit>(32, TemperatureUnit.FAHRENHEIT);
            var kelvin = new Quantity<TemperatureUnit>(273.15, TemperatureUnit.KELVIN);
            DemonstrateEquality(celsius, fahrenheit, "0 CELSIUS vs 32 FAHRENHEIT");
            DemonstrateEquality(celsius, kelvin, "0 CELSIUS vs 273.15 KELVIN");
            DemonstrateEquality(fahrenheit, kelvin, "32 FAHRENHEIT vs 273.15 KELVIN");

            // UC14: Temperature unit conversion
            DemonstrateConversion(celsius, TemperatureUnit.FAHRENHEIT, "0 CELSIUS to FAHRENHEIT");
            DemonstrateConversion(celsius, TemperatureUnit.KELVIN, "0 CELSIUS to KELVIN");
            DemonstrateConversion(fahrenheit, TemperatureUnit.CELSIUS, "32 FAHRENHEIT to CELSIUS");
            DemonstrateConversion(fahrenheit, TemperatureUnit.KELVIN, "32 FAHRENHEIT to KELVIN");
            DemonstrateConversion(kelvin, TemperatureUnit.CELSIUS, "273.15 KELVIN to CELSIUS");
            DemonstrateConversion(kelvin, TemperatureUnit.FAHRENHEIT, "273.15 KELVIN to FAHRENHEIT");

            // UC14: Temperature arithmetic operations are NOT supported
            Console.WriteLine("\nTemperature Arithmetic Operations (Unsupported):");
            try
            {
                var result = celsius.Add(fahrenheit);
                Console.WriteLine($"  ERROR: Addition should have failed but returned {result}");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"  Addition: 0 CELSIUS + 32 FAHRENHEIT → ERROR: {ex.Message}");
            }

            try
            {
                var result = celsius.Subtract(fahrenheit);
                Console.WriteLine($"  ERROR: Subtraction should have failed but returned {result}");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"  Subtraction: 0 CELSIUS - 32 FAHRENHEIT → ERROR: {ex.Message}");
            }

            try
            {
                var result = celsius.Divide(fahrenheit);
                Console.WriteLine($"  ERROR: Division should have failed but returned {result}");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"  Division: 0 CELSIUS ÷ 32 FAHRENHEIT → ERROR: {ex.Message}");
            }

            Console.WriteLine("\n  → Temperature units support only equality and conversion");
            Console.WriteLine("  → Arithmetic operations are selectively disabled for physical accuracy");
            Console.WriteLine("  → IMeasurable interface refactored with optional operation support");
        }
    }
}