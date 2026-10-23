using System.ComponentModel.DataAnnotations;
using System.Text;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Validation.Rules.Abstractions.Errors;
using Zhomfr.Validation.Rules.Files.Tests.Stubs;

namespace Zhomfr.Validation.Rules.Files.Tests.Unit;

[TestFixture]
public class FileEncodingAttributeTests
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    [Test]
    public void Should_ReturnSuccess_When_ValueIsNull()
    {
        FileEncodingAttribute attribute         = new("UTF-8");
        ValidationContext     validationContext = new(new());

        ValidationResult? result = attribute.GetValidationResult(null, validationContext);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_Throw_When_ValueIsUnsupportedType()
    {
        FileEncodingAttribute attribute         = new("UTF-8");
        ValidationContext     validationContext = new(new());

        Assert.Throws<UnsupportedTypeException>(() => attribute.GetValidationResult(0, validationContext));
    }

    [TestCase("UTF-8", "Привіт, світ!")]
    [TestCase("windows-1251", "Тестова стрічка")]
    [TestCase("ASCII", "Simple text")]
    public void Should_ReturnSuccess_When_EncodingMatches(string encodingName, string content)
    {
        FileEncodingAttribute attribute = new(encodingName);
        Encoding              encoding  = Encoding.GetEncoding(encodingName);
        using SmartFile       file      = StorageFileFixture.CreateWithEncoding(content, encoding);
        ValidationContext     context   = new(new());

        ValidationResult? result = attribute.GetValidationResult(file, context);

        Assert.That(result, Is.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnFailure_When_EncodingIsInvalidForContent()
    {
        FileEncodingAttribute attribute = new("ASCII");
        using SmartFile       file      = StorageFileFixture.CreateWithEncoding("Кириличний текст", Encoding.UTF8);
        ValidationContext     context   = new(new());

        ValidationResult? result = attribute.GetValidationResult(file, context);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnFailure_When_FileContainsCorruptedBytesForUtf8()
    {
        FileEncodingAttribute attribute = new("UTF-8");

        byte[] invalidUtf8 = [0x48, 0x65, 0x6C, 0x6C, 0x6F, 0xFF];

        MemoryStream      stream  = new(invalidUtf8);
        using SmartFile   file    = new(stream);
        ValidationContext context = new(new());

        ValidationResult? result = attribute.GetValidationResult(file, context);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
    }

    [Test]
    public void Should_ReturnFailure_When_EncodingNameIsUnknown()
    {
        FileEncodingAttribute attribute = new("not-real-encoding-123");
        using SmartFile       file      = StorageFileFixture.CreateWithEncoding("test", Encoding.UTF8);
        ValidationContext     context   = new(new());

        ValidationResult? result = attribute.GetValidationResult(file, context);

        Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
    }
}
