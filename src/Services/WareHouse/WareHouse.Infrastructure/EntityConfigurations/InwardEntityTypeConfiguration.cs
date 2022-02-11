using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WareHouse.Domain.Entity;

namespace WareHouse.Infrastructure.EntityConfigurations
{
    class InwardEntityTypeConfiguration : IEntityTypeConfiguration<Inward>
    {
        public void Configure(EntityTypeBuilder<Inward> entity)
        {
            entity.ToTable("Inward");

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
                .IsUnicode(true);

            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(true);

            entity.Property(e => e.ModifiedDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())");

         //   entity.Property(e => e.Reason).HasDefaultValueSql("");

            entity.Property(e => e.ReasonDescription)
                .HasMaxLength(255)
                .IsUnicode(true);

            entity.Property(e => e.Receiver)
                .HasMaxLength(255)
                .IsUnicode(true);

            entity.Property(e => e.Reference)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.VendorId)
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

            entity.HasOne(d => d.Vendor)
                .WithMany(p => p.Inwards)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("FK_WareHouseInwards_PK_Vendor");

            entity.HasOne(d => d.WareHouse)
                .WithMany(p => p.Inwards)
                .HasForeignKey(d => d.WareHouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WareHouseInwards_PK_WareHouse");

        }
    }
}
