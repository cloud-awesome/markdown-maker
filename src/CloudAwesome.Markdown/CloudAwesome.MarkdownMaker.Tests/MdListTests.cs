using System;
using CloudAwesome.MarkdownMaker.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.Tests
{
    [TestFixture]
    public class MdListTests
    {
        [Test]
        [TestCase(MdListType.Ordered, "1. First Item ")]
        [TestCase(MdListType.Unordered, "- First Item ")]
        public void Valid_Input_Generates_Correct_Markdown(MdListType listType, string expectedOutput)
        {
            var list = new MdList(listType)
                .AddItem(new MdPlainText("First Item"));
            var actualResult = list.Markdown;

            actualResult.Should().Be($"{expectedOutput}{Environment.NewLine}");
        }

        [Test]
        public void Invalid_Input_Throws_Validation_Error()
        {
            var list = new MdList(MdListType.Ordered);
            Func<string> function = () => list.Markdown;

            function.Should().Throw<MdInputValidationException>();
        }
    }
}