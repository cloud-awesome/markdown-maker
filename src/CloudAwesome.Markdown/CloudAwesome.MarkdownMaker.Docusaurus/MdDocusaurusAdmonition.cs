namespace CloudAwesome.MarkdownMaker.Docusaurus;

public class MdDocusaurusAdmonition: IDocumentPart
{
	private readonly string? _typeString;
	private readonly string _content;
	private readonly string _title;

	public string Markdown =>
		$"""
		 :::{_typeString!.ToLower()} {_title}

		 {_content}
		 :::
		 """;

	public MdDocusaurusAdmonition(AdmonitionType type, string content, string title = "")
	{
		_content = $"{content}{Environment.NewLine}";
		_title = title;

		_typeString = ParseEnumToString(type);
	}
	
	public MdDocusaurusAdmonition(AdmonitionType type, MdParagraph paragraph, string title = "")
	{
		_content = paragraph.Markdown;
		_title = title;
		
		_typeString = ParseEnumToString(type);
	}
	
	private string? ParseEnumToString(AdmonitionType type) => Enum.GetName(typeof(AdmonitionType), type);
}

public enum AdmonitionType
{
	Note,
	Tip,
	Info,
	Warning,
	Danger
}