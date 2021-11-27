using Ivas.Transactions.Domain.Services;
using Ivas.Transactions.Domain.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Ivas.Transactions.Domain.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection RegisterDomainDependencies(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<ITransactionValidator, TransactionValidator>()
                .AddTransient<ITransactionService, TransactionService>();
        }
    }
}