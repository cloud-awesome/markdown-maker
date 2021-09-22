using System;
using System.Xml;
using CloudAwesome.MarkdownMaker.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.Tests
{
    [TestFixture]
    public class MdParagraphTests
    {
        private const string InputText = "Here is some text in a paragraph";
        
        [Test]
        public void Paragraph_Constructed_With_Content_Returns_Valid_Markdown()
        {
            var paragraph = new MdParagraph(InputText);
            var actualResult = paragraph.Markdown;

            actualResult.Should().Be($"{InputText} {Environment.NewLine}");
        }
        
        [Test]
        public void Paragraph_With_Fluent_Content_Returns_Valid_Markdown()
        {
            var paragraph = new MdParagraph()
                .Add(new MdPlainText(InputText));
            var actualResult = paragraph.Markdown;

            actualResult.Should().Be($"{InputText} {Environment.NewLine}");
        }

        [Test]
        public void Paragraph_Constructed_With_Parts_Returns_Valid_Markdown()
        {
            var paragraph = new MdParagraph
            {
                DocumentParts =
                {
                    new MdPlainText("Line 1"),
                    new MdPlainText("Line 2")
                }
            };

            var actualResult = paragraph.Markdown;

            actualResult.Should().Be($"Line 1 " +
                                     $"Line 2 " +
                                     $"{Environment.NewLine}");
        }

        [Test]
        public void Empty_Paragraph_Returns_Validation_Error()
        {
            var paragraph = new MdParagraph();
            Func<string> sut = () => paragraph.Markdown;

            sut.Should().Throw<MdInputValidationException>();
        }
        
    }
}