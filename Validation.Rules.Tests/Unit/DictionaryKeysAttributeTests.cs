using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class DictionaryKeysAttributeTests
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsCorrect()
    {
        DictionaryKeysAttribute attribute = new("first", "second");

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Dictionary, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect()
    {
        DictionaryKeysAttribute attribute = new("first", "test") { ErrorMessage = "The {0} keys must contains: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Dictionary, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp keys must contains: \"first\", \"test\"."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        DictionaryKeysAttribute attribute         = new();
        ValidationContext       validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsUnhandledType()
    {
        DictionaryKeysAttribute attribute         = new("test", "blah test");
        ValidationContext       validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(123, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The field Object is invalid."));
    }
}
