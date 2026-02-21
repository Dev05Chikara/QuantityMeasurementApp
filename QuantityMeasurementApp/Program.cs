namespace QuantityMeasurementApp
{
    class Program
    {
        /// <summary>
        /// The Main method serves as the entry point for the Quantity Measurement application. It prompts the user to input two values, which are then parsed as doubles. The method creates two Feet objects using the input values and compares them using the Equals method. The result of the comparison is printed to the console.
        /// The method also includes error handling to ensure that the user inputs valid numbers, providing feedback if the input format is incorrect. Overall, this method demonstrates how to use the Feet class to compare measurements in feet and provides a simple user interface for testing the functionality of the class.
        /// </summary>
        /// <param name="args"></param>

        static void Main(string[] args)
        {
            /// <summary>
            /// The Main method serves as the entry point for the Quantity Measurement application.
            /// It prompts the user to input two values, which are then parsed as doubles. The method creates two Feet objects using the input values and compares them using the Equals method. The result of the comparison is printed to the console.
            /// The method also includes error handling to ensure that the user inputs valid numbers, providing feedback if the input format is incorrect. Overall, this method demonstrates how to use the Feet class to compare measurements in feet and provides a simple user interface for testing the functionality of the class.
            /// </summary>


            // Prompt the user for two values to compare
            Console.Write("Enter 1st value: ");
            // Validate the input to ensure it's a valid double
            if (!double.TryParse(Console.ReadLine(), out double input1))
            {
                // If the input is not a valid double, display an error message and exit
                Console.WriteLine("Invalid number format.");
                return;
            }

            Console.Write("Enter 2nd value: ");
            // Validate the input to ensure it's a valid double
            if (!double.TryParse(Console.ReadLine(), out double input2))
            {
                // If the input is not a valid double, display an error message and exit
                Console.WriteLine("Invalid number format.");
                return;
            }

            // Create Feet objects for the input values
            Feet value1 = new Feet(input1);
            Feet value2 = new Feet(input2);

            // Compare the two Feet objects using the Equals method and print the result
            bool result = value1.Equals(value2);
            Console.WriteLine($"Equals: {result}");
        }
    }
}