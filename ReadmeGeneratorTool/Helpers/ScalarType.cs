using System.Text;
using System.Xml.Linq;

using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.ReadmeGeneratorTool.Helpers;

public class ScalarType() : AbstractTask("Helpers.ScalarTypes")
{
    private readonly List<string>                 _decimalMethods    = [];
    private readonly List<string>                 _enumerableMethods = [];
    private readonly Dictionary<string, XElement> _feature           = new();
    private readonly List<string>                 _stringMethods     = [];

    /// <inheritdoc/>
    protected override Dictionary<string, StringBuilder> Execute(IEnumerable<XElement> members)
    {
        Dictionary<string, StringBuilder> results = new();

        foreach (XElement member in members) {
            string name       = member.Attribute("name")?.Value!;
            string methodName = name.Before("(").AfterLast(".");

            if (!name.StartsWith("M:")) {
                continue;
            }

            if (name.Contains("ScalarTypes.DecimalExtensions")) {
                this._decimalMethods.Add(methodName);
            } else if (name.Contains("ScalarTypes.EnumerableExtensions")) {
                this._enumerableMethods.Add(methodName);
            } else {
                this._stringMethods.Add(methodName);
            }

            this._feature.Add(methodName, member);
        }

        results.Add("table_string", AbstractTask.GenerateTable([.. this._stringMethods.Order()]));
        results.Add("table_decimal", AbstractTask.GenerateTable([.. this._decimalMethods.Order()]));
        results.Add("table_enumerable", AbstractTask.GenerateTable([.. this._enumerableMethods.Order()]));
        results.Add("features", AbstractTask.GenerateFeature(this._feature));

        return results;
    }
}
