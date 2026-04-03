using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Repository.Interfaces;
using QuantityMeasurementApp.Repository.Models;

namespace QuantityMeasurementApp.Repository.Implementations
{
    /// <summary>
    /// EF-backed repository for user credentials.
    /// </summary>
    public class UserCredentialEfRepository : IUserCredentialRepository
    {
        private readonly QuantityMeasurementDbContext _dbContext;

        public UserCredentialEfRepository(QuantityMeasurementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserCredentialRecord? GetByUsername(string username)
        {
            return _dbContext.UserCredentials
                .AsNoTracking()
                .FirstOrDefault(u => u.Username == username && u.IsActive);
        }

        public bool Exists(string username)
        {
            return _dbContext.UserCredentials.Any(u => u.Username == username);
        }

        public void Add(UserCredentialRecord user)
        {
            _dbContext.UserCredentials.Add(user);
            _dbContext.SaveChanges();
        }
    }
}



