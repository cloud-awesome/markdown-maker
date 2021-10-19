using System;
using System.Collections.Generic;
using System.Text;
using CloudAwesome.MarkdownMaker.Exceptions;
using CloudAwesome.MarkdownMaker.Validators;

namespace CloudAwesome.MarkdownMaker
{
    public class MdQuote: IDocumentPart
    {
        public readonly List<MdPlainText> DocumentParts;

        public string Markdown
        {
            get
            {
                this.Validate();
                
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

        public MdQuote(string inputText)
        {
            DocumentParts = new List<MdPlainText>();
            this.AddLine(new MdPlainText(inputText));
        }

        public MdQuote AddLine(MdPlainText line)
        {
            DocumentParts.Add(line);
            return this;
        }
        
        public MdQuote AddLine(string line)
        {
            DocumentParts.Add(new MdPlainText(line));
            return this;
        }
        
        private void Validate()
        {
            var validator = new MdQuoteValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                throw new MdInputValidationException(result.ToString());
            }
        }
    }
}