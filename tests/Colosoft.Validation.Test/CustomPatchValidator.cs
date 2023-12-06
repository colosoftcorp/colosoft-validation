using FluentValidation;

namespace Colosoft.Validation.Test
{
    internal class CustomPatchValidator : PatchValidator<PersonWithPathInput>
    {
        public CustomPatchValidator()
        {
        }

        public CustomPatchValidator(params Action<CustomPatchValidator>[] actions)
        {
            foreach (var action in actions)
            {
                action(this);
            }
        }
    }
}
