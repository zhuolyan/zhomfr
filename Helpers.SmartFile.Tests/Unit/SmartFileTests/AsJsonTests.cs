using System.Text.Json;

using Zhomfr.Helpers.SmartFile.Tests.Stubs;

namespace Zhomfr.Helpers.SmartFile.Tests.Unit.SmartFileTests;

[TestFixture]
public class AsJsonTests
{
    [Test]
    public void Should_ReturnDeserializedObject_When_JsonString()
    {
        using SmartFile file = new(FileFixtures.SampleObject, "user.json");

        FileFixtures.TestObject? result = file.AsJson<FileFixtures.TestObject>();

        Assert.That(result, Is.Not.Null);

        using (Assert.EnterMultipleScope()) {
            Assert.That(result.Name, Is.EqualTo(FileFixtures.SampleObject.Name));
            Assert.That(result.Id, Is.EqualTo(FileFixtures.SampleObject.Id));
        }
    }

    [Test]
    public void Should_ThrowException_When_JsonIsInvalid()
    {
        using SmartFile file = new("{ invalid json }", "data.json");

        Assert.Throws<JsonException>(() => file.AsJson<Dictionary<string, string>>());
    }

    [Test]
    [CancelAfter(1000)]
    public async Task Should_ReturnDeserializedObject_When_JsonString(CancellationToken ct)
    {
        const string          MESSAGE = "Success";
        var                   data    = new { Message = MESSAGE };
        await using SmartFile file    = new(data, "api.json");

        Dictionary<string, string>? result = await file.AsJsonAsync<Dictionary<string, string>>(ct: ct);

        Assert.That(result!["Message"], Is.EqualTo(MESSAGE));
    }

    [Test]
    [CancelAfter(1000)]
    public async Task Should_ThrowException_When_JsonIsInvalid(CancellationToken ct)
    {
        await using SmartFile file = new("{ invalid json }", "data.json");

        Assert.ThrowsAsync<JsonException>(() => _ = file.AsJsonAsync<Dictionary<string, string>>(ct: ct));
    }
}
