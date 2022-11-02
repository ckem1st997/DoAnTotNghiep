using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;
using Share.BaseCore.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace Share.BaseCore.Behaviors
{
    //check connect db khi có request xuống lấy db
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly DbContext _dbContext;
        private IDbContextTransaction _currentTransaction;

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;
        public TransactionBehaviour(DbContext context,
            ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(DbContext));
            _logger = logger ?? throw new ArgumentException(nameof(Microsoft.Extensions.Logging.ILogger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetGenericTypeName();

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

                _currentTransaction = await this._dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

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
                    await this._dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await RollbackTransaction();
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

            async Task RollbackTransaction()
            {
                try
                {
                    await _currentTransaction?.RollbackAsync();
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