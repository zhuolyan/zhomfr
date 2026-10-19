using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.GreaterThan;

[TestFixture(typeof(DateTime))]
[TestFixture(typeof(DateTimeOffset))]
public class DateGreaterThanAttributeTests<T> where T : IParsable<T>, IComparable
{
    [Test]
    public void Should_ReturnSuccessResult_When_IsCorrect()
    {
        GreaterThanAttribute attribute         = new("2026-10-18 07:00:00");
        ValidationContext    validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(T.Parse("2026-10-18 07:00:01", CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("2026-10-18 06:59:59")]
    [TestCase("2026-10-18 07:00:00")]
    public void Should_ReturnValidationError_When_IsIncorrect(string value)
    {
        GreaterThanAttribute attribute         = new("2026-10-18 07:00:00") { ErrorMessage = "The field {0} must be greater than {1}." };
        ValidationContext    validationContext = new(new()) { DisplayName                  = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(T.Parse(value, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater than 2026-10-18 07:00:00."));
    }

    [Test]
    public void Should_ReturnSuccessResult_When_IsCorrectField()
    {
        T                    value     = T.Parse("2026-10-18 07:00:00", CultureInfo.InvariantCulture);
        GreaterThanAttribute attribute = new(nameof(ModelWithAnotherField<>.AnotherField2));

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { AnotherField = value, AnotherField2 = value });

        ValidationResult? result = attribute.GetValidationResult(T.Parse("2026-10-18 07:00:01", CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("2026-10-18 07:00:00")]
    [TestCase("2026-10-18 06:59:59")]
    public void Should_ReturnValidationError_When_IsIncorrectField(string value)
    {
        T                    fFalue    = T.Parse("2026-10-18 07:00:00", CultureInfo.InvariantCulture);
        GreaterThanAttribute attribute = new(nameof(ModelWithAnotherField<>.AnotherField)) { ErrorMessage = "The {0} must be greater than {1}." };

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { AnotherField = fFalue, AnotherField2 = fFalue }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(T.Parse(value, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be greater than SomeField."));
    }
}
