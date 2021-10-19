using System.Reflection;
using Ivas.Transactions.Injection.Helpers;
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
            
            return serviceDescriptors.BuildServiceProvider();
        }

        private static void RegisterCoreServices(IServiceCollection serviceDescriptors)
        {
            InjectionHelper.Register(serviceDescriptors, "Ivas.Transactions.Core", "Service");
        }

        private static void RegisterMapper(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddAutoMapper(Assembly.Load("Ivas.Transactions.Core"));
        }
    }
}