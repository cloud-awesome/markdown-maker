using CloudAwesome.MarkdownMaker.Exceptions;
using CloudAwesome.MarkdownMaker.Validators;

namespace CloudAwesome.MarkdownMaker
{
    public class MdBoldText: IDocumentPart
    {
        public string Text { get; set; }

        public string Markdown
        {
            get
            {
                this.Validate();
                
                return $"**{Text}** ";
            }
        }

        public MdBoldText(string text)
        {
            Text = text;
        }
        
        private void Validate()
        {
            var validator = new MdBoldTextValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                throw new InputValidationException(result.ToString());
            }
        }
    }
}