using System;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace CloudAwesome.MarkdownMaker
{
    public class MdDocument
    {
        public string FilePath { get; set; }

        public List<IDocumentPart> DocumentParts { get; set; }

        private readonly IFileSystem _fileSystem;

        public MdDocument(string filePath) : this(filePath, new FileSystem()) { }

        public MdDocument(string filePath, IFileSystem fileSystem)
        {
            FilePath = filePath;
            _fileSystem = fileSystem;

            DocumentParts = new List<IDocumentPart>();
        }
        
        public MdDocument Add(IDocumentPart documentPart)
        {
            DocumentParts.Add(documentPart);
            return this;
        }
        
        public MdDocument Save()
        {
            _fileSystem.File.Delete(FilePath);
            
            foreach (var documentPart in DocumentParts)
            {
                _fileSystem.File.AppendAllText(FilePath, documentPart.Markdown + Environment.NewLine);
            }

            return this;
        }
    }
}