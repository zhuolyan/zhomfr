using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class NotNotInAttribute
{
    [Test]
    public void Should_ReturnSuccess_When_StringIsCorrect()
    {
        NotInAttribute    attribute         = new("apple", "banana");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult("test", validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnSuccess_When_NumberIsCorrect()
    {
        NotInAttribute    attribute         = new(1, 2, 3);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(4, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnSuccess_When_CollectionIsCorrect()
    {
        NotInAttribute    attribute         = new("test", "blah");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Collection, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnSuccess_When_EnumerableIsCorrect()
    {
        NotInAttribute    attribute         = new("wrong", "another");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.NotDistinctEnumeration, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnSuccess_When_DictionaryIsCorrect()
    {
        NotInAttribute    attribute         = new("wrong", "another");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Dictionary, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_StringIsNotCorrect()
    {
        NotInAttribute attribute = new("apple", "banana") { ErrorMessage = "The {0} must not be one of: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult("apple", validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must not be one of: \"apple\", \"banana\"."));
    }

    [Test]
    public void Should_ReturnValidationError_When_NumberIsNotCorrect()
    {
        NotInAttribute attribute = new(1, 2, 3) { ErrorMessage = "The {0} must not be one of: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(3, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must not be one of: \"1\", \"2\", \"3\"."));
    }

    [Test]
    public void Should_ReturnValidationError_When_CollectionIsNotCorrect()
    {
        NotInAttribute attribute = new("test", "blah") { ErrorMessage = "The {0} must not be one of: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Collection, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must not be one of: \"test\", \"blah\"."));
    }

    [Test]
    public void Should_ReturnValidationError_When_EnumerationIsNotCorrect()
    {
        NotInAttribute attribute = new("test", "another") { ErrorMessage = "The {0} must not be one of: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.NotDistinctEnumeration, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must not be one of: \"test\", \"another\"."));
    }

    [Test]
    public void Should_ReturnValidationError_When_DictionaryIsNotCorrect()
    {
        NotInAttribute attribute = new("test", "another") { ErrorMessage = "The {0} must not be one of: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Dictionary, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must not be one of: \"test\", \"another\"."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        NotInAttribute    attribute         = new("test", "blah");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsUnhandledType()
    {
        NotInAttribute    attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(new SizeValidationModel(), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
    }
}
