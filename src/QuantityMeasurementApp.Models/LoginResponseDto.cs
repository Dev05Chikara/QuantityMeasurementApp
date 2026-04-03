namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// JWT login response payload.
    /// </summary>
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresAtUtc { get; set; }
    }
}



