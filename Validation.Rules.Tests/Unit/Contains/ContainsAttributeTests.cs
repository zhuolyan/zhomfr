using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;
using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.Contains;

[TestFixture]
public class ContainsAttributeTests
{
    public static readonly List<object> TestCases = [DictionaryStubs.Default, StringStubs.CONTAINS, EnumerableStubs.ContainsEnumeration];

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        ContainsAttribute attribute         = new("test", "blah test");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        ContainsAttribute attribute         = new("test", "blah test");
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void ShouldThrow_When_ExpectedIsEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            ContainsAttribute containsAttribute = new();
        });
    }

    [TestCaseSource(typeof(ContainsAttributeTests), nameof(ContainsAttributeTests.TestCases))]
    public void Should_ReturnSuccess_When_IsCorrectValue(object input)
    {
        ContainsAttribute attribute         = new("test", "blah");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCaseSource(typeof(ContainsAttributeTests), nameof(ContainsAttributeTests.TestCases))]
    public void Should_ReturnValidationError_When_IsInvalidValue(object input)
    {
        ContainsAttribute attribute         = new("not", "value") { ErrorMessage = "The {0} must contains: {1}." };
        ValidationContext validationContext = new(new()) { DisplayName           = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must contains: \"not\", \"value\"."));
    }
}
