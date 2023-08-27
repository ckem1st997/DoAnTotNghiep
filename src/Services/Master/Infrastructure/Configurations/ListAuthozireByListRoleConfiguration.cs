using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.dbo;

namespace Infrastructure.Configurations
{
    public partial class ListAuthozireByListRoleConfiguration : IEntityTypeConfiguration<ListAuthozireByListRole>
    {
        public void Configure(EntityTypeBuilder<ListAuthozireByListRole> entity)
        {
            entity.ToTable("ListAuthozireByListRole");

            entity.Property(e => e.Id).HasMaxLength(50);

            entity.Property(e => e.AppId).HasMaxLength(250);


            entity.Property(e => e.AuthozireId)
                .IsRequired()
                .HasMaxLength(250);
            entity.Property(e => e.ListRoleId)
            .IsRequired()
            .HasMaxLength(250);
            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<ListAuthozireByListRole> entity);
    }
}
