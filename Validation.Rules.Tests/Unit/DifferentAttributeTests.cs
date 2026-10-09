using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class DifferentAttributeTests
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsDifferentValue()
    {
        ConfirmedModel model = new() { Password = "password123", PasswordConfirmation = "different" };

        DifferentAttribute attribute = new(nameof(ConfirmedModel.PasswordConfirmation));

        ValidationContext validationContext = new(model) { MemberName = nameof(ConfirmedModel.Password), DisplayName = nameof(ConfirmedModel.Password) };

        ValidationResult? result = attribute.GetValidationResult(model.Password, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsDifferentType()
    {
        ConfirmedModel model = new() { Password = "password123" };

        DifferentAttribute attribute = new(nameof(ConfirmedModel.SomethingElse));

        ValidationContext validationContext = new(model) { MemberName = nameof(ConfirmedModel.Password), DisplayName = nameof(ConfirmedModel.Password) };

        ValidationResult? result = attribute.GetValidationResult(model.Password, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueHasDifferentType()
    {
        DifferentAttribute attribute = new(nameof(ConfirmedModel.PasswordConfirmation));

        ValidationContext validationContext = new(new()) { MemberName = nameof(ConfirmedModel.Password), DisplayName = nameof(ConfirmedModel.Password) };

        ValidationResult? result = attribute.GetValidationResult(123, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect()
    {
        ConfirmedModel model = new() { Password = "password123", PasswordConfirmation = "password123" };

        DifferentAttribute attribute = new(nameof(ConfirmedModel.PasswordConfirmation)) { ErrorMessage = "The {0} must be different of {1}." };

        ValidationContext validationContext = new(model) { MemberName = nameof(ConfirmedModel.Password), DisplayName = nameof(ConfirmedModel.Password) };

        ValidationResult? result = attribute.GetValidationResult(model.Password, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The Password must be different of PasswordConfirmation."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        DifferentAttribute attribute         = new(nameof(ConfirmedModel.PasswordConfirmation));
        ValidationContext  validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }
}
