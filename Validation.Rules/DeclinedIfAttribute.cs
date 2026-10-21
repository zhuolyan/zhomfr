using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be "no", "off", 0, "0", false, or "false" if another field under validation is
///     equal to a specified value.
/// </summary>
/// <target-type>bool,string,int</target-type>
/// <example>
///     <code>
///         public class DeclinedIfDto
///         {
///             public string ExpectedField = "expected value";
///         
///             [DeclinedIf&lt;string&gt;("ExpectedField", "expected value")]
///             public bool BoolInvalid = true; // ValidationError
///         
///             [DeclinedIf&lt;string&gt;("ExpectedField", "expected value")]
///             public bool BoolValid = false; // Success
///         
///             [DeclinedIf&lt;string&gt;("ExpectedField", "expected value")]
///             public int IntInvalid = 1; // ValidationError
///         
///             [DeclinedIf&lt;string&gt;("ExpectedField", "expected value")]
///             public int IntValid = 0; // Success
///         
///             [DeclinedIf&lt;string&gt;("ExpectedField", "expected value")]
///             public string StringInvalid = "something"; // ValidationError
///         
///             [DeclinedIf&lt;string&gt;("ExpectedField", "expected value")]
///             public string StringValid = "0"; // Success
///         }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DeclinedIfAttribute<T>(string anotherField, string fixedValue) : AbstractRule where T : IEquatable<T>, IParsable<T>
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        T? anotherValue = AbstractRule.GetFieldValue<T>(anotherField, context);
        T  expected     = T.Parse(fixedValue, CultureInfo.InvariantCulture);

        return !expected.Equals(anotherValue) || value is false or 0 or "no" or "off" or "0" or "false";
    }
}
