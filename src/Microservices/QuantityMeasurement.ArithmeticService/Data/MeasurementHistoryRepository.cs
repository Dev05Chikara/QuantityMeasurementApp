using Microsoft.EntityFrameworkCore;
using QuantityMeasurement.SharedKernel.DTOs;
using QuantityMeasurement.SharedKernel.Repository;

namespace QuantityMeasurement.ArithmeticService.Data
{
    public class MeasurementHistoryRepository : IMeasurementHistoryRepository
    {
        private readonly ArithmeticDbContext _db;
        public MeasurementHistoryRepository(ArithmeticDbContext db) => _db = db;

        public void Save(QuantityMeasurementHistoryRecord record)
        {
            _db.QuantityMeasurementHistory.Add(record);
            _db.SaveChanges();
        }

        public List<QuantityMeasurementHistoryRecord> GetByUsername(string username, OperationType? operationType = null)
        {
            IQueryable<QuantityMeasurementHistoryRecord> q = _db.QuantityMeasurementHistory
                .AsNoTracking().Where(r => r.Username == username);
            if (operationType.HasValue) q = q.Where(r => r.Operation == operationType.Value);
            return q.OrderByDescending(r => r.Id).ToList();
        }
    }
}
