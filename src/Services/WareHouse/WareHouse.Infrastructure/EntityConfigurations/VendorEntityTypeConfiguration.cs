using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WareHouse.Domain.Entity;

namespace WareHouse.Infrastructure.EntityConfigurations
{
    class VendorEntityTypeConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> entity)
        {
            entity.ToTable("Vendor");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(true);

            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.ContactPerson)
                .HasMaxLength(100)
                .IsUnicode(true);

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(true)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        }
    }
}
