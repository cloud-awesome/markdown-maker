using YamlDotNet.Serialization;

namespace CloudAwesome.MarkdownMaker;

public class MdFrontMatter: IDocumentPart
{
	public string Markdown
	{
		get
		{
			var serialiser = new SerializerBuilder()
				.ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
				.Build();
			var yamlOutput = serialiser.Serialize(FrontMatter);
			
			return $"---{Environment.NewLine}{yamlOutput}---{Environment.NewLine}";
		}
	}
	
	private IFrontMatter FrontMatter { get; set; }

	public MdFrontMatter(IFrontMatter frontMatter)
	{
		FrontMatter = frontMatter;
	}
}