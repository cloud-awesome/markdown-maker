using CloudAwesome.MarkdownMaker.Exceptions;
using CloudAwesome.MarkdownMaker.Validators;

namespace CloudAwesome.MarkdownMaker
{
    public class MdPlainText: IDocumentPart, IListPart
    {
        public string Text { get; set; }

        public string Markdown
        {
            get
            { 
                this.Validate();
                
                return $"{Text} ";
            }
        }

        public MdPlainText(string text)
        {
            Text = text;
        }
        
        private void Validate()
        {
            var validator = new MdPlainTextValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                throw new MdInputValidationException(result.ToString());
            }
        }
    }
}