using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules;

/// <summary>
///     The field under validation must be a collection or string that does not contain any of the given parameter
///     values.
/// </summary>
/// <param name="values">The seek values.</param>
/// <example>
///     <code>
///         public class DoesntContainDto
///         {
///             [DoesntContain("input", "sharp")]
///             public IDictionary DictionaryInvalid = new Dictionary<string, string>
///             { { "first", "input" }, { "second",
///             "not" }, { "third", "blah" } }; // ValidationError [DoesntContain("input", "sharp")] public IDictionary
///             DictionaryValid = new Dictionary
///             <string, string>
///                 { { "first", "test" }, { "second", "not" }, { "third", "blah" } }; // Success
///                 [DoesntContain(4, 5)] public IEnumerable EnumerableInvalid = new List
///                 <int>
///                     { 1, 2, 3 }; // ValidationError [DoesntContain(1, 2)] public IEnumerable EnumerableValid = new
///                     List
///                     <int>
///                         { 1, 2, 3 }; // Success [DoesntContain("some")] public string StringInvalid = "something"; //
///                         ValidationError [DoesntContain("end", "start")] public string StringValid = "something"; //
///                         Success }
///         
///     </code>
/// </example>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DoesntContainAttribute(params object[] values) : ContainsAttribute(values)
{
    /// <inheritdoc/>
    protected override bool CheckCondition(object value, ValidationContext context)
    {
        return !base.CheckCondition(value, context);
    }
}
