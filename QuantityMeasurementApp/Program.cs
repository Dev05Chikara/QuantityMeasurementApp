﻿using System;

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



            // Sample usage of QuantityWeight and WeightUnit
            Console.WriteLine("\nWeight Equality Test:");
            var w1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            var w2 = new QuantityWeight(1000.0, WeightUnit.GRAM);

            Console.WriteLine(w1.Equals(w2));

            Console.WriteLine("\nWeight Conversion Test:");
            var w3 = w1.ConvertTo(WeightUnit.POUND);
            Console.WriteLine($"{w3.Value} {w3.Unit}");

            Console.WriteLine("\nWeight Addition Test:");
            var sumWeight = w1.Add(w2);
            Console.WriteLine($"{sumWeight.Value} {sumWeight.Unit}");
        }
    }
}