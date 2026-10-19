using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions.Errors;
using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.Same;

[TestFixture(typeof(ushort), "1")]
[TestFixture(typeof(short), "-1")]
[TestFixture(typeof(uint), "1")]
[TestFixture(typeof(int), "-1")]
[TestFixture(typeof(ulong), "1")]
[TestFixture(typeof(long), "-1")]
[TestFixture(typeof(float), "1.1")]
[TestFixture(typeof(double), "1.1")]
[TestFixture(typeof(decimal), "1.10")]
[TestFixture(typeof(string), "something1")]
[TestFixture(typeof(DateTime), "2026-09-18 06:40:00")]
[TestFixture(typeof(DateTimeOffset), "2026-09-18 06:40:00")]
public class SameAttributeTests<T>(string anotherFieldValue) where T : IEquatable<T>, IParsable<T>
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        T             anotherValue = T.Parse(anotherFieldValue, CultureInfo.InvariantCulture);
        SameAttribute attribute    = new(nameof(ModelWithAnotherField<>.AnotherField));

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { AnotherField = anotherValue, AnotherField2 = anotherValue });

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_FieldNotExists()
    {
        T             anotherValue = T.Parse(anotherFieldValue, CultureInfo.InvariantCulture);
        SameAttribute attribute    = new("wrongField");

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { AnotherField = anotherValue, AnotherField2 = anotherValue });

        Assert.Throws<MissingFieldException>(() => attribute.GetValidationResult("something", validationContext));
    }

    [Test]
    public void Should_Throw_When_FieldsIsDifferentTypes()
    {
        T             anotherValue = T.Parse(anotherFieldValue, CultureInfo.InvariantCulture);
        SameAttribute attribute    = new(nameof(ModelWithAnotherField<>.AnotherField));

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { AnotherField = anotherValue, AnotherField2 = anotherValue });

        Assert.Throws<ValuesHasDifferentTypesException>(() => attribute.GetValidationResult(new(), validationContext));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValuesIsDifferent()
    {
        T             anotherValue = T.Parse(anotherFieldValue, CultureInfo.InvariantCulture);
        T             different    = T.Parse(anotherFieldValue.Replace('1', '2'), CultureInfo.InvariantCulture);
        SameAttribute attribute    = new(nameof(ModelWithAnotherField<>.AnotherField)) { ErrorMessage = "The {0} and {1} must be same." };

        ValidationContext validationContext
            = new(new ModelWithAnotherField<T> { AnotherField = anotherValue, AnotherField2 = anotherValue }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(different, validationContext);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp and SomeField must be same."));
        }
    }

    [Test]
    public void Should_ReturnSuccess_When_ValuesIsSame()
    {
        T             anotherValue = T.Parse(anotherFieldValue, CultureInfo.InvariantCulture);
        SameAttribute attribute    = new(nameof(ModelWithAnotherField<>.AnotherField));

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { AnotherField = anotherValue, AnotherField2 = anotherValue });

        ValidationResult? result = attribute.GetValidationResult(anotherValue, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }
}
