using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Babylon.Investments.Injection.Helpers
{
    public static class InjectionHelper
    {
        public static void Register(IServiceCollection serviceDescriptors, string assemblyName, string objectKeyWord)
        {
            var servicesToInject = Assembly.Load(assemblyName)
                .GetTypes()
                .Where(x => !string.IsNullOrEmpty(x.Namespace))
                .Where(x => x.IsClass)
                .Where(x => !x.IsAbstract)
                .Where(x => x.Name.Contains(objectKeyWord))
                .ToList();

            servicesToInject.ForEach(service =>
            {
                var matchingInterface = service.GetInterfaces()
                    .FirstOrDefault(i => i.Name.Contains(service.Name));

                serviceDescriptors.AddScoped(matchingInterface, service);
            });
        }
    }
}