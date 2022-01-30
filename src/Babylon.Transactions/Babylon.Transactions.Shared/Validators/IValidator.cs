using Babylon.Transactions.Shared.Notifications;

namespace Babylon.Transactions.Shared.Validators
{
    public interface IValidator<in T>
    {
        Result Validate(T objectToValidate);
    }
}