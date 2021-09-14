using System;
using CloudAwesome.MarkdownMaker.Exceptions;
using CloudAwesome.MarkdownMaker.Validators;

namespace CloudAwesome.MarkdownMaker
{
    public class MdCodeBlock: IDocumentPart
    {
        public string Text { get; set; }
        
        public string Language { get; set; }

        public string Markdown
        {
            get
            {
                this.Validate();
                
                return
                    $"```{Language}{Environment.NewLine}" +
                    $"{Text}" +
                    $"```{Environment.NewLine}";
            }
        }

        public MdCodeBlock(string text, string language = "")
        {
            Text = text;
            Language = language;
        }
        
        private void Validate()
        {
            var validator = new MdCodeBlockValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                throw new MdInputValidationException(result.ToString());
            }
        }
    }
}