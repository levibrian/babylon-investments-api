using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Babylon.Investments.Domain.Cryptography;
using Babylon.Investments.Domain.Factories;
using Babylon.Investments.Domain.Mappers;
using Babylon.Investments.Domain.Services;
using Babylon.Investments.Domain.Strategies;
using Babylon.Investments.Domain.Validators;
using Babylon.Networking.Brokers;
using Babylon.Networking.Interfaces.Brokers;
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
                .AddTransient<IOperationFactory, BuyOperationFactory>()
                .AddTransient<IOperationFactory, SellOperationFactory>()
                .AddTransient<ITransactionValidator, TransactionValidator>()
                .AddTransient<IOperationValidator, OperationValidator>()
                .AddTransient<IOperationStrategy, OperationStrategy>()
                .AddTransient<ITransactionService, TransactionService>()
                .AddTransient<ITransactionsInBulkService, TransactionsInBulkService>()
                .AddTransient<IPortfolioService, PortfolioService>();
        }

        private static IServiceCollection RegisterOther(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<IAesCipher, AesCipher>()
                .AddTransient<IFinancialsBroker, FinancialsBroker>();
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