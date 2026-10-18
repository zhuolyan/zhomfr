namespace Zhomfr.Validation.Rules.Tests.Stubs;

public class ModelWithAnotherField<T>
{
    public required T AnotherField2;
    public required T Field        { get; set; }
    public required T AnotherField { get; set; }
}
