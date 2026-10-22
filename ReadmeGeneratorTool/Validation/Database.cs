using System.Text;
using System.Xml.Linq;

using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.ReadmeGeneratorTool.Validation;

public class Database() : AbstractTask("Validation.Rules.Database")
{
    private readonly Dictionary<string, XElement> _feature = new();
    private readonly List<string>                 _table   = [];

    /// <inheritdoc/>
    protected override Dictionary<string, StringBuilder> Execute(IEnumerable<XElement> members)
    {
        Dictionary<string, StringBuilder> results = new();

        foreach (XElement member in members) {
            string name = member.Attribute("name")?.Value!;

            if (!name.StartsWith("T:")) {
                continue;
            }

            string className = name.Before("`").AfterLast(".").Remove("Attribute");

            this._table.Add(className);
            this._feature.Add(className, member);
        }

        results.Add("table", AbstractTask.GenerateTable([.. this._table.Distinct().Order()]));

        results.Add("features", AbstractTask.GenerateFeature(this._feature));

        return results;
    }
}
