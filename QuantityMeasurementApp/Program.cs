using System;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Console application demonstrating QuantityLength addition with explicit target unit specification.
    /// Shows cross-unit addition results in different target units.
    /// </summary>
    class Program
    {
        static void Main()
        {
            // Create two quantities in different units
            var a = new QuantityLength(1.0, LengthUnit.FEET);
            var b = new QuantityLength(12.0, LengthUnit.INCHES);

            // 1 ft + 12 in = 2 ft (result in feet)
            Console.WriteLine(a.Add(b, LengthUnit.FEET));
            // 1 ft + 12 in = 24 in (result in inches)
            Console.WriteLine(a.Add(b, LengthUnit.INCHES));
            // 1 ft + 12 in ≈ 0.667 yd (result in yards)
            Console.WriteLine(a.Add(b, LengthUnit.YARDS));

            // 36 in + 1 yd = 6 ft (different operand order)
            Console.WriteLine(
                new QuantityLength(36.0, LengthUnit.INCHES)
                    .Add(new QuantityLength(1.0, LengthUnit.YARDS), LengthUnit.FEET)
            );

            // 2.54 cm + 1 in ≈ 5.08 cm (metric and imperial mix)
            Console.WriteLine(
                new QuantityLength(2.54, LengthUnit.CENTIMETERS)
                    .Add(new QuantityLength(1.0, LengthUnit.INCHES), LengthUnit.CENTIMETERS)
            );
        }
    }
}