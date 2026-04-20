namespace QuantityMeasurement.SharedKernel.DTOs
{
    public class QuantityDTO
    {
        public double Value { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public string MeasurementType { get; set; } = string.Empty;
    }
}
