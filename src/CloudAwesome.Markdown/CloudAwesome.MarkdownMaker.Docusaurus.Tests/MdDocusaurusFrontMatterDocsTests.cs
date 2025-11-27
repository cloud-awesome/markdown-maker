using FluentAssertions;

namespace CloudAwesome.MarkdownMaker.Docusaurus.Tests;

public class MdDocusaurusFrontMatterDocsTests
{
	[Test]
	public void Docusaurus_Front_Matter_Returns_Valid_Markdown()
	{
		var doc = new MdDocument();
		doc.Add(new MdFrontMatter(new MdDocusaurusFrontMatterDocs
		{
			title = "This is a doc title",
			hide_title = true,
			toc_min_heading_level = 4
		}));
		doc.Add(new MdHeader("This is a header", HeaderLevel.H1));
		var result = doc.ToString();

		result.Should().Be(
			$"""
			---
			title: This is a doc title
			hide_title: true
			toc_min_heading_level: 4
			---

			# This is a header
			
			
			""");
	}
	
	[Test]
	public void Docusaurus_Front_Matter_With_Nested_Object_Returns_Valid_Markdown()
	{
		var doc = new MdDocument();
		doc.Add(new MdFrontMatter(new MdDocusaurusFrontMatterDocs
		{
			title = "This is a doc title",
			last_update = new FrontMatterLastUpdate
			{
				date = "2021-01-01",
				author = "arthur"
			}
		}));
		doc.Add(new MdHeader("This is a header", HeaderLevel.H1));
		var result = doc.ToString();

		result.Should().Be(
			$"""
			 ---
			 title: This is a doc title
			 last_update:
			   date: 2021-01-01
			   author: arthur
			 ---

			 # This is a header


			 """);
	}
}