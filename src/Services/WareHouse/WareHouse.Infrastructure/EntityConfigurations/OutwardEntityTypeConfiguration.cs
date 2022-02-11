using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WareHouse.Domain.Entity;

namespace WareHouse.Infrastructure.EntityConfigurations
{
    class OutwardEntityTypeConfiguration : IEntityTypeConfiguration<Outward>
    {
        public void Configure(EntityTypeBuilder<Outward> entity)
        {
            entity.ToTable("Outward");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(true);

            entity.Property(e => e.CreatedDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Deliver)
                .HasMaxLength(255)
                .IsUnicode(true);

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(true);

            entity.Property(e => e.ModifiedDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Reason).HasDefaultValueSql("");

            entity.Property(e => e.ReasonDescription)
                .HasMaxLength(255)
                .IsUnicode(true);

            entity.Property(e => e.Receiver)
                .HasMaxLength(255)
                .IsUnicode(true);

            entity.Property(e => e.Reference)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.ToWareHouseId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.VoucherCode)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.VoucherDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.WareHouseId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("WareHouseID")
                .HasDefaultValueSql("('')");

            entity.HasOne(d => d.ToWareHouse)
                .WithMany(p => p.OutwardToWareHouses)
                .HasForeignKey(d => d.ToWareHouseId)
                .HasConstraintName("FK_ToWareHouse_Outwards_PK_ToWareHouse");

            entity.HasOne(d => d.WareHouse)
                .WithMany(p => p.OutwardWareHouses)
                .HasForeignKey(d => d.WareHouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_Outwards_PK_WareHouse");


        }
    }
}
