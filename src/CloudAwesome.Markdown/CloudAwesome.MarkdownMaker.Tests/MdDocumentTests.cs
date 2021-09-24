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

            var firstHeader = new MdHeader("The h1 header", 1);
            var secondHeader = new MdHeader("The h2 Header", 2);

            // Include different types of text in your paragraph
            var paragraph = new MdParagraph()
                    .Add("This is a line of text.")
                    .Add(new MdPlainText("And here's some more"))
                    .Add(new MdItalicText("This is a line of Italic text."))
                    .Add(new MdBoldText("And here's more in bold!"));

            var paragraphWithLink = new MdParagraph()
                .Add("Look at this text.")
                .Add("Please click")
                .Add(new MdLink("here", "https://google.com"))
                .Add("to see more");

            var image = new MdImage("here", "https://google.com");
            
            var docfxMetadata = 
                "---" + Environment.NewLine +
                "uid: this_is_a_tester" + Environment.NewLine +
                "---";
            var docFxHeader = new MdPlainText(docfxMetadata);

            var table = new MdTable()
                .AddColumn("First Column")
                .AddColumn("Second Column")
                .AddColumn("Third Column");

            table
                .AddRow(new MdTableRow()
                    // Use AddCell ...
                    .AddCell("1")
                    .AddCell("2")
                    .AddCell("3"))
                .AddRow(new MdTableRow
                {
                    // ... or use a Cell object
                    Cells =
                    {
                        new MdPlainText("4"),
                        new MdPlainText("5"),
                        new MdPlainText("6")
                    }
                });

            var code =
                "using System;" + Environment.NewLine +
                "using NUnit.Framework;" +
                "using System;" + Environment.NewLine +
                "using System.Text;" + Environment.NewLine +
                "using CloudAwesome.MarkdownMaker.Exceptions;" + Environment.NewLine +
                "using CloudAwesome.MarkdownMaker.Validators;" + Environment.NewLine;
            var codeBlock = new MdCodeBlock(code, "csharp");

            var quote = new MdQuote()
                .AddLine("All the world’s a stage, and all the men and women merely players.")
                .AddLine("They have their exits and their entrances;")
                .AddLine("And one man in his time plays many parts.");

            var bulletList = new MdList(MdListType.Unordered)
                .AddItem("First point")
                .AddItem("Second point")
                .AddItem("Third point")
                .AddItem("Fourth point");
            
            var numberedList = new MdList(MdListType.Ordered)
                .AddItem("First point")
                .AddItem("Second point")
                .AddItem("Third point")
                .AddItem("Fourth point");

            var todoList = new MdList(MdListType.Todo)
                .AddItem("Build something")
                .AddItem("Test it")
                .AddItem("Push it");
            
            document
                .Add(docFxHeader)
                .Add(firstHeader)
                .Add(secondHeader)
                .Add(new MdParagraph("This is a paragraph of interesting text..."))
                .Add(new MdHeader("The third header", 2))
                .Add(paragraphWithLink)
                .Add(new MdHorizontalLine())
                .Add(image)
                .Add(paragraph)
                .Add(codeBlock)
                .Add(table)
                .Add(quote)
                .Add(bulletList)
                .Add(numberedList)
                .Add(todoList)
                .Save();
            
            var savedDocument = mockFileSystem.GetFile(outputFilePath);
            savedDocument.Should().NotBeNull();
        }
    }
}