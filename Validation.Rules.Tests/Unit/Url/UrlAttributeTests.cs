using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.Url;

[TestFixture]
public class UrlAttributeTests
{
    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        UrlAttribute      attribute         = new();
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        UrlAttribute      attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("http://example.com")]
    [TestCase("https://google.com/search?q=test")]
    [TestCase("ftp://files.org")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string value)
    {
        UrlAttribute      attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("https://example.com", "https")]
    [TestCase("http://example.com", "http", "https")]
    public void Should_ReturnSuccess_When_ValueForProtocolsIsCorrect(string value, params string[] protocols)
    {
        UrlAttribute      attribute         = new(protocols);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("http://example.com", "https")]
    [TestCase("ftp://example.com", "http", "https")]
    public void Should_ReturnValidationError_When_ValueForProtocolsIsNotCorrect(string value, params string[] protocols)
    {
        UrlAttribute attribute = new(protocols) { ErrorMessage = "The {0} not a valid url." };

        ValidationContext validationContext = new(new()) { DisplayName = "Url" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The Url not a valid url."));
    }

    [TestCase("not-a-url")]
    [TestCase("example.com")]
    [TestCase("://example.com")]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect(string value)
    {
        UrlAttribute attribute = new() { ErrorMessage = "The {0} not a valid url." };

        ValidationContext validationContext = new(new()) { DisplayName = "Url" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The Url not a valid url."));
    }
}
