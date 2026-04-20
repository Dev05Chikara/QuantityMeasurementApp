using Microsoft.EntityFrameworkCore;
using QuantityMeasurement.SharedKernel.Repository;

namespace QuantityMeasurement.AuthService.Data
{
    public class UserCredentialEfRepository : IUserCredentialRepository
    {
        private readonly AuthDbContext _db;
        public UserCredentialEfRepository(AuthDbContext db) => _db = db;

        public UserCredentialRecord? GetByUsername(string username) =>
            _db.UserCredentials.AsNoTracking().FirstOrDefault(u => u.Username == username && u.IsActive);

        public bool Exists(string username) =>
            _db.UserCredentials.Any(u => u.Username == username);

        public void Add(UserCredentialRecord user)
        {
            _db.UserCredentials.Add(user);
            _db.SaveChanges();
        }
    }
}
