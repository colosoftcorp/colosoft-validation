using FluentValidation;

namespace Colosoft.Validation.Test
{
    internal class TestValidator : InlineValidator<Person>
    {
        public TestValidator()
        {
        }

        public TestValidator(params Action<TestValidator>[] actions)
        {
            foreach (var action in actions)
            {
                action(this);
            }
        }
    }
}
