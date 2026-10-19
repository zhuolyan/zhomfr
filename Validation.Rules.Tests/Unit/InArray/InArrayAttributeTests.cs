using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.InArray;

[TestFixtureSource(typeof(InArrayAttributeTests), nameof(InArrayAttributeTests.Tested))]
public class InArrayAttributeTests(object collection)
{
    public static readonly List<object> Tested = [DictionaryStubs.Default, EnumerableStubs.ContainsEnumeration];

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        InArrayAttribute attribute = new(nameof(ModelWithAnotherField<>.AnotherField));

        ValidationContext validationContext = new(new ModelWithAnotherField<object> { AnotherField = collection, AnotherField2 = collection });

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_FieldNotExists()
    {
        InArrayAttribute attribute = new("wrongField");

        ValidationContext validationContext = new(new ModelWithAnotherField<object> { AnotherField = collection, AnotherField2 = collection });

        Assert.Throws<MissingFieldException>(() => attribute.GetValidationResult("something", validationContext));
    }

    [TestCase("test")]
    [TestCase("not")]
    [TestCase("blah")]
    public void Should_ReturnSuccess_When_ValuesIsExists(string input)
    {
        InArrayAttribute attribute = new(nameof(ModelWithAnotherField<>.AnotherField));

        ValidationContext validationContext = new(new ModelWithAnotherField<object> { AnotherField = collection, AnotherField2 = collection });

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("wrong")]
    [TestCase(true)]
    [TestCase(1)]
    public void Should_ReturnValidationError_When_ValuesIsNotExists(object input)
    {
        InArrayAttribute attribute = new(nameof(ModelWithAnotherField<>.AnotherField)) { ErrorMessage = "The {0} must be exists in {1}." };

        ValidationContext validationContext
            = new(new ModelWithAnotherField<object> { AnotherField = collection, AnotherField2 = collection }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(input, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be exists in SomeField."));
    }
}
