using System;
using CloudAwesome.MarkdownMaker.DocFx.Tests.YamlHeaderModels;
using FluentAssertions;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.DocFx.Tests
{
    [TestFixture]
    public class DocfxYamlHeaderTests
    {
        [Test]
        public void Input_Class_Header_Generates_Valid_Yaml_Markdown_Header()
        {
            var expectedOutput =
                $"---{Environment.NewLine}" +
                $"uid: this_is_a_page_reference{Environment.NewLine}" +
                $"description: This is the description of the conceptual page{Environment.NewLine}" +
                $"references:{Environment.NewLine}" +
                $"- name: Reference page 1{Environment.NewLine}" +
                $"  uid: see_also_page_1{Environment.NewLine}" +
                $"- name: Reference page 2{Environment.NewLine}" +
                $"  uid: see_also_page_2{Environment.NewLine}" +
                $"---{Environment.NewLine}";
            
            var yamlHeaderData = new StandardPageData
            {
                Uid = "this_is_a_page_reference",
                Description = "This is the description of the conceptual page",
                References = new []
                {
                    new SeeAlsoReference
                    {
                        Uid = "see_also_page_1",
                        Name = "Reference page 1"
                    },
                    new SeeAlsoReference
                    {
                        Uid = "see_also_page_2",
                        Name = "Reference page 2"
                    }
                }
            };

            var yamlHeader = new DocfxYamlHeader(yamlHeaderData);

            yamlHeader.Markdown.Should().Be(expectedOutput);
        }
        
    }
}