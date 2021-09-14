using FluentValidation;

namespace CloudAwesome.MarkdownMaker.Validators
{
    public class MdListValidator: AbstractValidator<MdList>
    {
        public MdListValidator()
        {
            RuleFor(list => list.Items.Count).GreaterThan(0)
                .WithMessage("The list is empty. Add items to the list before writing to the document");
        }
    }
}