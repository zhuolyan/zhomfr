using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class InAttributeTests
{
    [Test]
    public void Should_ReturnSuccess_When_StringIsCorrect()
    {
        InAttribute       attribute         = new("apple", "banana");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult("banana", validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnSuccess_When_NumberIsCorrect()
    {
        InAttribute       attribute         = new(1, 2, 3);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(2, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnSuccess_When_CollectionIsCorrect()
    {
        InAttribute       attribute         = new("test", "blah");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.NotDistinctCollection, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnSuccess_When_EnumerableIsCorrect()
    {
        InAttribute       attribute         = new("first", "second", "third");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.NotDistinctEnumeration, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnSuccess_When_DictionaryIsCorrect()
    {
        InAttribute       attribute         = new("test", "not", "blah");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Dictionary, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_StringIsNotCorrect()
    {
        InAttribute attribute = new("apple", "banana") { ErrorMessage = "The {0} must be one of: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult("One", validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be one of: \"apple\", \"banana\"."));
    }

    [Test]
    public void Should_RReturnValidationError_When_NumberIsNotCorrect()
    {
        InAttribute attribute = new(1, 2, 3) { ErrorMessage = "The {0} must be one of: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(4, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be one of: \"1\", \"2\", \"3\"."));
    }

    [Test]
    public void Should_ReturnValidationError_When_CollectionIsNotCorrect()
    {
        InAttribute attribute = new("test", "blah") { ErrorMessage = "The {0} must be one of: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Collection, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be one of: \"test\", \"blah\"."));
    }

    [Test]
    public void Should_ReturnValidationError_When_EnumerationIsNotCorrect()
    {
        InAttribute attribute = new("test", "blah") { ErrorMessage = "The {0} must be one of: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.NotDistinctEnumeration, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be one of: \"test\", \"blah\"."));
    }

    [Test]
    public void Should_ReturnValidationError_When_DictionaryIsNotCorrect()
    {
        InAttribute attribute = new("test", "blah") { ErrorMessage = "The {0} must be one of: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Dictionary, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be one of: \"test\", \"blah\"."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        InAttribute       attribute         = new("test", "blah");
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsUnhandledType()
    {
        InAttribute       attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(new SizeValidationModel(), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
    }
}
