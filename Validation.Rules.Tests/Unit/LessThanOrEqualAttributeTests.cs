using System.ComponentModel.DataAnnotations;

using Zhomfr.Validation.Rules.Abstractions;
using Zhomfr.Validation.Rules.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Tests.Unit;

[TestFixture]
public class LessThanOrEqualAttributeTests
{
    private const string ERROR_MESSAGE = "The {0} must be less than or equal: {1}.";
    private const string ERROR_FIELD   = "MyProperty";

    [Test]
    public void Should_ReturnSuccess_Then_ValueIsCorrectCollection()
    {
        SizeValidationModel model = new();

        LessThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToCollectionValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanCollectionValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(3) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.CollectionValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.LessThanCollectionValidatedField, validationContextSize);
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

        LessThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToDictionaryValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanDictionaryValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(3) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.DictionaryValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.LessThanDictionaryValidatedField, validationContextSize);
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

        LessThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToEnumerableValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanEnumerableValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(3) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.EnumerableValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.LessThanEnumerableValidatedField, validationContextSize);
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

        LessThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToStringValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanStringValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(4) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.StringValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.LessThanStringValidatedField, validationContextSize);
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

        LessThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToIntValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanIntValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(2) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.IntValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.LessThanIntValidatedField, validationContextSize);
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

        LessThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToLongValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanLongValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(2) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.LongValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.LessThanLongValidatedField, validationContextSize);
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

        LessThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToFloatValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanFloatValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(2.0) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.FloatValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.LessThanFloatValidatedField, validationContextSize);
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

        LessThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToDoubleValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanDoubleValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(2.0) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.DoubleValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.LessThanDoubleValidatedField, validationContextSize);
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

        LessThanOrEqualAttribute attributeEqualField
            = new(nameof(SizeValidationModel.EqualToDecimalValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeGreaterField
            = new(nameof(SizeValidationModel.GreaterThanDecimalValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(2.0) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize1      = attributeSize.GetValidationResult(model.DecimalValidatedField, validationContextSize);
        ValidationResult? resultSize2      = attributeSize.GetValidationResult(model.LessThanDecimalValidatedField, validationContextSize);
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

        LessThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.GreaterThanCollectionValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(3) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanCollectionValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.CollectionValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: 3"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: EqualToCollectionValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectDictionary()
    {
        SizeValidationModel model = new();

        LessThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.GreaterThanDictionaryValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(3) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanDictionaryValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.DictionaryValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: 3"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: EqualToDictionaryValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectEnumerable()
    {
        SizeValidationModel model = new();

        LessThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.GreaterThanEnumerableValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(3) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanEnumerableValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.EnumerableValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: 3"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: EqualToEnumerableValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectString()
    {
        SizeValidationModel model = new();

        LessThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.GreaterThanStringValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(4) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanStringValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.StringValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: 4"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: EqualToStringValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectInt()
    {
        SizeValidationModel model = new();

        LessThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.GreaterThanIntValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(2) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanIntValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.IntValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: 2"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: EqualToIntValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectLong()
    {
        SizeValidationModel model = new();

        LessThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.GreaterThanLongValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(2) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanLongValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.LongValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: 2"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: EqualToLongValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectFloat()
    {
        SizeValidationModel model = new();

        LessThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.GreaterThanFloatValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(2) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanFloatValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.FloatValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: 2"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: EqualToFloatValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectDouble()
    {
        SizeValidationModel model = new();

        LessThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.GreaterThanDoubleValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(2) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanDoubleValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.DoubleValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: 2"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: EqualToDoubleValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnValidationError_Then_ValueIsNotCorrectDecimal()
    {
        SizeValidationModel model = new();

        LessThanOrEqualAttribute attributeField
            = new(nameof(SizeValidationModel.GreaterThanDecimalValidatedField)) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        LessThanOrEqualAttribute attributeSize = new(2) { ErrorMessage = LessThanOrEqualAttributeTests.ERROR_MESSAGE };

        ValidationContext validationContextSize  = new(new()) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };
        ValidationContext validationContextFiled = new(model) { DisplayName = LessThanOrEqualAttributeTests.ERROR_FIELD };

        ValidationResult? resultSize  = attributeSize.GetValidationResult(model.LessThanDecimalValidatedField, validationContextSize);
        ValidationResult? resultField = attributeField.GetValidationResult(model.DecimalValidatedField, validationContextFiled);

        using (Assert.EnterMultipleScope()) {
            Assert.That(resultSize, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(resultField, Is.Not.EqualTo(ValidationResult.Success));

            Assert.That(resultSize?.ErrorMessage, Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: 2"));

            Assert.That(resultField?.ErrorMessage,
                        Is.EqualTo($"The {LessThanOrEqualAttributeTests.ERROR_FIELD} must be less than or equal: EqualToDecimalValidatedField"));
        }
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        LessThanOrEqualAttribute attribute         = new(2);
        ValidationContext        validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ThrowUnsupportedTypeException_When_TestedValueIsUnsupportedTypeValue()
    {
        LessThanOrEqualAttribute attribute         = new(2);
        ValidationContext        validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(new SizeValidationModel(), validationContext));
    }

    [Test]
    public void Should_ThrowValuesHasDifferentTypesException_Then_FieldsAreDifferentTypes()
    {
        SizeValidationModel model = new();

        LessThanOrEqualAttribute attribute = new(nameof(SizeValidationModel.EqualToDecimalValidatedField));

        ValidationContext validationContext = new(model);

        Assert.Throws<ValuesHasDifferentTypesException>(() => attribute.GetValidationResult(model.FloatValidatedField, validationContext));
    }

    [Test]
    public void Should_ThrowArgumentException_Then_FieldsAreDifferentTypes()
    {
        SizeValidationModel model = new();

        LessThanOrEqualAttribute attribute = new("NotExistsProperty");

        ValidationContext validationContext = new(model);

        Assert.Throws<FiledNotExistsException>(() => attribute.GetValidationResult(model.FloatValidatedField, validationContext));
    }
}
