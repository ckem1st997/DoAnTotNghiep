using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WareHouse.Domain.Entity;

namespace WareHouse.Infrastructure.EntityConfigurations
{
    class SerialWareHouseEntityTypeConfiguration : IEntityTypeConfiguration<SerialWareHouse>
    {
        public void Configure(EntityTypeBuilder<SerialWareHouse> entity)
        {
            entity.ToTable("SerialWareHouse");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.InwardDetailId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.ItemId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.OutwardDetailId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.Serial)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.HasOne(d => d.InwardDetail)
                .WithMany(p => p.SerialWareHouses)
                .HasForeignKey(d => d.InwardDetailId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SerialWareHouses_PK_InwardDetail");

            entity.HasOne(d => d.Item)
                .WithMany(p => p.SerialWareHouses)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SerialWareHouses_PK_WareHouseItem");

            entity.HasOne(d => d.OutwardDetail)
                .WithMany(p => p.SerialWareHouses)
                .HasForeignKey(d => d.OutwardDetailId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SerialWareHouses_PK_OutwardDetail");
        }
    }
}
