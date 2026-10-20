using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.Required;

[TestFixture]
public class RequiredAttributeTests
{
    public static readonly List<object?> WrongValues = [null, string.Empty, "   ", DictionaryStubs.Empty, EnumerableStubs.GetEnumerable(0)];
    public static readonly List<object?> Values      = ["test", DictionaryStubs.Default, EnumerableStubs.GetEnumerable(1)];

    [TestCaseSource(typeof(RequiredAttributeTests), nameof(RequiredAttributeTests.WrongValues))]
    public void Should_ReturnValidationError_When_ValueIsEmpty(object? input)
    {
        RequiredAttribute attribute         = new() { ErrorMessage     = "The {0} is required." };
        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp is required."));
    }

    [TestCaseSource(typeof(RequiredAttributeTests), nameof(RequiredAttributeTests.Values))]
    public void Should_ReturnSuccess_When_ValueIsNotEmpty(object? input)
    {
        RequiredAttribute attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }
}
