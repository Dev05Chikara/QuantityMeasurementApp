using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.QuantityMeasurementRepo.Models;

namespace QuantityMeasurementApp.QuantityMeasurementRepo
{
    /// <summary>
    /// EF Core DbContext for quantity measurement history.
    /// </summary>
    public class QuantityMeasurementDbContext : DbContext
    {
        public QuantityMeasurementDbContext(DbContextOptions<QuantityMeasurementDbContext> options)
            : base(options)
        {
        }

        public DbSet<QuantityMeasurementHistoryRecord> QuantityMeasurementHistory => Set<QuantityMeasurementHistoryRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<QuantityMeasurementHistoryRecord>();

            entity.ToTable("QuantityMeasurementHistory", "dbo");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Operation)
                .HasColumnName("Operation")
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Operand1Value).HasColumnName("Operand1Value").IsRequired();
            entity.Property(e => e.Operand1UnitName).HasColumnName("Operand1UnitName").HasMaxLength(50).IsRequired();
            entity.Property(e => e.Operand1MeasurementType).HasColumnName("Operand1MeasurementType").HasMaxLength(50).IsRequired();

            entity.Property(e => e.Operand2Value).HasColumnName("Operand2Value");
            entity.Property(e => e.Operand2UnitName).HasColumnName("Operand2UnitName").HasMaxLength(50);
            entity.Property(e => e.Operand2MeasurementType).HasColumnName("Operand2MeasurementType").HasMaxLength(50);

            entity.Property(e => e.ResultValue).HasColumnName("ResultValue");
            entity.Property(e => e.ResultUnitName).HasColumnName("ResultUnitName").HasMaxLength(50);
            entity.Property(e => e.ResultMeasurementType).HasColumnName("ResultMeasurementType").HasMaxLength(50);

            entity.Property(e => e.ErrorMessage).HasColumnName("ErrorMessage").HasMaxLength(1000);
            entity.Property(e => e.CreatedAtUtc).HasColumnName("CreatedAtUtc").IsRequired();
        }
    }
}
