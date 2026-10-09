using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class EmailAttributeTests
{
    [TestCase("test@google.com", EmailAttribute.Mode.Dns)]
    [TestCase("admin@microsoft.com", EmailAttribute.Mode.Dns)]
    [TestCase("test@example.com", EmailAttribute.Mode.Filter)]
    [TestCase("олекс@пошта.укр", EmailAttribute.Mode.Filter)]
    [TestCase("user@xn--d1acpjq.xn--p1ai", EmailAttribute.Mode.Filter)]
    [TestCase("test@example.com", EmailAttribute.Mode.Rfc)]
    [TestCase("user.name+tag@sub.example.com", EmailAttribute.Mode.Rfc)]
    [TestCase("a@b.cd", EmailAttribute.Mode.Rfc)]
    [TestCase("test@example.com", EmailAttribute.Mode.Spoof)]
    [TestCase("simple@ascii.org", EmailAttribute.Mode.Spoof)]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string url, EmailAttribute.Mode mod)
    {
        EmailAttribute    attribute         = new(mod);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(url, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("test@non-existent-domain-123456789.com", EmailAttribute.Mode.Dns)]
    [TestCase("invalid-email", EmailAttribute.Mode.Dns)]
    [TestCase("invalid-email", EmailAttribute.Mode.Filter)]
    [TestCase("test@", EmailAttribute.Mode.Filter)]
    [TestCase("@example.com", EmailAttribute.Mode.Filter)]
    [TestCase("invalid-email", EmailAttribute.Mode.Rfc)]
    [TestCase("test@", EmailAttribute.Mode.Rfc)]
    [TestCase("@example.com", EmailAttribute.Mode.Rfc)]
    [TestCase("test@example", EmailAttribute.Mode.Rfc)]
    [TestCase("test..test@example.com", EmailAttribute.Mode.Rfc)]
    [TestCase(".test@example.com", EmailAttribute.Mode.Rfc)]
    [TestCase("test.@example.com", EmailAttribute.Mode.Rfc)]
    [TestCase("tést@exämple.com", EmailAttribute.Mode.Spoof)]
    [TestCase("алекс@почта.рф", EmailAttribute.Mode.Spoof)]
    [TestCase("user@exаmple.com", EmailAttribute.Mode.Spoof)]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect(string url, EmailAttribute.Mode mod)
    {
        EmailAttribute attribute = new(mod) { ErrorMessage = "The {0} must be real url." };

        ValidationContext validationContext = new(new()) { DisplayName = "Email" };

        ValidationResult? result = attribute.GetValidationResult(url, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The Email must be real url."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        EmailAttribute    attribute         = new(EmailAttribute.Mode.Spoof);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsUnhandledType()
    {
        EmailAttribute    attribute         = new(EmailAttribute.Mode.Spoof);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(12, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        EmailAttribute    attribute         = new(EmailAttribute.Mode.Spoof);
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(123, validationContext));
    }
}
