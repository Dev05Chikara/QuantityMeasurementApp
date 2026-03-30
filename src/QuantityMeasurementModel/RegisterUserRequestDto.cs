namespace QuantityMeasurementApp.QuantityMeasurementModel
{
    /// <summary>
    /// Registration payload for creating API users.
    /// </summary>
    public class RegisterUserRequestDto
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? Role { get; set; }
    }
}