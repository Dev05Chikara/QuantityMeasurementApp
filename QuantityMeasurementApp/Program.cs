using System;

namespace QuantityMeasurementApp
{
    class Program
    {
        static void Main()
        {
            // Create Length objects with different units and values to demonstrate equality comparison across units.

            var yard = new Length(1.0, LengthUnit.YARDS);
            var feet = new Length(3.0, LengthUnit.FEET);
            var inches = new Length(36.0, LengthUnit.INCHES);
            var cm = new Length(1.0, LengthUnit.CENTIMETERS);

            Console.WriteLine(yard.Equals(feet));   
            Console.WriteLine(yard.Equals(inches)); 
            Console.WriteLine(cm.Equals(new Length(0.393701, LengthUnit.INCHES)));
        }
    }
}