using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.Domain.Entity;
using WareHouse.Domain.SeeWork;
using WareHouse.Infrastructure.EntityConfigurations;

namespace WareHouse.Infrastructure
{
    public class WarehouseManagementContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "WarehouseManagement";
        public const string STRING_CONNECT = @"Server=sqlserver;Initial Catalog=WarehouseManagement;Persist Security Info=True;User ID=sa;Password=Aa!0977751021;MultipleActiveResultSets = true";
        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<AuditCouncil> AuditCouncils { get; set; }
        public virtual DbSet<AuditDetail> AuditDetails { get; set; }
        public virtual DbSet<AuditDetailSerial> AuditDetailSerials { get; set; }
        public virtual DbSet<BeginningWareHouse> BeginningWareHouses { get; set; }
        public virtual DbSet<Inward> Inwards { get; set; }
        public virtual DbSet<InwardDetail> InwardDetails { get; set; }
        public virtual DbSet<Outward> Outwards { get; set; }
        public virtual DbSet<OutwardDetail> OutwardDetails { get; set; }
        public virtual DbSet<SerialWareHouse> SerialWareHouses { get; set; }
        public virtual DbSet<Domain.Entity.Unit> Units { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Domain.Entity.WareHouse> WareHouses { get; set; }
        public virtual DbSet<WareHouseItem> WareHouseItems { get; set; }
        public virtual DbSet<WareHouseItemCategory> WareHouseItemCategories { get; set; }
        public virtual DbSet<WareHouseItemUnit> WareHouseItemUnits { get; set; }
        public virtual DbSet<WareHouseLimit> WareHouseLimits { get; set; }
        public virtual DbSet<WarehouseBalance> WarehouseBalances { get; set; }


        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public WarehouseManagementContext(DbContextOptions<WarehouseManagementContext> options)
            : base(options)
        {
        }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public WarehouseManagementContext(DbContextOptions<WarehouseManagementContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            System.Diagnostics.Debug.WriteLine("WarehouseManagementContext::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VendorEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AuditCouncilEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AuditDetailEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AuditDetailSerialEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BeginningWareHouseEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InwardDetailEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InwardEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OutwardDetailEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OutwardEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SerialWareHouseEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UnitEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseBalanceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WareHouseEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WareHouseItemCategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WareHouseItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WareHouseItemUnitEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WareHouseLimitEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AuditEntityTypeConfiguration());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string databasePath = $"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}WHSqliteDatabase.db";
            //optionsBuilder.UseSqlite($"Data Source={databasePath}");
            optionsBuilder.LogTo(Console.WriteLine).EnableSensitiveDataLogging();

        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
         //   await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);
            if (result > 0)
                return true;
            return false;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }

    public class WarehouseManagementContextDesignFactory : IDesignTimeDbContextFactory<WarehouseManagementContext>
    {
        public WarehouseManagementContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WarehouseManagementContext>()
                .UseSqlServer(WarehouseManagementContext.STRING_CONNECT);

            return new WarehouseManagementContext(optionsBuilder.Options, new NoMediator());
        }

        class NoMediator : IMediator
        {
            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }

            public Task<object> Send(object request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(default(object));
            }
        }
    }
}
