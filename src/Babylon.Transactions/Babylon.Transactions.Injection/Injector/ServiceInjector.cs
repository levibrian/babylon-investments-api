using Babylon.Transactions.Domain.Extensions;
using Babylon.Transactions.Persistency.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Babylon.Transactions.Injection.Injector
{
    public static class ServiceInjector
    {
        public static ServiceProvider Configure(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            return serviceDescriptors
                .RegisterDomainDependencies()
                .RegisterPersistencyDependencies(configuration)
                .BuildServiceProvider();
        }
    }
}