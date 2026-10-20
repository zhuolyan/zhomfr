using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.GreaterThan;

[TestFixture]
public class EnumerableGreaterThanAttributeTests
{
    [Test]
    public void Should_ReturnSuccessResult_When_IsCorrect()
    {
        GreaterThanAttribute attribute = new(nameof(ModelWithAnotherField<>.First));

        IEnumerable fieldVal = EnumerableStubs.GetEnumerable(3);

        ValidationContext validationContext = new(new ModelWithAnotherField<IEnumerable> { Second = fieldVal, First = fieldVal });

        ValidationResult? result = attribute.GetValidationResult(EnumerableStubs.GetEnumerable(4), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnSuccessResult_When_IsCorrectDictionary()
    {
        GreaterThanAttribute attribute = new(nameof(ModelWithAnotherField<>.First));

        IDictionary fieldVal = DictionaryStubs.GetDictionary(3);

        ValidationContext validationContext = new(new ModelWithAnotherField<IDictionary> { Second = fieldVal, First = fieldVal });

        ValidationResult? result = attribute.GetValidationResult(DictionaryStubs.GetDictionary(4), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase(3)]
    [TestCase(2)]
    public void Should_ReturnValidationError_When_IsInvalid(int input)
    {
        GreaterThanAttribute attribute = new(nameof(ModelWithAnotherField<>.Second)) { ErrorMessage = "The field {0} must be greater than {1}." };
        IEnumerable          value     = EnumerableStubs.GetEnumerable(3);

        ValidationContext validationContext = new(new ModelWithAnotherField<IEnumerable> { Second = value, First = value }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(EnumerableStubs.GetEnumerable(input), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater than SomeField."));
    }

    [TestCase(3)]
    [TestCase(2)]
    public void Should_ReturnValidationError_When_IsInvalidDictionary(int input)
    {
        GreaterThanAttribute attribute = new(nameof(ModelWithAnotherField<>.First)) { ErrorMessage = "The field {0} must be greater than {1}." };
        IDictionary          value     = DictionaryStubs.GetDictionary(3);

        ValidationContext validationContext = new(new ModelWithAnotherField<IDictionary> { Second = value, First = value }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(DictionaryStubs.GetDictionary(input), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater than First."));
    }
}
