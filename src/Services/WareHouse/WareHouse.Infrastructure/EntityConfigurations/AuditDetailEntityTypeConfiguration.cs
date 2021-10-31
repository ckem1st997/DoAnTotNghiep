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
    class AuditDetailEntityTypeConfiguration : IEntityTypeConfiguration<AuditDetail>
    {
        public void Configure(EntityTypeBuilder<AuditDetail> entity)
        {

            entity.ToTable("AuditDetail");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.AuditId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Conclude)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.ItemId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.HasOne(d => d.Audit)
                .WithMany(p => p.AuditDetails)
                .HasForeignKey(d => d.AuditId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditDetails_PK_Audit");

            entity.HasOne(d => d.Item)
                .WithMany(p => p.AuditDetails)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK_AuditDetails_PK_WareHouseItem");

        }
    }
}