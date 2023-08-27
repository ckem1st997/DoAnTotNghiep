﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using Infrastructure.dbo;
#nullable disable

#nullable disable

namespace Infrastructure
{
    public partial class MasterdataContext : DbContext
    {

        public MasterdataContext(DbContextOptions<MasterdataContext> options)
            : base(options)
        {
            System.Diagnostics.Debug.WriteLine("MasterdataContext::ctor ->" + this.GetHashCode());

        }

        public virtual DbSet<HistoryNotication> HistoryNotications { get; set; }
        public virtual DbSet<ListApp> ListApps { get; set; }
        public virtual DbSet<ListAuthozire> ListAuthozires { get; set; }
        public virtual DbSet<ListAuthozireRoleByUser> ListAuthozireRoleByUsers { get; set; }
        public virtual DbSet<ListRole> ListRoles { get; set; }
        public virtual DbSet<ListRoleByUser> ListRoleByUsers { get; set; }
        public virtual DbSet<UserMaster> UserMasters { get; set; }
        public virtual DbSet<ListAuthozireByListRole> ListAuthozireByListRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // nếu không setup trong startup
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Data Source=localhost,5433;Initial Catalog=MasterData;Persist Security Info=True;User ID=sa;Password=Aa!012345679",
            //         sqlServerOptionsAction: sqlOptions =>
            //         {
            //             sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            //         });
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.ApplyConfiguration(new Configurations.HistoryNoticationConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ListAppConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ListAuthozireConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ListAuthozireRoleByUserConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ListRoleConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ListRoleByUserConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.UserMasterConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ListAuthozireByListRoleConfiguration());
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
