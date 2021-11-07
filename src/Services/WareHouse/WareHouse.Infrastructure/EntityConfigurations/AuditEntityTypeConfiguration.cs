using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouse.Domain.Entity;

namespace WareHouse.Infrastructure.EntityConfigurations
{
    class AuditEntityTypeConfiguration : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> entity)
        {
            entity.ToTable("Audit");

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

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(true);

            entity.Property(e => e.ModifiedDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.VoucherCode)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.VoucherDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.WareHouseId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.HasOne(d => d.WareHouse)
                .WithMany(p => p.Audits)
                .HasForeignKey(d => d.WareHouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Audits_PK_WareHouse");
        }
    }
}