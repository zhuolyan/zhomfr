using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must have a matching field of {field}Confirmation. For example, if the field under
///     validation is password, a matching passwordConfirmation field must be present in the input.You may also pass a
///     custom confirmation field name. For example, repeat_username will expect the field repeat_username to match the
///     field under validation.
/// </summary>
/// <param name="anotherFieldName"></param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class ConfirmedAttribute(string? anotherFieldName = null) : AbstractRule
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        throw new NotImplementedException();
    }
}
