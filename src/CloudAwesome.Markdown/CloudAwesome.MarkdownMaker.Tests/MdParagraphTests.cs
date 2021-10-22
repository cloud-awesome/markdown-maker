using System;
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
        public void Add_Accepts_String_Input_Constructor()
        {
            var paragraph = new MdParagraph()
                .Add(InputText);
            var actualResult = paragraph.Markdown;

            actualResult.Should().Be($"{InputText} {Environment.NewLine}");
        }

        [Test]
        public void Empty_Paragraph_Returns_Validation_Error()
        {
            var paragraph = new MdParagraph();
            Func<string> sut = () => paragraph.Markdown;

            sut.Should().Throw<MdInputValidationException>();
        }

        [Test]
        public void Paragraph_Can_Accept_Text_Of_Various_Typographies()
        {
            var expectedOutput = $"This is some **important** text, which _should_ work. {Environment.NewLine}";

            var paragraph = new MdParagraph()
                .Add("This is some").Add(new MdBoldText("important"))
                .Add("text, which").Add(new MdItalicText("should")).Add("work.");

            paragraph.Markdown.Should().Be(expectedOutput);
        }
        
    }
}