using FluentValidation;

namespace CloudAwesome.MarkdownMaker.Validators
{
    public class MdTableValidator: AbstractValidator<MdTable>
    {
        public MdTableValidator()
        {
            RuleFor(table => table.ColumnsHeaders.Count).GreaterThan(0)
                .WithMessage("The table contains no headers. " +
                             "Add headers to the table before writing to the document");
            
            RuleFor(table => table.Rows.Count).GreaterThan(0)
                .WithMessage("The table contains no rows. " +
                             "Add rows to the table before writing to the document");
        }
    }
}