using Colosoft.Input;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Validation
{
    public abstract class PatchValidator<T> : AbstractValidator<T>
        where T : IPatchInput
    {
        public override Task<ValidationResult> ValidateAsync(ValidationContext<T> context, CancellationToken cancellation = default)
        {
            context = new ValidationContext<T>(
                context.InstanceToValidate,
                context.PropertyChain,
                new ValidatorSelector(context.InstanceToValidate));

            return base.ValidateAsync(context, cancellation);
        }

        private sealed class ValidatorSelector : IValidatorSelector
        {
            private readonly IPatchInput patchInput;

            public ValidatorSelector(IPatchInput patchInput)
            {
                this.patchInput = patchInput;
            }

            public bool CanExecute(IValidationRule rule, string propertyPath, IValidationContext context) =>
                string.IsNullOrEmpty(propertyPath) || this.patchInput.GetChanges().Contains(propertyPath);
        }
    }
}
