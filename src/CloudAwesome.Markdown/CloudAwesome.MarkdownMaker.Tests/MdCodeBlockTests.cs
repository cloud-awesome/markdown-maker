using System;
using CloudAwesome.MarkdownMaker.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.Tests
{
    [TestFixture]
    public class MdCodeBlockTests
    {
        private const string code = "var code = new MdCodeBlock";
        private const string language = "csharp";
        
        [Test]
        public void Code_Block_Returns_Valid_Markdown()
        {
            var sut = new MdCodeBlock(code, language);
            var actualOutput = sut.Markdown;

            actualOutput.Should().Be($"```csharp{Environment.NewLine}" +
                                     $"var code = new MdCodeBlock" +
                                     $"```{Environment.NewLine}");
        }
        
        [Test]
        public void Language_Is_Not_Mandatory_And_Returns_Valid_Markdown()
        {
            var sut = new MdCodeBlock(code);
            var actualOutput = sut.Markdown;

            actualOutput.Should().Be($"```{Environment.NewLine}" +
                                     $"var code = new MdCodeBlock" +
                                     $"```{Environment.NewLine}");
        }

        [Test]
        public void Empty_Code_Input_Throws_Validation_Error()
        {
            var codeBlock = new MdCodeBlock("");
            Func<string> function = () => codeBlock.Markdown;

            function.Should().Throw<MdInputValidationException>();
        }
        
    }
}