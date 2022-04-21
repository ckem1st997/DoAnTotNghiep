﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Configurations
{
    public partial class HistoryNoticationConfiguration : IEntityTypeConfiguration<HistoryNotication>
    {
        public void Configure(EntityTypeBuilder<HistoryNotication> entity)
        {
            entity.ToTable("HistoryNotication");

            entity.Property(e => e.Id).HasMaxLength(50);

            entity.Property(e => e.Body).HasMaxLength(250);

            entity.Property(e => e.Link).HasMaxLength(250);

            entity.Property(e => e.Method)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(50);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<HistoryNotication> entity);
    }
}
