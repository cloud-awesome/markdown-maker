using System;
using CloudAwesome.MarkdownMaker.Exceptions;
using CloudAwesome.MarkdownMaker.Validators;
using FluentAssertions;
using NUnit;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.Tests
{
    [TestFixture]
    public class MdQuoteTests
    {
        private const string InputText = "Here is some text in a quote";
        
        [Test]
        public void Quote_Constructed_With_Content_Returns_Valid_Markdown()
        {
            var quote = new MdQuote(InputText);
            var actualResult = quote.Markdown;

            actualResult.Should().Be($"> {InputText} " +
                                     $"{Environment.NewLine}" +
                                     $"> " +
                                     $"{Environment.NewLine}" +
                                     $"{Environment.NewLine}");
        }
        
        [Test]
        public void Quote_With_Fluent_Content_Returns_Valid_Markdown()
        {
            var quote = new MdQuote()
                .AddLine(new MdPlainText(InputText));
            
            var actualResult = quote.Markdown;

            actualResult.Should().Be($"> {InputText} " +
                                     $"{Environment.NewLine}" +
                                     $"> " +
                                     $"{Environment.NewLine}" +
                                     $"{Environment.NewLine}");
        }

        [Test]
        public void Empty_Quote_Returns_Validation_Error()
        {
            var quote = new MdQuote();
            Func<string> sut = () => quote.Markdown;

            sut.Should().Throw<MdInputValidationException>();
        }
        
    }
}