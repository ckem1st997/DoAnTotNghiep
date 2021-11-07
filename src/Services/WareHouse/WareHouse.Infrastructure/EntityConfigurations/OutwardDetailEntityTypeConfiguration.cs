using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WareHouse.Domain.Entity;

namespace WareHouse.Infrastructure.EntityConfigurations
{
    class OutwardDetailEntityTypeConfiguration : IEntityTypeConfiguration<OutwardDetail>
    {
        public void Configure(EntityTypeBuilder<OutwardDetail> entity)
        {
            entity.ToTable("OutwardDetail");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Amount)
                .HasColumnType("decimal(15, 2)")
                .HasDefaultValueSql("((0.00))");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .IsUnicode(true);

            entity.Property(e => e.DepartmentId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(255)
                .IsUnicode(true);

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.EmployeeName)
                .HasMaxLength(255)
                .IsUnicode(true);

            entity.Property(e => e.ItemId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.OutwardId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Price)
                .HasColumnType("decimal(15, 2)")
                .HasDefaultValueSql("((0.00))");

            entity.Property(e => e.ProjectId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.ProjectName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.StationId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.StationName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Uiprice)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("UIPrice")
                .HasDefaultValueSql("((0.00))");

            entity.Property(e => e.Uiquantity).HasColumnName("UIQuantity").HasColumnType("decimal(15, 2)").HasDefaultValueSql("((0.00))");

            entity.Property(e => e.UnitId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.HasOne(d => d.Item)
                .WithMany(p => p.OutwardDetails)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OutwardDetails_PK_WareHouseItem");

            entity.HasOne(d => d.Outward)
                .WithMany(p => p.OutwardDetails)
                .HasForeignKey(d => d.OutwardId)
                .HasConstraintName("FK_OutwardDetails_PK_Outward");

            entity.HasOne(d => d.Unit)
                .WithMany(p => p.OutwardDetails)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OutwardDetails_PK_Unit");
        }
    }
}
