using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.Email;

[TestFixture]
public class EmailAttributeTests
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        EmailAttribute    attribute         = new(EmailAttribute.DNS);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        EmailAttribute    attribute         = new(EmailAttribute.DNS);
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void ShouldThrow_When_ExpectedIsEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            EmailAttribute containsAttribute = new();
        });
    }

    [TestCase("test@google.com", EmailAttribute.DNS)]
    [TestCase("admin@microsoft.com", EmailAttribute.DNS)]
    [TestCase("test@example.com", EmailAttribute.FILTER_UNICODE)]
    [TestCase("олекс@пошта.укр", EmailAttribute.FILTER_UNICODE)]
    [TestCase("user@xn--d1acpjq.xn--p1ai", EmailAttribute.FILTER_UNICODE)]
    [TestCase("test@example.com", EmailAttribute.RFC)]
    [TestCase("user.name+tag@sub.example.com", EmailAttribute.RFC)]
    [TestCase("a@b.cd", EmailAttribute.RFC)]
    [TestCase("test@example.com", EmailAttribute.SPOOF)]
    [TestCase("simple@ascii.org", EmailAttribute.SPOOF)]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string email, string mod)
    {
        EmailAttribute    attribute         = new(mod);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(email, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("test@non-existent-domain-123456789.com", EmailAttribute.DNS)]
    [TestCase("invalid-email", EmailAttribute.DNS)]
    [TestCase("invalid-email", EmailAttribute.FILTER_UNICODE)]
    [TestCase("test@", EmailAttribute.FILTER_UNICODE)]
    [TestCase("@example.com", EmailAttribute.FILTER_UNICODE)]
    [TestCase("invalid-email", EmailAttribute.RFC)]
    [TestCase("test@", EmailAttribute.RFC)]
    [TestCase("@example.com", EmailAttribute.RFC)]
    [TestCase("test@example", EmailAttribute.RFC)]
    [TestCase("test..test@example.com", EmailAttribute.RFC)]
    [TestCase(".test@example.com", EmailAttribute.RFC)]
    [TestCase("test.@example.com", EmailAttribute.RFC)]
    [TestCase("tést@exämple.com", EmailAttribute.SPOOF)]
    [TestCase("алекс@почта.рф", EmailAttribute.SPOOF)]
    [TestCase("user@exаmple.com", EmailAttribute.SPOOF)]
    public void Should_ReturnValidationError_When_ValueIsInvalid(string email, string mod)
    {
        EmailAttribute attribute = new(mod) { ErrorMessage = "The {0} must be correct email." };

        ValidationContext validationContext = new(new()) { DisplayName = "Email" };

        ValidationResult? result = attribute.GetValidationResult(email, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The Email must be correct email."));
    }

    [Test]
    public void Should_ReturnValidationError_When_ModIsInvalid()
    {
        EmailAttribute attribute = new("test") { ErrorMessage = "The {0} must be correct email." };

        ValidationContext validationContext = new(new()) { DisplayName = "Email" };

        ValidationResult? result = attribute.GetValidationResult("test@google.com", validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The Email must be correct email."));
    }
}
