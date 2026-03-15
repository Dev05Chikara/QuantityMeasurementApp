using QuantityMeasurementApp.Controllers;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Repositories;
using QuantityMeasurementApp.Services;

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
            var controller = new QuantityMeasurementController(service);

            Console.WriteLine("=== Length Quantity Operations (UC1–UC8) ===\n");
            DemonstrateLengthOperations(controller);

            Console.WriteLine("\n=== Weight Quantity Operations (UC9) ===\n");
            DemonstrateWeightOperations(controller);

            Console.WriteLine("\n=== Volume Quantity Operations (UC11) ===\n");
            DemonstrateVolumeOperations(controller);

            Console.WriteLine("\n=== Subtraction Operations (UC12) ===\n");
            DemonstrateSubtractionOperations(controller);

            Console.WriteLine("\n=== Division Operations (UC12) ===\n");
            DemonstrateDivisionOperations(controller);

            Console.WriteLine("\n=== Cross-Category Prevention (UC10) ===\n");
            DemonstrateCrossCategoryPrevention(controller);

            Console.WriteLine("\n=== Generic Quantity Interface (UC10) ===\n");
            DemonstrateGenericInterface(controller);

            Console.WriteLine("\n=== Temperature Operations (UC14) ===\n");
            DemonstrateTemperatureOperations(controller);
        }

        /// <summary>
        /// Demonstrates length quantity operations.
        /// </summary>
        /// <param name="controller">The controller</param>
        static void DemonstrateLengthOperations(QuantityMeasurementController controller)
        {
            // UC1–UC3: Basic length equality
            var feetDto = new QuantityDTO { Value = 1, UnitName = "FEET", MeasurementType = "Length" };
            var inchesDto = new QuantityDTO { Value = 12, UnitName = "INCHES", MeasurementType = "Length" };
            controller.DemonstrateEquality(feetDto, inchesDto);

            // UC5: Unit conversion
            controller.DemonstrateConversion(feetDto, "INCHES");

            // UC6–UC7: Addition across units
            controller.DemonstrateAddition(feetDto, inchesDto);

            // Additional conversions
            var yardsDto = new QuantityDTO { Value = 1, UnitName = "YARDS", MeasurementType = "Length" };
            controller.DemonstrateConversion(yardsDto, "FEET");
        }

        /// <summary>
        /// Demonstrates weight quantity operations.
        /// </summary>
        /// <param name="controller">The controller</param>
        static void DemonstrateWeightOperations(QuantityMeasurementController controller)
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
        static void DemonstrateVolumeOperations(QuantityMeasurementController controller)
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
        static void DemonstrateSubtractionOperations(QuantityMeasurementController controller)
        {
            var feetDto = new QuantityDTO { Value = 2, UnitName = "FEET", MeasurementType = "Length" };
            var inchesDto = new QuantityDTO { Value = 6, UnitName = "INCHES", MeasurementType = "Length" };
            controller.DemonstrateSubtraction(feetDto, inchesDto);
        }

        /// <summary>
        /// Demonstrates division operations.
        /// </summary>
        /// <param name="controller">The controller</param>
        static void DemonstrateDivisionOperations(QuantityMeasurementController controller)
        {
            var feetDto = new QuantityDTO { Value = 12, UnitName = "FEET", MeasurementType = "Length" };
            var inchesDto = new QuantityDTO { Value = 6, UnitName = "INCHES", MeasurementType = "Length" };
            controller.DemonstrateDivision(feetDto, inchesDto);
        }

        /// <summary>
        /// Demonstrates cross-category prevention.
        /// </summary>
        /// <param name="controller">The controller</param>
        static void DemonstrateCrossCategoryPrevention(QuantityMeasurementController controller)
        {
            var feetDto = new QuantityDTO { Value = 1, UnitName = "FEET", MeasurementType = "Length" };
            var kilogramsDto = new QuantityDTO { Value = 1, UnitName = "KILOGRAM", MeasurementType = "Weight" };
            controller.DemonstrateEquality(feetDto, kilogramsDto);
        }

        /// <summary>
        /// Demonstrates generic interface.
        /// </summary>
        /// <param name="controller">The controller</param>
        static void DemonstrateGenericInterface(QuantityMeasurementController controller)
        {
            // Similar to length operations
            DemonstrateLengthOperations(controller);
        }

        /// <summary>
        /// Demonstrates temperature operations.
        /// </summary>
        /// <param name="controller">The controller</param>
        static void DemonstrateTemperatureOperations(QuantityMeasurementController controller)
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
