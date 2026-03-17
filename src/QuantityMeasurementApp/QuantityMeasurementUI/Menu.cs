using System;
using QuantityMeasurementApp.QuantityMeasurementBusiness.Units;
using QuantityMeasurementApp.QuantityMeasurementModel;
using ControllerType = QuantityMeasurementApp.QuantityMeasurementController.QuantityMeasurementController;

namespace QuantityMeasurementApp.QuantityMeasurementUI
{
    /// <summary>
    /// Handles all console-based user interaction for quantity measurement operations.
    /// </summary>
    public class Menu
    {
        private readonly ControllerType _controller;

        public Menu(ControllerType controller)
        {
            _controller = controller;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n=== Quantity Measurement Menu ===");
                Console.WriteLine("1) Length");
                Console.WriteLine("2) Weight");
                Console.WriteLine("3) Volume");
                Console.WriteLine("4) Temperature");
                Console.WriteLine("0) Exit");
                Console.Write("Select measurement type: ");

                var categoryInput = Console.ReadLine()?.Trim();
                if (categoryInput == "0")
                    break;

                string measurementType = categoryInput switch
                {
                    "1" => "Length",
                    "2" => "Weight",
                    "3" => "Volume",
                    "4" => "Temperature",
                    _ => null
                };

                if (measurementType == null)
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                    continue;
                }

                Console.WriteLine("----------------------------");
                Console.WriteLine($"Selected: {measurementType}");
                Console.WriteLine("\nOperations:");
                Console.WriteLine("1) Compare");
                Console.WriteLine("2) Convert");
                if (measurementType != "Temperature")
                {
                    Console.WriteLine("3) Add");
                    Console.WriteLine("4) Subtract");
                    Console.WriteLine("5) Divide");
                }
                Console.WriteLine("0) Back");
                Console.Write("Select operation: ");

                var opInput = Console.ReadLine()?.Trim();
                if (opInput == "0")
                    continue;

                bool isComparison = opInput == "1";
                bool isConversion = opInput == "2";
                bool isAdd = opInput == "3";
                bool isSubtract = opInput == "4";
                bool isDivide = opInput == "5";

                if (!isComparison && !isConversion && !isAdd && !isSubtract && !isDivide)
                {
                    Console.WriteLine("Invalid operation selection.");
                    continue;
                }

                var unit1 = PromptForUnit(measurementType, "Enter first unit");
                var value1 = PromptForDouble("Enter first value");

                var dto1 = new QuantityDTO
                {
                    MeasurementType = measurementType,
                    UnitName = unit1,
                    Value = value1
                };

                if (isConversion)
                {
                    var targetUnit = PromptForUnit(measurementType, "Enter target unit");
                    _controller.DemonstrateConversion(dto1, targetUnit);
                    continue;
                }

                var unit2 = PromptForUnit(measurementType, "Enter second unit");
                var value2 = PromptForDouble("Enter second value");
                var dto2 = new QuantityDTO
                {
                    MeasurementType = measurementType,
                    UnitName = unit2,
                    Value = value2
                };

                if (isComparison)
                    _controller.DemonstrateEquality(dto1, dto2);
                else if (isAdd)
                    _controller.DemonstrateAddition(dto1, dto2);
                else if (isSubtract)
                    _controller.DemonstrateSubtraction(dto1, dto2);
                else if (isDivide)
                    _controller.DemonstrateDivision(dto1, dto2);
            }

            Console.WriteLine("Goodbye.");
        }

        private static string PromptForUnit(string measurementType, string prompt)
        {
            while (true)
            {
                Console.WriteLine($"\n{prompt} for {measurementType}:");
                var units = GetUnitsForMeasurementType(measurementType);
                for (int i = 0; i < units.Length; i++)
                    Console.WriteLine($"{i + 1}) {units[i]}");

                Console.Write("Select option: ");
                var input = Console.ReadLine()?.Trim();
                if (int.TryParse(input, out var index) && index >= 1 && index <= units.Length)
                    return units[index - 1];

                Console.WriteLine("Invalid unit selection, try again.");
            }
        }

        private static double PromptForDouble(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                var input = Console.ReadLine()?.Trim();
                if (double.TryParse(input, out var value))
                    return value;
                Console.WriteLine("Invalid number, please enter a numeric value.");
            }
        }

        private static string[] GetUnitsForMeasurementType(string measurementType)
        {
            return measurementType switch
            {
                "Length" => Enum.GetNames(typeof(LengthUnit)),
                "Weight" => Enum.GetNames(typeof(WeightUnit)),
                "Volume" => Enum.GetNames(typeof(VolumeUnit)),
                "Temperature" => Enum.GetNames(typeof(TemperatureUnit)),
                _ => Array.Empty<string>()
            };
        }
    }
}
