using System.Collections.Generic;
using AutoMapper;
using Babylon.Transactions.Domain.Cryptography;
using Babylon.Transactions.Domain.Mappers;
using Babylon.Transactions.Domain.Services;
using Babylon.Transactions.Domain.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Babylon.Transactions.Domain.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection RegisterDomainDependencies(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .RegisterServices()
                .RegisterOther()
                .RegisterMappers();

        }

        private static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<ITransactionValidator, TransactionValidator>()
                .AddTransient<ITransactionService, TransactionService>()
                .AddTransient<ITransactionsInBulkService, TransactionsInBulkService>()
                .AddTransient<IPortfolioService, PortfolioService>();
        }

        private static IServiceCollection RegisterOther(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<IAesCipher, AesCipher>();
        }
        
        private static IServiceCollection RegisterMappers(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddAutoMapper(config => 
                    config.AddProfiles(new List<Profile>()
                    {
                        new TransactionProfile(),
                        new PositionSummaryProfile()
                    }));
        }
    }
}