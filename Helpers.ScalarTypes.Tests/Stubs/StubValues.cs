namespace Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

public static class StubValues
{
    public const string MASK_VALUE              = "username@example.com";
    public const string MASK_SHORT_VALUE        = "user";
    public const string EXCERPT_VALUE           = "This is my name";
    public const string CHOP_START_VALUE        = "https://example.com";
    public const string CHOP_END_VALUE          = "file.test.php";
    public const string VALUE                   = "the quick brown fox jumps over the lazy dog";
    public const string SUBSTRING_REPLACE_VALUE = "123456";

    public static List<SwapStubValue> SwapStub =>
    [
        new() { Input = "Tacos are great!", Expected   = "Burritos are fantastic!", Replacements = new() { { "Tacos", "Burritos" }, { "great", "fantastic" } } },
        new() { Input = "first second third", Expected = "third third third", Replacements       = new() { { "first", "second" }, { "second", "third" } } },
    ];

    public static List<UcSplitStubValue> UcSplitStub =>
    [
        new() { Input = string.Empty, Expected     = [] },
        new() { Input = StubValues.VALUE, Expected = [StubValues.VALUE] },
        new()
        {
            Input    = "The Quick Brown Fox Jumps Over The Lazy Dog",
            Expected = ["The ", "Quick ", "Brown ", "Fox ", "Jumps ", "Over ", "The ", "Lazy ", "Dog"],
        },
        new()
        {
            Input = "the Quick Brown Fox Jumps Over the Lazy Dog", Expected = ["the ", "Quick ", "Brown ", "Fox ", "Jumps ", "Over the ", "Lazy ", "Dog"],
        },
    ];
}
