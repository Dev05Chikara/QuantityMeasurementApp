using System;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Console application demonstrating length conversion and equality using the Length class.
    /// </summary>
    public class Program
    {
        // Convert a value between two units using static API
        public static double DemonstrateLengthConversion(
            double value,
            LengthUnit from,
            LengthUnit to)
        {
            return Length.Convert(value, from, to);
        }

        // Convert a Length instance to a different unit
        public static Length DemonstrateLengthConversion(
            Length length,
            LengthUnit toUnit)
        {
            return length.ConvertTo(toUnit);
        }

        // Check if two Length instances are equal within tolerance
        public static bool DemonstrateLengthEquality(
            Length l1,
            Length l2)
        {
            return l1.Equals(l2);
        }

        static void Main()
        {
            // Convert 1 foot to inches
            Console.WriteLine(
                DemonstrateLengthConversion(1.0, LengthUnit.FEET, LengthUnit.INCHES)
            ); 

            // Convert 3 yards to feet
            Console.WriteLine(
                DemonstrateLengthConversion(3.0, LengthUnit.YARDS, LengthUnit.FEET)
            ); 

            // Create a yard and convert to inches
            var yard = new Length(1, LengthUnit.YARDS);
            var inInches = DemonstrateLengthConversion(yard, LengthUnit.INCHES);

            Console.WriteLine(inInches);
        }
    }
}