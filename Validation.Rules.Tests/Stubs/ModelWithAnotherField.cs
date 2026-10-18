namespace Zhomfr.Validation.Rules.Tests.Stubs;

public class ModelWithAnotherField<T>
{
    public required T         Field        { get; set; }
    public required T         AnotherField { get; set; }
    public          WrongType WrongField   { get; set; } = new();
    public class WrongType;
}
