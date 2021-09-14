using CloudAwesome.MarkdownMaker.Exceptions;
using CloudAwesome.MarkdownMaker.Validators;

namespace CloudAwesome.MarkdownMaker
{
    public class MdItalicText: IDocumentPart
    {
        public string Text { get; set; }

        public string Markdown
        {
            get
            {
                this.Validate();

                return $"_{Text}_ ";
            }
        }

        public MdItalicText(string text)
        {
            Text = text;
        }
        
        private void Validate()
        {
            var validator = new MdItalicTextValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                throw new InputValidationException(result.ToString());
            }
        }
    }
}