using System;
using System.Collections.Generic;
using System.IO;

namespace CloudAwesome.MarkdownMaker
{
    public class MdDocument
    {
        public string FilePath { get; set; }

        public List<IDocumentPart> DocumentParts { get; set; }
        
        public MdDocument(string filePath)
        {
            FilePath = filePath;

            DocumentParts = new List<IDocumentPart>();
        }
        
        public MdDocument Save()
        {
            File.Delete(FilePath);
            
            foreach (var documentPart in DocumentParts)
            {
                File.AppendAllText(FilePath, documentPart.Markdown + Environment.NewLine);
            }

            return this;
        }

        public MdDocument Add(IDocumentPart documentPart)
        {
            DocumentParts.Add(documentPart);
            return this;
        }
    }
}