using System;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace CloudAwesome.MarkdownMaker
{
    public class MdDocument
    {
        /// <summary>
        /// File name or path for the output document.
        /// If this is a single MdDocument then it can be a fully qualified path.
        /// If this MdDocument is part of a MdDocumentSet then it should just be the filename 
        /// </summary>
        public string FileName { get; set; }

        public List<IDocumentPart> DocumentParts { get; }

        private readonly IFileSystem _fileSystem;

        public MdDocument(string fileName) : this(fileName, new FileSystem()) { }

        public MdDocument(string fileName, IFileSystem fileSystem)
        {
            FileName = fileName;
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
            _fileSystem.File.Delete(FileName);
            
            foreach (var documentPart in DocumentParts)
            {
                _fileSystem.File.AppendAllText(FileName, documentPart.Markdown + Environment.NewLine);
            }

            return this;
        }
    }
}