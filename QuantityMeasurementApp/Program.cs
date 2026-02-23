using System;

namespace QuantityMeasurementApp
{
    class Program
    {
        static void Main()
        {
            var l1 = new Length(1.0, LengthUnit.FEET);
            var l2 = new Length(12.0, LengthUnit.INCH);

            Console.WriteLine($"Are Equal: {l1.Equals(l2)}");
        }
    }
}