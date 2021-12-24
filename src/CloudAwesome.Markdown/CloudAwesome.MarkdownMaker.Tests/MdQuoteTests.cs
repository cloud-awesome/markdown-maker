using System;
using CloudAwesome.MarkdownMaker.Exceptions;
using FluentAssertions;
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
        public void Add_Line_Accepts_String_Constructor()
        {
            var quote = new MdQuote()
                .AddLine(InputText);
            
            var actualResult = quote.Markdown;

            actualResult.Should().Be($"> {InputText} " +
                                     $"{Environment.NewLine}" +
                                     $"> " +
                                     $"{Environment.NewLine}" +
                                     $"{Environment.NewLine}");
        }

        // [Test]
        // public void Quote_Constructed_With_Lines_List_Returns_Valid_Markdown()
        // {
        //     var quote = new MdQuote()
        //     {
        //         DocumentParts =
        //         {
        //             new MdPlainText(InputText),
        //             new MdPlainText(InputText)
        //         }
        //     };
        //     
        //     var actualResult = quote.Markdown;
        //
        //     actualResult.Should().Be($"> {InputText} " +
        //                              $"{Environment.NewLine}" +
        //                              $"> " +
        //                              $"{Environment.NewLine}" +
        //                              $"> {InputText} " +
        //                              $"{Environment.NewLine}" +
        //                              $"> " +
        //                              $"{Environment.NewLine}" +
        //                              $"{Environment.NewLine}");
        // }

        // [Test]
        // public void Quote_Constructed_With_Lines_List_Can_Add_More_Lines()
        // {
        //     var quote = new MdQuote()
        //         {
        //             DocumentParts =
        //             {
        //                 new MdPlainText(InputText),
        //                 new MdPlainText(InputText)
        //             }
        //         }
        //         .AddLine(new MdPlainText(InputText));
        //
        //     var actualResult = quote.Markdown;
        //
        //     actualResult.Should().Be($"> {InputText} " +
        //                              $"{Environment.NewLine}" +
        //                              $"> " +
        //                              $"{Environment.NewLine}" +
        //                              $"> {InputText} " +
        //                              $"{Environment.NewLine}" +
        //                              $"> " +
        //                              $"{Environment.NewLine}" +
        //                              $"> {InputText} " +
        //                              $"{Environment.NewLine}" +
        //                              $"> " +
        //                              $"{Environment.NewLine}" +
        //                              $"{Environment.NewLine}");
        // }

        [Test]
        public void Empty_Quote_Returns_Validation_Error()
        {
            var quote = new MdQuote();
            Func<string> sut = () => quote.Markdown;

            sut.Should().Throw<MdInputValidationException>();
        }

        // [Test]
        // [Ignore("TODO")]
        // public void Quote_Can_Accept_Various_Typographies()
        // {
        //
        // }
        
    }
}