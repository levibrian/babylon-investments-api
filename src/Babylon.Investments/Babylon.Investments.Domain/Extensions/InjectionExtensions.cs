using System.Collections.Generic;
using AutoMapper;
using Babylon.Investments.Domain.Cryptography;
using Babylon.Investments.Domain.Mappers;
using Babylon.Investments.Domain.Services;
using Babylon.Investments.Domain.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Babylon.Investments.Domain.Extensions
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