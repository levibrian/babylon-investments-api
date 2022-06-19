using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Babylon.Investments.Domain.Contracts.Repositories.Base;

namespace Babylon.Investments.Persistency.Repositories.Base
{
    public abstract class DynamoRepository<T> : IRepositoryAsync<T> where T : class, new()
    {
        protected readonly string TableName;
        
        private readonly IDynamoDBContext _dynamoDbContext;

        private readonly DynamoDBOperationConfig _dynamoDbOperationConfig;
        
        protected DynamoRepository(
            string tableName, 
            IDynamoDBContext dynamoDbContext)
        {
            TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
            _dynamoDbContext = dynamoDbContext ?? throw new ArgumentNullException(nameof(dynamoDbContext));

            _dynamoDbOperationConfig = new DynamoDBOperationConfig()
            {
                OverrideTableName = tableName,
                TableNamePrefix = string.Empty
            };
        }

        public async Task<IEnumerable<T>> LoadAsync()
        {
            return await _dynamoDbContext
                .ScanAsync<T>(new List<ScanCondition>(), _dynamoDbOperationConfig)
                .GetRemainingAsync();
        }

        public async Task<IEnumerable<T>> QueryAsync(object key)
        {
            return await _dynamoDbContext
                .QueryAsync<T>(key, _dynamoDbOperationConfig)
                .GetRemainingAsync();
        }

        public async Task<T> QuerySingleAsync(object key, object sortKey)
        {
            return await _dynamoDbContext
                .LoadAsync<T>(key, sortKey, _dynamoDbOperationConfig);
        }

        public async Task SaveAsync(T entity)
        {
            await _dynamoDbContext.SaveAsync<T>(entity, _dynamoDbOperationConfig);
        }

        public async Task SaveAsync(IEnumerable<T> entities)
        {
            var batchProcess = _dynamoDbContext.CreateBatchWrite<T>(_dynamoDbOperationConfig);
            
            batchProcess.AddPutItems(entities);

            await batchProcess.ExecuteAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            await _dynamoDbContext.DeleteAsync<T>(entity, _dynamoDbOperationConfig);
        }

        public async Task DeleteAsync(IEnumerable<T> entities)
        {
            var batchProcess = _dynamoDbContext.CreateBatchWrite<T>(_dynamoDbOperationConfig);
            
            batchProcess.AddDeleteItems(entities);

            await batchProcess.ExecuteAsync();
        }
    }
}