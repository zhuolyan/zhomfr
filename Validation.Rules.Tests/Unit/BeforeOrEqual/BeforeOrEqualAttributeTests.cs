using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.BeforeOrEqual;

[TestFixture(typeof(DateTime))]
[TestFixture(typeof(DateTimeOffset))]
public class BeforeOrEqualAttributeTests<T> where T : ISpanParsable<T>
{
    private const string MAXIMUM = "2026-10-18 07:00:00";

    [TestCase(BeforeOrEqualAttributeTests<T>.MAXIMUM)]
    [TestCase("2026-10-18 06:59:59")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string strValue)
    {
        BeforeOrEqualAttribute attribute         = new(BeforeOrEqualAttributeTests<T>.MAXIMUM);
        ValidationContext      validationContext = new(new());
        T                      value             = T.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect()
    {
        BeforeOrEqualAttribute attribute = new(BeforeOrEqualAttributeTests<T>.MAXIMUM) { ErrorMessage = "The date in {0} must be before or equal {1}." };

        T value = T.Parse("2026-10-18 07:00:01", CultureInfo.InvariantCulture);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo($"The date in MyProp must be before or equal {BeforeOrEqualAttributeTests<T>.MAXIMUM}."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsCorrectAndValidationValueIsDefault()
    {
        BeforeOrEqualAttribute attribute         = new();
        ValidationContext      validationContext = new(new());
        T                      value             = T.Parse(DateTime.Now.AddDays(-1).ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        BeforeOrEqualAttribute attribute         = new();
        ValidationContext      validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        BeforeOrEqualAttribute attribute         = new(BeforeOrEqualAttributeTests<T>.MAXIMUM);
        ValidationContext      validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }
}
