using Microsoft.EntityFrameworkCore;
using QuantityMeasurement.SharedKernel.Repository;

namespace QuantityMeasurement.AuthService.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        public DbSet<UserCredentialRecord> UserCredentials => Set<UserCredentialRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<UserCredentialRecord>();
            e.ToTable("UserCredentials", "dbo");
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.Username).IsUnique();
            e.Property(x => x.Id).HasColumnName("Id");
            e.Property(x => x.Username).HasColumnName("Username").HasMaxLength(100).IsRequired();
            e.Property(x => x.PasswordHash).HasColumnName("PasswordHash").HasMaxLength(256).IsRequired();
            e.Property(x => x.Role).HasColumnName("Role").HasMaxLength(50).IsRequired();
            e.Property(x => x.IsActive).HasColumnName("IsActive").IsRequired();
            e.Property(x => x.CreatedAtUtc).HasColumnName("CreatedAtUtc").IsRequired();
        }
    }
}
