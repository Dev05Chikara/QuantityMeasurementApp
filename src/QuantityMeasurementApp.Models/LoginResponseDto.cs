using System.Text.Json.Serialization;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// JWT login response payload.
    /// </summary>
    public class LoginResponseDto
    {
        [JsonPropertyName("Token")]
        public string Token { get; set; } = string.Empty;

        [JsonPropertyName("ExpiresAtUtc")]
        public DateTime ExpiresAtUtc { get; set; }
    }
}



