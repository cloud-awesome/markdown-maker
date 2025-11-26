using System;
using System.Text;
using CloudAwesome.MarkdownMaker.Exceptions;
using CloudAwesome.MarkdownMaker.Validators;

namespace CloudAwesome.MarkdownMaker
{
    public enum HeaderLevel
    {
        H1 = 1,
        H2,
        H3,
        H4,
        H5,
        H6
    }
    
    public class MdHeader: IDocumentPart
    {
        public string Text { get; set; }
        public int Level { get; set; }

        public string Markdown
        {
            get
            {
                this.Validate();

                var stringBuilder = new StringBuilder();
                stringBuilder.Append('#', Level);
                stringBuilder.Append(' ');
                stringBuilder.Append(Text);
                stringBuilder.Append(Environment.NewLine);

                return stringBuilder.ToString();
            }
        }
        
        public MdHeader(string text, int level)
        {
            Text = text;
            Level = level;
        }
        
        public MdHeader(string text, HeaderLevel level)
        {
            Text = text;
            Level = (int) level;
        }

        private void Validate()
        {
            var validator = new MdHeaderValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                throw new MdInputValidationException(result.ToString());
            }
        }
        
    }
}