using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WareHouse.Domain.Entity;

namespace WareHouse.Infrastructure.EntityConfigurations
{
    class AuditCouncilEntityTypeConfiguration : IEntityTypeConfiguration<AuditCouncil>
    {
        public void Configure(EntityTypeBuilder<AuditCouncil> entity)
        {

            entity.ToTable("AuditCouncil");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.AuditId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.EmployeeName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Role)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Audit)
                .WithMany(p => p.AuditCouncils)
                .HasForeignKey(d => d.AuditId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditCouncils_PK_Audit");

        }
    }
}
