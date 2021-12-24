using System;
using FluentAssertions;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.DocFx.Tests
{
    [TestFixture]
    public class DocfxTabbedContentTests
    {
        [Test]
        public void Input_Creates_Valid_Markdown()
        {
            var expectedOutput =
                $"# [Tab Text A](#tab/tabid-a){Environment.NewLine}" +
                $"Tab content-a-1. {Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"# [Tab Text B](#tab/tabid-b){Environment.NewLine}" +
                $"Tab content-b-1. {Environment.NewLine}" +
                $"{Environment.NewLine}" +
                $"***{Environment.NewLine}";

            var result = new DocfxTabbedContent()
                .AddTab(
                    new DocfxTab("Tab Text A", "tabid-a",
                        new MdParagraph("Tab content-a-1.")))
                .AddTab(
                    new DocfxTab("Tab Text B", "tabid-b",
                        new MdParagraph("Tab content-b-1.")));

            result.Markdown.Should().Be(expectedOutput);

        }

        [Test]
        [Ignore("TODO")]
        public void Tab_Content_Can_Accept_Various_Md_Types()
        {
            
        }
        
        [Test]
        [Ignore("TODO")]
        public void Tab_Content_Can_Accept_Prepared_Hierarchical_Paragraph()
        {
            
        }

        [Test]
        [Ignore("TODO")]
        public void Empty_Tabbed_Content_Fails_Validation()
        {
            
        }
    }
}