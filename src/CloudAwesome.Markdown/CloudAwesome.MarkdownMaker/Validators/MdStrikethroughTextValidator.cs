using FluentValidation;

namespace CloudAwesome.MarkdownMaker.Validators
{
    public class MdStrikethroughTextValidator: AbstractValidator<MdStrikethroughText>
    {
        public MdStrikethroughTextValidator()
        {
            RuleFor(text => text.Text).NotEmpty()
                .WithMessage("The text entered for the MdStrikethroughText element is empty. " +
                             "Strikethrough text cannot be null or empty");
        }
    }
}