using System;
using CloudAwesome.MarkdownMaker.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.Tests
{
    [TestFixture]
    public class MdTableRowTests
    {
        [Test]
        public void Table_Row_Returns_Valid_Markdown()
        {
            var expectedOutput = "| First cell | Second cell | ";
            
            var row = new MdTableRow()
                .AddCell(new MdPlainText("First cell"))
                .AddCell("Second cell");

            var actualOutput = row.Markdown;
            actualOutput.Should().Be(expectedOutput);
        }

        [Test]
        public void Table_Row_With_Object_Constructor_Returns_Valid_Markdown()
        {
            var expectedOutput = "| First cell | Second cell | ";

            var row = new MdTableRow
            {
                Cells =
                {
                    new MdPlainText("First cell"),
                    new MdPlainText("Second cell")
                }
            };
            
            var actualOutput = row.Markdown;
            actualOutput.Should().Be(expectedOutput);
        }

        [Test]
        public void Empty_Row_Throws_Validation_Error()
        {
            var row = new MdTableRow();
            Func<string> sut = () => row.Markdown;

            sut.Should().Throw<MdInputValidationException>();
        }

        [Test]
        public void Table_Row_Can_Accept_Multiple_Typographies()
        {
            var expectedOutput = 
                "| **First cell** | Second cell | _Third cell_ | [Click this](https://google.com) | ~~This is a mistake~~ | ";

            var row = new MdTableRow()
                .AddCell(new MdBoldText("First cell"))
                .AddCell("Second cell")
                .AddCell(new MdItalicText("Third cell"))
                .AddCell(new MdLink("Click this", "https://google.com"))
                .AddCell(new MdStrikethroughText("This is a mistake"));

            row.Markdown.Should().Be(expectedOutput);
        }
    }
}