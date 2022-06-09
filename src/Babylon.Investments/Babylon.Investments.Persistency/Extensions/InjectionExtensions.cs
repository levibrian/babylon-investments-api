using System;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoMapper;
using Babylon.Investments.Domain.Constants;
using Babylon.Investments.Domain.Contracts.Repositories;
using Babylon.Investments.Persistency.Mappers;
using Babylon.Investments.Persistency.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Babylon.Investments.Persistency.Extensions
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
            var InvestmentsTableName = 
                Environment.GetEnvironmentVariable(EnvironmentVariables.InvestmentsDynamoDbTable);

            return serviceCollection
                .AddTransient<ITransactionRepository>(s => 
                    new TransactionRepository(
                        InvestmentsTableName,
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