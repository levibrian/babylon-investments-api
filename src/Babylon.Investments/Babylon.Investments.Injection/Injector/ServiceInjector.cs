using Babylon.Investments.Domain.Extensions;
using Babylon.Investments.Persistency.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Babylon.Investments.Injection.Injector
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