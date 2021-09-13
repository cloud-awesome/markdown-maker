using System;
using CloudAwesome.MarkdownMaker.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.Tests
{
    [TestFixture]
    public class MdImageTests
    {
        [Test]
        [TestCase("Input string", "/images/tester.png", 
            "![Input string](/images/tester.png)")]
        public void Input_Returns_Valid_Markdown(string text, string url, string expectedOutput)
        {
            var image = new MdImage(text, url);
            var markdown = image.Markdown;

            markdown.Should().Be($"{expectedOutput}{Environment.NewLine}");
        }

        [Test]
        public void Empty_Url_Throws_Validation_Error()
        {
            var sut = new MdImage("", "");
            Func<string> function = () => sut.Markdown;

            function.Should().Throw<InputValidationException>();
        }
        
    }
}