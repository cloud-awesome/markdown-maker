using System;
using CloudAwesome.MarkdownMaker.Exceptions;
using CloudAwesome.MarkdownMaker.Validators;

namespace CloudAwesome.MarkdownMaker
{
    public class MdImage: IDocumentPart
    {
        public string Text { get; set; }
        
        public string Url { get; set; }

        public string Markdown
        {
            get
            {
                this.Validate();
                
                return $"![{Text}]({Url}){Environment.NewLine}";
            }
        }

        public MdImage(string text, string url)
        {
            Text = text;
            Url = url;
        }
        
        private void Validate()
        {
            var validator = new MdImageValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                throw new InputValidationException(result.ToString());
            }
        }
    }
}