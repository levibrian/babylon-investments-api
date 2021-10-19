using Ivas.Common.DependencyInjection;
using Ivas.Persistency.UnitOfWork;
using Ivas.Persistency.UnitOfWork.Interfaces;
using Ivas.Transactions.Persistency.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ivas.Transactions.Injection.Injector
{
    public static class ServiceInjector
    {
        public static ServiceProvider Configure(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            RegisterCoreServices(serviceDescriptors);

            RegisterCoreValidators(serviceDescriptors);
            
            RegisterMapper(serviceDescriptors);

            RegisterContext<IvasTransactionsDbContext>(serviceDescriptors, configuration);

            RegisterPersistency<IvasTransactionsDbContext>(serviceDescriptors);

            return serviceDescriptors.BuildServiceProvider();
        }

        private static void RegisterCoreServices(IServiceCollection serviceDescriptors)
        {
            InjectionHelper.Register(serviceDescriptors, "Ivas.Transactions.Core", "Service");
        }

        private static void RegisterCoreValidators(IServiceCollection serviceDescriptors)
        {
            InjectionHelper.Register(serviceDescriptors, "Ivas.Transactions.Core", "Validator");
        }

        private static void RegisterMapper(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddAutoMapper(Assembly.Load("Ivas.Transactions.Core"));
        }

        private static void RegisterContext<TContext>(IServiceCollection serviceDescriptors, IConfiguration configuration) where TContext : DbContext
        {
            serviceDescriptors.AddDbContext<TContext>(options => options.UseSqlServer(configuration.GetConnectionString("IvasTransactionsDbContext")));
        }

        private static void RegisterPersistency<TContext>(IServiceCollection serviceDescriptors) where TContext : DbContext
        {
            serviceDescriptors.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            
            serviceDescriptors.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();
        }
    }
}
