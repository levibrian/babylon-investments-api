using Ivas.Transactions.Shared.Notifications;

namespace Ivas.Transactions.Shared.Validators
{
    public interface IValidator<in T>
    {
        Result Validate(T objectToValidate);
    }
}