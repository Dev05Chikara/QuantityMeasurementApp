namespace QuantityMeasurementApp.QuantityMeasurementModel
{
    /// <summary>
    /// Login payload used to request a JWT.
    /// </summary>
    public class LoginRequestDto
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
