using System;
using QuantityMeasurementApp.QuantityMeasurementBusiness.Interfaces;
using QuantityMeasurementApp.QuantityMeasurementBusiness.Exceptions;
using QuantityMeasurementApp.QuantityMeasurementModel;

namespace QuantityMeasurementApp.QuantityMeasurementController
{
    /// <summary>
    /// QuantityMeasurementController serves as the entry point for the QuantityMeasurementApp.
    /// This controller is responsible for handling requests related to quantity measurements,
    /// including comparison, conversion, and arithmetic operations on various units of measurement.
    /// </summary>
    public class QuantityMeasurementController
    {
        private readonly IQuantityMeasurementService _service;

        /// <summary>
        /// Initializes a new instance of QuantityMeasurementController.
        /// </summary>
        /// <param name="service">The quantity measurement service</param>
        public QuantityMeasurementController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        /// <summary>
        /// Demonstrates equality comparison between two quantities.
        /// </summary>
        /// <param name="dto1">First quantity DTO</param>
        /// <param name="dto2">Second quantity DTO</param>
        public void DemonstrateEquality(QuantityDTO dto1, QuantityDTO dto2)
        {
            try
            {
                var result = _service.Compare(dto1, dto2);
                DisplayResult($"Comparison of {dto1.Value} {dto1.UnitName} and {dto2.Value} {dto2.UnitName}", result);
            }
            catch (QuantityMeasurementException ex)
            {
                DisplayError($"Equality comparison failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Demonstrates conversion of a quantity to another unit.
        /// </summary>
        /// <param name="dto">Quantity DTO to convert</param>
        /// <param name="targetUnitName">Target unit name</param>
        public void DemonstrateConversion(QuantityDTO dto, string targetUnitName)
        {
            try
            {
                var result = _service.Convert(dto, targetUnitName);
                DisplayResult($"Conversion of {dto.Value} {dto.UnitName} to {targetUnitName}", result);
            }
            catch (QuantityMeasurementException ex)
            {
                DisplayError($"Conversion failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Demonstrates addition of two quantities.
        /// </summary>
        /// <param name="dto1">First quantity DTO</param>
        /// <param name="dto2">Second quantity DTO</param>
        public void DemonstrateAddition(QuantityDTO dto1, QuantityDTO dto2)
        {
            try
            {
                var result = _service.Add(dto1, dto2);
                DisplayResult($"Addition of {dto1.Value} {dto1.UnitName} and {dto2.Value} {dto2.UnitName}", result);
            }
            catch (QuantityMeasurementException ex)
            {
                DisplayError($"Addition failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Demonstrates subtraction of two quantities.
        /// </summary>
        /// <param name="dto1">First quantity DTO</param>
        /// <param name="dto2">Second quantity DTO</param>
        public void DemonstrateSubtraction(QuantityDTO dto1, QuantityDTO dto2)
        {
            try
            {
                var result = _service.Subtract(dto1, dto2);
                DisplayResult($"Subtraction of {dto1.Value} {dto1.UnitName} from {dto2.Value} {dto2.UnitName}", result);
            }
            catch (QuantityMeasurementException ex)
            {
                DisplayError($"Subtraction failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Demonstrates division of two quantities.
        /// </summary>
        /// <param name="dto1">First quantity DTO</param>
        /// <param name="dto2">Second quantity DTO</param>
        public void DemonstrateDivision(QuantityDTO dto1, QuantityDTO dto2)
        {
            try
            {
                var result = _service.Divide(dto1, dto2);
                DisplayResult($"Division of {dto1.Value} {dto1.UnitName} by {dto2.Value} {dto2.UnitName}", result);
            }
            catch (QuantityMeasurementException ex)
            {
                DisplayError($"Division failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Displays the result of an operation.
        /// </summary>
        /// <param name="description">Description of the operation</param>
        /// <param name="result">Result DTO</param>
        private void DisplayResult(string description, QuantityDTO result)
        {
            Console.WriteLine($"{description}: {result.Value} {result.UnitName}");
        }

        /// <summary>
        /// Displays an error message.
        /// </summary>
        /// <param name="message">Error message</param>
        private void DisplayError(string message)
        {
            Console.WriteLine($"Error: {message}");
        }
    }
}