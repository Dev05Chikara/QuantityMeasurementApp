using System.Text.Json.Serialization;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Request payload for conversion operations.
    /// </summary>
    public class QuantityConvertRequestDto
    {
        [JsonPropertyName("quantity")]
        public QuantityDTO Quantity { get; set; } = new();

        [JsonPropertyName("targetUnitName")]
        public string TargetUnitName { get; set; } = string.Empty;
    }
}



