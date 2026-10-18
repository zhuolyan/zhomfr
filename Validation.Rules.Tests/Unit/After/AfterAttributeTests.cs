using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.After;

[TestFixture(typeof(DateTime))]
[TestFixture(typeof(DateTimeOffset))]
public class AfterAttributeTests<T> where T : ISpanParsable<T>
{
    private const string MINIMAL = "2026-10-18 07:00:00";

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        AfterAttribute    attribute         = new();
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsCorrect()
    {
        AfterAttribute    attribute         = new(AfterAttributeTests<T>.MINIMAL);
        ValidationContext validationContext = new(new());
        T                 value             = T.Parse("2026-10-18 07:00:01", CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase(AfterAttributeTests<T>.MINIMAL)]
    [TestCase("2026-10-18 06:59:59")]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect(string strValue)
    {
        AfterAttribute attribute = new(AfterAttributeTests<T>.MINIMAL) { ErrorMessage = "The date in {0} must be after {1}." };

        T value = T.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo($"The date in MyProp must be after {AfterAttributeTests<T>.MINIMAL}."));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsCorrectAndValidationValueIsDefault()
    {
        AfterAttribute    attribute         = new();
        ValidationContext validationContext = new(new());
        T                 value             = T.Parse(DateTime.Now.AddDays(1).ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        AfterAttribute    attribute         = new();
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }
}
