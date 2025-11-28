using System.Diagnostics.CodeAnalysis;

namespace CloudAwesome.MarkdownMaker.Converters;

[ExcludeFromCodeCoverage(Justification = "Not yet implemented")]
public class ClassToMarkdownConverter: IDocumentPart
{
	public string Markdown { get; }

	public ClassToMarkdownConverter(IDocumentMe documentMeClass)
	{
		
	}
}