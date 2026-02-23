using System;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Console application demonstrating addition of QuantityLength instances across different units.
    /// Shows how quantities in different units can be added and automatically converted.
    /// </summary>
    class Program
    {
        static void Main()
        {
            // Add: 1 foot + 12 inches = 2 feet
            var result1 = new QuantityLength(1.0, LengthUnit.FEET)
                .Add(new QuantityLength(12.0, LengthUnit.INCHES));

            Console.WriteLine(result1);

            // Add: 1 yard + 3 feet = 2 yards
            var result2 = new QuantityLength(1.0, LengthUnit.YARDS)
                .Add(new QuantityLength(3.0, LengthUnit.FEET));

            Console.WriteLine(result2);

            // Add: 2.54 cm + 1 inch = 5.08 cm
            var result3 = new QuantityLength(2.54, LengthUnit.CENTIMETERS)
                .Add(new QuantityLength(1.0, LengthUnit.INCHES));

            Console.WriteLine(result3);
        }
    }
}