using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WareHouse.Domain.Entity;

namespace WareHouse.Infrastructure.EntityConfigurations
{
    class WareHouseItemUnitEntityTypeConfiguration : IEntityTypeConfiguration<WareHouseItemUnit>
    {
        public void Configure(EntityTypeBuilder<WareHouseItemUnit> entity)
        {
            entity.ToTable("WareHouseItemUnit");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.IsPrimary)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.ItemId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.UnitId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            // entity.Property(e => e.UnitName)
            //     .HasMaxLength(255)
            //     .IsUnicode(true);

            entity.HasOne(d => d.Item)
                .WithMany(p => p.WareHouseItemUnits)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK_WareHouseItemUnits_PK_WareHouseItem");

            entity.HasOne(d => d.Unit)
                .WithMany(p => p.WareHouseItemUnits)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WareHouseItemUnits_PK_Unit");
        }
    }
}
