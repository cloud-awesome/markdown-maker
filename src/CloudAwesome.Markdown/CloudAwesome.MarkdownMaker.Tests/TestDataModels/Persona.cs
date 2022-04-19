using System;

namespace CloudAwesome.MarkdownMaker.Tests.TestDataModels
{
    public class Persona: IDocumentMe
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public DateTime DateOfBirth { get; set; }
        
    }
}