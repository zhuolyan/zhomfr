using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.GreaterThanOrEqual;

[TestFixture(typeof(DateTime))]
[TestFixture(typeof(DateTimeOffset))]
public class DateGreaterThanOrEqualAttributeTests<T> where T : IParsable<T>, IComparable
{
    [TestCase("2026-10-18 07:00:00")]
    [TestCase("2026-10-18 07:00:01")]
    public void Should_ReturnSuccessResult_When_IsCorrect(string input)
    {
        T                           value     = T.Parse("2026-10-18 07:00:00", CultureInfo.InvariantCulture);
        GreaterThanOrEqualAttribute attribute = new(nameof(ModelWithAnotherField<>.First));

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { Second = value, First = value });

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_IsInvalid()
    {
        T                           value     = T.Parse("2026-10-18 07:00:00", CultureInfo.InvariantCulture);
        GreaterThanOrEqualAttribute attribute = new(nameof(ModelWithAnotherField<>.Second)) { ErrorMessage = "The {0} must be greater than or equal {1}." };

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { Second = value, First = value }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(T.Parse("2026-10-18 06:59:59", CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be greater than or equal SomeField."));
    }
}
