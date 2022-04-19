using CloudAwesome.MarkdownMaker.Tests.TestDataModels;
using NUnit.Framework;

namespace CloudAwesome.MarkdownMaker.Tests
{
    public class ConvertPocoToMarkdownTests
    {
        [Test]
        public void FirstTest()
        {
            var persona = new Persona
            {
                FirstName = "Pedro",
                LastName = "Gumula",
                Description = "This person is a sample person, which is very interesting"
            };
            
            
        }
    }
}