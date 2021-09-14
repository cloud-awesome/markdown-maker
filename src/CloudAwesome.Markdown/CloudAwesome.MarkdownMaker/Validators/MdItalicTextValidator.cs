using FluentValidation;

namespace CloudAwesome.MarkdownMaker.Validators
{
    public class MdItalicTextValidator: AbstractValidator<MdItalicText>
    {
        public MdItalicTextValidator()
        {
            RuleFor(italicText => italicText.Text).NotEmpty();
        }
    }
}