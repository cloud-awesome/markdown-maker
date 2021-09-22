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
        [TestCase(MdListType.Todo, "- [ ] First Item ")]
        public void Valid_Input_Generates_Correct_Markdown(MdListType listType, string expectedOutput)
        {
            var list = new MdList(listType)
                .AddItem(new MdPlainText("First Item"));
            var actualResult = list.Markdown;

            actualResult.Should().Be($"{expectedOutput}{Environment.NewLine}");
        }

        [Test]
        [TestCase(MdListType.Ordered, "1. First Item ")]
        [TestCase(MdListType.Unordered, "- First Item ")]
        public void Add_Item_Accepts_Plain_String_Input(MdListType listType, string expectedOutput)
        {
            var list = new MdList(listType)
                .AddItem("First Item");
            var actualResult = list.Markdown;

            actualResult.Should().Be($"{expectedOutput}{Environment.NewLine}");
        }

        [Test]
        [TestCase(MdListType.Ordered, "1. First Item ")]
        [TestCase(MdListType.Unordered, "- First Item ")]
        public void List_Constructed_With_List_Returns_Valid_Markdown(MdListType listType, string expectedOutput)
        {
            var list = new MdList
            {
                ListType = listType,
                Items =
                {
                    new MdPlainText("First Item")
                }
            };

            var actualResult = list.Markdown;
            
            actualResult.Should().Be($"{expectedOutput}{Environment.NewLine}");
        }

        [Test]
        public void List_With_Any_Items_Throws_Validation_Error()
        {
            var list = new MdList(MdListType.Ordered);
            Func<string> function = () => list.Markdown;

            function.Should().Throw<MdInputValidationException>();
        }

        [Test]
        public void List_Without_List_Type_Throws_Validation_Error()
        {
            var list = new MdList
            {
                Items =
                {
                    new MdPlainText("Test items")
                }
            };
            
            Func<string> function = () => list.Markdown;

            function.Should().Throw<MdInputValidationException>();
        }
    }
}