using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class ActiveUrlAttributeTests
{
    [TestCase("google.com")]
    [TestCase("https://microsoft.com")]
    [TestCase("[::1]")]
    [TestCase("https://[2001:4860:4860::8888]")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string url)
    {
        ActiveUrlAttribute attribute         = new();
        ValidationContext  validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(url, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("non-existent-domain-123456789.com")]
    [TestCase("not-a-url")]
    public void Should_ReturnValidationError_When_ValueIsInvalid(string url)
    {
        ActiveUrlAttribute attribute = new() { ErrorMessage = "The {0} must be real url." };

        ValidationContext validationContext = new(new()) { DisplayName = "Profile Url" };

        ValidationResult? result = attribute.GetValidationResult(url, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The Profile Url must be real url."));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        ActiveUrlAttribute attribute         = new();
        ValidationContext  validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        ActiveUrlAttribute attribute         = new();
        ValidationContext  validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }
}
