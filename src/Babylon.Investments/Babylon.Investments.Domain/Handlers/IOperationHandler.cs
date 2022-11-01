using System.Threading.Tasks;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Domain.Objects.Base;

namespace Babylon.Investments.Domain.Handlers
{
    public interface IOperationHandler
    {
         new Task<Transaction> HandleAsync(TransactionPostRequest request);
    }
}