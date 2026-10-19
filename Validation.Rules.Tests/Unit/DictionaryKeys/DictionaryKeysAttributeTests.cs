using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;
using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.DictionaryKeys;

[TestFixture]
public class DictionaryKeysAttributeTests
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsCorrect()
    {
        DictionaryKeysAttribute attribute = new("first", "second");

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(DictionaryStubs.Default, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect()
    {
        DictionaryKeysAttribute attribute = new("first", "test") { ErrorMessage = "The {0} keys must contains: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(DictionaryStubs.Default, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp keys must contains: \"first\", \"test\"."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        DictionaryKeysAttribute attribute         = new("first", "test");
        ValidationContext       validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        DictionaryKeysAttribute attribute         = new("test", "blah test");
        ValidationContext       validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ThrowArgumentException_When_ExpectedIsEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            DictionaryKeysAttribute containsAttribute = new();
        });
    }
}
