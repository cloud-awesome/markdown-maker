using FluentAssertions;

namespace CloudAwesome.MarkdownMaker.Tests
{
    [TestFixture]
    public class MdHorizontalLineTests
    {
        [Test]
        public void Horizontal_Line_Returns_Valid_Markdown()
        {
            var hr = new MdHorizontalLine();
            hr.Markdown.Should().Be($"{Environment.NewLine}" +
                                    $"---" +
                                    $"{Environment.NewLine}" +
                                    $"{Environment.NewLine}");
        }
        
    }
}