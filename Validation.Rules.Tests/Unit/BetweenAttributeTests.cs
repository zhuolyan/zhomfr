using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class BetweenAttributeTests
{
    private const string STRING_MIN = "4";
    private const string STRING_MAX = "6";
    private const string INT_MIN    = "-10";
    private const string INT_MAX    = "10";
    private const string UINT_MIN   = "2";
    private const string UINT_MAX   = "4";
    private const string FLOAT_MIN  = "-9.9999";
    private const string FLOAT_MAX  = "9.9999";
    private const string DATE_MIN   = "2026-04-13 12:21:00";
    private const string DATE_MAX   = "2026-04-13 12:21:20";

    [TestCase("2026-04-13 12:21:00")]
    [TestCase("2026-04-13 12:21:10")]
    [TestCase("2026-04-13 12:21:20")]
    public void Should_ReturnSuccess_When_ValueIsDateTimeAndCorrect(string strValue)
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX);
        ValidationContext validationContext = new(new());
        DateTime          value             = DateTime.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("2026-04-13 12:20:59")]
    [TestCase("2026-04-13 12:21:21")]
    public void Should_ReturnValidationError_When_ValueIsDateTimeAndNotCorrect(string strValue)
    {
        BetweenAttribute attribute
            = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX) { ErrorMessage = "The value of {0} must be between {1} and {2}." };

        DateTime value = DateTime.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage,
                    Is.EqualTo($"The value of MyProp must be between {BetweenAttributeTests.DATE_MIN} and {BetweenAttributeTests.DATE_MAX}."));
    }

    [TestCase("2026-04-13 12:21:00")]
    [TestCase("2026-04-13 12:21:10")]
    [TestCase("2026-04-13 12:21:20")]
    public void Should_ReturnSuccess_When_ValueIsDateTimeOffsetAndCorrect(string strValue)
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX);
        ValidationContext validationContext = new(new());
        DateTimeOffset    value             = DateTimeOffset.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("2026-04-13 12:20:59")]
    [TestCase("2026-04-13 12:21:21")]
    public void Should_ReturnValidationError_When_ValueIsDateTimeOffsetAndNotCorrect(string strValue)
    {
        BetweenAttribute attribute
            = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX) { ErrorMessage = "The value of {0} must be between {1} and {2}." };

        DateTimeOffset value = DateTimeOffset.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage,
                    Is.EqualTo($"The value of MyProp must be between {BetweenAttributeTests.DATE_MIN} and {BetweenAttributeTests.DATE_MAX}."));
    }

    [TestCase("test")]
    [TestCase("test1")]
    [TestCase("test12")]
    public void Should_ReturnSuccess_When_ValueIsStringAndCorrect(string value)
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests.STRING_MIN, BetweenAttributeTests.STRING_MAX);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("tes")]
    [TestCase("test123")]
    public void Should_ReturnValidationError_When_ValueIsStringAndNotCorrect(string value)
    {
        BetweenAttribute attribute
            = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX) { ErrorMessage = "The value of {0} must be between {1} and {2}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage,
                    Is.EqualTo($"The value of MyProp must be between {BetweenAttributeTests.STRING_MIN} and {BetweenAttributeTests.STRING_MAX}."));
    }

    [TestCase(BetweenAttributeTests.INT_MIN)]
    [TestCase("0")]
    [TestCase(BetweenAttributeTests.INT_MAX)]
    public void Should_ReturnSuccess_When_ValueIsShortAndCorrect(string strValue)
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests.INT_MIN, BetweenAttributeTests.INT_MAX);
        ValidationContext validationContext = new(new());
        short             value             = short.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("-11")]
    [TestCase("11")]
    public void Should_ReturnValidationError_When_ValueIsShortAndNotCorrect(string strValue)
    {
        BetweenAttribute attribute
            = new(BetweenAttributeTests.INT_MIN, BetweenAttributeTests.INT_MAX) { ErrorMessage = "The value of {0} must be between {1} and {2}." };

        short value = short.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo($"The value of MyProp must be between {BetweenAttributeTests.INT_MIN} and {BetweenAttributeTests.INT_MAX}."));
    }

    [TestCase(BetweenAttributeTests.INT_MIN)]
    [TestCase("0")]
    [TestCase(BetweenAttributeTests.INT_MAX)]
    public void Should_ReturnSuccess_When_ValueIsIntAndCorrect(string strValue)
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX);
        ValidationContext validationContext = new(new());
        int               value             = int.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("-11")]
    [TestCase("11")]
    public void Should_ReturnValidationError_When_ValueIsIntAndNotCorrect(string strValue)
    {
        BetweenAttribute attribute
            = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX) { ErrorMessage = "The value of {0} must be between {1} and {2}." };

        int value = int.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo($"The value of MyProp must be between {BetweenAttributeTests.INT_MIN} and {BetweenAttributeTests.INT_MAX}."));
    }

    [TestCase(BetweenAttributeTests.INT_MIN)]
    [TestCase("0")]
    [TestCase(BetweenAttributeTests.INT_MAX)]
    public void Should_ReturnSuccess_When_ValueIsLongAndCorrect(string strValue)
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX);
        ValidationContext validationContext = new(new());
        long              value             = long.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("-11")]
    [TestCase("11")]
    public void Should_ReturnValidationError_When_ValueIsLongAndNotCorrect(string strValue)
    {
        BetweenAttribute attribute
            = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX) { ErrorMessage = "The value of {0} must be between {1} and {2}." };

        long value = long.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage, Is.EqualTo($"The value of MyProp must be between {BetweenAttributeTests.INT_MIN} and {BetweenAttributeTests.INT_MAX}."));
    }

    [TestCase(BetweenAttributeTests.UINT_MIN)]
    [TestCase("3")]
    [TestCase(BetweenAttributeTests.UINT_MAX)]
    public void Should_ReturnSuccess_When_ValueIsUShortAndCorrect(string strValue)
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX);
        ValidationContext validationContext = new(new());
        ushort            value             = ushort.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("1")]
    [TestCase("5")]
    public void Should_ReturnValidationError_When_ValueIsUShortAndNotCorrect(string strValue)
    {
        BetweenAttribute attribute
            = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX) { ErrorMessage = "The value of {0} must be between {1} and {2}." };

        ushort value = ushort.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage,
                    Is.EqualTo($"The value of MyProp must be between {BetweenAttributeTests.UINT_MIN} and {BetweenAttributeTests.UINT_MAX}."));
    }

    [TestCase(BetweenAttributeTests.UINT_MIN)]
    [TestCase("3")]
    [TestCase(BetweenAttributeTests.UINT_MAX)]
    public void Should_ReturnSuccess_When_ValueIsUIntAndCorrect(string strValue)
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX);
        ValidationContext validationContext = new(new());
        uint              value             = uint.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("1")]
    [TestCase("5")]
    public void Should_ReturnValidationError_When_ValueIsUIntAndNotCorrect(string strValue)
    {
        BetweenAttribute attribute
            = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX) { ErrorMessage = "The value of {0} must be between {1} and {2}." };

        uint value = uint.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage,
                    Is.EqualTo($"The value of MyProp must be between {BetweenAttributeTests.UINT_MIN} and {BetweenAttributeTests.UINT_MAX}."));
    }

    [TestCase(BetweenAttributeTests.UINT_MIN)]
    [TestCase("3")]
    [TestCase(BetweenAttributeTests.UINT_MAX)]
    public void Should_ReturnSuccess_When_ValueIsULongAndCorrect(string strValue)
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX);
        ValidationContext validationContext = new(new());
        ulong             value             = ulong.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("1")]
    [TestCase("5")]
    public void Should_ReturnValidationError_When_ValueIsULongAndNotCorrect(string strValue)
    {
        BetweenAttribute attribute
            = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX) { ErrorMessage = "The value of {0} must be between {1} and {2}." };

        ulong value = ulong.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage,
                    Is.EqualTo($"The value of MyProp must be between {BetweenAttributeTests.UINT_MIN} and {BetweenAttributeTests.UINT_MAX}."));
    }

    [TestCase(BetweenAttributeTests.FLOAT_MIN)]
    [TestCase("0")]
    [TestCase(BetweenAttributeTests.FLOAT_MAX)]
    public void Should_ReturnSuccess_When_ValueIsFloatAndCorrect(string strValue)
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX);
        ValidationContext validationContext = new(new());
        float             value             = float.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("-10")]
    [TestCase("10")]
    public void Should_ReturnValidationError_When_ValueIsFloatAndNotCorrect(string strValue)
    {
        BetweenAttribute attribute
            = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX) { ErrorMessage = "The value of {0} must be between {1} and {2}." };

        float value = float.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage,
                    Is.EqualTo($"The value of MyProp must be between {BetweenAttributeTests.FLOAT_MIN} and {BetweenAttributeTests.FLOAT_MAX}."));
    }

    [TestCase(BetweenAttributeTests.FLOAT_MIN)]
    [TestCase("0")]
    [TestCase(BetweenAttributeTests.FLOAT_MAX)]
    public void Should_ReturnSuccess_When_ValueIsDoubleAndCorrect(string strValue)
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX);
        ValidationContext validationContext = new(new());
        double            value             = double.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("-10")]
    [TestCase("10")]
    public void Should_ReturnValidationError_When_ValueIsDoubleAndNotCorrect(string strValue)
    {
        BetweenAttribute attribute
            = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX) { ErrorMessage = "The value of {0} must be between {1} and {2}." };

        double value = double.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage,
                    Is.EqualTo($"The value of MyProp must be between {BetweenAttributeTests.FLOAT_MIN} and {BetweenAttributeTests.FLOAT_MAX}."));
    }

    [TestCase(BetweenAttributeTests.FLOAT_MIN)]
    [TestCase("0")]
    [TestCase(BetweenAttributeTests.FLOAT_MAX)]
    public void Should_ReturnSuccess_When_ValueIsDecimalAndCorrect(string strValue)
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX);
        ValidationContext validationContext = new(new());
        decimal           value             = decimal.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCase("-10")]
    [TestCase("10")]
    public void Should_ReturnValidationError_When_ValueIsDecimalAndNotCorrect(string strValue)
    {
        BetweenAttribute attribute
            = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX) { ErrorMessage = "The value of {0} must be between {1} and {2}." };

        decimal value = decimal.Parse(strValue, CultureInfo.InvariantCulture);

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage,
                    Is.EqualTo($"The value of MyProp must be between {BetweenAttributeTests.FLOAT_MIN} and {BetweenAttributeTests.FLOAT_MAX}."));
    }

    [TestCaseSource(typeof(TestedCollections), nameof(TestedCollections.BetweenCorrectValues))]
    public void Should_ReturnSuccess_When_ValueIsCollectionAndCorrect(List<int> value)
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests.UINT_MIN, BetweenAttributeTests.UINT_MAX);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCaseSource(typeof(TestedCollections), nameof(TestedCollections.BetweenNotCorrectValues))]
    public void Should_ReturnValidationError_When_ValueIsCollectionAndNotCorrect(List<int> value)
    {
        BetweenAttribute attribute
            = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX) { ErrorMessage = "The value of {0} must be between {1} and {2}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage,
                    Is.EqualTo($"The value of MyProp must be between {BetweenAttributeTests.UINT_MIN} and {BetweenAttributeTests.UINT_MAX}."));
    }

    [TestCaseSource(typeof(TestedCollections), nameof(TestedCollections.BetweenCorrectEnumerationValues))]
    public void Should_ReturnSuccess_When_ValueIsCollectionAndCorrect(IEnumerable<int> value)
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests.UINT_MIN, BetweenAttributeTests.UINT_MAX);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [TestCaseSource(typeof(TestedCollections), nameof(TestedCollections.BetweenNotCorrectEnumerationValues))]
    public void Should_ReturnValidationError_When_ValueIsCollectionAndNotCorrect(IEnumerable<int> value)
    {
        BetweenAttribute attribute
            = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX) { ErrorMessage = "The value of {0} must be between {1} and {2}." };

        ValidationContext validationContext = new(new()) { DisplayName = "MyProp" };

        ValidationResult? result = attribute.GetValidationResult(value, validationContext);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));

        Assert.That(result?.ErrorMessage,
                    Is.EqualTo($"The value of MyProp must be between {BetweenAttributeTests.UINT_MIN} and {BetweenAttributeTests.UINT_MAX}."));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX);
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        BetweenAttribute  attribute         = new(BetweenAttributeTests.DATE_MIN, BetweenAttributeTests.DATE_MAX);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }
}
