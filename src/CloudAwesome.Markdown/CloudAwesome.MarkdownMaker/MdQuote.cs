using System;
using System.Collections.Generic;
using System.Text;

namespace CloudAwesome.MarkdownMaker
{
    public class MdQuote: IDocumentPart
    {
        public List<MdPlainText> DocumentParts;

        public string Markdown
        {
            get
            {
                var stringBuilder = new StringBuilder();

                foreach (var documentPart in DocumentParts)
                {
                    stringBuilder.Append($"> {documentPart.Markdown}");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("> ");
                    stringBuilder.Append(Environment.NewLine);
                }
                stringBuilder.Append(Environment.NewLine);

                return stringBuilder.ToString();
            }
        }

        public MdQuote()
        {
            DocumentParts = new List<MdPlainText>();
        }

        public MdQuote AddLine(MdPlainText line)
        {
            DocumentParts.Add(line);

            return this;
        }
    }
}