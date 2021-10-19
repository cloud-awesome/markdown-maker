using System;
using System.IO.Abstractions.TestingHelpers;
using CloudAwesome.MarkdownMaker.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.Tests
{
    [TestFixture]
    public class MdDocumentSetTests
    {
        [Test]
        public void A_Document_In_A_Set_Cannot_Have_A_File_Path()
        {
            var mockFileSystem = new MockFileSystem();

            var document = new MdDocument("c:/contact.md", mockFileSystem)
                .Add(new MdPlainText("Just a little contact tester!!"));

            var set = 
                new MdDocumentSet("C:\\")
                    .AddDocument(document);

            Action sut = () => set.Generate();
            sut.Should().Throw<MdInputValidationException>();
        }
        
        [Test]
        public void Multiple_Documents_In_Set_Are_Generated_And_Contain_Valid_Markdown()
        {
            var mockFileSystem = new MockFileSystem();

            var document1 = new MdDocument("contact.md", mockFileSystem)
                .Add(new MdPlainText("Just a little contact tester!!"));
            
            var document2 = new MdDocument("account.md", mockFileSystem)
                .Add(new MdPlainText("Just a little account tester!!"));

            var set =
                new MdDocumentSet("C:\\")
                    .AddDocument(document1)
                    .AddDocument(document2);
            
            set.Generate();
            
            var savedDocument1 = mockFileSystem.GetFile(set.Documents[0].FileName);
            savedDocument1.TextContents.Should().Be($"Just a little contact tester!! {Environment.NewLine}");
            
            var savedDocument2 = mockFileSystem.GetFile(set.Documents[1].FileName);
            savedDocument2.TextContents.Should().Be($"Just a little account tester!! {Environment.NewLine}");
        }

        [Test]
        public void Multiple_Documents_In_Set_Are_Created_Using_Fluent_Api()
        {
            var mockFileSystem = new MockFileSystem();

            var set =
                new MdDocumentSet(@"C:\")
                    .AddDocument(
                        new MdDocument("contact.md", mockFileSystem)
                            .Add(new MdPlainText("Just a little contact tester!!"))
                    )
                    .AddDocument(
                        new MdDocument("account.md", mockFileSystem)
                            .Add(new MdPlainText("Just a little account tester!!"))
                    );
            
            set.Generate();
            
            var savedDocument1 = mockFileSystem.GetFile(set.Documents[0].FileName);
            savedDocument1.TextContents.Should().Be($"Just a little contact tester!! {Environment.NewLine}");
            
            var savedDocument2 = mockFileSystem.GetFile(set.Documents[1].FileName);
            savedDocument2.TextContents.Should().Be($"Just a little account tester!! {Environment.NewLine}");
            
        }

        [Test]
        [TestCase(@"C:/OutputFolder/DataModel/", Description = "Writes to folder, accepts a trailing slash")]
        [TestCase(@"C:/OutputFolder/DataModel", Description = "Writes to folder, doesn't need a trailing slash")]
        public void Documents_Are_Generated_In_Specified_Folder(string folder)
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.Directory.CreateDirectory(folder);

            var document = new MdDocument("Contact.md", mockFileSystem)
                .Add(new MdPlainText("Just a little contact tester!!"));

            var set =
                new MdDocumentSet(folder)
                    .AddDocument(document);
            
            set.Generate();

            mockFileSystem.FileExists($"C:/OutputFolder/DataModel/Contact.md").Should().Be(true);
        }
        
    }
}