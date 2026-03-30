namespace QuantityMeasurementApp.QuantityMeasurementModel
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
