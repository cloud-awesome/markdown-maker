using FluentValidation;

namespace CloudAwesome.MarkdownMaker.Validators
{
    public class MdBoldTextValidator: AbstractValidator<MdBoldText>
    {
        public MdBoldTextValidator()
        {
            RuleFor(boldText => boldText.Text).NotEmpty();
        }
    }
}