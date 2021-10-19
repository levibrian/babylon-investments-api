using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Ivas.Common.DependencyInjection
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

            servicesToInject.ForEach(x =>
            {
                var matchingInterface = x.GetInterfaces()
                                         .FirstOrDefault(i => i.Name.Contains(x.Name));

                serviceDescriptors.AddScoped(matchingInterface, x);
            });
        }
    }
}
