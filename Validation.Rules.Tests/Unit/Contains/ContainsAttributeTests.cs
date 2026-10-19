using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;
using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.Contains;

[TestFixture]
public class ContainsAttributeTests
{
    public static List<object> TestCases = [DictionaryStubs.Default, StringStubs.CONTAINS, EnumerableStubs.ContainsEnumeration];

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        ContainsAttribute attribute         = new("test", "blah test");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        ContainsAttribute attribute         = new("test", "blah test");
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ThrowArgumentException_When_ExpectedIsEmpty()
    {
        ContainsAttribute attribute         = new();
        ValidationContext validationContext = new(new());

        Assert.Throws<ArgumentException>(() => attribute.GetValidationResult(DictionaryStubs.Default, validationContext));
    }

    [TestCaseSource(typeof(ContainsAttributeTests), nameof(ContainsAttributeTests.TestCases))]
    public void Should_ReturnSuccess_When_IsCorrectValue(object value)
    {
        ContainsAttribute attribute         = new("test", "blah");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCaseSource(typeof(ContainsAttributeTests), nameof(ContainsAttributeTests.TestCases))]
    public void Should_ReturnValidationError_When_IsIncorrectValue(object value)
    {
        ContainsAttribute attribute         = new("not", "value") { ErrorMessage = "The {0} must contains: {1}." };
        ValidationContext validationContext = new(new()) { DisplayName           = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must contains: \"not\", \"value\"."));
    }
}
