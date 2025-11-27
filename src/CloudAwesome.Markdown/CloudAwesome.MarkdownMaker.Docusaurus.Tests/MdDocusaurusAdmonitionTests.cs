using FluentAssertions;

namespace CloudAwesome.MarkdownMaker.Docusaurus.Tests;

[TestFixture]
public class MdDocusaurusAdmonitionTests
{
	[Test]
	public void Note_Admonition_Returns_Valid_Markdown()
	{
		var doc = new MdDocument();
		doc.Add(new MdDocusaurusAdmonition(
			AdmonitionType.Note, 
			"Here is some content for the note."));
		var result = doc.ToString();
		
		result.Should().Be(
			"""
			:::note 
			
			Here is some content for the note.
			
			:::
			
			"""
			);
	}
	
	[Test]
	public void Admonition_With_Custom_Title_Returns_Valid_Markdown()
	{
		var doc = new MdDocument();
		doc.Add(new MdDocusaurusAdmonition(AdmonitionType.Note, "Here is some content for the note.", "Custom Title"));
		var result = doc.ToString();
		
		result.Should().Be(
			"""
			:::note Custom Title

			Here is some content for the note.

			:::

			"""
		);
	}
	
	[Test]
	public void Admonition_With_Nested_Markdown_Returns_Valid_Markdown()
	{
		var doc = new MdDocument();
		
		var paragraph = new MdParagraph()
			.Add(new MdBoldText("Here is some content for the note."))
			.Add(new MdLink("And here is an extra link", "https://google.co.uk"));
		
		doc.Add(new MdDocusaurusAdmonition(AdmonitionType.Note, 
			paragraph, 
			"Custom Title"));
		var result = doc.ToString();
		
		Console.WriteLine(doc);
		
		result.Should().Be(
			"""
			:::note Custom Title

			**Here is some content for the note.** [And here is an extra link](https://google.co.uk) 

			:::

			"""
		);
	}
	
	[Test]
	public void Tip_Admonition_Returns_Valid_Markdown()
	{
		var doc = new MdDocument();
		doc.Add(new MdDocusaurusAdmonition(AdmonitionType.Tip, "Here is some content for the note."));
		var result = doc.ToString();
		
		result.Should().Be(
			"""
			:::tip 

			Here is some content for the note.

			:::

			"""
		);
	}
	
	[Test]
	public void Info_Admonition_Returns_Valid_Markdown()
	{
		var doc = new MdDocument();
		doc.Add(new MdDocusaurusAdmonition(AdmonitionType.Info, "Here is some content for the note."));
		var result = doc.ToString();
		
		result.Should().Be(
			"""
			:::info 

			Here is some content for the note.

			:::

			"""
		);
	}
	
	[Test]
	public void Warning_Admonition_Returns_Valid_Markdown()
	{
		var doc = new MdDocument();
		doc.Add(new MdDocusaurusAdmonition(AdmonitionType.Warning, "Here is some content for the note."));
		var result = doc.ToString();
		
		result.Should().Be(
			"""
			:::warning 

			Here is some content for the note.

			:::

			"""
		);
	}
	
	[Test]
	public void Danger_Admonition_Returns_Valid_Markdown()
	{
		var doc = new MdDocument();
		doc.Add(new MdDocusaurusAdmonition(AdmonitionType.Danger, "Here is some content for the note."));
		var result = doc.ToString();
		
		result.Should().Be(
			"""
			:::danger 

			Here is some content for the note.

			:::

			"""
		);
	}
}