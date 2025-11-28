using System;
using System.Collections.Generic;
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
        public void Throws_Exception_If_No_Output_Folder_Provided()
        {
            var mockFileSystem = new MockFileSystem();

            var document1 = new MdDocument("contact.md", mockFileSystem)
                .Add(new MdPlainText("Just a little contact tester!!"));
            
            var document2 = new MdDocument("account.md", mockFileSystem)
                .Add(new MdPlainText("Just a little account tester!!"));

            var set =
                new MdDocumentSet()
                    .AddDocument(document1)
                    .AddDocument(document2);
            
            var sut = () => set.Generate();
            
            sut.Should().Throw<MdInputValidationException>().WithMessage("FolderPath cannot be null");
        }
        
        [Test]
        public void Folder_Can_Provided_In_Generate_Method()
        {
            var mockFileSystem = new MockFileSystem();

            var document1 = new MdDocument("contact.md", mockFileSystem)
                .Add(new MdPlainText("Just a little contact tester!!"));
            
            var document2 = new MdDocument("account.md", mockFileSystem)
                .Add(new MdPlainText("Just a little account tester!!"));

            var set =
                new MdDocumentSet()
                    .AddDocument(document1)
                    .AddDocument(document2);
            
            set.Generate("C:\\");

            mockFileSystem.AllFiles.Count().Should().Be(2);
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

        [Test]
        public void Example_Generate_Set_Through_A_Loop()
        {
            var folderPath = "C:/OutputFolder/DataModel/";
            
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.Directory.CreateDirectory(folderPath);
            
            var sampleDataList = _sampleData;

            var documentSet = new MdDocumentSet(folderPath);
            
            foreach (var sampleData in sampleDataList)
            {
                documentSet.AddDocument(
                    new MdDocument($"{sampleData.Name}.md", mockFileSystem)
                        .Add(new MdHeader(sampleData.Name, 1))
                        .Add(new MdParagraph(sampleData.Description))
                    );
            }
            
            documentSet.Generate();

            mockFileSystem.FileExists($"{folderPath}Contact.md").Should().Be(true);
        }

        private readonly List<MdSampleDataModel> _sampleData =
        [
            new MdSampleDataModel
            {
                Name = "Contact",
                Description = "This is some sample data about contacts"
            },

            new MdSampleDataModel
            {
                Name = "Account",
                Description = "This is some sample data about accounts"
            },

            new MdSampleDataModel
            {
                Name = "Opportunity",
                Description = "This is some sample data about opportunities"
            }
        ];
    }

    class MdSampleDataModel()
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}