using Microsoft.EntityFrameworkCore;
using QuantityMeasurement.SharedKernel.DTOs;
using QuantityMeasurement.SharedKernel.Repository;

namespace QuantityMeasurement.CompareService.Data
{
    public class CompareDbContext : DbContext
    {
        public CompareDbContext(DbContextOptions<CompareDbContext> options) : base(options) { }

        public DbSet<QuantityMeasurementHistoryRecord> QuantityMeasurementHistory => Set<QuantityMeasurementHistoryRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<QuantityMeasurementHistoryRecord>();
            e.ToTable("QuantityMeasurementHistory", "dbo");
            e.HasKey(x => x.Id);
            e.Property(x => x.Operation).HasConversion<string>().HasMaxLength(50).IsRequired();
            e.Property(x => x.Username).HasMaxLength(100).IsRequired();
            e.Property(x => x.Operand1UnitName).HasMaxLength(50).IsRequired();
            e.Property(x => x.Operand1MeasurementType).HasMaxLength(50).IsRequired();
            e.Property(x => x.Operand2UnitName).HasMaxLength(50);
            e.Property(x => x.Operand2MeasurementType).HasMaxLength(50);
            e.Property(x => x.ResultUnitName).HasMaxLength(50);
            e.Property(x => x.ResultMeasurementType).HasMaxLength(50);
            e.Property(x => x.ErrorMessage).HasMaxLength(1000);
            e.Property(x => x.CreatedAtUtc).IsRequired();
        }
    }
}
