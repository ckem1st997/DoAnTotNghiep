using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WareHouse.Domain.Entity;

namespace WareHouse.Infrastructure.EntityConfigurations
{
    class WareHouseEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entity.WareHouse>
    {
        public void Configure(EntityTypeBuilder<WareHouse.Domain.Entity.WareHouse> entity)
        {
            entity.ToTable("WareHouse");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(true);

            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(true);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.ParentId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.Path)
                .HasMaxLength(255)
                .IsUnicode(false);
        }
    }
}
