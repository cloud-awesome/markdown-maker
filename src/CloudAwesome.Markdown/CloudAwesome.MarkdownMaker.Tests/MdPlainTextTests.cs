using System;
using CloudAwesome.MarkdownMaker.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.Tests
{
    [TestFixture]
    public class MdPlainTextTests
    {
        [Test]
        [TestCase("This is bold text", "This is bold text ")]
        [TestCase(
            "Kulturalna osoba mogłaby być widziana jakościowo lepszą i całokształtną", 
            "Kulturalna osoba mogłaby być widziana jakościowo lepszą i całokształtną ")]
        public void Plain_Text_Returns_Valid_Markdown(string inputString, string expectedOutput)
        {
            var text = new MdPlainText(inputString);
            var actualResult = text.Markdown;

            actualResult.Should().Be(expectedOutput);
        }

        [Test]
        public void Empty_Text_Returns_Validation_Error()
        {
            var text = new MdPlainText("");
            Func<string> sut = () => text.Markdown;

            sut.Should().Throw<MdInputValidationException>();
        }
        
    }
}