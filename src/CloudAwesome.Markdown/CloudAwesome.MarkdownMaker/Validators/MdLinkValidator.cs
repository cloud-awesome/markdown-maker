using FluentValidation;

namespace CloudAwesome.MarkdownMaker.Validators
{
    public class MdLinkValidator: AbstractValidator<MdLink>
    {
        public MdLinkValidator()
        {
            RuleFor(link => link.Url).NotEmpty();
            RuleFor(link => link.Text).NotEmpty();
        }
    }
}