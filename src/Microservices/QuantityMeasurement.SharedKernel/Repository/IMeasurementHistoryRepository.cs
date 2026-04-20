using QuantityMeasurement.SharedKernel.DTOs;

namespace QuantityMeasurement.SharedKernel.Repository
{
    public interface IMeasurementHistoryRepository
    {
        void Save(QuantityMeasurementHistoryRecord record);
        List<QuantityMeasurementHistoryRecord> GetByUsername(string username, OperationType? operationType = null);
    }
}
