using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture(typeof(DateTime))]
[TestFixture(typeof(DateTimeOffset))]
public class BeforeAttributeTests<T> where T : ISpanParsable<T>
{
    private const string VALUE = "2026-04-13 12:21:00";

    [Test]
    public void Should_ReturnSuccess_When_ValueIsCorrect()
    {
        BeforeAttribute   attribute         = new(BeforeAttributeTests<T>.VALUE);
        ValidationContext validationContext = new(new());
        T                 value             = T.Parse("2026-04-13 12:20:59", CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsCorrectAndValidationValueIsDefault()
    {
        AfterAttribute    attribute         = new();
        ValidationContext validationContext = new(new());
        T                 value             = T.Parse(DateTime.Now.AddDays(-1).ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("2026-04-13 12:21:00")]
    [TestCase("2026-04-13 12:21:01")]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect(string strValue)
    {
        BeforeAttribute attribute = new(BeforeAttributeTests<T>.VALUE) { ErrorMessage = "The date in {0} must be before {1}." };

        T value = T.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo($"The date in MyProp must be before {BeforeAttributeTests<T>.VALUE}."));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        BeforeAttribute   attribute         = new();
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        BeforeAttribute   attribute         = new(BeforeAttributeTests<T>.VALUE);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }
}
