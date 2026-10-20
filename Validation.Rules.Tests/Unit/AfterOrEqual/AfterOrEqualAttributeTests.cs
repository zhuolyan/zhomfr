using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions.Errors;

namespace Zhomfr.Validation.Rules.Tests.Unit.AfterOrEqual;

[TestFixture(typeof(DateTime))]
[TestFixture(typeof(DateTimeOffset))]
public class AfterOrEqualAttributeTests<T> where T : ISpanParsable<T>
{
    private const string MINIMAL = "2026-10-18 07:00:00";

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        AfterOrEqualAttribute attribute         = new();
        ValidationContext     validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase(AfterOrEqualAttributeTests<T>.MINIMAL)]
    [TestCase("2026-10-18 07:00:01")]
    public void Should_ReturnSuccess_When_ValueIsCorrect(string input)
    {
        AfterOrEqualAttribute attribute         = new(AfterOrEqualAttributeTests<T>.MINIMAL);
        ValidationContext     validationContext = new(new());
        T                     value             = T.Parse(input, CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValueIsInvalid()
    {
        AfterOrEqualAttribute attribute = new(AfterOrEqualAttributeTests<T>.MINIMAL) { ErrorMessage = "The date in {0} must be after or equal {1}." };

        T value = T.Parse("2026-10-18 06:59:59", CultureInfo.InvariantCulture);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo($"The date in MyProp must be after or equal {AfterOrEqualAttributeTests<T>.MINIMAL}."));
    }

    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        AfterOrEqualAttribute attribute         = new();
        ValidationContext     validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsCorrectAndExpectedIsDefault()
    {
        AfterOrEqualAttribute attribute         = new();
        ValidationContext     validationContext = new(new());
        T                     value             = T.Parse(DateTime.Now.AddDays(1).ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }
}
