using System;
using CloudAwesome.MarkdownMaker.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.DocFx.Tests
{
    [TestFixture]
    public class DocfxNoteTests
    {
        [Test]
        [TestCase(DocfxNoteType.Caution, "Caution")]
        [TestCase(DocfxNoteType.Important, "Important")]
        [TestCase(DocfxNoteType.Note, "Note")]
        [TestCase(DocfxNoteType.Tip, "Tip")]
        [TestCase(DocfxNoteType.Warning, "Warning")]
        public void Note_Creates_Valid_Markdown(
            DocfxNoteType noteType, string expectedTypeString)
        {
            var expectedOutput = 
                $"> [!{expectedTypeString}] {Environment.NewLine}" + 
                $"> This is a new note {Environment.NewLine}" + 
                $"{Environment.NewLine}";
            
            var note = new DocfxNote(noteType,"This is a new note");
            var result = note.Markdown;

            result.Should().Be(expectedOutput);
        }

        [Test]
        public void Note_With_No_Items_Can_Be_Added_After_Construction()
        {
            var expectedOutput = 
                $"> [!Warning] {Environment.NewLine}" +
                $"> This is a new note {Environment.NewLine}" + 
                $"> This is more text {Environment.NewLine}" + 
                $"{Environment.NewLine}";

            var note = new DocfxNote(DocfxNoteType.Warning);
            note.AddItem("This is a new note");
            note.AddItem(new MdPlainText("This is more text"));

            note.Markdown.Should().Be(expectedOutput);
        }

        [Test]
        public void Invalid_Note_Throws_Exception()
        {
            var note = new DocfxNote();
            Func<string> sut = () => note.Markdown;

            sut.Should().Throw<MdInputValidationException>();
        }
    }
}