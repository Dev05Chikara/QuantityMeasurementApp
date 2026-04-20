using QuantityMeasurement.SharedKernel.DTOs;

namespace QuantityMeasurement.SharedKernel.Repository
{
    /// <summary>
    /// EF Core persistence model for quantity measurement operation history.
    /// Maps to the shared dbo.QuantityMeasurementHistory table.
    /// </summary>
    public class QuantityMeasurementHistoryRecord
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public OperationType Operation { get; set; }
        public double Operand1Value { get; set; }
        public string Operand1UnitName { get; set; } = string.Empty;
        public string Operand1MeasurementType { get; set; } = string.Empty;
        public double? Operand2Value { get; set; }
        public string? Operand2UnitName { get; set; }
        public string? Operand2MeasurementType { get; set; }
        public double? ResultValue { get; set; }
        public string? ResultUnitName { get; set; }
        public string? ResultMeasurementType { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
