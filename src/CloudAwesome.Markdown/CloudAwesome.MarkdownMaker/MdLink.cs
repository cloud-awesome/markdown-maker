using CloudAwesome.MarkdownMaker.Exceptions;
using CloudAwesome.MarkdownMaker.Validators;

namespace CloudAwesome.MarkdownMaker
{
    public class MdLink: IDocumentPart, ISingleLinePart
    {
        public string Text { get; set; }
        
        public string Url { get; set; }

        public string Markdown
        {
            get
            {
                this.Validate();

                return $"[{Text}]({Url}) ";
            }
        }

        public MdLink(string text, string url)
        {
            Text = text;
            Url = url;
        }
        
        private void Validate()
        {
            var validator = new MdLinkValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                throw new MdInputValidationException(result.ToString());
            }
        }
    }
}