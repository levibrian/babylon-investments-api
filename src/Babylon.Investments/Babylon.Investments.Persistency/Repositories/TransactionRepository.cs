using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using AutoMapper;
using Babylon.Investments.Domain.Contracts.Repositories;
using Babylon.Investments.Domain.Objects.Base;
using Babylon.Investments.Persistency.Entities;
using Babylon.Investments.Persistency.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace Babylon.Investments.Persistency.Repositories
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
            _logger.LogInformation($"Saving Transaction into DynamoDB Table: { TableName }");

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

        public async Task InsertInBulk(IEnumerable<Transaction> InvestmentsToInsert)
        {
            _logger.LogInformation($"Saving Transaction into DynamoDB Table: { TableName }");

            var transactionEntities = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionEntity>>(InvestmentsToInsert);

            await SaveAsync(transactionEntities);
            
            _logger.LogInformation("Successfully saved transaction into DynamoDB..");
        }
        
        public async Task Delete(Transaction transaction)
        {
            _logger.LogInformation($"Deleting Transaction with TransactionId: {transaction.TransactionId} from DynamoDB Table: { TableName }");
            
            var transactionEntity = _mapper.Map<Transaction, TransactionEntity>(transaction);

            await DeleteAsync(transactionEntity);
            
            _logger.LogInformation("Successfully deleted transaction from DynamoDB..");
        }

        public async Task DeleteInBulk(IEnumerable<Transaction> InvestmentsToDelete)
        {
            _logger.LogInformation($"Deleting Investments in Bulk from DynamoDB Table: { TableName }");

            var transactionEntities = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionEntity>>(InvestmentsToDelete);

            await DeleteAsync(transactionEntities);
            
            _logger.LogInformation("Successfully deleted in bulk Investments from DynamoDB..");
        }

        public async Task<IEnumerable<Transaction>> GetByTenantAsync(string tenantId)
        {
            _logger.LogInformation($"Getting all Investments from tenant: {tenantId}");
            
            var userInvestments = await QueryAsync(tenantId);

            _logger.LogInformation($"Successfully fetched all Investments of tenant: {tenantId}");
            
            return _mapper
                .Map<IEnumerable<TransactionEntity>, IEnumerable<Transaction>>(userInvestments);
        }

        public async Task<Transaction> GetByIdAsync(string tenantId, string transactionId)
        {
            _logger.LogInformation($"Getting single transaction for tenant: {tenantId} with TransactionId: {transactionId}");
            
            var transaction = await QuerySingleAsync(tenantId, transactionId);

            _logger.LogInformation($"Successfully fetched transaction {transactionId}..");
            
            return _mapper
                .Map<TransactionEntity, Transaction>(transaction);
        }
    }
}