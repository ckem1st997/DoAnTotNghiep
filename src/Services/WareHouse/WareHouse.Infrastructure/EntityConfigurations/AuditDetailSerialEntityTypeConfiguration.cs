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
    class AuditDetailSerialEntityTypeConfiguration : IEntityTypeConfiguration<AuditDetailSerial>
    {
        public void Configure(EntityTypeBuilder<AuditDetailSerial> entity)
        {
            entity.ToTable("AuditDetailSerial");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.AuditDetailId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.ItemId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Serial)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.HasOne(d => d.AuditDetail)
                .WithMany(p => p.AuditDetailSerials)
                .HasForeignKey(d => d.AuditDetailId)
                .HasConstraintName("FK_AuditDetailSerials_PK_AuditDetail");

            entity.HasOne(d => d.Item)
                .WithMany(p => p.AuditDetailSerials)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditDetailSerials_PK_WareHouseItem");


        }
    }
}