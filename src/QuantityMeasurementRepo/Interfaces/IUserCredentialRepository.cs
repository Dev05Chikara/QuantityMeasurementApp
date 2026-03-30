using QuantityMeasurementApp.QuantityMeasurementRepo.Models;

namespace QuantityMeasurementApp.QuantityMeasurementRepo.Interfaces
{
    /// <summary>
    /// User credential persistence access for authentication.
    /// </summary>
    public interface IUserCredentialRepository
    {
        UserCredentialRecord? GetByUsername(string username);

        bool Exists(string username);

        void Add(UserCredentialRecord user);
    }
}
