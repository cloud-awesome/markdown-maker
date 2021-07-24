using FluentValidation;

namespace CloudAwesome.MarkdownMaker.Validators
{
    public class MdHeaderValidator: AbstractValidator<MdHeader>
    {
        public MdHeaderValidator()
        {
            RuleFor(header => header.Level).LessThanOrEqualTo(6);
        }
    }
}