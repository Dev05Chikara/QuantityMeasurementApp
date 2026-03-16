using System;
using QuantityMeasurementApp.QuantityMeasurementBusiness.Units;
using QuantityMeasurementApp.QuantityMeasurementBusiness.Services;
using QuantityMeasurementApp.QuantityMeasurementModel;
using QuantityMeasurementApp.QuantityMeasurementRepo.Implementations;
using ControllerType = QuantityMeasurementApp.QuantityMeasurementController.QuantityMeasurementController;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// QuantityMeasurementApp is the main application class for the Quantity Measurement system.
    /// This singleton class serves as the entry point of the application and is responsible for
    /// initiating the application and creating instances of the controller and repository.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize components
            var repository = QuantityMeasurementCacheRepository.Instance;
            var service = new QuantityMeasurementServiceImpl(repository);
            var controller = new ControllerType(service);

            RunInteractiveMenu(controller);
        }

        private static void RunInteractiveMenu(ControllerType controller)
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

                Console.WriteLine($"Selected: {measurementType}");
                Console.WriteLine("Operations:");
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

                QuantityDTO dto1 = new QuantityDTO { MeasurementType = measurementType, UnitName = unit1, Value = value1 };

                if (isConversion)
                {
                    var targetUnit = PromptForUnit(measurementType, "Enter target unit");
                    controller.DemonstrateConversion(dto1, targetUnit);
                    continue;
                }

                var unit2 = PromptForUnit(measurementType, "Enter second unit");
                var value2 = PromptForDouble("Enter second value");
                QuantityDTO dto2 = new QuantityDTO { MeasurementType = measurementType, UnitName = unit2, Value = value2 };

                if (isComparison)
                    controller.DemonstrateEquality(dto1, dto2);
                else if (isAdd)
                    controller.DemonstrateAddition(dto1, dto2);
                else if (isSubtract)
                    controller.DemonstrateSubtraction(dto1, dto2);
                else if (isDivide)
                    controller.DemonstrateDivision(dto1, dto2);
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

        /// <summary>
        /// Demonstrates length quantity operations.
        /// </summary>
        /// <param name="controller">The controller</param>
        static void DemonstrateLengthOperations(ControllerType controller)
        {
            // UC1�UC3: Basic length equality
            var feetDto = new QuantityDTO { Value = 1, UnitName = "FEET", MeasurementType = "Length" };
            var inchesDto = new QuantityDTO { Value = 12, UnitName = "INCHES", MeasurementType = "Length" };
            controller.DemonstrateEquality(feetDto, inchesDto);

            // UC5: Unit conversion
            controller.DemonstrateConversion(feetDto, "INCHES");

            // UC6�UC7: Addition across units
            controller.DemonstrateAddition(feetDto, inchesDto);

            // Additional conversions
            var yardsDto = new QuantityDTO { Value = 1, UnitName = "YARDS", MeasurementType = "Length" };
            controller.DemonstrateConversion(yardsDto, "FEET");
        }

        /// <summary>
        /// Demonstrates weight quantity operations.
        /// </summary>
        /// <param name="controller">The controller</param>
        static void DemonstrateWeightOperations(ControllerType controller)
        {
            // Equality across weight units
            var kilogramsDto = new QuantityDTO { Value = 1, UnitName = "KILOGRAM", MeasurementType = "Weight" };
            var gramsDto = new QuantityDTO { Value = 1000, UnitName = "GRAM", MeasurementType = "Weight" };
            controller.DemonstrateEquality(kilogramsDto, gramsDto);

            // Unit conversion
            controller.DemonstrateConversion(kilogramsDto, "GRAM");

            // Addition across units
            controller.DemonstrateAddition(kilogramsDto, gramsDto);
        }

        /// <summary>
        /// Demonstrates volume quantity operations.
        /// </summary>
        /// <param name="controller">The controller</param>
        static void DemonstrateVolumeOperations(ControllerType controller)
        {
            // Equality across volume units
            var litresDto = new QuantityDTO { Value = 1, UnitName = "LITRE", MeasurementType = "Volume" };
            var millilitresDto = new QuantityDTO { Value = 1000, UnitName = "MILLILITRE", MeasurementType = "Volume" };
            controller.DemonstrateEquality(litresDto, millilitresDto);

            // Unit conversion
            controller.DemonstrateConversion(litresDto, "MILLILITRE");

            // Addition across units
            controller.DemonstrateAddition(litresDto, millilitresDto);

            // Additional conversions
            var gallonsDto = new QuantityDTO { Value = 1, UnitName = "GALLON", MeasurementType = "Volume" };
            controller.DemonstrateConversion(gallonsDto, "LITRE");
        }

        /// <summary>
        /// Demonstrates subtraction operations.
        /// </summary>
        /// <param name="controller">The controller</param>
        static void DemonstrateSubtractionOperations(ControllerType controller)
        {
            var feetDto = new QuantityDTO { Value = 2, UnitName = "FEET", MeasurementType = "Length" };
            var inchesDto = new QuantityDTO { Value = 6, UnitName = "INCHES", MeasurementType = "Length" };
            controller.DemonstrateSubtraction(feetDto, inchesDto);
        }

        /// <summary>
        /// Demonstrates division operations.
        /// </summary>
        /// <param name="controller">The controller</param>
        static void DemonstrateDivisionOperations(ControllerType controller)
        {
            var feetDto = new QuantityDTO { Value = 12, UnitName = "FEET", MeasurementType = "Length" };
            var inchesDto = new QuantityDTO { Value = 6, UnitName = "INCHES", MeasurementType = "Length" };
            controller.DemonstrateDivision(feetDto, inchesDto);
        }

        /// <summary>
        /// Demonstrates cross-category prevention.
        /// </summary>
        /// <param name="controller">The controller</param>
        static void DemonstrateCrossCategoryPrevention(ControllerType controller)
        {
            var feetDto = new QuantityDTO { Value = 1, UnitName = "FEET", MeasurementType = "Length" };
            var kilogramsDto = new QuantityDTO { Value = 1, UnitName = "KILOGRAM", MeasurementType = "Weight" };
            controller.DemonstrateEquality(feetDto, kilogramsDto);
        }

        /// <summary>
        /// Demonstrates generic interface.
        /// </summary>
        /// <param name="controller">The controller</param>
        static void DemonstrateGenericInterface(ControllerType controller)
        {
            // Similar to length operations
            DemonstrateLengthOperations(controller);
        }

        /// <summary>
        /// Demonstrates temperature operations.
        /// </summary>
        /// <param name="controller">The controller</param>
        static void DemonstrateTemperatureOperations(ControllerType controller)
        {
            var celsiusDto = new QuantityDTO { Value = 0, UnitName = "CELSIUS", MeasurementType = "Temperature" };
            var fahrenheitDto = new QuantityDTO { Value = 32, UnitName = "FAHRENHEIT", MeasurementType = "Temperature" };
            controller.DemonstrateEquality(celsiusDto, fahrenheitDto);

            controller.DemonstrateConversion(celsiusDto, "FAHRENHEIT");

            // Note: Addition for temperature is not supported
            try
            {
                controller.DemonstrateAddition(celsiusDto, fahrenheitDto);
            }
            catch
            {
                Console.WriteLine("Addition not supported for temperature.");
            }
        }
    }
}
