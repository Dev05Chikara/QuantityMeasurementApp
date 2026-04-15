using System.Text.Json.Serialization;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Request payload for two-operand operations (compare, add, subtract, divide).
    /// </summary>
    public class QuantityPairRequestDto
    {
        [JsonPropertyName("operand1")]
        public QuantityDTO Operand1 { get; set; } = new();

        [JsonPropertyName("operand2")]
        public QuantityDTO Operand2 { get; set; } = new();
    }
}



