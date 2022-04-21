﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
#nullable disable

#nullable disable

namespace Infrastructure
{
    public partial class MasterdataContext : DbContext
    {
        public MasterdataContext()
        {
        }

        public MasterdataContext(DbContextOptions<MasterdataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HistoryNotication> HistoryNotications { get; set; }
        public virtual DbSet<UserMaster> UserMasters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost,5433;Initial Catalog=MasterData;Persist Security Info=True;User ID=sa;Password=Aa!0977751021");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.ApplyConfiguration(new Configurations.HistoryNoticationConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.UserMasterConfiguration());
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
