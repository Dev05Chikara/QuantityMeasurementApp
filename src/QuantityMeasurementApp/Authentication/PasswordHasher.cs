using System.Security.Cryptography;
using System.Text;

namespace QuantityMeasurementApp.Authentication
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes);
        }

        public static bool Verify(string plainPassword, string expectedHash)
        {
            var hash = Hash(plainPassword);
            return string.Equals(hash, expectedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
