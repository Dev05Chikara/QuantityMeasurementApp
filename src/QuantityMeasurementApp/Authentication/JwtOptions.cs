namespace QuantityMeasurementApp.Authentication
{
    /// <summary>
    /// JWT configuration values bound from appsettings.
    /// </summary>
    public class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;

        public string SecretKey { get; set; } = string.Empty;

        public int ExpiryMinutes { get; set; } = 60;
    }
}
