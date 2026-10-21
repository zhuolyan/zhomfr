using System.IO.Abstractions;
using System.Text;
using System.Xml.Linq;

using Zhomfr.Helpers.ScalarTypes.Strings;

namespace Zhomfr.ReadmeGeneratorTool;

public abstract class AbstractTask(string project)
{
    private const    string        TEMPLATE = "README.template.md";
    private const    string        RESULT   = "README.md";
    private readonly FileSystem    _fs      = new();
    private readonly StringBuilder _sb      = new();

    public async Task Run(CancellationToken ct = default)
    {
        string basePath     = Path.Combine("..", "..", "..", "..");
        string templatePath = Path.Combine(basePath, project, AbstractTask.TEMPLATE);
        string resultPath   = Path.Combine(basePath, project, AbstractTask.RESULT);
        string docPath      = Path.Combine(basePath, "docs", $"{project}.xml");

        this._sb.Append(await this._fs.File.ReadAllTextAsync(templatePath, ct));

        await using Stream result = this._fs.File.OpenRead(resultPath);
        await using Stream xmlDoc = this._fs.File.OpenRead(docPath);

        XDocument document = await XDocument.LoadAsync(xmlDoc, LoadOptions.None, ct);

        IEnumerable<XElement> members = document.Descendants("member")
                                                .Where(i => i.Attribute("name")?.Value.Contains($"Zhomfr.{project}") == true && i.Element("summary") is not null);

        Dictionary<string, StringBuilder> replacements = this.Execute(members);

        foreach (KeyValuePair<string, StringBuilder> item in replacements) {
            this._sb.Replace($"{{{{{item.Key.ToUpper()}}}}}", item.Value.ToString());
        }

        await this._fs.File.WriteAllTextAsync(resultPath, this._sb.ToString(), ct);
    }

    protected static StringBuilder GenerateFeature(Dictionary<string, XElement> items)
    {
        StringBuilder result = new();

        foreach (KeyValuePair<string, XElement> item in items.OrderBy(i => i.Key)) {
            string summary = item.Value.Element("summary")?.Value.Trim()!;
            string example = item.Value.Element("example")?.Element("code")?.Value.Trim() ?? string.Empty;

            result.AppendLine($"### {item.Key}");
            result.AppendLine();
            result.AppendLine(summary.Deduplicate().Replace("\n ", "\n"));
            result.AppendLine();

            if (!string.IsNullOrWhiteSpace(example)) {
                AbstractTask.AddExample(result, example);
            }
        }

        return result;
    }

    private static void AddExample(StringBuilder result, string example)
    {
        result.AppendLine("```csharp");

        string[] lines = example.Split("\n                    ");

        foreach (string line in lines) {
            ;
            result.AppendLine(line);
        }

        result.AppendLine("```");
    }

    protected static StringBuilder GenerateTable(string[] items)
    {
        StringBuilder result    = new();
        int           rowsCount = (items.Length + 2) / 3;

        result.AppendLine("<table>");

        result.AppendLine("<tr valign=\"top\">");

        for (int j = 1; j <= 3; j++) {
            result.AppendLine("<td>");
            result.AppendLine();

            for (int i = 0; i < rowsCount; i++) {
                int current = i * j;

                if (current >= items.Length) {
                    continue;
                }

                string item = items[i * j];
                result.AppendLine($" - [{item}](#{item.ToLowerInvariant()})");
            }

            result.AppendLine();
            result.AppendLine("</td>");
        }

        result.AppendLine("</tr>");

        result.AppendLine("</table>");

        return result;
    }

    protected abstract Dictionary<string, StringBuilder> Execute(IEnumerable<XElement> members);
}
