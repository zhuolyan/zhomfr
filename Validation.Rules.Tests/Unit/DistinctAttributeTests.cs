using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class DistinctAttributeTests
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsDictionaryAndCorrect()
    {
        DistinctAttribute attribute = new();

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Dictionary, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsDictionaryAndNotCorrect()
    {
        DistinctAttribute attribute = new() { ErrorMessage = "The {0} must be distinct." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.NotDistinctDictionary, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be distinct."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsCollectionAndCorrect()
    {
        DistinctAttribute attribute = new();

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Collection, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsCollectionAndNotCorrect()
    {
        DistinctAttribute attribute = new() { ErrorMessage = "The {0} must be distinct." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.NotDistinctCollection, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be distinct."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        DistinctAttribute attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsUnhandledType()
    {
        DistinctAttribute attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(123, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The field Object is invalid."));
    }
}
