namespace QuantityMeasurementApp.QuantityMeasurementRepo.Models
{
    /// <summary>
    /// EF persistence model for API login users.
    /// </summary>
    public class UserCredentialRecord
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAtUtc { get; set; }
    }
}
