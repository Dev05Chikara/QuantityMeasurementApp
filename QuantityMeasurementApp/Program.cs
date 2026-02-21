namespace QuantityMeasurementApp
{
    class Program
    {
        // Main method to demonstrate the functionality of the Feet and Inches classes
        static void Main(string[] args)
        {
            // Demonstrate equality of Feet objects
            DemonstrateFeetEquality();

            // Demonstrate equality of Inches objects
            DemonstrateInchesEquality();
        }

        // Method to demonstrate the equality of two Feet objects
        public static void DemonstrateFeetEquality()
        {
            // Create two Feet objects with the same value
            Feet value1 = new Feet(1.0);
            Feet value2 = new Feet(1.0);

            // Print the result of the equality comparison using the Equals method
            Console.WriteLine($"Feet Equal: {value1.Equals(value2)}");
        }

        // Method to demonstrate the equality of two Inches objects
        public static void DemonstrateInchesEquality()
        {
            // Create two Inches objects with the same value
            Inches value1 = new Inches(1.0);
            Inches value2 = new Inches(1.0);

            // Print the result of the equality comparison using the Equals method
            Console.WriteLine($"Inches Equal: {value1.Equals(value2)}");
        }
    }
}