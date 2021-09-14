using System;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.Tests
{
    [TestFixture]
    public class MdDocumentTests
    {
        [Test]
        public void Valid_Inputs_Generates_Valid_Document()
        {
            var outputFilePath = "C:\\output1.md";

            var mockFileSystem = new MockFileSystem();
            var document = new MdDocument(outputFilePath, mockFileSystem);

            document
                .Add(new MdHeader("The header", 1))
                .Save();

            var savedDocument = mockFileSystem.GetFile(outputFilePath);

            savedDocument.TextContents.Should().Be($"# The header{Environment.NewLine}{Environment.NewLine}");

        }
        
        [Test]
        //[Ignore("Just used for manual testing output")]
        public void InitialOutputTester()
        {
            // TODO - get rid of me and replace with proper tests once the immediate requirement is out of the way! ;) 
            
            var mockFileSystem = new MockFileSystem();
            
            // Mocked output
            var outputFilePath = "C:\\output.md";
            var document = new MdDocument(outputFilePath, mockFileSystem);
            
            // Real output
            //var outputFilePath = "C:\\source\\output1.md";
            //var document = new MdDocument(outputFilePath);

            var firstHeader = new MdHeader("The header", 1);
            var secondHeader = new MdHeader("Second Header", 2);

            var newFangledParagraph = new MdParagraph()
                .Add(new MdPlainText("This is a line of text."))
                .Add(new MdPlainText("And here's some more"));

            var differentText = new MdParagraph()
                .Add(new MdItalicText("This is a line of Italic text."))
                .Add(new MdBoldText("And here's bold more"));

            var paragraphWithLink = new MdParagraph()
                .Add(new MdPlainText("Look at this text."))
                .Add(new MdPlainText("Please click"))
                .Add(new MdLink("here", "https://google.com"))
                .Add(new MdPlainText("to see more"));

            var image = new MdImage("here", "https://google.com");
            
            var docfxMetadata = 
                "---" + Environment.NewLine +
                "uid: this_is_a_tester" + Environment.NewLine +
                "---";

            var docFxHeader = new MdPlainText(docfxMetadata);

            var table = new MdTable()
                .AddColumn(new MdPlainText("First Column"))
                .AddColumn(new MdPlainText("Second Column"))
                .AddColumn(new MdPlainText("Third Column"));

            table
                .AddRow(new MdTableRow()
                    .AddCell(new MdPlainText("1"))
                    .AddCell(new MdPlainText("2"))
                    .AddCell(new MdPlainText("3")))
                .AddRow(new MdTableRow()
                    .AddCell(new MdPlainText("4"))
                    .AddCell(new MdPlainText("5"))
                    .AddCell(new MdPlainText("6")));

            var code =
                "using System;" + Environment.NewLine +
                "using NUnit.Framework;" +
                "using System;" + Environment.NewLine +
                "using System.Text;" + Environment.NewLine +
                "using CloudAwesome.MarkdownMaker.Exceptions;" + Environment.NewLine +
                "using CloudAwesome.MarkdownMaker.Validators;" + Environment.NewLine;
            var codeBlock = new MdCodeBlock(code, "csharp");

            var quote = new MdQuote()
                .AddLine(new MdPlainText("All the world’s a stage, and all the men and women merely players."))
                .AddLine(new MdPlainText("They have their exits and their entrances;"))
                .AddLine(new MdPlainText("And one man in his time plays many parts."));

            var bulletList = new MdList(MdListType.Unordered)
                .AddItem(new MdPlainText("First point"))
                .AddItem(new MdPlainText("Second point"))
                .AddItem(new MdPlainText("Third point"))
                .AddItem(new MdPlainText("Fourth point"));
            
            var numberedList = new MdList(MdListType.Ordered)
                .AddItem(new MdPlainText("First point"))
                .AddItem(new MdPlainText("Second point"))
                .AddItem(new MdPlainText("Third point"))
                .AddItem(new MdPlainText("Fourth point"));

            document
                .Add(docFxHeader)
                .Add(firstHeader)
                .Add(secondHeader)
                .Add(new MdParagraph("This is a paragraph of interesting text..."))
                .Add(newFangledParagraph)
                .Add(new MdHeader("The third header", 2))
                .Add(newFangledParagraph)
                .Add(paragraphWithLink)
                .Add(new MdHorizontalLine())
                .Add(image)
                .Add(differentText)
                .Add(codeBlock)
                .Add(table)
                .Add(quote)
                .Add(bulletList)
                .Add(numberedList)
                .Save();
        }
    }
}