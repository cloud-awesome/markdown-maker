using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudAwesome.MarkdownMaker
{
    public class MdParagraph: IDocumentPart
    {
        public List<IDocumentPart> DocumentParts;
        
        public string Markdown
        {
            get
            {
                var stringBuilder = new StringBuilder();

                foreach (var documentPart in DocumentParts)
                {
                    stringBuilder.Append(documentPart.Markdown);
                }
                stringBuilder.Append(Environment.NewLine);

                return stringBuilder.ToString();
            }
        }

        public MdParagraph()
        {
            DocumentParts = new List<IDocumentPart>();
        }

        public MdParagraph(string text)
        {
            DocumentParts = new List<IDocumentPart>
            {
                new MdPlainText(text)
            };
        }

        public MdParagraph Add(IDocumentPart documentPart)
        {
            DocumentParts.Add(documentPart);

            return this;
        }
    }
}