using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit.AcceptedIf;

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
public class AcceptedIfAttributeTests<T>(string anotherFieldValue) where T : IEquatable<T>, IParsable<T>
{
    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        T                      anotherValue = T.Parse(anotherFieldValue, CultureInfo.InvariantCulture);
        AcceptedIfAttribute<T> attribute    = new(nameof(ModelWithAnotherField<>.AnotherField), anotherFieldValue);

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { AnotherField = anotherValue, AnotherField2 = anotherValue });

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_FieldNotExists()
    {
        T                      anotherValue = T.Parse(anotherFieldValue, CultureInfo.InvariantCulture);
        AcceptedIfAttribute<T> attribute    = new("wrongField", anotherFieldValue);

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { AnotherField = anotherValue, AnotherField2 = anotherValue });

        Assert.Throws<MissingFieldException>(() => attribute.GetValidationResult("something", validationContext));
    }

    [TestCase("yes")]
    [TestCase("on")]
    [TestCase("true")]
    [TestCase("1")]
    [TestCase(1)]
    [TestCase(true)]
    [TestCase("some text")]
    [TestCase("no")]
    [TestCase("off")]
    [TestCase("false")]
    [TestCase("0")]
    [TestCase("2")]
    [TestCase(0)]
    [TestCase(2)]
    [TestCase(false)]
    public void Should_ReturnSuccess_When_AnotherFieldValueIsNotExpected(object value)
    {
        T                      anotherValue = T.Parse(anotherFieldValue.Replace('1', '2'), CultureInfo.InvariantCulture);
        AcceptedIfAttribute<T> attribute    = new(nameof(ModelWithAnotherField<>.AnotherField2), anotherFieldValue);

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { AnotherField = anotherValue, AnotherField2 = anotherValue });

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("yes")]
    [TestCase("on")]
    [TestCase("true")]
    [TestCase("1")]
    [TestCase(1)]
    [TestCase(true)]
    public void Should_ReturnSuccess_When_TestedValueIsAcceptable(object value)
    {
        T                      anotherValue = T.Parse(anotherFieldValue, CultureInfo.InvariantCulture);
        AcceptedIfAttribute<T> attribute    = new(nameof(ModelWithAnotherField<>.AnotherField), anotherFieldValue);

        ValidationContext validationContext = new(new ModelWithAnotherField<T> { AnotherField = anotherValue, AnotherField2 = anotherValue });

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("some text")]
    [TestCase("no")]
    [TestCase("off")]
    [TestCase("false")]
    [TestCase("0")]
    [TestCase("2")]
    [TestCase(0)]
    [TestCase(2)]
    [TestCase(false)]
    public void Should_ReturnFailure_When_TestedValueIsNotAcceptable(object value)
    {
        T                      anotherValue = T.Parse(anotherFieldValue, CultureInfo.InvariantCulture);
        AcceptedIfAttribute<T> attribute    = new(nameof(ModelWithAnotherField<>.AnotherField), anotherFieldValue) { ErrorMessage = "The {0} must be accepted." };

        ValidationContext validationContext
            = new(new ModelWithAnotherField<T> { AnotherField = anotherValue, AnotherField2 = anotherValue }) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Is.EqualTo("The MyProp must be accepted."));
        }
    }
}
