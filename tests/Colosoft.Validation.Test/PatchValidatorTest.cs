using FluentValidation;

namespace Colosoft.Validation.Test
{
    public class PatchValidatorTest
    {
        [Fact]
        public void InvalidChange()
        {
            var validator = new CustomPatchValidator(
                f => f.RuleFor(f => f.Name).NotEmpty());

            var person = new PersonWithPathInput();
            person.AddChange(nameof(PersonWithPathInput.Name));

            var result = validator.Validate(person);

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task CustomValidatePropertyChangedAsync()
        {
            var validator = new CustomPatchValidator(
                f => f.RuleFor(f => f)
                    .CustomValidatePropertyChangedAsync(
                        f => f.Name,
                        (instance, value, context, cancellationToken) =>
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                context.AddFailure("Name is empty");
                            }

                            return Task.CompletedTask;
                        }));

            var person = new PersonWithPathInput();
            person.AddChange(nameof(PersonWithPathInput.Name));

            var result = await validator.ValidateAsync(person, default);

            Assert.False(result.IsValid);
        }
    }
}