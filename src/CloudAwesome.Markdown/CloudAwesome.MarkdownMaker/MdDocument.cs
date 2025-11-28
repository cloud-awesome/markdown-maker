using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO.Abstractions;
using System.Text;

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

        public MdDocument() : this(String.Empty, new FileSystem()) { }

        [ExcludeFromCodeCoverage(Justification = "Injects real FileSystem outisde of unit tests")]
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
            if (string.IsNullOrEmpty(FileName))
            {
                throw new InvalidOperationException("Filename cannot be null if saving document to a file");
            }
            
            _fileSystem.File.Delete(FileName);
            _fileSystem.File.AppendAllText(FileName, GenerateDocumentContents());
            return this;
        }

        public MdDocument Save(string fileName)
        {
            FileName = fileName;

            this.Save();

            return this;
        }

        public override string ToString()
        {
            return GenerateDocumentContents();
        }

        private string GenerateDocumentContents()
        {
            var stringBuilder = new StringBuilder();
            
            foreach (var documentPart in DocumentParts)
            {
                stringBuilder.Append(documentPart.Markdown + Environment.NewLine);
            }

            return stringBuilder.ToString();
        } 
    }
}