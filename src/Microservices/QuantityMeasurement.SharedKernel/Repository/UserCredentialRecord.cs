namespace QuantityMeasurement.SharedKernel.Repository
{
    /// <summary>
    /// EF Core persistence model for user credentials.
    /// Maps to the shared dbo.UserCredentials table.
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
