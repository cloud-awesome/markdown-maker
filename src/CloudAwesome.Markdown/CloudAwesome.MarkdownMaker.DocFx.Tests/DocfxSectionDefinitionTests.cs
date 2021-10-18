using System;
using FluentAssertions;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.DocFx.Tests
{
    [TestFixture]
    public class DocfxSectionDefinitionTests
    {
        [Test]
        public void Input_Returns_Valid_Markdown()
        {
            var expectedOutput = 
                $"> [!div class=\"hiddenSection col-sm-3\" data-resources=\"OutlookServices.Calendar\" ]{Environment.NewLine}" +
                $"> Here is some text in a custom section {Environment.NewLine}";

            var section = 
                new DocfxSectionDefinition("Here is some text in a custom section")
                    .AddDivAttribute("class", "hiddenSection col-sm-3")
                    .AddDivAttribute("data-resources", "OutlookServices.Calendar");
            
            section.Markdown.Should().Be(expectedOutput);
        }

        [Test]
        public void Input_Can_Add_Multiple_Lines_Within_Custom_Section()
        {
            var expectedOutput = 
                $"> [!div class=\"hiddenSection col-sm-6\" data-resources=\"SomeService.MailBox\" ]{Environment.NewLine}" +
                $"> Here is some text in the custom section {Environment.NewLine}" +
                $"> And here is some more {Environment.NewLine}";

            var section =
                new DocfxSectionDefinition()
                    .AddDivAttribute("class", "hiddenSection col-sm-6")
                    .AddDivAttribute("data-resources", "SomeService.MailBox")
                    .AddItem("Here is some text in the custom section")
                    .AddItem(new MdPlainText("And here is some more"));
            section.Markdown.Should().Be(expectedOutput);
        }

        [Test]
        public void Input_Without_Custom_Attributes_Is_Valid()
        {
            var expectedOutput =
                $"> [!div ]{Environment.NewLine}" +
                $"> Here is some text in the custom section {Environment.NewLine}";

            var sectionText = new MdPlainText("Here is some text in the custom section");
            var section = new DocfxSectionDefinition(sectionText);
            section.Markdown.Should().Be(expectedOutput);
        }
        
    }
}