using FluentValidation;

namespace CloudAwesome.MarkdownMaker.Validators
{
    public class MdBoldTextValidator: AbstractValidator<MdBoldText>
    {
        public MdBoldTextValidator()
        {
            RuleFor(boldText => boldText.Text).NotEmpty()
                .WithMessage("The text entered for the MdBoldText element is empty. " +
                             "Bold text cannot be null or empty");
        }
    }
}