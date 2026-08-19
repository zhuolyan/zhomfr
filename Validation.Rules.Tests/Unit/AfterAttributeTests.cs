using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture(typeof(DateTime))]
[TestFixture(typeof(DateTimeOffset))]
public class AfterAttributeTests<T> where T : ISpanParsable<T>
{
    private const string VALIDATION_VALUE = "2026-04-13 12:21:00";

    [Test]
    public void Should_ReturnSuccess_When_ValueIsCorrect()
    {
        AfterAttribute    attribute         = new(AfterAttributeTests<T>.VALIDATION_VALUE);
        ValidationContext validationContext = new(new());
        T                 value             = T.Parse("2026-04-13 12:21:01", CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
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
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        AfterAttribute    attribute         = new(AfterAttributeTests<T>.VALIDATION_VALUE);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

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
