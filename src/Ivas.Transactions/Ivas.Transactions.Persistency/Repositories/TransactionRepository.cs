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

        public async Task InsertInBulk(IEnumerable<Transaction> transactionsToInsert)
        {
            _logger.LogInformation($"Saving Transaction into DynamoDB Table: { _tableName }");

            var transactionEntities = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionEntity>>(transactionsToInsert);

            await SaveAsync(transactionEntities);
            
            _logger.LogInformation("Successfully saved transaction into DynamoDB..");
        }
        
        public async Task Delete(Transaction transaction)
        {
            _logger.LogInformation($"Deleting Transaction with TransactionId: {transaction.TransactionId} from DynamoDB Table: { _tableName }");
            
            var transactionEntity = _mapper.Map<Transaction, TransactionEntity>(transaction);

            await DeleteAsync(transactionEntity);
            
            _logger.LogInformation("Successfully deleted transaction from DynamoDB..");
        }

        public async Task DeleteInBulk(IEnumerable<Transaction> transactionsToDelete)
        {
            _logger.LogInformation($"Deleting Transactions in Bulk from DynamoDB Table: { _tableName }");

            var transactionEntities = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionEntity>>(transactionsToDelete);

            await DeleteAsync(transactionEntities);
            
            _logger.LogInformation("Successfully deleted in bulk transactions from DynamoDB..");
        }

        public async Task<IEnumerable<Transaction>> GetByClientAsync(string clientIdentifier)
        {
            _logger.LogInformation($"Getting all transactions from user: {clientIdentifier}");
            
            var userTransactions = await QueryAsync(clientIdentifier);

            _logger.LogInformation($"Successfully fetched all transactions of user {clientIdentifier}");
            
            return _mapper
                .Map<IEnumerable<TransactionEntity>, IEnumerable<Transaction>>(userTransactions);
        }

        public async Task<Transaction> GetByIdAsync(string clientIdentifier, string transactionId)
        {
            _logger.LogInformation($"Getting single transaction for: {clientIdentifier} with TransactionId: {transactionId}");
            
            var transaction = await QuerySingleAsync(clientIdentifier, transactionId);

            _logger.LogInformation($"Successfully fetched transaction {transactionId}..");
            
            return _mapper
                .Map<TransactionEntity, Transaction>(transaction);
        }
    }
}