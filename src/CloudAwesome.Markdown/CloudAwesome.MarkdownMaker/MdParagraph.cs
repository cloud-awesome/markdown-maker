using System;
using System.Collections.Generic;
using System.Text;
using CloudAwesome.MarkdownMaker.Exceptions;
using CloudAwesome.MarkdownMaker.Validators;

namespace CloudAwesome.MarkdownMaker
{
    public class MdParagraph: IDocumentPart
    {
        internal readonly List<IDocumentPart> DocumentParts;
        
        public string Markdown
        {
            get
            {
                this.Validate();
                
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
        
        public MdParagraph Add(string inputString)
        {
            DocumentParts.Add(new MdPlainText(inputString));
            return this;
        }
        
        private void Validate()
        {
            var validator = new MdParagraphValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                throw new MdInputValidationException(result.ToString());
            }
        }
    }
}