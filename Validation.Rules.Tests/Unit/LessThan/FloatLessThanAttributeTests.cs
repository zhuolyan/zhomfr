using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.LessThan;

[TestFixture(typeof(float))]
[TestFixture(typeof(double))]
[TestFixture(typeof(decimal))]
public class FloatLessThanAttributeTests<T> where T : IParsable<T>
{
    [Test]
    public void Should_ReturnSuccessResult_When_IsCorrect()
    {
        T                    value             = T.Parse("3.3", CultureInfo.InvariantCulture);
        LessThanAttribute attribute         = new(nameof(ModelWithAnotherField<>.Second));
        ValidationContext    validationContext = new(new ModelWithAnotherField<T> { Second = value, First = value });

        ValidationResult? result = attribute.GetValidationResult(T.Parse("3.2", CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("3.300001")]
    [TestCase("3.3")]
    public void Should_ReturnValidationError_When_IsInvalid(string input)
    {
        T                    value     = T.Parse("3.3", CultureInfo.InvariantCulture);
        LessThanAttribute attribute = new(nameof(ModelWithAnotherField<>.First)) { ErrorMessage = "The field {0} must be less than {1}." };

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { Second = value, First = value }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(T.Parse(input, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be less than First."));
    }
}
