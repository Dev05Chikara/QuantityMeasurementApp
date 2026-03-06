using System;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Entry point for the Quantity Measurement application.
    /// Demonstrates basic usage of QuantityLength and LengthUnit.
    /// This class is kept minimal as the main focus is on the QuantityLength implementation and its tests.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Sample usage of QuantityLength and LengthUnit
            var length1 = new QuantityLength(1.0, LengthUnit.FEET);
            var length2 = new QuantityLength(12.0, LengthUnit.INCHES);

            Console.WriteLine("Equality Test:");
            Console.WriteLine(length1.Equals(length2)); // true

            Console.WriteLine("\nConversion Test:");
            var converted = length1.ConvertTo(LengthUnit.INCHES);
            Console.WriteLine($"{converted.Value} {converted.Unit}");

            Console.WriteLine("\nAddition Test:");
            var sum = length1.Add(length2, LengthUnit.FEET);
            Console.WriteLine($"{sum.Value} {sum.Unit}");
        }
    }
}