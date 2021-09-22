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
        public void Empty_Row_Throws_Validation_Error()
        {
            var row = new MdTableRow();
            Func<string> sut = () => row.Markdown;

            sut.Should().Throw<MdInputValidationException>();
        }
    }
}