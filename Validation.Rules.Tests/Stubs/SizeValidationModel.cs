namespace Zhomfr.Validation.Rules.Tests.Stubs;

public class SizeValidationModel
{
    public readonly string[] CollectionValidatedField = ["test", "not", "blah"];

    public readonly decimal                    DecimalValidatedField    = 2m;
    public readonly Dictionary<string, string> DictionaryValidatedField = new() { { "first", "test" }, { "second", "not" }, { "third", "blah" } };

    public readonly double                     DoubleValidatedField            = 2;
    public readonly IEnumerable<int>           EnumerableValidatedField        = TestedCollections.GetEnumerable(3);
    public readonly string[]                   EqualToCollectionValidatedField = ["test", "not", "blah"];
    public readonly decimal                    EqualToDecimalValidatedField    = 2m;
    public readonly Dictionary<string, string> EqualToDictionaryValidatedField = new() { { "fourth", "test1" }, { "five", "not" }, { "six", "blah" } };
    public readonly double                     EqualToDoubleValidatedField     = 2;
    public readonly IEnumerable<int>           EqualToEnumerableValidatedField = TestedCollections.GetEnumerable(3);
    public readonly float                      EqualToFloatValidatedField      = 2f;
    public readonly int                        EqualToIntValidatedField        = 2;
    public readonly long                       EqualToLongValidatedField       = 2L;
    public readonly string                     EqualToStringValidatedField     = "test";

    public readonly float FloatValidatedField = 2f;

    public readonly string[] GreaterThanCollectionValidatedField = ["test", "not", "blah", "something"];
    public readonly decimal  GreaterThanDecimalValidatedField    = 2.000001m;

    public readonly Dictionary<string, string> GreaterThanDictionaryValidatedField
        = new() { { "first", "test" }, { "second", "not" }, { "third", "blah" }, { "fourth", "test1" } };

    public readonly double GreaterThanDoubleValidatedField = 2.000001;

    public readonly IEnumerable<int> GreaterThanEnumerableValidatedField = TestedCollections.GetEnumerable(4);
    public readonly float            GreaterThanFloatValidatedField      = 2.000001f;
    public readonly int              GreaterThanIntValidatedField        = 3;
    public readonly long             GreaterThanLongValidatedField       = 3L;
    public readonly string           GreaterThanStringValidatedField     = "test1";

    public readonly int IntValidatedField = 2;

    public readonly string[] LessThanCollectionValidatedField = ["test", "not"];
    public readonly decimal  LessThanDecimalValidatedField    = 1.999999m;

    public readonly Dictionary<string, string> LessThanDictionaryValidatedField = new() { { "first", "test" }, { "second", "not" } };
    public readonly double                     LessThanDoubleValidatedField     = 1.999999;
    public readonly IEnumerable<int>           LessThanEnumerableValidatedField = TestedCollections.GetEnumerable(2);
    public readonly float                      LessThanFloatValidatedField      = 1.999999f;
    public readonly int                        LessThanIntValidatedField        = 1;
    public readonly long                       LessThanLongValidatedField       = 1L;
    public readonly string                     LessThanStringValidatedField     = "tes";

    public readonly long LongValidatedField = 2L;

    public readonly string StringValidatedField = "Test";
}
