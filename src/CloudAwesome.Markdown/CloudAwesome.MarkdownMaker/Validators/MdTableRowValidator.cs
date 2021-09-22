using FluentValidation;

namespace CloudAwesome.MarkdownMaker.Validators
{
    public class MdTableRowValidator: AbstractValidator<MdTableRow>
    {
        public MdTableRowValidator()
        {
            RuleFor(row => row.Cells.Count).GreaterThan(0)
                .WithMessage("The table row element is empty. " +
                             "Add items to the row before writing to the document");
        }
    }
}