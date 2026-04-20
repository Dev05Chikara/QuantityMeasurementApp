namespace QuantityMeasurement.SharedKernel.DTOs
{
    public class QuantityConvertRequestDto
    {
        public QuantityDTO Quantity { get; set; } = new();
        public string TargetUnitName { get; set; } = string.Empty;
    }
}
