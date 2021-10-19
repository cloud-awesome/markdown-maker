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
        [TestCase(MdListType.Todo, "- [ ] First Item ")]
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
        [TestCase(MdListType.Todo, "- [ ] First Item ")]
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

        [Test]
        public void List_Can_Contain_A_Child_List()
        {
            var expectedOutput =
                $"1. Item 1 {Environment.NewLine}" +
                $"1. Item 2 {Environment.NewLine}" +
                $"    1. Sub-Item 1 {Environment.NewLine}" +
                $"    1. Sub-Item 2 {Environment.NewLine}" +
                $"1. Item 3 {Environment.NewLine}";

            var list = new MdList(MdListType.Ordered)
                .AddItem("Item 1")
                .AddItem("Item 2")
                .AddChildList(
                    new MdList(MdListType.Ordered)
                        .AddItem("Sub-Item 1")
                        .AddItem("Sub-Item 2"))
                .AddItem("Item 3");
            
            list.Markdown.Should().Be(expectedOutput);
        }

        /// <summary>
        /// Given an MdList contains inline child and grandchild lists
        /// Then the child and grandchild lists return valid markdown
        /// And respect the hierarchical indentation 
        /// </summary>
        [Test]
        public void List_Can_Contain_Multiple_Child_And_Grandchild_Lists()
        {
            var expectedOutput =
                $"1. Item 1 {Environment.NewLine}" +
                $"1. Item 2 {Environment.NewLine}" +
                $"    - Child 1, Item 1 {Environment.NewLine}" +
                $"    - Child 1, Item 2 {Environment.NewLine}" +
                $"1. Item 3 {Environment.NewLine}" +
                $"    - Child 2, Item 1 {Environment.NewLine}" +
                $"        1. Grandchild 1, Item 1 {Environment.NewLine}" +
                $"        1. Grandchild 1, Item 2 {Environment.NewLine}" +
                $"    - Child 2, Item 2 {Environment.NewLine}" +
                $"        - [ ] Grandchild 2, Item 1 {Environment.NewLine}" +
                $"        - [ ] Grandchild 2, Item 2 {Environment.NewLine}" +
                $"1. Item 4 {Environment.NewLine}";

            var list = new MdList(MdListType.Ordered)
                .AddItem("Item 1")
                .AddItem("Item 2")
                .AddChildList(
                    new MdList(MdListType.Unordered)
                        .AddItem("Child 1, Item 1")
                        .AddItem("Child 1, Item 2"))
                .AddItem("Item 3")
                .AddChildList(
                    new MdList(MdListType.Unordered)
                        .AddItem("Child 2, Item 1")
                        .AddChildList(
                            new MdList(MdListType.Ordered)
                                .AddItem("Grandchild 1, Item 1")
                                .AddItem("Grandchild 1, Item 2")
                        )
                        .AddItem("Child 2, Item 2")
                        .AddChildList(
                            new MdList(MdListType.Todo)
                                .AddItem("Grandchild 2, Item 1")
                                .AddItem("Grandchild 2, Item 2")
                        )
                )
                .AddItem("Item 4");
            
            list.Markdown.Should().Be(expectedOutput);
        }

        /// <summary>
        /// Given an MdList contains child and grandchild lists
        /// Then the child and grandchild lists return valid markdown
        /// And respect the hierarchical indentation 
        /// </summary>
        [Test]
        public void Child_Lists_Return_Valid_Markdown_When_Created_Separately()
        {
            var expectedOutput =
                $"1. Item 1 {Environment.NewLine}" +
                $"1. Item 2 {Environment.NewLine}" +
                $"    - Child 1, Item 1 {Environment.NewLine}" +
                $"    - Child 1, Item 2 {Environment.NewLine}" +
                $"1. Item 3 {Environment.NewLine}" +
                $"    - Child 2, Item 1 {Environment.NewLine}" +
                $"        1. Grandchild 1, Item 1 {Environment.NewLine}" +
                $"        1. Grandchild 1, Item 2 {Environment.NewLine}" +
                $"    - Child 2, Item 2 {Environment.NewLine}" +
                $"        - [ ] Grandchild 2, Item 1 {Environment.NewLine}" +
                $"        - [ ] Grandchild 2, Item 2 {Environment.NewLine}" +
                $"1. Item 4 {Environment.NewLine}";

            var grandchildList1 = 
                new MdList(MdListType.Ordered)
                    .AddItem("Grandchild 1, Item 1")
                    .AddItem("Grandchild 1, Item 2");

            var grandchildList2 =
                new MdList(MdListType.Todo)
                    .AddItem("Grandchild 2, Item 1")
                    .AddItem("Grandchild 2, Item 2");
            
            var childList1 =
                new MdList(MdListType.Unordered)
                    .AddItem("Child 1, Item 1")
                    .AddItem("Child 1, Item 2");

            var childList2 =
                new MdList(MdListType.Unordered)
                    .AddItem("Child 2, Item 1")
                    .AddChildList(grandchildList1)
                    .AddItem("Child 2, Item 2")
                    .AddChildList(
                        grandchildList2
                    );
            
            var list = new MdList(MdListType.Ordered)
                .AddItem("Item 1")
                .AddItem("Item 2")
                .AddChildList(childList1)
                .AddItem("Item 3")
                .AddChildList(childList2)
                .AddItem("Item 4");
            
            list.Markdown.Should().Be(expectedOutput);
        }
    }
}