using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using Serilog.Context;
using Share.Base.Core.Extensions;
using Share.Base.Core.Infrastructure;
using Share.Base.Service;
using System.Data;


namespace BaseHA.Core.Behaviors
{
    //check connect db khi có request xuống lấy db
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly DbContext _dbContext;
        private IDbContextTransaction _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;
        public TransactionBehavior()
        {
            _dbContext = EngineContext.Current.Resolve<DbContext>(DataConnectionHelper.ConnectionStringNames.Warehouse) ?? throw new ArgumentException(nameof(DbContext));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = default(TResponse);
            var typeName = request?.GetGenericTypeName();
            bool count = _dbContext.ChangeTracker.Entries<BaseEntity>().Any();
            bool HasActiveTransaction = false;

            if (_dbContext.Database != null && _dbContext.Database.CurrentTransaction != null)
            {
                _currentTransaction = _dbContext.Database.CurrentTransaction;
                HasActiveTransaction = _currentTransaction != null;
            }

            try
            {

                if (HasActiveTransaction)
                {
                    return await next();
                }

                var strategy = _dbContext.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;

                    using (var transaction = await BeginTransactionAsync())
                    using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
                    {
                        Log.Information("----- Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

                        response = await next();

                        Log.Information("----- Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);
                        await CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }

                });

                return response;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ERROR Handling transaction for {CommandName} ({@Command})", "", request);

                throw;
            }

            async Task<IDbContextTransaction> BeginTransactionAsync()
            {
                if (_currentTransaction != null)
                    return null;

                _currentTransaction = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

                return _currentTransaction;
            }

            async Task CommitTransactionAsync(IDbContextTransaction transaction)
            {
                if (transaction == null)
                    throw new ArgumentNullException(nameof(transaction));
                if (transaction != _currentTransaction)
                    throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

                try
                {
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync(cancellationToken);
                }
                catch
                {
                    await RollbackTransaction(cancellationToken);
                    throw;
                }
                finally
                {
                    if (_currentTransaction != null)
                    {
                        await _currentTransaction.DisposeAsync();
                        _currentTransaction = null;
                    }
                }
            }

            async Task RollbackTransaction(CancellationToken cancellationToken)
            {
                try
                {

                    if (_currentTransaction != null)
                    {
                        await _currentTransaction.RollbackAsync(cancellationToken);
                    }
                }
                finally
                {
                    if (_currentTransaction != null)
                    {
                        await _currentTransaction.DisposeAsync();
                        _currentTransaction = null;
                    }
                }
            }
        }
    }
}