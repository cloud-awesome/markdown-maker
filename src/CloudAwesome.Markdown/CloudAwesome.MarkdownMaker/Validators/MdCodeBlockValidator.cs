using FluentValidation;

namespace CloudAwesome.MarkdownMaker.Validators
{
    public class MdCodeBlockValidator: AbstractValidator<MdCodeBlock>
    {
        public MdCodeBlockValidator()
        {
            RuleFor(codeBlock => codeBlock.Text).NotEmpty();
        }
    }
}