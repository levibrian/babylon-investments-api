using System.Reflection;
using Ivas.Transactions.Core.Abstractions.Services;
using Ivas.Transactions.Core.Services;
using Ivas.Transactions.Domain.Mappers;
using Ivas.Transactions.Injection.Helpers;
using Ivas.Transactions.Persistency.Abstractions.UnitOfWork;
using Ivas.Transactions.Persistency.Abstractions.UnitOfWork.Interfaces;
using Ivas.Transactions.Persistency.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ivas.Transactions.Injection.Injector
{
    public static class ServiceInjector
    {
        public static ServiceProvider Configure(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            RegisterCoreServices(serviceDescriptors);

            RegisterMapper(serviceDescriptors);
            
            RegisterContext<TransactionsDbContext>(serviceDescriptors, configuration);
            
            RegisterPersistency<TransactionsDbContext>(serviceDescriptors);
            
            return serviceDescriptors.BuildServiceProvider();
        }

        private static void RegisterCoreServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<ITransactionCreateService, TransactionCreateService>();
            // InjectionHelper.Register(serviceDescriptors, "Ivas.Transactions.Core.Abstractions", "Service");
        }

        private static void RegisterMapper(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddAutoMapper(config => config.AddProfile(new TransactionCreateProfile()));
        }
        
        private static void RegisterContext<TContext>(IServiceCollection serviceDescriptors, IConfiguration configuration) where TContext : DbContext
        {
            serviceDescriptors.AddDbContext<TContext>();
        }
        
        private static void RegisterPersistency<TContext>(IServiceCollection serviceDescriptors) where TContext : DbContext
        {
            serviceDescriptors.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            
            serviceDescriptors.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();
        }
    }
}