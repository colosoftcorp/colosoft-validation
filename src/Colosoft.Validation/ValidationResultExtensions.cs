using FluentValidation;
using FluentValidation.Results;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Colosoft.Validation
{
    public static class ValidationResultExtensions
    {
        private static string Compose(ValidationResult resultado) =>
            string.Join(Environment.NewLine, resultado.Errors.Select(f => f.ErrorMessage));

        public static void ThrowOnFail(this ValidationResult validationResult)
        {
            if (validationResult != null && !validationResult.IsValid)
            {
                throw new ValidationException(Compose(validationResult));
            }
        }

        public static async Task ThrowOnFail(this Task<ValidationResult> validationResult)
        {
            (await validationResult).ThrowOnFail();
        }
    }
}
