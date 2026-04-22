using CloudAwesome.MarkdownMaker.Converters;
using FluentAssertions;

namespace CloudAwesome.MarkdownMaker.Tests.Converters;

[TestFixture]
public class ListToMdListConverterTests
{
	[Test]
	public void ListOfStrings_Returns_Valid_Unordered_Markdown()
	{
		var list = new List<string> {"one", "two", "three"};
		var expectedMarkdown = 
			$"- one {Environment.NewLine}" +
			$"- two {Environment.NewLine}" +
			$"- three {Environment.NewLine}";

		var result = list.ToMdList(MdListType.Unordered);
		result.Markdown.Should().Be(expectedMarkdown);
	}
	
	[Test]
	public void ListOfStrings_Returns_Valid_Ordered_Markdown()
	{
		var list = new List<string> {"one", "two", "three"};
		var expectedMarkdown = 
			$"1. one {Environment.NewLine}" +
			$"1. two {Environment.NewLine}" +
			$"1. three {Environment.NewLine}";

		var result = list.ToMdList(MdListType.Ordered);
		result.Markdown.Should().Be(expectedMarkdown);
	}
}