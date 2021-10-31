using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WareHouse.Domain.Entity;

namespace WareHouse.Infrastructure.EntityConfigurations
{
    class WareHouseItemEntityTypeConfiguration : IEntityTypeConfiguration<WareHouseItem>
    {
        public void Configure(EntityTypeBuilder<WareHouseItem> entity)
        {
            entity.ToTable("WareHouseItem");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("CategoryID");

            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Inactive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.UnitId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.VendorId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("VendorID");

            entity.Property(e => e.VendorName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Category)
                .WithMany(p => p.WareHouseItems)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_WareHouseItems_PK_WareHouseItemCategory");

            entity.HasOne(d => d.Unit)
                .WithMany(p => p.WareHouseItems)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WareHouseItems_PK_Unit");

            entity.HasOne(d => d.Vendor)
                .WithMany(p => p.WareHouseItems)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("FK_WareHouseItems_PK_Vendor");
        }
    }
}
