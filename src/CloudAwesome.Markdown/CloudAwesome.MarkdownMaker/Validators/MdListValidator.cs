using FluentValidation;

namespace CloudAwesome.MarkdownMaker.Validators
{
    public class MdListValidator: AbstractValidator<MdList>
    {
        public MdListValidator()
        {
            RuleFor(list => list.Items.Count).GreaterThan(0)
                .WithMessage("The list is empty. Add items to the list before writing to the document");

            RuleFor(list => list.ListType.HasValue).Equal(true)
                .WithMessage("List type is null. " +
                             "It must be populated with either the 'Ordered' or 'Unordered' enum value");
        }
    }
}