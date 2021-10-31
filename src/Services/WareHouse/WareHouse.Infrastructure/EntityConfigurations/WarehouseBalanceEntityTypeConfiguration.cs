using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WareHouse.Domain.Entity;

namespace WareHouse.Infrastructure.EntityConfigurations
{
    class WarehouseBalanceEntityTypeConfiguration : IEntityTypeConfiguration<WarehouseBalance>
    {
        public void Configure(EntityTypeBuilder<WarehouseBalance> entity)
        {
            entity.ToTable("WarehouseBalance");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Amount)
                .HasColumnType("decimal(15, 2)")
                .HasDefaultValueSql("((0.00))");

            entity.Property(e => e.ItemId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.WarehouseId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
        }
    }
}
