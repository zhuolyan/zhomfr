using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class ContainsAttributeTests
{
    private static string[] Correct    => ["test", "not"];
    private static string[] NotCorrect => ["test", "blah test"];

    private const string ERROR_MESSAGE    = "The {0} must contains: {1}.";
    private const string EXPECTED_MESSAGE = "The MyProp must contains: \"test\", \"blah test\".";
    private const string DISPLAY_NAME     = "MyProp";

    [Test]
    public void Should_ReturnSuccess_When_ValueIsDictionaryAndCorrect()
    {
        ContainsAttribute attribute = new(ContainsAttributeTests.Correct);

        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Dictionary, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsDictionaryAndNotCorrect()
    {
        ContainsAttribute attribute = new(ContainsAttributeTests.NotCorrect) { ErrorMessage = ContainsAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContext = new(new()) { DisplayName = ContainsAttributeTests.DISPLAY_NAME };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Dictionary, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo(ContainsAttributeTests.EXPECTED_MESSAGE));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsCollectionAndCorrect()
    {
        ContainsAttribute attribute = new(ContainsAttributeTests.Correct);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Collection, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsCollectionAndNotCorrect()
    {
        ContainsAttribute attribute = new(ContainsAttributeTests.NotCorrect) { ErrorMessage = "The {0} must contains: {1}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Collection, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must contains: \"test\", \"blah test\"."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        ContainsAttribute attribute         = new(ContainsAttributeTests.NotCorrect);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnSuccess_When_ExpectedEmpty()
    {
        ContainsAttribute attribute = new();

        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(TestedCollections.Collection, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }
}
