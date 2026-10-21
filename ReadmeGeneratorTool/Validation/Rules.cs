using System.Text;
using System.Xml.Linq;

using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.ReadmeGeneratorTool.Validation;

public class Rules() : AbstractTask("Validation.Rules")
{
    private readonly List<string> _booleans   = [];
    private readonly List<string> _collection = [];
    private readonly List<string> _dateTime   = [];
    private readonly List<string> _decimal    = [];
    private readonly List<string> _dictionary = [];

    private readonly Dictionary<string, XElement> _feature = new();
    private readonly List<string>                 _float   = [];
    private readonly List<string>                 _int     = [];
    private readonly List<string>                 _string  = [];

    /// <inheritdoc/>
    protected override Dictionary<string, StringBuilder> Execute(IEnumerable<XElement> members)
    {
        Dictionary<string, StringBuilder> results = new();

        foreach (XElement member in members) {
            string name = member.Attribute("name")?.Value!;

            if (!name.StartsWith("T:")) {
                continue;
            }

            string   className = name.Before("`").AfterLast(".").Remove("Attribute");
            string[] types     = (member.Element("target-type")?.Value ?? "object").Split(",");

            foreach (string type in types) {
                switch (type) {
                    case "bool":
                        this._booleans.Add(className);

                        break;
                    case "string":
                        this._string.Add(className);

                        break;
                    case "short":
                    case "ushort":
                    case "int":
                    case "uint":
                    case "long":
                    case "ulong":
                        this._int.Add(className);

                        break;
                    case "float":
                        this._float.Add(className);

                        break;
                    case "double":
                        this._float.Add(className);

                        break;
                    case "decimal":
                        this._decimal.Add(className);

                        break;
                    case "DateTime":
                    case "DateTimeOffset":
                        this._dateTime.Add(className);

                        break;
                    case "ICollection":
                    case "IEnumerable":
                        this._collection.Add(className);

                        break;
                    case "IDictionary":
                        this._dictionary.Add(className);

                        break;
                }
            }

            this._feature.Add(className, member);
        }

        results.Add("table_boolean", AbstractTask.GenerateTable([.. this._booleans.Distinct().Order()]));
        results.Add("table_string", AbstractTask.GenerateTable([.. this._string.Distinct().Order()]));
        results.Add("table_int", AbstractTask.GenerateTable([.. this._int.Distinct().Order()]));
        results.Add("table_float", AbstractTask.GenerateTable([.. this._float.Distinct().Order()]));
        results.Add("table_decimal", AbstractTask.GenerateTable([.. this._decimal.Distinct().Order()]));
        results.Add("table_dateTime", AbstractTask.GenerateTable([.. this._dateTime.Distinct().Order()]));
        results.Add("table_collection", AbstractTask.GenerateTable([.. this._collection.Distinct().Order()]));
        results.Add("table_dictionary", AbstractTask.GenerateTable([.. this._dictionary.Distinct().Order()]));

        results.Add("features", AbstractTask.GenerateFeature(this._feature));

        return results;
    }
}
