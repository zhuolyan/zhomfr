using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.LessThanOrEqual;

[TestFixture(typeof(short))]
[TestFixture(typeof(ushort))]
[TestFixture(typeof(int))]
[TestFixture(typeof(uint))]
[TestFixture(typeof(long))]
[TestFixture(typeof(ulong))]
public class IntLessThanOrEqualAttributeTests<T> where T : IParsable<T>
{
    [TestCase("3")]
    [TestCase("2")]
    public void Should_ReturnSuccessResult_When_IsCorrect(string input)
    {
        T                        value             = T.Parse("3", CultureInfo.InvariantCulture);
        LessThanOrEqualAttribute attribute         = new(nameof(ModelWithAnotherField<>.Second));
        ValidationContext        validationContext = new(new ModelWithAnotherField<T> { Second = value, First = value });

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_IsInvalid()
    {
        T value = T.Parse("3", CultureInfo.InvariantCulture);

        LessThanOrEqualAttribute attribute = new(nameof(ModelWithAnotherField<>.First)) { ErrorMessage = "The field {0} must be less than or equal {1}." };

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { Second = value, First = value }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(T.Parse("4", CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be less than or equal First."));
    }
}
