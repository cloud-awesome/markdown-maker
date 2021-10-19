using System;
using System.Collections.Generic;
using System.Text;

namespace CloudAwesome.MarkdownMaker
{
    public class MdComment: IDocumentPart
    {
        public List<MdPlainText> Lines { get; }

        public string Markdown
        {
            get
            {
                var conditionalNewLine = Lines.Count < 2 ? string.Empty : Environment.NewLine;
                
                var stringBuilder = new StringBuilder();

                stringBuilder.Append($"<!-- {conditionalNewLine}");

                foreach (var line in Lines)
                {
                    stringBuilder.Append($"{line.Markdown}{conditionalNewLine}");
                }
                
                stringBuilder.Append("-->");
                
                return stringBuilder.ToString();
            }
        }

        public MdComment(string text)
        {
            Lines = new List<MdPlainText>
            {
                new MdPlainText(text)
            };
        }
        
        public MdComment(MdPlainText text)
        {
            Lines = new List<MdPlainText> { text };
        }

        public MdComment()
        {
            Lines = new List<MdPlainText>();
        }

        public MdComment AddLine(string text)
        {
            Lines.Add(new MdPlainText(text));
            return this;
        }
        
        public MdComment AddLine(MdPlainText text)
        {
            Lines.Add(text);
            return this;
        }
    }
}