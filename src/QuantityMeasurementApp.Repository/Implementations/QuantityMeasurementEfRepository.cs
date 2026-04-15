using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Repository.Interfaces;
using QuantityMeasurementApp.Repository.Models;

namespace QuantityMeasurementApp.Repository.Implementations
{
    /// <summary>
    /// EF-backed repository for quantity measurement operation history.
    /// </summary>
    public class QuantityMeasurementEfRepository : IQuantityMeasurementRepository
    {
        private readonly QuantityMeasurementDbContext _dbContext;

        public QuantityMeasurementEfRepository(QuantityMeasurementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save(QuantityMeasurementEntity entity, string username)
        {
            var record = new QuantityMeasurementHistoryRecord
            {
                Username = username,
                Operation = entity.Operation,
                Operand1Value = entity.Operand1.Value,
                Operand1UnitName = entity.Operand1.UnitName,
                Operand1MeasurementType = entity.Operand1.MeasurementType,
                Operand2Value = entity.Operand2?.Value,
                Operand2UnitName = entity.Operand2?.UnitName,
                Operand2MeasurementType = entity.Operand2?.MeasurementType,
                ResultValue = entity.Result?.Value,
                ResultUnitName = entity.Result?.UnitName,
                ResultMeasurementType = entity.Result?.MeasurementType,
                ErrorMessage = entity.ErrorMessage,
                CreatedAtUtc = entity.Timestamp.ToUniversalTime()
            };

            _dbContext.QuantityMeasurementHistory.Add(record);
            _dbContext.SaveChanges();
        }

        public List<QuantityMeasurementEntity> GetAllMeasurements(string username, OperationType? operationType = null)
        {
            IQueryable<QuantityMeasurementHistoryRecord> query = _dbContext
                .QuantityMeasurementHistory
                .AsNoTracking()
                .Where(row => row.Username == username);

            if (operationType.HasValue)
            {
                query = query.Where(row => row.Operation == operationType.Value);
            }

            var records = query
                .OrderByDescending(row => row.Id)
                .ToList();

            return records.Select(MapToEntity).ToList();
        }

        private static QuantityMeasurementEntity MapToEntity(QuantityMeasurementHistoryRecord row)
        {
            var operand1 = new QuantityDTO
            {
                Value = row.Operand1Value,
                UnitName = row.Operand1UnitName,
                MeasurementType = row.Operand1MeasurementType
            };

            QuantityDTO? operand2 = null;
            if (row.Operand2Value.HasValue && !string.IsNullOrWhiteSpace(row.Operand2UnitName) && !string.IsNullOrWhiteSpace(row.Operand2MeasurementType))
            {
                operand2 = new QuantityDTO
                {
                    Value = row.Operand2Value.Value,
                    UnitName = row.Operand2UnitName,
                    MeasurementType = row.Operand2MeasurementType
                };
            }

            QuantityDTO? result = null;
            if (row.ResultValue.HasValue && !string.IsNullOrWhiteSpace(row.ResultUnitName) && !string.IsNullOrWhiteSpace(row.ResultMeasurementType))
            {
                result = new QuantityDTO
                {
                    Value = row.ResultValue.Value,
                    UnitName = row.ResultUnitName,
                    MeasurementType = row.ResultMeasurementType
                };
            }

            var localTimestamp = DateTime.SpecifyKind(row.CreatedAtUtc, DateTimeKind.Utc).ToLocalTime();

            if (!string.IsNullOrWhiteSpace(row.ErrorMessage))
            {
                return operand2 == null
                    ? new QuantityMeasurementEntity(operand1, row.Operation, row.ErrorMessage, localTimestamp)
                    : new QuantityMeasurementEntity(operand1, operand2, row.Operation, row.ErrorMessage, localTimestamp);
            }

            return operand2 == null
                ? new QuantityMeasurementEntity(operand1, row.Operation, result, localTimestamp)
                : new QuantityMeasurementEntity(operand1, operand2, row.Operation, result, localTimestamp);
        }

        public List<QuantityMeasurementHistoryRecord> GetAllMeasurementsFlattened(string username, OperationType? operationType = null)
        {
            IQueryable<QuantityMeasurementHistoryRecord> query = _dbContext
                .QuantityMeasurementHistory
                .AsNoTracking()
                .Where(row => row.Username == username);

            if (operationType.HasValue)
            {
                query = query.Where(row => row.Operation == operationType.Value);
            }

            return query
                .OrderByDescending(row => row.Id)
                .ToList();
        }
    }
}



