using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.GreaterThanOrEqual;

[TestFixture(typeof(float))]
[TestFixture(typeof(double))]
[TestFixture(typeof(decimal))]
public class FloatGreaterThanOrEqualAttributeTests<T> where T : IParsable<T>
{
    [TestCase("3.2")]
    [TestCase("3.200001")]
    public void Should_ReturnSuccessResult_When_IsCorrect(string value)
    {
        GreaterThanOrEqualAttribute attribute         = new("3.2");
        ValidationContext           validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(T.Parse(value, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_IsIncorrect()
    {
        GreaterThanOrEqualAttribute attribute         = new("3.3") { ErrorMessage = "The field {0} must be greater than or equal {1}." };
        ValidationContext           validationContext = new(new()) { DisplayName  = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(T.Parse("3.299999", CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater than or equal 3.3."));
    }

    [TestCase("3.2")]
    [TestCase("3.200001")]
    public void Should_ReturnSuccessResult_When_IsCorrectField(string value)
    {
        T                           fieldVal          = T.Parse("3.2", CultureInfo.InvariantCulture);
        GreaterThanOrEqualAttribute attribute         = new(nameof(ModelWithAnotherField<>.AnotherField));
        ValidationContext           validationContext = new(new ModelWithAnotherField<T> { AnotherField = fieldVal, AnotherField2 = fieldVal });

        ValidationResult? result = attribute.GetValidationResult(T.Parse(value, CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnValidationError_When_IsIncorrectField()
    {
        T fieldVal = T.Parse("3.3", CultureInfo.InvariantCulture);

        GreaterThanOrEqualAttribute attribute
            = new(nameof(ModelWithAnotherField<>.AnotherField2)) { ErrorMessage = "The field {0} must be greater than or equal {1}." };

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { AnotherField = fieldVal, AnotherField2 = fieldVal }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(T.Parse("3.299999", CultureInfo.InvariantCulture), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater than or equal AnotherField2."));
    }
}
