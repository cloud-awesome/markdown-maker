using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text;
using CloudAwesome.MarkdownMaker.Converters;

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

        public MdDocument(string fileName) 
            : this(fileName, new FileSystem()) { }
        
        public MdDocument(string fileName, IDocumentMe pocoClass, IFileSystem fileSystem = null)
            : this(fileName, new List<IDocumentMe>() { pocoClass }, fileSystem) { }

        public MdDocument(string fileName, IEnumerable<IDocumentMe> pocoClasses, 
            IFileSystem fileSystem = null)
        {
            FileName = fileName;
            _fileSystem = fileSystem ?? new FileSystem();

            DocumentParts = pocoClasses.Convert();
        }

        public MdDocument(string fileName, IFileSystem fileSystem = null)
        {
            FileName = fileName;
            _fileSystem = fileSystem ?? new FileSystem();

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
        
        public override string ToString()
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