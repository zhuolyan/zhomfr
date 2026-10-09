using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class GreaterThanOrEqualAttributeTests
{
    private const string ERROR_MESSAGE = "The {0} must be greater than or equal: {1}.";
    private const string ERROR_FIELD   = "MyProperty";

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectCollection()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToCollectionValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanCollectionValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(3) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.CollectionValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.GreaterThanCollectionValidatedField, validationContextSize);
        ValidationResult? resultEqualField = attributeEqualField.GetValidationResult(model.CollectionValidatedField, validationContextFiled);
        ValidationResult? resultLessField  = attributeGreaterField.GetValidationResult(model.CollectionValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultLessField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectDictionary()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToDictionaryValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanDictionaryValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(3) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.DictionaryValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.GreaterThanDictionaryValidatedField, validationContextSize);
        ValidationResult? resultEqualField = attributeEqualField.GetValidationResult(model.DictionaryValidatedField, validationContextFiled);
        ValidationResult? resultLessField  = attributeGreaterField.GetValidationResult(model.DictionaryValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultLessField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectEnumerable()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToEnumerableValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanEnumerableValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(3) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.EnumerableValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.GreaterThanEnumerableValidatedField, validationContextSize);
        ValidationResult? resultEqualField = attributeEqualField.GetValidationResult(model.EnumerableValidatedField, validationContextFiled);
        ValidationResult? resultLessField  = attributeGreaterField.GetValidationResult(model.EnumerableValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultLessField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectString()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToStringValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanStringValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(4) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.StringValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.GreaterThanStringValidatedField, validationContextSize);
        ValidationResult? resultEqualField = attributeEqualField.GetValidationResult(model.StringValidatedField, validationContextFiled);
        ValidationResult? resultLessField  = attributeGreaterField.GetValidationResult(model.StringValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultLessField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectInt()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToIntValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanIntValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(2) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.IntValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.GreaterThanIntValidatedField, validationContextSize);
        ValidationResult? resultEqualField = attributeEqualField.GetValidationResult(model.IntValidatedField, validationContextFiled);
        ValidationResult? resultLessField  = attributeGreaterField.GetValidationResult(model.IntValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultLessField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectLong()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToLongValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanLongValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(2) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.LongValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.GreaterThanLongValidatedField, validationContextSize);
        ValidationResult? resultEqualField = attributeEqualField.GetValidationResult(model.LongValidatedField, validationContextFiled);
        ValidationResult? resultLessField  = attributeGreaterField.GetValidationResult(model.LongValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultLessField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectFloat()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToFloatValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanFloatValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(2.0) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.FloatValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.GreaterThanFloatValidatedField, validationContextSize);
        ValidationResult? resultEqualField = attributeEqualField.GetValidationResult(model.FloatValidatedField, validationContextFiled);
        ValidationResult? resultLessField  = attributeGreaterField.GetValidationResult(model.FloatValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultLessField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectDouble()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToDoubleValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanDoubleValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(2.0) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.DoubleValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.GreaterThanDoubleValidatedField, validationContextSize);
        ValidationResult? resultEqualField = attributeEqualField.GetValidationResult(model.DoubleValidatedField, validationContextFiled);
        ValidationResult? resultLessField  = attributeGreaterField.GetValidationResult(model.DoubleValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultLessField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectDecimal()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToDecimalValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.LessThanDecimalValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(2.0) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.DecimalValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.GreaterThanDecimalValidatedField, validationContextSize);
        ValidationResult? resultEqualField = attributeEqualField.GetValidationResult(model.DecimalValidatedField, validationContextFiled);
        ValidationResult? resultLessField  = attributeGreaterField.GetValidationResult(model.DecimalValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize1, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultSize2, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultEqualField, Is.EqualTo(ValidationResult.Success));
            Assert.That(resultLessField, Is.EqualTo(ValidationResult.Success));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectCollection()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.LessThanCollectionValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(3) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanCollectionValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.CollectionValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: 3"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: EqualToCollectionValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectDictionary()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.LessThanDictionaryValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(3) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanDictionaryValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.DictionaryValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: 3"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: EqualToDictionaryValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectEnumerable()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.LessThanEnumerableValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(3) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanEnumerableValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.EnumerableValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: 3"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: EqualToEnumerableValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectString()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.LessThanStringValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(4) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanStringValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.StringValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: 4"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: EqualToStringValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectInt()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.LessThanIntValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(2) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanIntValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.IntValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: 2"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: EqualToIntValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectLong()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.LessThanLongValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(2) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanLongValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.LongValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: 2"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: EqualToLongValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectFloat()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.LessThanFloatValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(2) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanFloatValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.FloatValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: 2"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: EqualToFloatValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectDouble()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.LessThanDoubleValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(2) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanDoubleValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.DoubleValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: 2"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: EqualToDoubleValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectDecimal()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.LessThanDecimalValidatedField)) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        GreaterThanOrEqualAttribute attributeSize = new(2) { ErrorMessage = GreaterThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = GreaterThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanDecimalValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.DecimalValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: 2"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {GreaterThanOrEqualAttributeTests.ERROR_FIELD} must be greater than or equal: EqualToDecimalValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        GreaterThanOrEqualAttribute attribute         = new(2);
        ValidationContext           validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        GreaterThanOrEqualAttribute attribute         = new(2);
        ValidationContext           validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(new SizeValidationModel(), validationContext));
    }

    [Test]
    public void Should_ThrowValuesHasDifferentTypesException_Then_FieldsAreDifferentTypes()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attribute = new(nameof(SizeValidationModel.EqualToDecimalValidatedField));

        ValidationContext validationContext = new(model);

        Assert.Throws<ValuesHasDifferentTypesException>(() => attribute.GetValidationResult(model.FloatValidatedField, validationContext));
    }

    [Test]
    public void Should_ThrowArgumentException_Then_FieldsAreDifferentTypes()
    {
        SizeValidationModel model = new();

        GreaterThanOrEqualAttribute attribute = new("NotExistsProperty");

        ValidationContext validationContext = new(model);

        Assert.Throws<FiledNotExistsException>(() => attribute.GetValidationResult(model.FloatValidatedField, validationContext));
    }
}
