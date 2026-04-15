using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Repository.Models;

namespace QuantityMeasurementApp.Repository.Interfaces
{
    /// <summary>
    /// The IQuantityMeasurementRepository serves as the data access layer for the application,
    /// abstracting the implementation details either in-memory caching or database interactions,
    /// and providing a clean interface for managing quantity measurement data.
    /// </summary>
    public interface IQuantityMeasurementRepository
    {
        /// <summary>
        /// Saves a QuantityMeasurementEntity to the repository for a specific user.
        /// </summary>
        /// <param name="entity">The entity to save</param>
        /// <param name="username">The username of the user performing the operation</param>
        void Save(QuantityMeasurementEntity entity, string username);

        /// <summary>
        /// Retrieves all measurement entities for a specific user from the repository.
        /// </summary>
        /// <param name="username">The username to filter by</param>
        /// <param name="operationType">Optional operation type filter</param>
        /// <returns>List of user's entities</returns>
        List<QuantityMeasurementEntity> GetAllMeasurements(string username, OperationType? operationType = null);

        /// <summary>
        /// Retrieves all measurement history records for a specific user (raw from database).
        /// Returns flattened records without entity conversion to avoid data loss.
        /// </summary>
        /// <param name="username">The username to filter by</param>
        /// <param name="operationType">Optional operation type filter</param>
        /// <returns>List of flattened database records</returns>
        List<QuantityMeasurementHistoryRecord> GetAllMeasurementsFlattened(string username, OperationType? operationType = null);
    }
}


