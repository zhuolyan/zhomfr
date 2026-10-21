using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be "yes", "on", 1, "1", true, or "true" if another field under validation is
///     equal to a specified value. This is useful for validating "Terms of Service" acceptance or similar fields.
/// </summary>
/// <example>
///     <code>
///         public class AcceptedIfDto
///         {
///             [AcceptedIf<string>
///             ("ExpectedField", "expected value")] public bool BoolInvalid = false; // ValidationError
///             [AcceptedIf
///             <string>
///                 ("ExpectedField", "another value")] public bool BoolNotValidateInvalid = false; // Success
///                 [AcceptedIf
///                 <string>
///                     ("ExpectedField", "another value")] public bool BoolNotValidateValid = true; // Success
///                     [AcceptedIf
///                     <string>
///                         ("ExpectedField", "expected value")] public bool BoolValid = true; // Success public string
///                         ExpectedField = "expected value"; [AcceptedIf
///                         <string>
///                             ("ExpectedField", "expected value")] public int IntInvalid = 0; // ValidationError
///                             [AcceptedIf
///                             <string>
///                                 ("ExpectedField", "another value")] public int IntNotValidateInvalid = 0; //
///                                 Success [AcceptedIf
///                                 <string>
///                                     ("ExpectedField", "another value")] public int IntNotValidateValid = 1; //
///                                     Success [AcceptedIf
///                                     <string>
///                                         ("ExpectedField", "expected value")] public int IntValid = 1; // Success
///                                         [AcceptedIf
///                                         <string>
///                                             ("ExpectedField", "expected value")] public string StringInvalid =
///                                             "something"; // ValidationError [AcceptedIf
///                                             <string>
///                                                 ("ExpectedField", "another value")] public string
///                                                 StringNotValidateInvalid = "something"; // Success [AcceptedIf
///                                                 <string>
///                                                     ("ExpectedField", "another value")] public string
///                                                     StringNotValidateValid = "yes"; // Success [AcceptedIf
///                                                     <string>
///                                                         ("ExpectedField", "expected value")] public string
///                                                         StringValid = "yes"; // Success }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class AcceptedIfAttribute<T>(string anotherField, string fixedValue) : AbstractRule where T : IEquatable<T>, IParsable<T>
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        T? anotherValue = AbstractRule.GetFieldValue<T>(anotherField, context);
        T  expected     = T.Parse(fixedValue, CultureInfo.InvariantCulture);

        return !expected.Equals(anotherValue) || value is "yes" or "on" or "1" or "true" or 1 or true;
    }
}
