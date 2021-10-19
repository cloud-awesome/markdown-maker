using CloudAwesome.MarkdownMaker.Exceptions;
using CloudAwesome.MarkdownMaker.Validators;

namespace CloudAwesome.MarkdownMaker
{
    public class MdStrikethroughText: IDocumentPart
    {
        public string Text { get; set; }

        public string Markdown
        {
            get
            {
                this.Validate();
                
                return $"~~{Text}~~ ";
            }
        }

        public MdStrikethroughText(string text)
        {
            Text = text;
        }

        private void Validate()
        {
            var validator = new MdStrikethroughTextValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                throw new MdInputValidationException(result.ToString());
            }
        }
    }
}