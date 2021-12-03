using System;
using System.Net.Http;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using AutoMapper;
using Ivas.Transactions.Domain.Constants;
using Ivas.Transactions.Domain.Contracts.Repositories;
using Ivas.Transactions.Persistency.Mappers;
using Ivas.Transactions.Persistency.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Ivas.Transactions.Persistency.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection RegisterPersistencyDependencies(
            this IServiceCollection serviceCollection, 
            IConfiguration configuration)
        {
            return serviceCollection
                .RegisterRepositories()
                .RegisterMappers()
                .RegisterAwsDependencies(configuration);
        }

        private static IServiceCollection RegisterRepositories(
            this IServiceCollection serviceCollection)
        {
            var transactionsTableName = 
                Environment.GetEnvironmentVariable(EnvironmentVariables.TransactionsDynamoDbTable);

            return serviceCollection
                .AddTransient<ITransactionRepository>(s => 
                    new TransactionRepository(
                        transactionsTableName,
                        s.GetRequiredService<IDynamoDBContext>(),
                        s.GetRequiredService<IMapper>(),
                        s.GetRequiredService<ILogger<TransactionRepository>>()));
        }
        
        private static IServiceCollection RegisterMappers(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddAutoMapper(config => 
                    config.AddProfile(new TransactionProfile()));
        }

        private static IServiceCollection RegisterAwsDependencies(
            this IServiceCollection serviceCollection, 
            IConfiguration configuration)
        {
            return serviceCollection
                .AddDefaultAWSOptions(configuration.GetAWSOptions())
                .AddSingleton<IAmazonDynamoDB>(x => CreateDynamoDb(string.Empty))
                .AddTransient<IDynamoDBContext, DynamoDBContext>();
        }

        private static IAmazonDynamoDB CreateDynamoDb(string serviceUrl)
        {
            var clientConfig = 
                new AmazonDynamoDBConfig() {RegionEndpoint = RegionEndpoint.EUWest1};

            if (!string.IsNullOrEmpty(serviceUrl))
            {
                clientConfig.ServiceURL = serviceUrl;
            }

            return new AmazonDynamoDBClient(clientConfig);
        }
    }
}