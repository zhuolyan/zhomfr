using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class GreaterThanAttributeTests
{
    private const string ERROR_MESSAGE = "The {0} must be greater than: {1}.";
    private const string ERROR_FIELD   = "MyProperty";

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectCollection()
    {
        SizeValidationModel model = new();

        GreaterThanAttribute attributeField = new(nameof(SizeValidationModel.LessThanCollectionValidatedField));
        GreaterThanAttribute attributeSize  = new(3);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.GreaterThanCollectionValidatedField, validationContextSize);
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

        GreaterThanAttribute attributeField = new(nameof(SizeValidationModel.LessThanDictionaryValidatedField));
        GreaterThanAttribute attributeSize  = new(3);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.GreaterThanDictionaryValidatedField, validationContextSize);
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

        GreaterThanAttribute attributeField = new(nameof(SizeValidationModel.LessThanEnumerableValidatedField));
        GreaterThanAttribute attributeSize  = new(3);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.GreaterThanEnumerableValidatedField, validationContextSize);
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

        GreaterThanAttribute attributeField = new(nameof(SizeValidationModel.LessThanStringValidatedField));
        GreaterThanAttribute attributeSize  = new(4);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.GreaterThanStringValidatedField, validationContextSize);
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

        GreaterThanAttribute attributeField = new(nameof(SizeValidationModel.LessThanIntValidatedField));
        GreaterThanAttribute attributeSize  = new(2);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.GreaterThanIntValidatedField, validationContextSize);
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

        GreaterThanAttribute attributeField = new(nameof(SizeValidationModel.LessThanLongValidatedField));
        GreaterThanAttribute attributeSize  = new(2);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.GreaterThanLongValidatedField, validationContextSize);
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

        GreaterThanAttribute attributeField = new(nameof(SizeValidationModel.LessThanFloatValidatedField));
        GreaterThanAttribute attributeSize  = new(2.0);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.GreaterThanFloatValidatedField, validationContextSize);
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

        GreaterThanAttribute attributeField = new(nameof(SizeValidationModel.LessThanDoubleValidatedField));
        GreaterThanAttribute attributeSize  = new(2.0);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.GreaterThanDoubleValidatedField, validationContextSize);
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

        GreaterThanAttribute attributeField = new(nameof(SizeValidationModel.LessThanDecimalValidatedField));
        GreaterThanAttribute attributeSize  = new(2.0);

        ValidationContext validationContextSize  = new(new());
        ValidationContext validationContextFiled = new(model);

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.GreaterThanDecimalValidatedField, validationContextSize);
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

        GreaterThanAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToCollectionValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanCollectionValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeSize1 = new(3) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };
        GreaterThanAttribute attributeSize2 = new(3) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.CollectionValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.LessThanCollectionValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.CollectionValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.CollectionValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));

            Assert.That(resultEqualField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: EqualToCollectionValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: GreaterThanCollectionValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectDictionary()
    {
        SizeValidationModel model = new();

        GreaterThanAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToDictionaryValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanDictionaryValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeSize1 = new(3) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };
        GreaterThanAttribute attributeSize2 = new(3) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.DictionaryValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.LessThanDictionaryValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.DictionaryValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.DictionaryValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));

            Assert.That(resultEqualField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: EqualToDictionaryValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: GreaterThanDictionaryValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectEnumerable()
    {
        SizeValidationModel model = new();

        GreaterThanAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToEnumerableValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanEnumerableValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeSize1 = new(3) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };
        GreaterThanAttribute attributeSize2 = new(3) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.EnumerableValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.LessThanEnumerableValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.EnumerableValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.EnumerableValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));

            Assert.That(resultEqualField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: EqualToEnumerableValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: GreaterThanEnumerableValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectString()
    {
        SizeValidationModel model = new();

        GreaterThanAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToStringValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanStringValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeSize1 = new(4) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };
        GreaterThanAttribute attributeSize2 = new(4) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.StringValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.LessThanStringValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.StringValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.StringValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));

            Assert.That(resultEqualField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: EqualToStringValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: GreaterThanStringValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectInt()
    {
        SizeValidationModel model = new();

        GreaterThanAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToIntValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanIntValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeSize1 = new(2) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };
        GreaterThanAttribute attributeSize2 = new(2) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.IntValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.LessThanIntValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.IntValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.IntValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));
            Assert.That(resultEqualField?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: EqualToIntValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: GreaterThanIntValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectLong()
    {
        SizeValidationModel model = new();

        GreaterThanAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToLongValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanLongValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeSize1 = new(2) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };
        GreaterThanAttribute attributeSize2 = new(2) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.LongValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.LessThanLongValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.LongValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.LongValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));

            Assert.That(resultEqualField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: EqualToLongValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: GreaterThanLongValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectFloat()
    {
        SizeValidationModel model = new();

        GreaterThanAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToFloatValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanFloatValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeSize1 = new(2) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };
        GreaterThanAttribute attributeSize2 = new(2) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.FloatValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.LessThanFloatValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.FloatValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.FloatValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));

            Assert.That(resultEqualField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: EqualToFloatValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: GreaterThanFloatValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectDouble()
    {
        SizeValidationModel model = new();

        GreaterThanAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToDoubleValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanDoubleValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeSize1 = new(2) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };
        GreaterThanAttribute attributeSize2 = new(2) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.DoubleValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.LessThanDoubleValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.DoubleValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.DoubleValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));

            Assert.That(resultEqualField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: EqualToDoubleValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: GreaterThanDoubleValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectDecimal()
    {
        SizeValidationModel model = new();

        GreaterThanAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToDecimalValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanDecimalValidatedField)) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        GreaterThanAttribute attributeSize1 = new(2) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };
        GreaterThanAttribute attributeSize2 = new(2) { ErrorMessage = GreaterThanAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1        = attributeSize1.GetValidationResult(model.DecimalValidatedField, validationContextSize);
        ValidationResult? resultSize2        = attributeSize2.GetValidationResult(model.LessThanDecimalValidatedField, validationContextSize);
        ValidationResult? resultEqualField   = attributeEqualField.GetValidationResult(model.DecimalValidatedField, validationContextFiled);
        ValidationResult? resultGreaterField = attributeGreaterField.GetValidationResult(model.DecimalValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultGreaterField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize1?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));
            Assert.That(resultSize2?.ErrorMessage, Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: 3"));

            Assert.That(resultEqualField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: EqualToDecimalValidatedField"));

            Assert.That(resultGreaterField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanAttributeTests.ERROR_FIELD} must be greater than: GreaterThanDecimalValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        GreaterThanAttribute attribute         = new(2);
        ValidationContext    validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        GreaterThanAttribute attribute         = new(2);
        ValidationContext    validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(new SizeValidationModel(), validationContext));
    }

    [Test]
    public void Should_ThrowValuesHasDifferentTypesException_Then_FieldsAreDifferentTypes()
    {
        SizeValidationModel model = new();

        GreaterThanAttribute attribute = new(nameof(SizeValidationModel.EqualToDecimalValidatedField));

        ValidationContext validationContext = new(model);

        Assert.Throws<ValuesHasDifferentTypesException>(() => attribute.GetValidationResult(model.FloatValidatedField, validationContext));
    }

    [Test]
    public void Should_ThrowArgumentException_Then_FieldsAreDifferentTypes()
    {
        SizeValidationModel model = new();

        GreaterThanAttribute attribute = new("NotExistsProperty");

        ValidationContext validationContext = new(model);

        Assert.Throws<FiledNotExistsException>(() => attribute.GetValidationResult(model.FloatValidatedField, validationContext));
    }
}
