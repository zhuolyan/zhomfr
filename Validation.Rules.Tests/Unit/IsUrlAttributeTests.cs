using System.ComponentModel.DataAnnotations;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class IsUrlAttributeTests
{
    [TestCase("http://example.com")]
    [TestCase("https://google.com/search?q=test")]
    [TestCase("ftp://files.org")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string value)
    {
        IsUrlAttribute    attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    [TestCase("https://example.com", "https")]
    [TestCase("http://example.com", "http", "https")]
    public void Should_ReturnSuccess_When_ValueForProtocolsIsCorrect(string value, params string[] protocols)
    {
        IsUrlAttribute    attribute         = new(protocols);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    [TestCase("http://example.com", "https")]
    [TestCase("ftp://example.com", "http", "https")]
    public void Should_ReturnValidationError_When_ValueForProtocolsIsNotCorrect(string value, params string[] protocols)
    {
        IsUrlAttribute attribute = new(protocols) { ErrorMessage = "The {0} not a valid url." };

        ValidationContext validationContext = new(new()) { DisplayName = "Url" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The Url not a valid url."));
    }

    [Test]
    [TestCase("not-a-url")]
    [TestCase("example.com")]
    [TestCase("://example.com")]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect(string value)
    {
        IsUrlAttribute attribute = new() { ErrorMessage = "The {0} not a valid url." };

        ValidationContext validationContext = new(new()) { DisplayName = "Url" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The Url not a valid url."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        IsUrlAttribute    attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsUnhandledType()
    {
        IsUrlAttribute    attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(12, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The field Object is invalid."));
    }
}
