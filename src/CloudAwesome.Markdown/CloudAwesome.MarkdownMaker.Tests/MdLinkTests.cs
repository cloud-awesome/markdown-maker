using System;
using CloudAwesome.MarkdownMaker.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.Tests
{
    [TestFixture]
    public class MdLinkTests
    {
        [Test]
        [TestCase("Input string", "https://google.co.uk", 
            "[Input string](https://google.co.uk) ")]
        public void Input_Returns_Valid_Markdown(string text, string url, string expectedOutput)
        {
            var image = new MdLink(text, url);
            var markdown = image.Markdown;

            markdown.Should().Be($"{expectedOutput}");
        }

        [Test]
        [TestCase("This link goes nowhere", "")]
        [TestCase("", "link-with-no-text.com")]
        public void Invalid_Input_Throws_Validation_Error(string text, string url)
        {
            var sut = new MdLink(text, url);
            Func<string> function = () => sut.Markdown;

            function.Should().Throw<MdInputValidationException>();
        }
        
    }
}