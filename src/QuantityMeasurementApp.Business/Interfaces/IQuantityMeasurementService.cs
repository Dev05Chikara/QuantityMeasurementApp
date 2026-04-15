using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Repository.Models;

namespace QuantityMeasurementApp.Business.Interfaces
{
    /// <summary>
    /// IQuantityMeasurementService interface provides contract methods for performing quantity measurement operations,
    /// including conversion, comparison, arithmetic operations, and division.
    /// </summary>
    public interface IQuantityMeasurementService
    {
        /// <summary>
        /// Sets the current username for history tracking.
        /// </summary>
        /// <param name="username">The username to set</param>
        void SetCurrentUsername(string username);

        /// <summary>
        /// Compares two quantities.
        /// </summary>
        /// <param name="dto1">First quantity DTO</param>
        /// <param name="dto2">Second quantity DTO</param>
        /// <returns>Result DTO with comparison result</returns>
        QuantityDTO Compare(QuantityDTO dto1, QuantityDTO dto2);

        /// <summary>
        /// Converts a quantity to another unit.
        /// </summary>
        /// <param name="dto">Quantity DTO to convert</param>
        /// <param name="targetUnitName">Target unit name</param>
        /// <returns>Result DTO with converted quantity</returns>
        QuantityDTO Convert(QuantityDTO dto, string targetUnitName);

        /// <summary>
        /// Adds two quantities.
        /// </summary>
        /// <param name="dto1">First quantity DTO</param>
        /// <param name="dto2">Second quantity DTO</param>
        /// <returns>Result DTO with sum</returns>
        QuantityDTO Add(QuantityDTO dto1, QuantityDTO dto2);

        /// <summary>
        /// Subtracts the second quantity from the first.
        /// </summary>
        /// <param name="dto1">First quantity DTO</param>
        /// <param name="dto2">Second quantity DTO</param>
        /// <returns>Result DTO with difference</returns>
        QuantityDTO Subtract(QuantityDTO dto1, QuantityDTO dto2);

        /// <summary>
        /// Divides the first quantity by the second.
        /// </summary>
        /// <param name="dto1">First quantity DTO</param>
        /// <param name="dto2">Second quantity DTO</param>
        /// <returns>Result DTO with quotient</returns>
        QuantityDTO Divide(QuantityDTO dto1, QuantityDTO dto2);

        /// <summary>
        /// Returns all persisted operation history records.
        /// </summary>
        /// <param name="operationType">Optional operation type filter</param>
        /// <returns>List of history entities</returns>
        List<QuantityMeasurementEntity> GetOperationHistory(OperationType? operationType = null);

        /// <summary>
        /// Returns all persisted operation history records in flattened format for API responses.
        /// </summary>
        /// <param name="operationType">Optional operation type filter</param>
        /// <returns>List of flattened history records</returns>
        List<QuantityMeasurementHistoryRecord> GetOperationHistoryFlattened(OperationType? operationType = null);
    }
}


