using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.Different;

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
public class DifferentAttributeTests<T>(string anotherFieldValue) where T : IEquatable<T>, IParsable<T>
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        T                  anotherValue = T.Parse(anotherFieldValue, CultureInfo.InvariantCulture);
        DifferentAttribute attribute    = new(nameof(ModelWithAnotherField<>.AnotherField2));

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { AnotherField = anotherValue, AnotherField2 = anotherValue });

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_FieldNotExists()
    {
        T                  anotherValue = T.Parse(anotherFieldValue, CultureInfo.InvariantCulture);
        DifferentAttribute attribute    = new("wrongField");

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { AnotherField = anotherValue, AnotherField2 = anotherValue });

        Assert.Throws<MissingFieldException>(() => attribute.GetValidationResult("something", validationContext));
    }

    [Test]
    public void Should_ReturnValidationError_When_ValuesIsSame()
    {
        T                  anotherValue = T.Parse(anotherFieldValue, CultureInfo.InvariantCulture);
        DifferentAttribute attribute    = new(nameof(ModelWithAnotherField<>.AnotherField2)) { ErrorMessage = "The {0} and {1} must be different." };

        ValidationContext validationContext
            = new(new ModelWithAnotherField<T> { AnotherField = anotherValue, AnotherField2 = anotherValue }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(anotherValue, validationContext);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp and AnotherField2 must be different."));
        }
    }

    [Test]
    public void Should_ReturnSuccess_When_ValuesIsDifferent()
    {
        T                  anotherValue = T.Parse(anotherFieldValue, CultureInfo.InvariantCulture);
        T                  different    = T.Parse(anotherFieldValue.Replace('1', '2'), CultureInfo.InvariantCulture);
        DifferentAttribute attribute    = new(nameof(ModelWithAnotherField<>.AnotherField2));

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { AnotherField = anotherValue, AnotherField2 = anotherValue });

        ValidationResult? result = attribute.GetValidationResult(different, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }
}
