namespace QuantityMeasurement.SharedKernel.Repository
{
    public interface IUserCredentialRepository
    {
        UserCredentialRecord? GetByUsername(string username);
        bool Exists(string username);
        void Add(UserCredentialRecord user);
    }
}
