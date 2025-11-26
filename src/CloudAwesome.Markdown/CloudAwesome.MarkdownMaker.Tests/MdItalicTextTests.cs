using CloudAwesome.MarkdownMaker.Exceptions;
using FluentAssertions;

namespace CloudAwesome.MarkdownMaker.Tests
{
    [TestFixture]
    public class MdItalicTextTests
    {
        [Test]
        public void Valid_Input_Returns_Correct_Markdown()
        {
            var text = new MdItalicText("This is slanty text");
            var actualResult = text.Markdown;

            actualResult.Should().Be("_This is slanty text_ ");
        }

        [Test]
        [TestCase("")]
        public void Empty_Input_Returns_Validation_Exception(string inputValue)
        {
            var text = new MdItalicText(inputValue);
            Func<string> function = () => text.Markdown;

            function.Should().Throw<MdInputValidationException>();
        }
    }
}