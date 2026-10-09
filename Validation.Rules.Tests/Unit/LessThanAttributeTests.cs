using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class LessThanAttributeTests
{
    private const string ERROR_MESSAGE = "The {0} must be less than: {1}.";
    private const string ERROR_FIELD   = "MyProperty";

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectCollection()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeField = new(nameof(SizeValidationModel.GreaterThanCollectionValidatedField));
        LessThanAttribute attributeSize  = new(3);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanCollectionValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.CollectionValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectDictionary()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeField = new(nameof(SizeValidationModel.GreaterThanDictionaryValidatedField));
        LessThanAttribute attributeSize  = new(3);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanDictionaryValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.DictionaryValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectEnumerable()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeField = new(nameof(SizeValidationModel.GreaterThanEnumerableValidatedField));
        LessThanAttribute attributeSize  = new(3);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanEnumerableValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.EnumerableValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectString()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeField = new(nameof(SizeValidationModel.GreaterThanStringValidatedField));
        LessThanAttribute attributeSize  = new(4);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanStringValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.StringValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectInt()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeField = new(nameof(SizeValidationModel.GreaterThanIntValidatedField));
        LessThanAttribute attributeSize  = new(2);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanIntValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.IntValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectLong()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeField = new(nameof(SizeValidationModel.GreaterThanLongValidatedField));
        LessThanAttribute attributeSize  = new(2);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanLongValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.LongValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectFloat()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeField = new(nameof(SizeValidationModel.GreaterThanFloatValidatedField));
        LessThanAttribute attributeSize  = new(2.0);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanFloatValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.FloatValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectDouble()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeField = new(nameof(SizeValidationModel.GreaterThanDoubleValidatedField));
        LessThanAttribute attributeSize  = new(2.0);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanDoubleValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.DoubleValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectDecimal()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeField = new(nameof(SizeValidationModel.GreaterThanDecimalValidatedField));
        LessThanAttribute attributeSize  = new(2.0);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanDecimalValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.DecimalValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectCollection()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToCollectionValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanCollectionValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeSize1 = new(3) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };
        LessThanAttribute attributeSize2 = new(3) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.CollectionValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.CollectionValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.CollectionValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.CollectionValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));

            Assert.That(resultEqualField?.ErrorMessage,
                        Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: EqualToCollectionValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage,
                        Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: LessThanCollectionValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectDictionary()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToDictionaryValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanDictionaryValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeSize1 = new(3) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };
        LessThanAttribute attributeSize2 = new(3) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.DictionaryValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.DictionaryValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.DictionaryValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.DictionaryValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));

            Assert.That(resultEqualField?.ErrorMessage,
                        Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: EqualToDictionaryValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage,
                        Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: LessThanDictionaryValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectEnumerable()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToEnumerableValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanEnumerableValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeSize1 = new(3) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };
        LessThanAttribute attributeSize2 = new(3) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.EnumerableValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.EnumerableValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.EnumerableValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.EnumerableValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));

            Assert.That(resultEqualField?.ErrorMessage,
                        Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: EqualToEnumerableValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage,
                        Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: LessThanEnumerableValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectString()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToStringValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanStringValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeSize1 = new(4) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };
        LessThanAttribute attributeSize2 = new(4) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.StringValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.GreaterThanStringValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.StringValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.StringValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));

            Assert.That(resultEqualField?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: EqualToStringValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: LessThanStringValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectInt()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeEqualField = new(nameof(SizeValidationModel.EqualToIntValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanIntValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeSize1 = new(2) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };
        LessThanAttribute attributeSize2 = new(2) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.IntValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.GreaterThanIntValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.IntValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.IntValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));
            Assert.That(resultEqualField?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: EqualToIntValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: LessThanIntValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectLong()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeEqualField = new(nameof(SizeValidationModel.EqualToLongValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanLongValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeSize1 = new(2) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };
        LessThanAttribute attributeSize2 = new(2) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.LongValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.GreaterThanLongValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.LongValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.LongValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));

            Assert.That(resultEqualField?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: EqualToLongValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: LessThanLongValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectFloat()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToFloatValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanFloatValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeSize1 = new(2) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };
        LessThanAttribute attributeSize2 = new(2) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.FloatValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.GreaterThanFloatValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.FloatValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.FloatValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));

            Assert.That(resultEqualField?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: EqualToFloatValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: LessThanFloatValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectDouble()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToDoubleValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanDoubleValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeSize1 = new(2) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };
        LessThanAttribute attributeSize2 = new(2) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.DoubleValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.GreaterThanDoubleValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.DoubleValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.DoubleValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));

            Assert.That(resultEqualField?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: EqualToDoubleValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: LessThanDoubleValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectDecimal()
    {
        SizeValidationModel model = new();

        LessThanAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToDecimalValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanDecimalValidatedField)) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        LessThanAttribute attributeSize1 = new(2) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };
        LessThanAttribute attributeSize2 = new(2) { ErrorMessage = LessThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.DecimalValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.GreaterThanDecimalValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.DecimalValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.DecimalValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: 3"));

            Assert.That(resultEqualField?.ErrorMessage, Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: EqualToDecimalValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage,
                        Is.EqualTo($"The {LessThanAttributeTests.ERROR_FIELD} must be less than: LessThanDecimalValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        LessThanAttribute attribute         = new(2);
        ValidationContext validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        LessThanAttribute attribute         = new(2);
        ValidationContext validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(new SizeValidationModel(), validationContext));
    }

    [Test]
    public void Should_ThrowValuesHasDifferentTypesException_Then_FieldsAreDifferentTypes()
    {
        SizeValidationModel model = new();

        LessThanAttribute attribute = new(nameof(SizeValidationModel.EqualToDecimalValidatedField));

        ValidationContext validationContext = new(model);

        Assert.Throws<ValuesHasDifferentTypesException>(() => attribute.GetValidationResult(model.FloatValidatedField, validationContext));
    }

    [Test]
    public void Should_ThrowArgumentException_Then_FieldsAreDifferentTypes()
    {
        SizeValidationModel model = new();

        LessThanAttribute attribute = new("NotExistsProperty");

        ValidationContext validationContext = new(model);

        Assert.Throws<FiledNotExistsException>(() => attribute.GetValidationResult(model.FloatValidatedField, validationContext));
    }
}
