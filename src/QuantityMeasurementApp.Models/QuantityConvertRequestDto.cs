namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Request payload for conversion operations.
    /// </summary>
    public class QuantityConvertRequestDto
    {
        public QuantityDTO Quantity { get; set; } = new();

        public string TargetUnitName { get; set; } = string.Empty;
    }
}



