using System.Collections;
using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.GreaterThan;

[TestFixture]
public class EnumerableGreaterThanAttributeTests
{
    [Test]
    public void Should_ReturnSuccessResult_When_IsCorrectEnumerable()
    {
        GreaterThanAttribute attribute         = new("2");
        ValidationContext    validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(EnumerableStubs.GetEnumerable(3), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnSuccessResult_When_IsCorrectCollection()
    {
        GreaterThanAttribute attribute         = new("2");
        ValidationContext    validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(DictionaryStubs.GetDictionary(3), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase(3)]
    [TestCase(2)]
    public void Should_ReturnValidationError_When_IsIncorrectEnumerable(int value)
    {
        GreaterThanAttribute attribute         = new("3") { ErrorMessage  = "The field {0} must be greater than {1}." };
        ValidationContext    validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(EnumerableStubs.GetEnumerable(value), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater than 3."));
    }

    [TestCase(3)]
    [TestCase(2)]
    public void Should_ReturnValidationError_When_IsIncorrectDictionary(int value)
    {
        GreaterThanAttribute attribute         = new("3") { ErrorMessage  = "The field {0} must be greater than {1}." };
        ValidationContext    validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(DictionaryStubs.GetDictionary(value), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater than 3."));
    }

    [Test]
    public void Should_ReturnSuccessResult_When_IsCorrectEnumerableField()
    {
        GreaterThanAttribute attribute = new(nameof(ModelWithAnotherField<>.AnotherField2));

        IEnumerable fieldVal = EnumerableStubs.GetEnumerable(3);

        ValidationContext validationContext = new(new ModelWithAnotherField<IEnumerable> { AnotherField = fieldVal, AnotherField2 = fieldVal });

        ValidationResult? result = attribute.GetValidationResult(EnumerableStubs.GetEnumerable(4), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnSuccessResult_When_IsCorrectDictionaryField()
    {
        GreaterThanAttribute attribute = new(nameof(ModelWithAnotherField<>.AnotherField2));

        IDictionary fieldVal = DictionaryStubs.GetDictionary(3);

        ValidationContext validationContext = new(new ModelWithAnotherField<IDictionary> { AnotherField = fieldVal, AnotherField2 = fieldVal });

        ValidationResult? result = attribute.GetValidationResult(DictionaryStubs.GetDictionary(4), validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase(3)]
    [TestCase(2)]
    public void Should_ReturnValidationError_When_IsIncorrectEnumerableField(int value)
    {
        GreaterThanAttribute attribute = new(nameof(ModelWithAnotherField<>.AnotherField)) { ErrorMessage = "The field {0} must be greater than {1}." };
        IEnumerable          fieldVal  = EnumerableStubs.GetEnumerable(3);

        ValidationContext validationContext
            = new(new ModelWithAnotherField<IEnumerable> { AnotherField = fieldVal, AnotherField2 = fieldVal }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(EnumerableStubs.GetEnumerable(value), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater than SomeField."));
    }

    [TestCase(3)]
    [TestCase(2)]
    public void Should_ReturnValidationError_When_IsIncorrectDictionaryField(int value)
    {
        GreaterThanAttribute attribute = new(nameof(ModelWithAnotherField<>.AnotherField2)) { ErrorMessage = "The field {0} must be greater than {1}." };
        IDictionary          fieldVal  = DictionaryStubs.GetDictionary(3);

        ValidationContext validationContext
            = new(new ModelWithAnotherField<IDictionary> { AnotherField = fieldVal, AnotherField2 = fieldVal }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(DictionaryStubs.GetDictionary(value), validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo("The field MyProp must be greater than AnotherField2."));
    }
}
