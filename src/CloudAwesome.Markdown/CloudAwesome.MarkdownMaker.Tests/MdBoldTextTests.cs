using System;
using CloudAwesome.MarkdownMaker.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.Tests
{
    [TestFixture]
    public class  MdBoldTextTests
    {
        [Test]
        [TestCase("This is bold text", "**This is bold text** ")]
        [TestCase(
            "Kulturalna osoba mogłaby być widziana jakościowo lepszą i całokształtną", 
            "**Kulturalna osoba mogłaby być widziana jakościowo lepszą i całokształtną** ")]
        public void Bold_Test_Returns_Valid_Markdown(string inputText, string expectedOutput)
        {
            var text = new MdBoldText(inputText);
            var actualResult = text.Markdown;

            actualResult.Should().Be(expectedOutput);
        }

        [Test]
        public void Empty_String_Throws_Validation_Error()
        {
            var text = new MdBoldText("");
            Func<string> function = () => text.Markdown;

            function.Should().Throw<MdInputValidationException>();
        }

        [Test]
        public void Input_String_Can_Be_Changed_After_Initiation()
        {
            var text = new MdBoldText("This is the original text");
            text.Text = "This is updated text";

            var actualResult = text.Markdown;

            actualResult.Should().Be("**This is updated text** ");

        }
    }
}