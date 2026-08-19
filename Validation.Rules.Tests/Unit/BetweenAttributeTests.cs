using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture(typeof(DateTime))]
[TestFixture(typeof(DateTimeOffset))]
public class BetweenAttributeTests<T> where T : ISpanParsable<T>
{
    private const string MIN = "2026-04-13 12:21:00";
    private const string MAX = "2026-04-13 12:21:20";

    [TestCase("2026-04-13 12:21:01")]
    [TestCase("2026-04-13 12:21:10")]
    [TestCase("2026-04-13 12:21:19")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string strValue)
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests<T>.MIN, BetweenAttributeTests<T>.MAX);
        ValidationContext validationContext = new(new());
        T                 value             = T.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("2026-04-13 12:20:59")]
    [TestCase("2026-04-13 12:21:00")]
    [TestCase("2026-04-13 12:21:20")]
    [TestCase("2026-04-13 12:21:21")]
    public void Should_ReturnValidationError_When_ValueIsNotCorrect(string strValue)
    {
        BetweenAttribute attribute
            = new(BetweenAttributeTests<T>.MIN, BetweenAttributeTests<T>.MAX) { ErrorMessage = "The value of {0} must be between {1} and {2}." };

        T value = T.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo($"The value of MyProp must be between {BetweenAttributeTests<T>.MIN} and {BetweenAttributeTests<T>.MAX}."));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests<T>.MIN, BetweenAttributeTests<T>.MAX);
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests<T>.MIN, BetweenAttributeTests<T>.MAX);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }
}
