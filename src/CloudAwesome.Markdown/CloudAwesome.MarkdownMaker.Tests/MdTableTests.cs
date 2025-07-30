using System;
using CloudAwesome.MarkdownMaker.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.Tests
{
    [TestFixture]
    public class MdTableTests
    {
        [Test]
        public void Table_Returns_Valid_Markdown()
        {
            var expectedResult = $"| First column | Second column | {Environment.NewLine}" +
                                 $"|---|---|{Environment.NewLine}" +
                                 $"| Row 1, Column 1 | Row 1, Column 2 | {Environment.NewLine}" +
                                 $"| Row 2, Column 1 | Row 2, Column 2 | {Environment.NewLine}" +
                                 $"{Environment.NewLine}";
            
            var table = new MdTable()
                .AddColumn("First column")
                .AddColumn(new MdPlainText("Second column"))
                .AddRow(new MdTableRow()
                    .AddCell("Row 1, Column 1")
                    .AddCell("Row 1, Column 2"))
                .AddRow(new MdTableRow()
                    .AddCell("Row 2, Column 1")
                    .AddCell("Row 2, Column 2"));

            var actualResult = table.Markdown;

            actualResult.Should().Be(expectedResult);
        }

        [Test]
        public void Table_Constructed_With_Object_Returns_Valid_Markdown()
        {
            var expectedResult = $"| First column | Second column | {Environment.NewLine}" +
                                 $"|---|---|{Environment.NewLine}" +
                                 $"| Row 1, Column 1 | Row 1, Column 2 | {Environment.NewLine}" +
                                 $"| Row 2, Column 1 | Row 2, Column 2 | {Environment.NewLine}" +
                                 $"{Environment.NewLine}";

            var table = new MdTable
            {
                ColumnsHeaders =
                {
                    new MdPlainText("First column"),
                    new MdPlainText("Second column")
                },
                Rows =
                {
                    new MdTableRow
                    {
                        Cells =
                        {
                            new MdPlainText("Row 1, Column 1"),
                            new MdPlainText("Row 1, Column 2")
                        }
                    },
                    new MdTableRow
                    {
                        Cells =
                        {
                            new MdPlainText("Row 2, Column 1"),
                            new MdPlainText("Row 2, Column 2")
                        }
                    }
                }
            };
            
            var actualResult = table.Markdown;

            actualResult.Should().Be(expectedResult);
        }

        [Test]
        public void Table_Without_Headers_Should_Throw_Validation_Error()
        {
            var table = new MdTable()
                .AddColumn("First column")
                .AddColumn(new MdPlainText("Second column"));
            Func<string> sut = () => table.Markdown;

            sut.Should().Throw<MdInputValidationException>();
        }
        
        [Test]
        public void Table_No_Rows_Should_Throw_Validation_Error()
        {
            var table = new MdTable()
                .AddRow(new MdTableRow()
                    .AddCell("Row 1, Column 1")
                    .AddCell("Row 1, Column 2"));
            Func<string> sut = () => table.Markdown;

            sut.Should().Throw<MdInputValidationException>();
        }

        [Test]
        public void Column_headers_Can_Accept_Various_Typographies()
        {
            var expectedResult = 
                $"| **First header** | Second header | Third header | _Alternate header_ | {Environment.NewLine}" +
                $"|---|---|---|---|{Environment.NewLine}" +
                $"| Datum 1 | Datum 2 | Datum 3 | Datum 4 | {Environment.NewLine}" +
                $"{Environment.NewLine}";
            
            var table = new MdTable()
                .AddColumn(new MdBoldText("First header"))
                .AddColumn(new MdPlainText("Second header"))
                .AddColumn(new MdPlainText("Third header"))
                .AddColumn(new MdItalicText("Alternate header"))
                .AddRow(new MdTableRow()
                    .AddCell("Datum 1")
                    .AddCell("Datum 2")
                    .AddCell("Datum 3")
                    .AddCell("Datum 4")
                );
            
            table.Markdown.Should().Be(expectedResult);
        }

        [Test]
        public void Headers_can_be_added_in_a_single_call()
        {
            var table = new MdTable()
                .AddColumns("First column", "Second column", "Third column", "Fourth column")
                .AddRow(new MdTableRow()
                    .AddCell("Datum 1")
                    .AddCell("Datum 2")
                    .AddCell("Datum 3")
                    .AddCell("Datum 4")
                );


            table.Markdown.Should().Be($"| First column | Second column | Third column | Fourth column | {Environment.NewLine}" +
                                       $"|---|---|---|---|{Environment.NewLine}" +
                                       $"| Datum 1 | Datum 2 | Datum 3 | Datum 4 | {Environment.NewLine}" +
                                       $"{Environment.NewLine}");
        }
        
        [Test]
        public void Rows_can_be_added_in_a_single_call()
        {
            var table = new MdTable()
                .AddColumn("First column")
                .AddColumn("Second column")
                .AddColumn("Third column")
                .AddColumn("Fourth column")
                .AddRowCells("Datum 1", "Datum 2", "Datum 3", "Datum 4");
            
            table.Markdown.Should().Be($"| First column | Second column | Third column | Fourth column | {Environment.NewLine}" +
                                       $"|---|---|---|---|{Environment.NewLine}" +
                                       $"| Datum 1 | Datum 2 | Datum 3 | Datum 4 | {Environment.NewLine}" +
                                       $"{Environment.NewLine}");
        }
        
        [Test]
        public void Headers_and_rows_of_different_formats_can_be_added_in_a_single_call()
        {
            var table = new MdTable()
                .AddColumns(new MdBoldText("First column"), new MdPlainText("Second column"),
                    new MdItalicText("Third column"), new MdBoldText("Fourth column"))
                .AddRowCells(new MdBoldText("Datum 1"), new MdPlainText("Datum 2"), 
                    new MdItalicText("Datum 3"), new MdBoldText("Datum 4"));
            
            table.Markdown.Should().Be($"| **First column** | Second column | _Third column_ | **Fourth column** | {Environment.NewLine}" +
                                       $"|---|---|---|---|{Environment.NewLine}" +
                                       $"| **Datum 1** | Datum 2 | _Datum 3_ | **Datum 4** | {Environment.NewLine}" +
                                       $"{Environment.NewLine}");
        }

        
    }
}