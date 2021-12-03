using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using AutoMapper;
using Ivas.Transactions.Domain.Contracts.Repositories;
using Ivas.Transactions.Domain.Objects;
using Ivas.Transactions.Persistency.Entities;
using Ivas.Transactions.Persistency.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace Ivas.Transactions.Persistency.Repositories
{
    public class TransactionRepository : DynamoRepository<TransactionEntity>, ITransactionRepository
    {
        private readonly IMapper _mapper;

        private readonly ILogger<TransactionRepository> _logger;
        
        public TransactionRepository(
            string tableName, 
            IDynamoDBContext dynamoDbContext,
            IMapper mapper,
            ILogger<TransactionRepository> logger) : base(tableName, dynamoDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Insert(Transaction transaction)
        {
            _logger.LogInformation($"Saving Transaction into DynamoDB Table: { _tableName }");

            try
            {
                var transactionEntity = _mapper.Map<Transaction, TransactionEntity>(transaction);

                await SaveAsync(transactionEntity);
            }
            catch (Exception e)
            {
                _logger.LogError("Error during saving transaction into dynamo db", e);
                
                throw;
            }
            
            _logger.LogInformation("Successfully saved transaction into DynamoDB..");
        }

        public async Task Delete(Transaction transaction)
        {
            var transactionEntity = _mapper.Map<Transaction, TransactionEntity>(transaction);

            await DeleteAsync(transactionEntity);
        }

        public async Task<IEnumerable<Transaction>> GetByUserAsync(long userId)
        {
            var userTransactions = await QueryAsync(userId);

            return _mapper
                .Map<IEnumerable<TransactionEntity>, IEnumerable<Transaction>>(userTransactions);
        }

        public async Task<Transaction> GetByIdAsync(long userId, string transactionId)
        {
            var transaction = await QuerySingleAsync(userId, transactionId);

            return _mapper
                .Map<TransactionEntity, Transaction>(transaction);
        }
    }
}