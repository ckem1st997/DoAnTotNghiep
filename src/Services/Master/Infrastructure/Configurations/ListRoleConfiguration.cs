﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Configurations
{
    public partial class ListRoleConfiguration : IEntityTypeConfiguration<ListRole>
    {
        public void Configure(EntityTypeBuilder<ListRole> entity)
        {
            entity.ToTable("ListRole");

            entity.Property(e => e.Id).HasMaxLength(50);

            entity.Property(e => e.AppId)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.Description).HasMaxLength(250);

            entity.Property(e => e.IsAPI).HasDefaultValueSql("((1))");

            entity.Property(e => e.Key)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            entity.Property(e => e.SoftShow).HasDefaultValueSql("((1))");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<ListRole> entity);
    }
}