using Babylon.Investments.Shared.Notifications;

namespace Babylon.Investments.Shared.Validators
{
    public interface IValidator<in T>
    {
        Result Validate(T objectToValidate);
    }
}