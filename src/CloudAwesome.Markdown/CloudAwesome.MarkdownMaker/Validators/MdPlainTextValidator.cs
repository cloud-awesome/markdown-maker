using FluentValidation;

namespace CloudAwesome.MarkdownMaker.Validators
{
    public class MdPlainTextValidator: AbstractValidator<MdPlainText>
    {
        public MdPlainTextValidator()
        {
            RuleFor(text => text.Text).NotEmpty();
        }
    }
}