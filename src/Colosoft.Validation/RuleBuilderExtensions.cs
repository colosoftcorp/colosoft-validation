using Colosoft.Input;
using FluentValidation;
using FluentValidation.Internal;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Validation
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptionsConditions<T, TProperty> CustomValidatePropertyChangedAsync<T, TProperty, TPropertyValue>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            System.Linq.Expressions.Expression<Func<T, TPropertyValue>> property,
            Func<TProperty, TPropertyValue, ValidationContext<T>, CancellationToken, Task> action)
            where TProperty : IPatchInput
        {
            var propertyName = property.GetMember().Name;
            return ruleBuilder.CustomAsync((instance, context, cancellationToken) =>
            {
                if (instance.GetChanges().Contains(propertyName))
                {
                    var member = property.GetMember() as System.Reflection.PropertyInfo;
                    var value2 = (TPropertyValue)member.GetValue(instance);
                    return action(instance, value2, context, cancellationToken);
                }

                return Task.CompletedTask;
            });
        }
    }
}
