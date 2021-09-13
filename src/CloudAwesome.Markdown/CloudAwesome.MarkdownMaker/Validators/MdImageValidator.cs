using FluentValidation;

namespace CloudAwesome.MarkdownMaker.Validators
{
    public class MdImageValidator: AbstractValidator<MdImage>
    {
        public MdImageValidator()
        {
            RuleFor(image => image.Url).NotEmpty();
        }
    }
}