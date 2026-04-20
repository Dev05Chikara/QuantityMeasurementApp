namespace QuantityMeasurement.SharedKernel.Auth
{
    public interface IJwtTokenService
    {
        bool ValidateCredentials(string username, string password, out string role);
        string GenerateToken(string username, string role);
    }
}
